' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="FileSystemUtilTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports DevCase.Core.Imaging.Tools

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " FileSystemUtilTests "

Namespace Tools

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="FileSystemUtil"/> class.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    Public NotInheritable Class FileSystemUtilTests

        ''' <summary>Directory paths without invalid path characters are valid.</summary>
        <TestMethod>
        <DataRow("C:\Windows\System32")>
        <DataRow("C:\Program Files (x86)\My App")>
        <DataRow("\\server\share\dir")>
        <DataRow("relative\dir")>
        <DataRow("")>
        Public Sub IsValidWindowsDirectoryPath_ValidPaths_ReturnsTrue(directoryPath As String)

            Assert.IsTrue(FileSystemUtil.IsValidWindowsDirectoryPath(directoryPath))

        End Sub

        ''' <summary>Directory paths with invalid path characters are not valid.</summary>
        <TestMethod>
        <DataRow("C:\Dir|Name")>
        <DataRow("C:\Dir""Name")>
        <DataRow("C:\Dir<Name")>
        <DataRow("C:\Dir>Name")>
        <DataRow("C:\Dir" & vbNullChar & "Name")>
        Public Sub IsValidWindowsDirectoryPath_InvalidPaths_ReturnsFalse(directoryPath As String)

            Assert.IsFalse(FileSystemUtil.IsValidWindowsDirectoryPath(directoryPath))

        End Sub

        ''' <summary>File names without invalid file name characters are valid.</summary>
        <TestMethod>
        <DataRow("app.exe")>
        <DataRow("My File (1).txt")>
        <DataRow("name.with.dots")>
        Public Sub IsValidWindowsFileName_ValidNames_ReturnsTrue(fileName As String)

            Assert.IsTrue(FileSystemUtil.IsValidWindowsFileName(fileName))

        End Sub

        ''' <summary>File names with invalid file name characters (including path separators) are not valid.</summary>
        <TestMethod>
        <DataRow("dir\app.exe")>
        <DataRow("dir/app.exe")>
        <DataRow("C:app.exe")>
        <DataRow("app?.exe")>
        <DataRow("app*.exe")>
        <DataRow("app"".exe")>
        <DataRow("app|.exe")>
        Public Sub IsValidWindowsFileName_InvalidNames_ReturnsFalse(fileName As String)

            Assert.IsFalse(FileSystemUtil.IsValidWindowsFileName(fileName))

        End Sub

    End Class

End Namespace

#End Region
