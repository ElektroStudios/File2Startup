' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="DwmWindowAttribute.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " DwmWindowAttribute "

Namespace DevCase.Interop.Win32.Enums

    ''' <summary>
    ''' Specifies the Desktop Window Manager (DWM) window attributes used by
    ''' <see cref="NativeMethods.DwmSetWindowAttribute"/>.
    ''' </summary>
    ''' <remarks>
    ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/dwmapi/ne-dwmapi-dwmwindowattribute"/>
    ''' </remarks>
    Public Enum DwmWindowAttribute As Integer

        ''' <summary>
        ''' Undocumented value of <c>DWMWA_USE_IMMERSIVE_DARK_MODE</c> used by Windows 10 versions 1809 to 1909
        ''' (builds 17763 to 18363).
        ''' </summary>
        UseImmersiveDarkModeBefore20H1 = 19

        ''' <summary>
        ''' <c>DWMWA_USE_IMMERSIVE_DARK_MODE</c>: allows the window frame to be drawn in dark mode colors
        ''' (Windows 10 version 2004 and later, and Windows 11).
        ''' </summary>
        UseImmersiveDarkMode = 20

    End Enum

End Namespace

#End Region
