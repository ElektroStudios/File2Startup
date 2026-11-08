' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ThemedToolStripRendererTests.vb" company="Elektro Studios">
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

Imports UserInterface.Theming

#End Region

#Region " ThemedToolStripRendererTests "

Namespace UserInterface

    ''' <summary>
    ''' Tests for the <see cref="ThemedToolStripRenderer"/> class.
    ''' </summary>
    <TestClass>
    Public NotInheritable Class ThemedToolStripRendererTests

        ''' <summary>The constructor rejects a null palette.</summary>
        <TestMethod>
        Public Sub New_NullPalette_ThrowsArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() ThemedToolStripRendererTests.CreateRenderer(Nothing))

        End Sub

        ''' <summary>The dark palette provides a dark color table for the menus.</summary>
        <TestMethod>
        Public Sub DarkPalette_UsesDarkColorTable()

            Dim palette As ThemePalette = ThemePalette.Dark
            Dim renderer As New ThemedToolStripRenderer(palette)

            Assert.AreSame(palette, renderer.Palette)
            Assert.AreEqual(palette.Surface, renderer.ColorTable.ToolStripDropDownBackground)
            Assert.AreEqual(palette.Border, renderer.ColorTable.MenuBorder)
            Assert.AreEqual(palette.ButtonHoverBackground, renderer.ColorTable.MenuItemSelected)

        End Sub

        ''' <summary>The light palette keeps the default Windows color table for the menus.</summary>
        <TestMethod>
        Public Sub LightPalette_UsesDefaultColorTable()

            Dim renderer As New ThemedToolStripRenderer(ThemePalette.Light)
            Dim defaultTable As New ProfessionalColorTable()

            Assert.AreEqual(defaultTable.ToolStripDropDownBackground, renderer.ColorTable.ToolStripDropDownBackground)
            Assert.AreEqual(defaultTable.MenuItemSelected, renderer.ColorTable.MenuItemSelected)

        End Sub

        ''' <summary>
        ''' A checked tool bar button is painted as the active page of a tab strip:
        ''' highlighted background and an accent line at its bottom edge.
        ''' </summary>
        <STATestMethod>
        <TestCategory("UI")>
        <DataRow(AppTheme.Light)>
        <DataRow(AppTheme.Dark)>
        Public Sub CheckedButton_IsPaintedAsActiveTab(theme As AppTheme)

            Dim palette As ThemePalette = ThemePalette.FromTheme(theme)

            Using toolBar As New ToolStrip() With {.Renderer = New ThemedToolStripRenderer(palette), .AutoSize = False, .Size = New Size(300, 40)},
                  bmp As New Bitmap(300, 40)

                Dim checkedButton As New ToolStripButton("Page") With {.Checked = True, .AutoSize = False, .Size = New Size(100, 38)}
                toolBar.Items.Add(checkedButton)
                toolBar.PerformLayout()
                toolBar.DrawToBitmap(bmp, New Rectangle(Point.Empty, bmp.Size))

                Dim bounds As Rectangle = checkedButton.Bounds
                Dim markerPixel As Color = bmp.GetPixel(bounds.Left + 4, bounds.Bottom - 1)
                Dim backgroundPixel As Color = bmp.GetPixel(bounds.Left + 4, bounds.Top + 4)

                Assert.AreEqual(palette.Accent.ToArgb(), markerPixel.ToArgb())
                Assert.AreEqual(palette.ButtonPressedBackground.ToArgb(), backgroundPixel.ToArgb())
            End Using

        End Sub

        ''' <summary>Creates a renderer (used by the lambdas of exception assertions).</summary>
        ''' <param name="palette">The palette.</param>
        ''' <returns>The renderer.</returns>
        Private Shared Function CreateRenderer(palette As ThemePalette) As ThemedToolStripRenderer

            Return New ThemedToolStripRenderer(palette)

        End Function

    End Class

End Namespace

#End Region
