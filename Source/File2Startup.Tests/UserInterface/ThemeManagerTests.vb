' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ThemeManagerTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Drawing
Imports System.Windows.Forms

Imports Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UserInterface.Controls
Imports UserInterface.Theming

#End Region

#Region " ThemeManagerTests "

Namespace UserInterface

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="ThemeManager"/> class.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    <TestCategory("UI")>
    Public NotInheritable Class ThemeManagerTests

#Region " Sample Form "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A form with one control of each type supported by <see cref="ThemeManager"/>, some of them nested.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private NotInheritable Class SampleForm : Inherits Form

            ''' <summary>A nested container.</summary>
            Public ReadOnly Panel As New Panel()
            ''' <summary>A table layout nested in the panel.</summary>
            Public ReadOnly Table As New TableLayoutPanel()
            ''' <summary>A regular label.</summary>
            Public ReadOnly RegularLabel As New Label()
            ''' <summary>A caption label.</summary>
            Public ReadOnly CaptionLabel As New Label() With {.Tag = ThemeManager.CaptionTag}
            ''' <summary>A warning label.</summary>
            Public ReadOnly WarningLabel As New Label() With {.Tag = ThemeManager.WarningTag}
            ''' <summary>A link label.</summary>
            Public ReadOnly Link As New LinkLabel()
            ''' <summary>A text box.</summary>
            Public ReadOnly TextBox As New TextBox()
            ''' <summary>A regular button.</summary>
            Public ReadOnly RegularButton As New Button()
            ''' <summary>An accent button.</summary>
            Public ReadOnly AccentButton As New Button() With {.Tag = ThemeManager.AccentTag}
            ''' <summary>A radio button.</summary>
            Public ReadOnly Radio As New RadioButton()
            ''' <summary>A grid.</summary>
            Public ReadOnly Grid As New DataGridView()
            ''' <summary>A tool bar with a drop-down button.</summary>
            Public ReadOnly ToolBar As New ToolStrip()
            ''' <summary>A drop-down button of the tool bar.</summary>
            Public ReadOnly DropDownButton As New ToolStripDropDownButton("Menu")
            ''' <summary>A toggle switch hosted in the tool bar.</summary>
            Public ReadOnly ToggleHost As New ToolStripToggleSwitch()
            ''' <summary>A stand-alone toggle switch.</summary>
            Public ReadOnly Toggle As New ToggleSwitch()

            ''' <summary>Initializes a new instance of the <see cref="SampleForm"/> class.</summary>
            Public Sub New()
                Me.ShowInTaskbar = False
                Me.DropDownButton.DropDownItems.Add("Item")
                Me.ToolBar.Items.AddRange({Me.DropDownButton, Me.ToggleHost})
                Me.Table.Controls.Add(Me.CaptionLabel)
                Me.Table.Controls.Add(Me.TextBox)
                Me.Table.Controls.Add(Me.AccentButton)
                Me.Panel.Controls.Add(Me.Table)
                Me.Controls.AddRange({Me.Panel, Me.RegularLabel, Me.WarningLabel, Me.Link, Me.RegularButton,
                                      Me.Radio, Me.Grid, Me.ToolBar, Me.Toggle})
            End Sub

        End Class

#End Region

#Region " Cleanup "

        ''' <summary>Restores the default (light) theme of the shared manager state.</summary>
        <TestCleanup>
        Public Sub Cleanup()

            Using dummy As New Panel()
                ThemeManager.Apply(dummy, AppTheme.Light)
            End Using

        End Sub

#End Region

#Region " Apply "

        ''' <summary>The dark theme is applied to every supported control of the tree, including nested ones.</summary>
        <STATestMethod>
        Public Sub Apply_Dark_PaintsWholeTree()

            Dim palette As ThemePalette = ThemePalette.Dark

            Using form As New SampleForm()
                ThemeManager.Apply(form, AppTheme.Dark)

                Assert.AreEqual(AppTheme.Dark, ThemeManager.CurrentTheme)
                Assert.AreEqual(palette.Background, form.BackColor)
                Assert.AreEqual(palette.Background, form.Panel.BackColor)
                Assert.AreEqual(palette.Background, form.Table.BackColor)

                Assert.AreEqual(palette.Text, form.RegularLabel.ForeColor)
                Assert.AreEqual(palette.SecondaryText, form.CaptionLabel.ForeColor)
                Assert.AreEqual(palette.Warning, form.WarningLabel.ForeColor)
                Assert.AreEqual(palette.Link, form.Link.LinkColor)

                Assert.AreEqual(palette.InputBackground, form.TextBox.BackColor)
                Assert.AreEqual(palette.Text, form.TextBox.ForeColor)
                Assert.AreEqual(BorderStyle.FixedSingle, form.TextBox.BorderStyle)

                Assert.AreEqual(FlatStyle.Flat, form.RegularButton.FlatStyle)
                Assert.AreEqual(palette.ButtonBackground, form.RegularButton.BackColor)
                Assert.AreEqual(palette.Text, form.RegularButton.ForeColor)
                Assert.AreEqual(palette.Accent, form.AccentButton.BackColor)
                Assert.AreEqual(palette.AccentText, form.AccentButton.ForeColor)

                Assert.AreEqual(palette.Text, form.Radio.ForeColor)

                Assert.AreEqual(palette.InputBackground, form.Grid.BackgroundColor)
                Assert.AreEqual(palette.Text, form.Grid.DefaultCellStyle.ForeColor)
                Assert.AreEqual(palette.Selection, form.Grid.DefaultCellStyle.SelectionBackColor)
                Assert.IsFalse(form.Grid.EnableHeadersVisualStyles)

                Assert.IsInstanceOfType(form.ToolBar.Renderer, GetType(ThemedToolStripRenderer))
                Assert.IsInstanceOfType(form.DropDownButton.DropDown.Renderer, GetType(ThemedToolStripRenderer))
                Assert.AreEqual(palette.Text, form.DropDownButton.ForeColor)

                Assert.AreEqual(palette.Accent, form.Toggle.OnColor)
                Assert.AreEqual(palette.Accent, form.ToggleHost.ToggleSwitch.OnColor)
                Assert.AreEqual(palette.Surface, form.ToggleHost.ToggleSwitch.BackColor)
            End Using

        End Sub

        ''' <summary>The light theme restores the default Windows look (system colors and visual styles).</summary>
        <STATestMethod>
        Public Sub Apply_LightAfterDark_RestoresWindowsDefaults()

            Using form As New SampleForm()
                ThemeManager.Apply(form, AppTheme.Dark)
                ThemeManager.Apply(form, AppTheme.Light)

                Assert.AreEqual(AppTheme.Light, ThemeManager.CurrentTheme)
                Assert.AreEqual(SystemColors.Control, form.BackColor)
                Assert.AreEqual(SystemColors.Window, form.TextBox.BackColor)
                Assert.AreEqual(BorderStyle.Fixed3D, form.TextBox.BorderStyle)

                For Each button As Button In {form.RegularButton, form.AccentButton}
                    Assert.AreEqual(FlatStyle.Standard, button.FlatStyle)
                    Assert.IsTrue(button.UseVisualStyleBackColor)
                    Assert.AreEqual(SystemColors.ControlText, button.ForeColor)
                Next button

                Assert.IsTrue(form.Grid.EnableHeadersVisualStyles)
                Assert.IsTrue(form.Radio.UseVisualStyleBackColor)
            End Using

        End Sub

        ''' <summary>A disabled accent button is painted as a regular button, and gets the accent again when enabled.</summary>
        <STATestMethod>
        Public Sub Apply_Dark_DisabledAccentButton_FollowsEnabledState()

            Dim palette As ThemePalette = ThemePalette.Dark

            Using form As New SampleForm()
                form.AccentButton.Enabled = False
                ThemeManager.Apply(form, AppTheme.Dark)

                Assert.AreEqual(palette.ButtonBackground, form.AccentButton.BackColor)

                form.AccentButton.Enabled = True
                Assert.AreEqual(palette.Accent, form.AccentButton.BackColor)

                form.AccentButton.Enabled = False
                Assert.AreEqual(palette.ButtonBackground, form.AccentButton.BackColor)
            End Using

        End Sub

        ''' <summary>A very deep control tree is themed without recursion (no stack overflow).</summary>
        <STATestMethod>
        Public Sub Apply_DeepTree_IsTraversedIteratively()

            Using root As New Panel()
                Dim parent As Control = root
                For level As Integer = 1 To 300
                    Dim child As New Panel()
                    parent.Controls.Add(child)
                    parent = child
                Next level
                Dim deepest As New Label()
                parent.Controls.Add(deepest)

                ThemeManager.Apply(root, AppTheme.Dark)

                Assert.AreEqual(ThemePalette.Dark.Text, deepest.ForeColor)
                Assert.AreEqual(ThemePalette.Dark.Background, parent.BackColor)
            End Using

        End Sub

        ''' <summary>A stand-alone context menu is themed with <see cref="ThemeManager.ApplyToDropDown"/>.</summary>
        <STATestMethod>
        Public Sub ApplyToDropDown_ThemesMenuAndSubMenus()

            Using menu As New ContextMenuStrip()
                Dim parentItem As New ToolStripMenuItem("Parent")
                parentItem.DropDownItems.Add("Child")
                menu.Items.Add(parentItem)

                ThemeManager.ApplyToDropDown(menu, AppTheme.Dark)

                Assert.IsInstanceOfType(menu.Renderer, GetType(ThemedToolStripRenderer))
                Assert.IsInstanceOfType(parentItem.DropDown.Renderer, GetType(ThemedToolStripRenderer))
                Assert.AreEqual(ThemePalette.Dark.Text, parentItem.DropDownItems(0).ForeColor)
            End Using

        End Sub

        ''' <summary>Null arguments are rejected.</summary>
        <TestMethod>
        Public Sub NullArguments_ThrowArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() ThemeManager.Apply(Nothing, AppTheme.Dark))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() ThemeManager.ApplyToDropDown(Nothing, AppTheme.Dark))

        End Sub

#End Region

    End Class

End Namespace

#End Region
