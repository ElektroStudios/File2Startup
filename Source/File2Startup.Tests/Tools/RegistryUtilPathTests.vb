' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="RegistryUtilPathTests.vb" company="Elektro Studios">
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

Imports DevCase.Core.System.Tools

Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Microsoft.Win32

#End Region

#Region " RegistryUtilPathTests "

Namespace Tools

    ''' <summary>
    ''' Tests for the registry path formatting and argument validation of the <see cref="RegistryUtil"/> class.
    ''' <para></para>
    ''' These tests do not write to the registry.
    ''' </summary>
    <TestClass>
    Public NotInheritable Class RegistryUtilPathTests

#Region " GetRootKeyName "

        ''' <summary>Root key abbreviations and full names are resolved to the full root key name, case-insensitive.</summary>
        <TestMethod>
        <DataRow("HKCU\Software", "HKEY_CURRENT_USER")>
        <DataRow("hkcu\Software", "HKEY_CURRENT_USER")>
        <DataRow("HKEY_CURRENT_USER\Software", "HKEY_CURRENT_USER")>
        <DataRow("HKLM\SOFTWARE", "HKEY_LOCAL_MACHINE")>
        <DataRow("HKEY_LOCAL_MACHINE\SOFTWARE", "HKEY_LOCAL_MACHINE")>
        <DataRow("HKCR\.txt", "HKEY_CLASSES_ROOT")>
        <DataRow("HKEY_CLASSES_ROOT\.txt", "HKEY_CLASSES_ROOT")>
        <DataRow("HKCC\System", "HKEY_CURRENT_CONFIG")>
        <DataRow("HKEY_CURRENT_CONFIG\System", "HKEY_CURRENT_CONFIG")>
        <DataRow("HKU\.DEFAULT", "HKEY_USERS")>
        <DataRow("HKEY_USERS\.DEFAULT", "HKEY_USERS")>
        <DataRow("HKEY_DYN_DATA\Key", "HKEY_DYN_DATA")>
        <DataRow("HKEY_PERFORMANCE_DATA\Key", "HKEY_PERFORMANCE_DATA")>
        <DataRow("HKCU", "HKEY_CURRENT_USER")>
        Public Sub GetRootKeyName_KnownRoots(registryPath As String, expected As String)

            Assert.AreEqual(expected, RegistryUtil.GetRootKeyName(registryPath))

        End Sub

        ''' <summary>Paths without a known root key return an empty root key name.</summary>
        <TestMethod>
        <DataRow("Software\Microsoft")>
        <DataRow("\HKCU\Software")>
        <DataRow("HKXX\Software")>
        <DataRow("")>
        Public Sub GetRootKeyName_UnknownRoot_ReturnsEmpty(registryPath As String)

            Assert.AreEqual(String.Empty, RegistryUtil.GetRootKeyName(registryPath))

        End Sub

#End Region

#Region " GetSubKeyPath "

        ''' <summary>The subkey path is the path without the root key and without leading/trailing backslashes.</summary>
        <TestMethod>
        <DataRow("HKCU\Software\Microsoft", "Software\Microsoft")>
        <DataRow("HKEY_CURRENT_USER\Software\Microsoft\", "Software\Microsoft")>
        <DataRow("HKLM\\SOFTWARE\\", "SOFTWARE")>
        <DataRow("Software\Microsoft", "Software\Microsoft")>
        <DataRow("\Software\Microsoft\", "Software\Microsoft")>
        Public Sub GetSubKeyPath_ReturnsSubKey(registryPath As String, expected As String)

            Assert.AreEqual(expected, RegistryUtil.GetSubKeyPath(registryPath))

        End Sub

        ''' <summary>
        ''' Regression test: a path that only contains the root key has an empty subkey path
        ''' (it threw <see cref="ArgumentOutOfRangeException"/> before).
        ''' </summary>
        <TestMethod>
        <DataRow("HKCU")>
        <DataRow("HKEY_LOCAL_MACHINE")>
        Public Sub GetSubKeyPath_RootOnly_ReturnsEmpty(registryPath As String)

            Assert.AreEqual(String.Empty, RegistryUtil.GetSubKeyPath(registryPath))

        End Sub

        ''' <summary>The startup registry keys used by the application are parsed into the expected root and subkey.</summary>
        <TestMethod>
        Public Sub StartupRegistryKeys_AreParsedCorrectly()

            Assert.AreEqual("HKEY_CURRENT_USER", RegistryUtil.GetRootKeyName(StartupRegistryKeys.CurrentUserRunKeyPath))
            Assert.AreEqual("HKEY_LOCAL_MACHINE", RegistryUtil.GetRootKeyName(StartupRegistryKeys.AllUsersRunKeyPath))
            Assert.AreEqual("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", RegistryUtil.GetSubKeyPath(StartupRegistryKeys.CurrentUserRunKeyPath))
            Assert.AreEqual("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", RegistryUtil.GetSubKeyPath(StartupRegistryKeys.AllUsersRunKeyPath))

        End Sub

#End Region

#Region " GetRootKey "

        ''' <summary>The root <see cref="RegistryKey"/> corresponds to the root key of the path.</summary>
        <TestMethod>
        <DataRow("HKCU\Software", "HKEY_CURRENT_USER")>
        <DataRow("HKLM\SOFTWARE", "HKEY_LOCAL_MACHINE")>
        <DataRow("HKCR\.txt", "HKEY_CLASSES_ROOT")>
        <DataRow("HKU\.DEFAULT", "HKEY_USERS")>
        <DataRow("HKCC\System", "HKEY_CURRENT_CONFIG")>
        Public Sub GetRootKey_KnownRoots_OpensExpectedHive(registryPath As String, expectedName As String)

            Using rootKey As RegistryKey = RegistryUtil.GetRootKey(registryPath, RegistryView.Default)
                Assert.IsNotNull(rootKey)
                Assert.AreEqual(expectedName, rootKey.Name)
            End Using

        End Sub

        ''' <summary>An unknown root key returns Nothing.</summary>
        <TestMethod>
        Public Sub GetRootKey_UnknownRoot_ReturnsNothing()

            Assert.IsNull(RegistryUtil.GetRootKey("Software\Microsoft", RegistryView.Default))

        End Sub

#End Region

#Region " Argument Validation "

        ''' <summary>The methods reject an empty root key name or subkey path.</summary>
        <TestMethod>
        Public Sub Methods_EmptyRootOrSubKey_ThrowArgumentNullException()

            Dim view As RegistryView = RegistryView.Default

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.ExistSubKey(view, " ", "Software"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.ExistSubKey(view, "HKCU", " "))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.ExistSubKey(view, "Software\NoRoot"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.ExistValue(view, "", "Software", "Value"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.ExistValue(view, "HKCU", "", "Value"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.GetValueData(Of String)(view, "", "Software", "Value", Nothing))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.GetValueData(Of String)(view, "HKCU", "", "Value", Nothing))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.CreateValue(Of String)(view, "", "Software", "Value", "Data"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.CreateValue(Of String)(view, "HKCU", "", "Value", "Data"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.DeleteValue(view, "", "Software", "Value"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.DeleteValue(view, "HKCU", "", "Value"))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RegistryUtil.DeleteValue(view, "HKCU", "Software", ""))

        End Sub

#End Region

    End Class

End Namespace

#End Region
