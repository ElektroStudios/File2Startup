' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ThemeManager.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.Generic
Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms

Imports Core

Imports UserInterface.Controls

#End Region

#Region " ThemeManager "

Namespace UserInterface.Theming

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Applies a visual theme (<see cref="AppTheme"/>) to a tree of Windows Forms controls.
    ''' <para></para>
    ''' The control tree is traversed iteratively, and every supported control type
    ''' (forms, containers, labels, text boxes, buttons, radio buttons, grids, tool bars, menus, scroll bars
    ''' and <see cref="ToggleSwitch"/>) is painted with the colors of the theme's <see cref="ThemePalette"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' The <see cref="Control.Tag"/> of labels, buttons and tool strip items can mark a special role:
    ''' <see cref="ThemeManager.CaptionTag"/> (field caption label),
    ''' <see cref="ThemeManager.AccentTag"/> (primary button), 
    ''' <see cref="ThemeManager.WarningTag"/> (warning text) and 
    ''' <see cref="ThemeManager.PageToolBarTag"/> (tool bar inside a page).
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class ThemeManager

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The <see cref="Control.Tag"/> value that marks a label as a field caption.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Const CaptionTag As String = "Theme:Caption"

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The <see cref="Control.Tag"/> value that marks a button as the primary (accent) button.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Const AccentTag As String = "Theme:Accent"

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The <see cref="Control.Tag"/> or <see cref="ToolStripItem.Tag"/> value that marks a warning text.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Const WarningTag As String = "Theme:Warning"

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The <see cref="ToolStrip.Tag"/> value that marks a tool bar placed inside a page, 
        ''' which is painted with the page background and without border, so it blends with the page.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Const PageToolBarTag As String = "Theme:PageToolBar"

#End Region

#Region " Fields "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The controls whose <see cref="Control.HandleCreated"/> event is already handled to apply
        ''' the native visual style once their window handle exists.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared ReadOnly handleCreatedHooks As New ConditionalWeakTable(Of Control, Object)

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The accent buttons whose <see cref="Control.EnabledChanged"/> event is already handled.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared ReadOnly enabledChangedHooks As New ConditionalWeakTable(Of Control, Object)

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The last theme applied by <see cref="ThemeManager.Apply"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared currentThemeValue As AppTheme = AppTheme.Light

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the last theme applied by <see cref="ThemeManager.Apply"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property CurrentTheme As AppTheme
            Get
                Return ThemeManager.currentThemeValue
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the palette of the last theme applied by <see cref="ThemeManager.Apply"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property CurrentPalette As ThemePalette
            Get
                Return ThemePalette.FromTheme(ThemeManager.CurrentTheme)
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="ThemeManager"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the specified theme to a control and all its child controls (including the drop-down menus
        ''' of its tool bars and, for a form, its title bar).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="root">
        ''' The root control (usually a <see cref="Form"/>).
        ''' </param>
        '''
        ''' <param name="theme">
        ''' The theme to apply.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="root"/> is <see langword="Nothing"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Sub Apply(root As Control, theme As AppTheme)

            If root Is Nothing Then
                Throw New ArgumentNullException(NameOf(root))
            End If

            Dim palette As ThemePalette = ThemePalette.FromTheme(theme)
            ThemeManager.currentThemeValue = theme

            root.SuspendLayout()
            Try
                Dim pending As New Stack(Of Control)
                pending.Push(root)

                Do While pending.Count <> 0
                    Dim ctrl As Control = pending.Pop()
                    ThemeManager.ApplyToControl(ctrl, palette)

                    For Each child As Control In ctrl.Controls
                        pending.Push(child)
                    Next child
                Loop

            Finally
                root.ResumeLayout(performLayout:=True)

            End Try

            root.Invalidate(invalidateChildren:=True)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the specified theme to a stand-alone tool strip drop-down
        ''' (for example, a <see cref="ContextMenuStrip"/> that is not part of the control tree of a form).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="dropDown">
        ''' The drop-down.
        ''' </param>
        '''
        ''' <param name="theme">
        ''' The theme to apply.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="dropDown"/> is <see langword="Nothing"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Sub ApplyToDropDown(dropDown As ToolStripDropDown, theme As AppTheme)

            If dropDown Is Nothing Then
                Throw New ArgumentNullException(NameOf(dropDown))
            End If

            ThemeManager.ApplyToToolStrip(dropDown, ThemePalette.FromTheme(theme))

        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the palette to a single control (not to its children).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="ctrl">
        ''' The control.
        ''' </param>
        '''
        ''' <param name="palette">
        ''' The palette.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub ApplyToControl(ctrl As Control, palette As ThemePalette)

            If TypeOf ctrl Is Form Then
                Dim frm As Form = DirectCast(ctrl, Form)
                frm.BackColor = palette.Background
                frm.ForeColor = palette.Text
                If frm.IsHandleCreated Then
                    WindowThemeUtil.SetDarkTitleBar(frm, palette.IsDark)
                End If

            ElseIf TypeOf ctrl Is ToolStrip Then
                ThemeManager.ApplyToToolStrip(DirectCast(ctrl, ToolStrip), palette)

            ElseIf TypeOf ctrl Is ToggleSwitch Then
                Dim toggle As ToggleSwitch = DirectCast(ctrl, ToggleSwitch)
                toggle.BackColor = If(TypeOf toggle.Parent Is ToolStrip, palette.Surface, palette.Background)
                toggle.OnColor = palette.Accent
                toggle.OffColor = If(palette.IsDark, palette.ButtonBackground, palette.InputBackground)
                toggle.BorderColor = If(palette.IsDark, palette.Border, palette.Text)
                toggle.ThumbOnColor = palette.AccentText
                toggle.ThumbOffColor = palette.Text
                toggle.ForeColor = palette.Text

            ElseIf TypeOf ctrl Is DataGridView Then
                ThemeManager.ApplyToDataGridView(DirectCast(ctrl, DataGridView), palette)

            ElseIf TypeOf ctrl Is TextBoxBase Then
                Dim tb As TextBoxBase = DirectCast(ctrl, TextBoxBase)
                tb.BackColor = palette.InputBackground
                tb.ForeColor = palette.Text
                tb.BorderStyle = If(palette.IsDark, BorderStyle.FixedSingle, BorderStyle.Fixed3D)

            ElseIf TypeOf ctrl Is ButtonBase AndAlso Not (TypeOf ctrl Is RadioButton OrElse TypeOf ctrl Is CheckBox) Then
                ThemeManager.ApplyToButton(DirectCast(ctrl, ButtonBase), palette)

            ElseIf TypeOf ctrl Is RadioButton OrElse TypeOf ctrl Is CheckBox Then
                Dim btn As ButtonBase = DirectCast(ctrl, ButtonBase)
                btn.ForeColor = palette.Text
                btn.BackColor = Color.Transparent
                btn.UseVisualStyleBackColor = Not palette.IsDark

            ElseIf TypeOf ctrl Is LinkLabel Then
                Dim lnk As LinkLabel = DirectCast(ctrl, LinkLabel)
                lnk.BackColor = Color.Transparent
                lnk.LinkColor = palette.Link
                lnk.ActiveLinkColor = palette.Link
                lnk.VisitedLinkColor = palette.Link

            ElseIf TypeOf ctrl Is Label Then
                ctrl.BackColor = Color.Transparent
                ctrl.ForeColor = ThemeManager.GetLabelColor(ctrl.Tag, palette)

            ElseIf TypeOf ctrl Is ScrollBar Then
                ThemeManager.ApplyNativeVisualStyle(ctrl)

            ElseIf TypeOf ctrl Is PictureBox Then
                ctrl.BackColor = Color.Transparent

            ElseIf TypeOf ctrl Is Panel OrElse TypeOf ctrl Is ContainerControl Then
                ' Panel, TableLayoutPanel, FlowLayoutPanel, UserControl...
                ctrl.BackColor = palette.Background
                ctrl.ForeColor = palette.Text

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the text color of a label according to the role marked in its tag.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="tag">
        ''' The tag of the label.
        ''' </param>
        '''
        ''' <param name="palette">
        ''' The palette.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The text color.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function GetLabelColor(tag As Object, palette As ThemePalette) As Color

            If ThemeManager.CaptionTag.Equals(tag) Then
                Return palette.SecondaryText
            ElseIf ThemeManager.WarningTag.Equals(tag) Then
                Return palette.Warning
            Else
                Return palette.Text
            End If

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the palette to a button.
        ''' <para></para>
        ''' With the light palette, the button gets the default Windows look (system button with visual styles);
        ''' with the dark palette, the button is flat.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="button">
        ''' The button.
        ''' </param>
        '''
        ''' <param name="palette">
        ''' The palette.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub ApplyToButton(button As ButtonBase, palette As ThemePalette)

            Dim iconButton As IconButton = TryCast(button, IconButton)
            If iconButton IsNot Nothing Then
                iconButton.DisabledForeColor = palette.DisabledText
            End If

            If Not palette.IsDark Then
                button.FlatStyle = FlatStyle.Standard
                button.BackColor = SystemColors.Control
                button.ForeColor = SystemColors.ControlText
                button.UseVisualStyleBackColor = True
                Exit Sub
            End If

            ' A disabled accent button is painted as a regular button, so it does not look clickable.
            Dim isAccent As Boolean = ThemeManager.AccentTag.Equals(button.Tag)
            If isAccent Then
                ThemeManager.HookEnabledChanged(button)
                isAccent = button.Enabled
            End If

            button.FlatStyle = FlatStyle.Flat
            button.UseVisualStyleBackColor = False
            button.BackColor = If(isAccent, palette.Accent, palette.ButtonBackground)
            button.ForeColor = If(isAccent, palette.AccentText, palette.Text)
            button.FlatAppearance.BorderSize = 1
            button.FlatAppearance.BorderColor = If(isAccent, palette.Accent, palette.Border)
            button.FlatAppearance.MouseOverBackColor = If(isAccent, palette.AccentHover, palette.ButtonHoverBackground)
            button.FlatAppearance.MouseDownBackColor = If(isAccent, palette.Accent, palette.ButtonPressedBackground)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles (once) the <see cref="Control.EnabledChanged"/> event of an accent button, 
        ''' to repaint it with the current palette when it is enabled or disabled.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="button">
        ''' The button.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub HookEnabledChanged(button As ButtonBase)

            Dim hook As Object = Nothing
            If Not ThemeManager.enabledChangedHooks.TryGetValue(button, hook) Then
                ThemeManager.enabledChangedHooks.Add(button, New Object())
                AddHandler button.EnabledChanged, AddressOf ThemeManager.AccentButton_EnabledChanged
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.EnabledChanged"/> event of an accent button.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub AccentButton_EnabledChanged(sender As Object, e As EventArgs)

            ThemeManager.ApplyToButton(DirectCast(sender, ButtonBase), ThemeManager.CurrentPalette)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the palette to a tool strip (tool bar, status bar or menu) and to the drop-down menus of its items.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="toolStrip">
        ''' The tool strip.
        ''' </param>
        '''
        ''' <param name="palette">
        ''' The palette.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub ApplyToToolStrip(toolStrip As ToolStrip, palette As ThemePalette)

            Dim renderer As New ThemedToolStripRenderer(palette)

            Dim pending As New Stack(Of ToolStrip)
            pending.Push(toolStrip)

            Do While pending.Count <> 0
                Dim strip As ToolStrip = pending.Pop()
                strip.Renderer = renderer
                strip.BackColor = If(ThemeManager.PageToolBarTag.Equals(strip.Tag), palette.Background, palette.Surface)
                strip.ForeColor = palette.Text

                For Each item As ToolStripItem In strip.Items
                    item.ForeColor = If(ThemeManager.WarningTag.Equals(item.Tag), palette.Warning, palette.Text)

                    Dim dropDownItem As ToolStripDropDownItem = TryCast(item, ToolStripDropDownItem)
                    If dropDownItem IsNot Nothing AndAlso dropDownItem.HasDropDownItems Then
                        pending.Push(dropDownItem.DropDown)
                    End If
                Next item
            Loop

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the palette to a grid.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="grid">
        ''' The grid.
        ''' </param>
        '''
        ''' <param name="palette">
        ''' The palette.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub ApplyToDataGridView(grid As DataGridView, palette As ThemePalette)

            grid.BackgroundColor = palette.InputBackground
            grid.GridColor = palette.GridLine
            grid.BorderStyle = BorderStyle.FixedSingle

            ' The light theme keeps the native (visual styles) column headers.
            grid.EnableHeadersVisualStyles = Not palette.IsDark

            With grid.DefaultCellStyle
                .BackColor = palette.InputBackground
                .ForeColor = palette.Text
                .SelectionBackColor = palette.Selection
                .SelectionForeColor = palette.SelectionText
            End With

            With grid.AlternatingRowsDefaultCellStyle
                .BackColor = palette.AlternateInputBackground
                .ForeColor = palette.Text
                .SelectionBackColor = palette.Selection
                .SelectionForeColor = palette.SelectionText
            End With

            With grid.ColumnHeadersDefaultCellStyle
                .BackColor = palette.Surface
                .ForeColor = palette.Text
                .SelectionBackColor = palette.Surface
                .SelectionForeColor = palette.Text
            End With

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the native dark/light visual style of the current theme to a control (e.g. a scroll bar),
        ''' now if its window handle exists, and each time its window handle is created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="ctrl">
        ''' The control.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub ApplyNativeVisualStyle(ctrl As Control)

            Dim hook As Object = Nothing
            If Not ThemeManager.handleCreatedHooks.TryGetValue(ctrl, hook) Then
                ThemeManager.handleCreatedHooks.Add(ctrl, New Object())
                AddHandler ctrl.HandleCreated, AddressOf ThemeManager.NativeVisualStyleControl_HandleCreated
            End If

            If ctrl.IsHandleCreated Then
                WindowThemeUtil.SetDarkScrollBars(ctrl, ThemeManager.CurrentTheme = AppTheme.Dark)
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.HandleCreated"/> event of a control that uses the native visual style.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub NativeVisualStyleControl_HandleCreated(sender As Object, e As EventArgs)

            WindowThemeUtil.SetDarkScrollBars(DirectCast(sender, Control), ThemeManager.CurrentTheme = AppTheme.Dark)

        End Sub

#End Region

    End Class

End Namespace

#End Region
