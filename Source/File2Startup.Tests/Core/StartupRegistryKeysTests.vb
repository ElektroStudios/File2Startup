' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="StartupRegistryKeysTests.vb" company="Elektro Studios">
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

#End Region

#Region " StartupRegistryKeysTests "

Namespace Core

    ''' <summary>
    ''' Tests for the <see cref="StartupRegistryKeys"/> class.
    ''' </summary>
    <TestClass>
    Public NotInheritable Class StartupRegistryKeysTests

        ''' <summary>The registry key paths are the standard Windows <c>Run</c> keys of HKCU and HKLM.</summary>
        <TestMethod>
        Public Sub RunKeyPaths_AreStandardWindowsRunKeys()

            Dim keyPaths As String() = {StartupRegistryKeys.CurrentUserRunKeyPath, StartupRegistryKeys.AllUsersRunKeyPath}

            CollectionAssert.AreEqual({"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run",
                                       "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"}, keyPaths)

        End Sub

        ''' <summary>
        ''' The identifiers match the <c>Tag</c> values assigned to the radio buttons in the designer of the main form.
        ''' </summary>
        <TestMethod>
        Public Sub Ids_MatchRadioButtonTags()

            Dim ids As String() = {StartupRegistryKeys.CurrentUserId, StartupRegistryKeys.AllUsersId}

            CollectionAssert.AreEqual({"CurrentUserKey", "AllUsersKey"}, ids)

        End Sub

        ''' <summary>The key path is resolved from the startup type identifier.</summary>
        <TestMethod>
        Public Sub GetKeyPathFromId_KnownIds()

            Assert.AreEqual(StartupRegistryKeys.CurrentUserRunKeyPath, StartupRegistryKeys.GetKeyPathFromId(StartupRegistryKeys.CurrentUserId))
            Assert.AreEqual(StartupRegistryKeys.AllUsersRunKeyPath, StartupRegistryKeys.GetKeyPathFromId(StartupRegistryKeys.AllUsersId))

        End Sub

        ''' <summary>The key path is resolved from the startup type display name.</summary>
        <TestMethod>
        Public Sub GetKeyPathFromDisplayName_KnownNames()

            Assert.AreEqual(StartupRegistryKeys.CurrentUserRunKeyPath, StartupRegistryKeys.GetKeyPathFromDisplayName("Current User"))
            Assert.AreEqual(StartupRegistryKeys.AllUsersRunKeyPath, StartupRegistryKeys.GetKeyPathFromDisplayName("All Users"))

        End Sub

        ''' <summary>The display name is resolved from the startup type identifier.</summary>
        <TestMethod>
        Public Sub GetDisplayNameFromId_KnownIds()

            Assert.AreEqual("Current User", StartupRegistryKeys.GetDisplayNameFromId(StartupRegistryKeys.CurrentUserId))
            Assert.AreEqual("All Users", StartupRegistryKeys.GetDisplayNameFromId(StartupRegistryKeys.AllUsersId))

        End Sub

        ''' <summary>Unknown identifiers and display names are rejected (case-sensitive, as the original comparisons).</summary>
        <TestMethod>
        <DataRow(Nothing)>
        <DataRow("")>
        <DataRow("currentuserkey")>
        <DataRow("Unknown")>
        Public Sub UnknownValues_ThrowArgumentException(value As String)

            Assert.ThrowsExactly(Of ArgumentException)(Sub() StartupRegistryKeys.GetKeyPathFromId(value))
            Assert.ThrowsExactly(Of ArgumentException)(Sub() StartupRegistryKeys.GetKeyPathFromDisplayName(value))
            Assert.ThrowsExactly(Of ArgumentException)(Sub() StartupRegistryKeys.GetDisplayNameFromId(value))

        End Sub

    End Class

End Namespace

#End Region
