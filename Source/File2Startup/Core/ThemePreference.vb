' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ThemePreference.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports Microsoft.Win32

#End Region

#Region " ThemePreference "

Namespace Core

    ''' <summary>
    ''' Converts the theme stored in the user settings from/to <see cref="AppTheme"/>,
    ''' and detects the theme that Windows uses for applications.
    ''' </summary>
    Public NotInheritable Class ThemePreference

#Region " Constants "

        ''' <summary>
        ''' The registry key that contains the Windows personalization settings of the current user.
        ''' </summary>
        Public Const PersonalizeSubKeyPath As String = "Software\Microsoft\Windows\CurrentVersion\Themes\Personalize"

        ''' <summary>
        ''' The registry value that indicates whether applications use the light theme (1) or the dark theme (0).
        ''' </summary>
        Public Const AppsUseLightThemeValueName As String = "AppsUseLightTheme"

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ThemePreference"/> class from being created.
        ''' </summary>
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Resolves the theme to use from the value stored in the user settings.
        ''' <para></para>
        ''' When the setting is empty (the program was never run, or the user never chose a theme),
        ''' the theme that Windows uses for applications is returned.
        ''' </summary>
        ''' <param name="settingValue">
        ''' The value stored in the user settings: <c>Light</c>, <c>Dark</c>, or an empty string.
        ''' </param>
        '''
        ''' <param name="isSystemDarkTheme">
        ''' A value indicating whether Windows uses the dark theme for applications.
        ''' </param>
        ''' <returns>
        ''' The resolved <see cref="AppTheme"/>.
        ''' </returns>
        ''' <exception cref="FormatException">
        ''' <paramref name="settingValue"/> is not empty and is not a valid theme name.
        ''' </exception>
        Public Shared Function Resolve(settingValue As String, isSystemDarkTheme As Boolean) As AppTheme

            If String.IsNullOrWhiteSpace(settingValue) Then
                Return If(isSystemDarkTheme, AppTheme.Dark, AppTheme.Light)
            End If

            Select Case settingValue.Trim().ToLowerInvariant()
                Case "light"
                    Return AppTheme.Light
                Case "dark"
                    Return AppTheme.Dark
                Case Else
                    Throw New FormatException($"Unknown theme name in user settings: '{settingValue}'.")
            End Select

        End Function

        ''' <summary>
        ''' Gets the value to store in the user settings for the specified theme.
        ''' </summary>
        ''' <param name="theme">
        ''' The theme.
        ''' </param>
        ''' <returns>
        ''' <c>Light</c> or <c>Dark</c>.
        ''' </returns>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' <paramref name="theme"/> is not a defined <see cref="AppTheme"/> value.
        ''' </exception>
        Public Shared Function ToSettingValue(theme As AppTheme) As String

            Select Case theme
                Case AppTheme.Light
                    Return "Light"
                Case AppTheme.Dark
                    Return "Dark"
                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(theme), theme, "Undefined theme value.")
            End Select

        End Function

        ''' <summary>
        ''' Determines whether the specified value of the <c>AppsUseLightTheme</c> registry value means a dark theme.
        ''' </summary>
        ''' <param name="appsUseLightThemeValue">
        ''' The registry value data, or <see langword="Nothing"/> if the value does not exist
        ''' (Windows versions without dark mode).
        ''' </param>
        ''' <returns>
        ''' <see langword="True"/> if the value is the integer zero; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Shared Function IsDarkThemeValue(appsUseLightThemeValue As Object) As Boolean

            Return TypeOf appsUseLightThemeValue Is Integer AndAlso
                   DirectCast(appsUseLightThemeValue, Integer) = 0

        End Function

        ''' <summary>
        ''' Determines whether Windows uses the dark theme for applications (Windows 10 version 1809 and later).
        ''' </summary>
        ''' <returns>
        ''' <see langword="True"/> if Windows uses the dark theme for applications;
        ''' <see langword="False"/> if it uses the light theme or the setting does not exist.
        ''' </returns>
        Public Shared Function IsSystemDarkTheme() As Boolean

            Using key As RegistryKey = Registry.CurrentUser.OpenSubKey(ThemePreference.PersonalizeSubKeyPath, writable:=False)
                If key Is Nothing Then
                    ' Windows versions without dark mode support do not have this key.
                    Return False
                End If

                Return ThemePreference.IsDarkThemeValue(key.GetValue(ThemePreference.AppsUseLightThemeValueName, Nothing))
            End Using

        End Function

#End Region

    End Class

End Namespace

#End Region
