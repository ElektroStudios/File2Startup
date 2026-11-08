' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ThemePaletteTests.vb" company="Elektro Studios">
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

Imports Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UserInterface.Theming

#End Region

#Region " ThemePaletteTests "

Namespace UserInterface

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="ThemePalette"/> class.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    Public NotInheritable Class ThemePaletteTests

#Region " Constants "

        ''' <summary>The WCAG AA minimum contrast ratio for normal text.</summary>
        Private Const MinimumTextContrast As Double = 4.5R

        ''' <summary>The WCAG minimum contrast ratio for user interface components and disabled/secondary content.</summary>
        Private Const MinimumComponentContrast As Double = 3.0R

#End Region

#Region " FromTheme "

        ''' <summary>The palette of each theme is returned.</summary>
        <TestMethod>
        Public Sub FromTheme_ReturnsPaletteOfTheme()

            Assert.AreEqual(AppTheme.Light, ThemePalette.FromTheme(AppTheme.Light).Theme)
            Assert.AreEqual(AppTheme.Dark, ThemePalette.FromTheme(AppTheme.Dark).Theme)
            Assert.IsFalse(ThemePalette.Light.IsDark)
            Assert.IsTrue(ThemePalette.Dark.IsDark)

        End Sub

        ''' <summary>An undefined theme value is rejected.</summary>
        <TestMethod>
        Public Sub FromTheme_UndefinedTheme_ThrowsArgumentOutOfRangeException()

            Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() ThemePalette.FromTheme(CType(42, AppTheme)))

        End Sub

#End Region

#Region " Light Palette "

        ''' <summary>The light palette uses the default Windows system colors.</summary>
        <TestMethod>
        Public Sub Light_UsesWindowsSystemColors()

            Dim palette As ThemePalette = ThemePalette.Light

            Assert.AreEqual(SystemColors.Control, palette.Background)
            Assert.AreEqual(SystemColors.Window, palette.InputBackground)
            Assert.AreEqual(SystemColors.ControlText, palette.Text)
            Assert.AreEqual(SystemColors.GrayText, palette.HintText)
            Assert.AreEqual(SystemColors.Highlight, palette.Selection)
            Assert.AreEqual(SystemColors.HighlightText, palette.SelectionText)
            Assert.AreEqual(SystemColors.Highlight, palette.Accent)

        End Sub

#End Region

#Region " Dark Palette Contrast "

        ''' <summary>The texts of the dark palette meet the WCAG AA contrast ratio (4.5:1) against their backgrounds.</summary>
        <TestMethod>
        Public Sub Dark_TextColors_MeetWcagAA()

            Dim palette As ThemePalette = ThemePalette.Dark

            Dim pairs As Tuple(Of String, Color, Color)() = {
                Tuple.Create("Text/Background", palette.Text, palette.Background),
                Tuple.Create("Text/Surface", palette.Text, palette.Surface),
                Tuple.Create("Text/InputBackground", palette.Text, palette.InputBackground),
                Tuple.Create("Text/AlternateInputBackground", palette.Text, palette.AlternateInputBackground),
                Tuple.Create("Text/ButtonBackground", palette.Text, palette.ButtonBackground),
                Tuple.Create("Text/ButtonHoverBackground", palette.Text, palette.ButtonHoverBackground),
                Tuple.Create("SecondaryText/Background", palette.SecondaryText, palette.Background),
                Tuple.Create("HintText/InputBackground", palette.HintText, palette.InputBackground),
                Tuple.Create("AccentText/Accent", palette.AccentText, palette.Accent),
                Tuple.Create("AccentText/AccentHover", palette.AccentText, palette.AccentHover),
                Tuple.Create("SelectionText/Selection", palette.SelectionText, palette.Selection),
                Tuple.Create("Link/Background", palette.Link, palette.Background),
                Tuple.Create("Warning/Surface", palette.Warning, palette.Surface)
            }

            For Each pair As Tuple(Of String, Color, Color) In pairs
                Dim ratio As Double = ThemePalette.GetContrastRatio(pair.Item2, pair.Item3)
                Assert.IsTrue(ratio >= ThemePaletteTests.MinimumTextContrast, $"{pair.Item1} contrast is {ratio:0.00}:1.")
            Next pair

        End Sub

        ''' <summary>The disabled text and the borders of the dark palette are distinguishable (3:1 for disabled text).</summary>
        <TestMethod>
        Public Sub Dark_DisabledText_IsDistinguishable()

            Dim palette As ThemePalette = ThemePalette.Dark

            Assert.IsTrue(ThemePalette.GetContrastRatio(palette.DisabledText, palette.Background) >= ThemePaletteTests.MinimumComponentContrast)
            Assert.IsTrue(ThemePalette.GetContrastRatio(palette.Border, palette.Background) > 1.5R)

        End Sub

        ''' <summary>The dark palette is actually dark (dark backgrounds and light text).</summary>
        <TestMethod>
        Public Sub Dark_BackgroundsAreDarkAndTextIsLight()

            Dim palette As ThemePalette = ThemePalette.Dark

            Assert.IsTrue(palette.Background.GetBrightness() < 0.2F)
            Assert.IsTrue(palette.InputBackground.GetBrightness() < 0.2F)
            Assert.IsTrue(palette.Text.GetBrightness() > 0.9F)

        End Sub

#End Region

#Region " GetContrastRatio "

        ''' <summary>The contrast ratio follows the WCAG 2.x formula.</summary>
        <TestMethod>
        Public Sub GetContrastRatio_KnownValues()

            Assert.AreEqual(21.0R, ThemePalette.GetContrastRatio(Color.Black, Color.White), 0.001R)
            Assert.AreEqual(1.0R, ThemePalette.GetContrastRatio(Color.Gray, Color.Gray), 0.001R)
            Assert.AreEqual(ThemePalette.GetContrastRatio(Color.Red, Color.White),
                            ThemePalette.GetContrastRatio(Color.White, Color.Red), 0.000001R)

            ' #767676 on white is the well-known minimum gray for WCAG AA (4.54:1).
            Assert.AreEqual(4.54R, ThemePalette.GetContrastRatio(Color.FromArgb(118, 118, 118), Color.White), 0.01R)

        End Sub

#End Region

    End Class

End Namespace

#End Region
