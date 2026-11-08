' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ThemePreferenceTests.vb" company="Elektro Studios">
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

Imports Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Win32

#End Region

#Region " ThemePreferenceTests "

Namespace Core

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="ThemePreference"/> class.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    Public NotInheritable Class ThemePreferenceTests

#Region " Resolve "

        ''' <summary>An empty setting follows the Windows theme.</summary>
        <TestMethod>
        <DataRow(Nothing, False, AppTheme.Light)>
        <DataRow(Nothing, True, AppTheme.Dark)>
        <DataRow("", False, AppTheme.Light)>
        <DataRow("", True, AppTheme.Dark)>
        <DataRow("   ", True, AppTheme.Dark)>
        Public Sub Resolve_EmptySetting_FollowsSystemTheme(settingValue As String, isSystemDarkTheme As Boolean, expected As AppTheme)

            Assert.AreEqual(expected, ThemePreference.Resolve(settingValue, isSystemDarkTheme))

        End Sub

        ''' <summary>A stored theme takes precedence over the Windows theme (case-insensitive, trimmed).</summary>
        <TestMethod>
        <DataRow("Light", True, AppTheme.Light)>
        <DataRow("light", True, AppTheme.Light)>
        <DataRow(" LIGHT ", True, AppTheme.Light)>
        <DataRow("Dark", False, AppTheme.Dark)>
        <DataRow("dark", False, AppTheme.Dark)>
        Public Sub Resolve_StoredTheme_TakesPrecedence(settingValue As String, isSystemDarkTheme As Boolean, expected As AppTheme)

            Assert.AreEqual(expected, ThemePreference.Resolve(settingValue, isSystemDarkTheme))

        End Sub

        ''' <summary>An unknown stored value fails fast.</summary>
        <TestMethod>
        <DataRow("Blue")>
        <DataRow("1")>
        <DataRow("System")>
        Public Sub Resolve_UnknownSetting_ThrowsFormatException(settingValue As String)

            Assert.ThrowsExactly(Of FormatException)(Sub() ThemePreference.Resolve(settingValue, False))

        End Sub

#End Region

#Region " ToSettingValue "

        ''' <summary>The setting value round-trips through <see cref="ThemePreference.Resolve"/>.</summary>
        <TestMethod>
        <DataRow(AppTheme.Light, "Light")>
        <DataRow(AppTheme.Dark, "Dark")>
        Public Sub ToSettingValue_RoundTrip(theme As AppTheme, expected As String)

            Dim value As String = ThemePreference.ToSettingValue(theme)

            Assert.AreEqual(expected, value)
            Assert.AreEqual(theme, ThemePreference.Resolve(value, isSystemDarkTheme:=theme <> AppTheme.Dark))

        End Sub

        ''' <summary>An undefined theme value is rejected.</summary>
        <TestMethod>
        Public Sub ToSettingValue_UndefinedTheme_ThrowsArgumentOutOfRangeException()

            Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() ThemePreference.ToSettingValue(CType(99, AppTheme)))

        End Sub

#End Region

#Region " System Theme "

        ''' <summary>Only the integer zero of <c>AppsUseLightTheme</c> means dark theme.</summary>
        <TestMethod>
        Public Sub IsDarkThemeValue_InterpretsRegistryValue()

            Assert.IsTrue(ThemePreference.IsDarkThemeValue(0))
            Assert.IsFalse(ThemePreference.IsDarkThemeValue(1))
            Assert.IsFalse(ThemePreference.IsDarkThemeValue(Nothing))
            Assert.IsFalse(ThemePreference.IsDarkThemeValue("0"))

        End Sub

        ''' <summary>The Windows theme is read from the <c>AppsUseLightTheme</c> value of the current user.</summary>
        <TestMethod>
        <TestCategory("Integration")>
        Public Sub IsSystemDarkTheme_MatchesRegistryValue()

            Dim registryValue As Object = Registry.GetValue($"HKEY_CURRENT_USER\{ThemePreference.PersonalizeSubKeyPath}",
                                                            ThemePreference.AppsUseLightThemeValueName, Nothing)

            Assert.AreEqual(ThemePreference.IsDarkThemeValue(registryValue), ThemePreference.IsSystemDarkTheme())

        End Sub

#End Region

    End Class

End Namespace

#End Region
