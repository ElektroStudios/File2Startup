' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="RegistryUtilIntegrationTests.vb" company="Elektro Studios">
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

#Region " RegistryUtilIntegrationTests "

Namespace Tools

    ''' <summary>
    ''' Integration tests for the <see cref="RegistryUtil"/> class.
    ''' <para></para>
    ''' Each test works on its own temporary key under <c>HKEY_CURRENT_USER\Software\File2Startup.Tests</c>,
    ''' which is deleted after the test. The real Windows startup keys are never modified.
    ''' </summary>
    <TestClass>
    <TestCategory("Integration")>
    Public NotInheritable Class RegistryUtilIntegrationTests

#Region " Constants "

        ''' <summary>
        ''' The parent subkey (relative to <c>HKEY_CURRENT_USER</c>) of the temporary test keys.
        ''' </summary>
        Private Const TestsParentSubKey As String = "Software\File2Startup.Tests"

#End Region

#Region " Fields "

        ''' <summary>
        ''' The subkey path (relative to <c>HKEY_CURRENT_USER</c>) of the temporary key of the current test.
        ''' </summary>
        Private testSubKey As String

        ''' <summary>
        ''' The full path (with abbreviated root key) of the temporary key of the current test.
        ''' </summary>
        Private testKeyFullPath As String

#End Region

#Region " Initialization / Cleanup "

        ''' <summary>Creates the temporary key of the current test.</summary>
        <TestInitialize>
        Public Sub Initialize()

            Me.testSubKey = $"{RegistryUtilIntegrationTests.TestsParentSubKey}\{Guid.NewGuid():N}"
            Me.testKeyFullPath = $"HKCU\{Me.testSubKey}"
            Registry.CurrentUser.CreateSubKey(Me.testSubKey).Dispose()

        End Sub

        ''' <summary>Deletes the temporary key of the current test (and the parent key, if it became empty).</summary>
        <TestCleanup>
        Public Sub Cleanup()

            Registry.CurrentUser.DeleteSubKeyTree(Me.testSubKey, throwOnMissingSubKey:=False)

            Using parentKey As RegistryKey = Registry.CurrentUser.OpenSubKey(RegistryUtilIntegrationTests.TestsParentSubKey, writable:=False)
                Dim isParentEmpty As Boolean = parentKey IsNot Nothing AndAlso parentKey.SubKeyCount = 0 AndAlso parentKey.ValueCount = 0
                If isParentEmpty Then
                    parentKey.Close()
                    Registry.CurrentUser.DeleteSubKey(RegistryUtilIntegrationTests.TestsParentSubKey, throwOnMissingSubKey:=False)
                End If
            End Using

        End Sub

#End Region

#Region " Subkeys "

        ''' <summary>An existing subkey is detected, through both overloads.</summary>
        <TestMethod>
        Public Sub ExistSubKey_ExistingKey_ReturnsTrue()

            Assert.IsTrue(RegistryUtil.ExistSubKey(RegistryView.Default, Me.testKeyFullPath))
            Assert.IsTrue(RegistryUtil.ExistSubKey(RegistryView.Default, "HKEY_CURRENT_USER", Me.testSubKey))

        End Sub

        ''' <summary>A missing subkey is not detected.</summary>
        <TestMethod>
        Public Sub ExistSubKey_MissingKey_ReturnsFalse()

            Assert.IsFalse(RegistryUtil.ExistSubKey(RegistryView.Default, $"{Me.testKeyFullPath}\Missing"))

        End Sub

#End Region

#Region " Values "

        ''' <summary>A string value is created, detected and read back.</summary>
        <TestMethod>
        Public Sub CreateValue_String_CanBeReadBack()

            RegistryUtil.CreateValue(Of String)(RegistryView.Default, Me.testKeyFullPath, "MyApp", """C:\App\app.exe"" -x")

            Assert.IsTrue(RegistryUtil.ExistValue(RegistryView.Default, Me.testKeyFullPath, "MyApp"))
            Assert.AreEqual("""C:\App\app.exe"" -x", RegistryUtil.GetValueData(Of String)(RegistryView.Default, Me.testKeyFullPath, "MyApp"))

            Using key As RegistryKey = Registry.CurrentUser.OpenSubKey(Me.testSubKey, writable:=False)
                Assert.AreEqual(RegistryValueKind.String, key.GetValueKind("MyApp"))
            End Using

        End Sub

        ''' <summary>A DWORD value is created and read back with its type.</summary>
        <TestMethod>
        Public Sub CreateValue_DWord_CanBeReadBack()

            RegistryUtil.CreateValue(Of Integer)(RegistryView.Default, Me.testKeyFullPath, "Number", 1234, RegistryValueKind.DWord)

            Assert.AreEqual(1234, RegistryUtil.GetValueData(Of Integer)(RegistryView.Default, Me.testKeyFullPath, "Number"))

        End Sub

        ''' <summary>Creating an existing value replaces its data.</summary>
        <TestMethod>
        Public Sub CreateValue_ExistingValue_IsReplaced()

            RegistryUtil.CreateValue(Of String)(RegistryView.Default, Me.testKeyFullPath, "MyApp", "old")
            RegistryUtil.CreateValue(Of String)(RegistryView.Default, Me.testKeyFullPath, "MyApp", "new")

            Assert.AreEqual("new", RegistryUtil.GetValueData(Of String)(RegistryView.Default, Me.testKeyFullPath, "MyApp"))

        End Sub

        ''' <summary>Creating a value in a missing subkey creates the subkey.</summary>
        <TestMethod>
        Public Sub CreateValue_MissingSubKey_CreatesSubKey()

            Dim nestedKeyPath As String = $"{Me.testKeyFullPath}\Nested\Deeper"

            RegistryUtil.CreateValue(Of String)(RegistryView.Default, nestedKeyPath, "Value", "Data")

            Assert.IsTrue(RegistryUtil.ExistSubKey(RegistryView.Default, nestedKeyPath))
            Assert.IsTrue(RegistryUtil.ExistValue(RegistryView.Default, nestedKeyPath, "Value"))

        End Sub

        ''' <summary>A missing value is not detected, and reading it returns the default value.</summary>
        <TestMethod>
        Public Sub MissingValue_IsNotDetected_AndReturnsDefault()

            Assert.IsFalse(RegistryUtil.ExistValue(RegistryView.Default, Me.testKeyFullPath, "Missing"))
            Assert.IsNull(RegistryUtil.GetValueData(Of String)(RegistryView.Default, Me.testKeyFullPath, "Missing"))
            Assert.AreEqual("default", RegistryUtil.GetValueData(Of String)(RegistryView.Default, "HKEY_CURRENT_USER", Me.testSubKey, "Missing", "default"))

        End Sub

        ''' <summary>Reading a value of a missing subkey returns the default value instead of failing.</summary>
        <TestMethod>
        Public Sub GetValueData_MissingSubKey_ReturnsDefault()

            Dim value As String = RegistryUtil.GetValueData(Of String)(RegistryView.Default, "HKEY_CURRENT_USER", $"{Me.testSubKey}\Missing", "Value", "default")

            Assert.AreEqual("default", value)
            Assert.IsFalse(RegistryUtil.ExistValue(RegistryView.Default, $"{Me.testKeyFullPath}\Missing", "Value"))

        End Sub

        ''' <summary>Expandable strings are read unexpanded when requested, as the startup list does.</summary>
        <TestMethod>
        Public Sub GetValueData_ExpandString_DoNotExpandEnvironmentNames()

            RegistryUtil.CreateValue(Of String)(RegistryView.Default, Me.testKeyFullPath, "Expandable", "%WINDIR%\notepad.exe", RegistryValueKind.ExpandString)

            Dim unexpanded As String = RegistryUtil.GetValueData(Of String)(RegistryView.Default, Me.testKeyFullPath, "Expandable", RegistryValueOptions.DoNotExpandEnvironmentNames)
            Dim expanded As String = RegistryUtil.GetValueData(Of String)(RegistryView.Default, Me.testKeyFullPath, "Expandable")

            Assert.AreEqual("%WINDIR%\notepad.exe", unexpanded)
            Assert.AreEqual(Environment.ExpandEnvironmentVariables("%WINDIR%\notepad.exe"), expanded)

        End Sub

        ''' <summary>An existing value is deleted.</summary>
        <TestMethod>
        Public Sub DeleteValue_ExistingValue_IsDeleted()

            RegistryUtil.CreateValue(Of String)(RegistryView.Default, Me.testKeyFullPath, "MyApp", "data")

            RegistryUtil.DeleteValue(RegistryView.Default, Me.testKeyFullPath, "MyApp")

            Assert.IsFalse(RegistryUtil.ExistValue(RegistryView.Default, Me.testKeyFullPath, "MyApp"))

        End Sub

        ''' <summary>Deleting a missing value only fails when requested.</summary>
        <TestMethod>
        Public Sub DeleteValue_MissingValue_ThrowsOnlyWhenRequested()

            RegistryUtil.DeleteValue(RegistryView.Default, Me.testKeyFullPath, "Missing", throwOnMissingValue:=False)

            Assert.ThrowsExactly(Of ArgumentException)(Sub() RegistryUtil.DeleteValue(RegistryView.Default, Me.testKeyFullPath, "Missing", throwOnMissingValue:=True))

        End Sub

#End Region

#Region " Startup Entry Round-trip "

        ''' <summary>
        ''' End-to-end: a startup command stored the same way as the application does,
        ''' is read back unexpanded and parsed into the same file path and arguments.
        ''' </summary>
        <TestMethod>
        <DataRow("C:\Program Files\My App\app.exe", "")>
        <DataRow("C:\Program Files\My App\app.exe", "-minimized ""D:\Some File.txt""")>
        <DataRow("%LOCALAPPDATA%\My App\app.exe", "/tray")>
        Public Sub StartupCommand_StoreAndParse_RoundTrip(filePath As String, arguments As String)

            Dim original As New StartupCommand(filePath, arguments)
            RegistryUtil.CreateValue(Of String)(RegistryView.Default, Me.testKeyFullPath, "My App", original.ToCommandLine())

            Dim data As String = RegistryUtil.GetValueData(Of String)(RegistryView.Default, Me.testKeyFullPath, "My App", RegistryValueOptions.DoNotExpandEnvironmentNames)
            Dim parsed As StartupCommand = StartupCommand.Parse(data)

            Assert.AreEqual(original.ToCommandLine(), data)
            Assert.AreEqual(original.FilePath, parsed.FilePath)
            Assert.AreEqual(original.Arguments, parsed.Arguments)

        End Sub

#End Region

    End Class

End Namespace

#End Region
