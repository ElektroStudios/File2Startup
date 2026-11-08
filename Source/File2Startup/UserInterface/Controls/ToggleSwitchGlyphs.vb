' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ToggleSwitchGlyphs.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " ToggleSwitchGlyphs "

Namespace UserInterface.Controls

    ''' <summary>
    ''' Specifies the glyphs drawn inside the thumb of a <see cref="ToggleSwitch"/>.
    ''' </summary>
    Public Enum ToggleSwitchGlyphs As Integer

        ''' <summary>
        ''' No glyphs.
        ''' </summary>
        None = 0

        ''' <summary>
        ''' A sun when the switch is off (light theme) and a moon when the switch is on (dark theme).
        ''' </summary>
        SunAndMoon = 1

    End Enum

End Namespace

#End Region
