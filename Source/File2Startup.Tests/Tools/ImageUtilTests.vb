' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ImageUtilTests.vb" company="Elektro Studios">
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
Imports System.Drawing
Imports System.IO

Imports DevCase.Core.Imaging.Tools

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " ImageUtilTests "

Namespace Tools

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="ImageUtil"/> class.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    <TestCategory("Integration")>
    Public NotInheritable Class ImageUtilTests

        ''' <summary>The icon of an existing executable file is extracted at 32x32.</summary>
        <TestMethod>
        Public Sub ExtractIconFromFile_ExistingExecutable_Returns32x32Icon()

            Dim filePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "notepad.exe")

            Using ico As Icon = ImageUtil.ExtractIconFromFile(filePath)
                Assert.IsNotNull(ico)
                Assert.AreEqual(New Size(32, 32), ico.Size)
            End Using

        End Sub

        ''' <summary>
        ''' The icon associated with the file name extension is returned even if the file does not exist,
        ''' so the startup list can show an icon for entries whose target file was removed.
        ''' </summary>
        <TestMethod>
        Public Sub ExtractIconFromFile_MissingFile_ReturnsExtensionIcon()

            Dim filePath As String = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}", "missing.txt")

            Using ico As Icon = ImageUtil.ExtractIconFromFile(filePath)
                Assert.IsNotNull(ico)
            End Using

        End Sub

        ''' <summary>The extracted icon can be converted to a bitmap, as the recent files menu does.</summary>
        <TestMethod>
        Public Sub ExtractIconFromFile_Icon_CanBeConvertedToBitmap()

            Dim filePath As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "notepad.exe")

            Using ico As Icon = ImageUtil.ExtractIconFromFile(filePath),
                  bmp As Bitmap = ico.ToBitmap()

                Assert.AreEqual(32, bmp.Width)
                Assert.AreEqual(32, bmp.Height)
            End Using

        End Sub

        ''' <summary>An image is resized to the requested size, keeping its colors.</summary>
        <TestMethod>
        Public Sub ResizeImage_ResizesAndKeepsColors()

            Using source As New Bitmap(32, 32)
                Using g As Graphics = Graphics.FromImage(source)
                    g.Clear(Color.Red)
                End Using

                Using resized As Bitmap = ImageUtil.ResizeImage(source, New Size(16, 16))
                    Assert.AreEqual(New Size(16, 16), resized.Size)
                    Assert.AreEqual(Color.Red.ToArgb(), resized.GetPixel(8, 8).ToArgb())
                End Using
            End Using

        End Sub

        ''' <summary>Invalid arguments are rejected.</summary>
        <TestMethod>
        Public Sub ResizeImage_InvalidArguments_Throw()

            Using source As New Bitmap(8, 8)
                Assert.ThrowsExactly(Of ArgumentNullException)(Sub() ImageUtil.ResizeImage(Nothing, New Size(4, 4)))
                Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() ImageUtil.ResizeImage(source, New Size(0, 4)))
                Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() ImageUtil.ResizeImage(source, New Size(4, -1)))
            End Using

        End Sub

    End Class

End Namespace

#End Region
