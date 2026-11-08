' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ThemedToolStripRenderer.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Drawing
Imports System.Windows.Forms

#End Region

#Region " ThemedToolStripRenderer "

Namespace UserInterface.Theming

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' A <see cref="ToolStripProfessionalRenderer"/> that paints tool bars, status bars and menus
    ''' with the colors of a <see cref="ThemePalette"/>.
    ''' <para></para>
    ''' With the light palette, menus keep the default Windows professional look;
    ''' with the dark palette, everything is painted with dark colors.
    ''' <para></para>
    ''' In both palettes, a checked <see cref="ToolStripButton"/> of a tool bar is painted as the active page of a tab strip
    ''' (a highlighted background with an accent line at the bottom).
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class ThemedToolStripRenderer : Inherits ToolStripProfessionalRenderer

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The height, in pixels (at 96 DPI), of the accent line drawn under a checked tool bar button.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Const CheckedMarkerHeight As Integer = 2

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the palette used to paint.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Palette As ThemePalette

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="ThemedToolStripRenderer"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="palette">
        ''' The palette used to paint.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="palette"/> is <see langword="Nothing"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(palette As ThemePalette)

            MyBase.New(ThemedToolStripRenderer.CreateColorTable(palette))
            Me.Palette = palette
            Me.RoundedEdges = False

        End Sub

#End Region

#Region " Overridden Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Paints the background of a tool bar or status bar with a flat color.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnRenderToolStripBackground(e As ToolStripRenderEventArgs)

            If TypeOf e.ToolStrip Is ToolStripDropDown Then
                MyBase.OnRenderToolStripBackground(e)
                Exit Sub
            End If

            Dim isPageToolBar As Boolean = ThemeManager.PageToolBarTag.Equals(e.ToolStrip.Tag)
            Using brush As New SolidBrush(If(isPageToolBar, Me.Palette.Background, Me.Palette.Surface))
                e.Graphics.FillRectangle(brush, e.AffectedBounds)
            End Using

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Paints a single separator line between a tool bar (bottom edge) or a status bar (top edge) and the content.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnRenderToolStripBorder(e As ToolStripRenderEventArgs)

            If TypeOf e.ToolStrip Is ToolStripDropDown Then
                MyBase.OnRenderToolStripBorder(e)
                Exit Sub
            End If

            ' A tool bar inside a page blends with the page, so it has no separator line.
            If ThemeManager.PageToolBarTag.Equals(e.ToolStrip.Tag) Then
                Exit Sub
            End If

            Dim bounds As Rectangle = e.ToolStrip.ClientRectangle
            Dim y As Integer = If(TypeOf e.ToolStrip Is StatusStrip, bounds.Top, bounds.Bottom - 1)
            Using pen As New Pen(Me.Palette.Border)
                e.Graphics.DrawLine(pen, bounds.Left, y, bounds.Right, y)
            End Using

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Paints the background of a tool bar button, painting a checked button as the active page of a tab strip.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnRenderButtonBackground(e As ToolStripItemRenderEventArgs)

            Dim button As ToolStripButton = TryCast(e.Item, ToolStripButton)
            Dim isChecked As Boolean = button IsNot Nothing AndAlso button.Checked

            If isChecked Then
                Me.PaintItemBackground(e.Graphics, e.Item, Me.Palette.ButtonPressedBackground)
                Me.PaintCheckedMarker(e.Graphics, e.Item)

            ElseIf e.Item.Enabled AndAlso (e.Item.Pressed OrElse e.Item.Selected) Then
                Me.PaintItemBackground(e.Graphics, e.Item, If(e.Item.Pressed, Me.Palette.ButtonPressedBackground, Me.Palette.ButtonHoverBackground))

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Paints the background of a tool bar drop-down button.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnRenderDropDownButtonBackground(e As ToolStripItemRenderEventArgs)

            If e.Item.Enabled AndAlso (e.Item.Pressed OrElse e.Item.Selected) Then
                Me.PaintItemBackground(e.Graphics, e.Item, If(e.Item.Pressed, Me.Palette.ButtonPressedBackground, Me.Palette.ButtonHoverBackground))
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the text color of the items.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnRenderItemText(e As ToolStripItemTextRenderEventArgs)

            If e.Item.Tag IsNot Nothing AndAlso ThemeManager.WarningTag.Equals(e.Item.Tag) Then
                e.TextColor = Me.Palette.Warning
            Else
                e.TextColor = If(e.Item.Enabled, Me.Palette.Text, Me.Palette.DisabledText)
            End If

            MyBase.OnRenderItemText(e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the color of the drop-down arrows.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnRenderArrow(e As ToolStripArrowRenderEventArgs)

            e.ArrowColor = If(e.Item Is Nothing OrElse e.Item.Enabled, Me.Palette.Text, Me.Palette.DisabledText)
            MyBase.OnRenderArrow(e)

        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Fills the background of an item.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="g">
        ''' The graphics.
        ''' </param>
        '''
        ''' <param name="item">
        ''' The item.
        ''' </param>
        '''
        ''' <param name="color">
        ''' The background color.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub PaintItemBackground(g As Graphics, item As ToolStripItem, color As Color)

            Dim bounds As New Rectangle(Point.Empty, item.Size)
            Using brush As New SolidBrush(color)
                g.FillRectangle(brush, bounds)
            End Using

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Draws the accent line at the bottom of a checked item.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="g">
        ''' The graphics.
        ''' </param>
        '''
        ''' <param name="item">
        ''' The item.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub PaintCheckedMarker(g As Graphics, item As ToolStripItem)

            Dim markerHeight As Integer = Math.Max(1, CInt(Math.Round(ThemedToolStripRenderer.CheckedMarkerHeight * g.DpiY / 96.0F)))
            Dim bounds As New Rectangle(0, item.Height - markerHeight, item.Width, markerHeight)
            Using brush As New SolidBrush(Me.Palette.Accent)
                g.FillRectangle(brush, bounds)
            End Using

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates the color table for the specified palette.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="palette">
        ''' The palette.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The default Windows color table for the light palette; a <see cref="DarkColorTable"/> for the dark palette.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="palette"/> is <see langword="Nothing"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function CreateColorTable(palette As ThemePalette) As ProfessionalColorTable

            If palette Is Nothing Then
                Throw New ArgumentNullException(NameOf(palette))
            End If

            Return If(palette.IsDark, New DarkColorTable(palette), New ProfessionalColorTable())

        End Function

#End Region

#Region " DarkColorTable "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A <see cref="ProfessionalColorTable"/> that provides the colors of a dark <see cref="ThemePalette"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private NotInheritable Class DarkColorTable : Inherits ProfessionalColorTable

            ''' <summary>The palette.</summary>
            Private ReadOnly palette As ThemePalette

            ''' <summary>Initializes a new instance of the <see cref="DarkColorTable"/> class.</summary>
            ''' <param name="palette">The palette.</param>
            Public Sub New(palette As ThemePalette)
                Me.palette = palette
                Me.UseSystemColors = False
            End Sub

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property ToolStripDropDownBackground As Color
                Get
                    Return Me.palette.Surface
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property ImageMarginGradientBegin As Color
                Get
                    Return Me.palette.Surface
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property ImageMarginGradientMiddle As Color
                Get
                    Return Me.palette.Surface
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property ImageMarginGradientEnd As Color
                Get
                    Return Me.palette.Surface
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property MenuBorder As Color
                Get
                    Return Me.palette.Border
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property MenuItemBorder As Color
                Get
                    Return Me.palette.ButtonHoverBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property MenuItemSelected As Color
                Get
                    Return Me.palette.ButtonHoverBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property MenuItemSelectedGradientBegin As Color
                Get
                    Return Me.palette.ButtonHoverBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property MenuItemSelectedGradientEnd As Color
                Get
                    Return Me.palette.ButtonHoverBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property MenuItemPressedGradientBegin As Color
                Get
                    Return Me.palette.ButtonPressedBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property MenuItemPressedGradientMiddle As Color
                Get
                    Return Me.palette.ButtonPressedBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property MenuItemPressedGradientEnd As Color
                Get
                    Return Me.palette.ButtonPressedBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property CheckBackground As Color
                Get
                    Return Me.palette.ButtonPressedBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property CheckSelectedBackground As Color
                Get
                    Return Me.palette.ButtonPressedBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property CheckPressedBackground As Color
                Get
                    Return Me.palette.ButtonPressedBackground
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property SeparatorDark As Color
                Get
                    Return Me.palette.Border
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property SeparatorLight As Color
                Get
                    Return Me.palette.Surface
                End Get
            End Property

        End Class

#End Region

    End Class

End Namespace

#End Region
