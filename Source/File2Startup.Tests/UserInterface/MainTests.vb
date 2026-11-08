' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="MainTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Security.Principal

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " MainTests "

Namespace UserInterface

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the shared members of the <see cref="Global.UserInterface.Main"/> form.
    ''' <para></para>
    ''' The form itself is not instantiated, because its constructor reads and saves the user settings.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    Public NotInheritable Class MainTests

        ''' <summary><see cref="Global.UserInterface.Main.IsProcessElevated"/> reflects the administrator role of the current process.</summary>
        <TestMethod>
        Public Sub IsProcessElevated_MatchesAdministratorRole()

            Dim expected As Boolean
            Using identity As WindowsIdentity = WindowsIdentity.GetCurrent()
                expected = New WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator)
            End Using

            Assert.AreEqual(expected, Global.UserInterface.Main.IsProcessElevated)

        End Sub

    End Class

End Namespace

#End Region
