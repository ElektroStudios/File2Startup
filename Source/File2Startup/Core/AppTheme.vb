' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="AppTheme.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " AppTheme "

Namespace Core

    ''' <summary>
    ''' Specifies the visual theme of the application.
    ''' </summary>
    Public Enum AppTheme As Integer

        ''' <summary>
        ''' Light theme, that uses the default Windows system colors.
        ''' </summary>
        Light = 0

        ''' <summary>
        ''' Dark theme.
        ''' </summary>
        Dark = 1

    End Enum

End Namespace

#End Region
