' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="NativeRectangleTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Drawing
Imports System.Runtime.InteropServices

Imports DevCase.Interop.Win32.Types

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " NativeRectangleTests "

Namespace Interop

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="NativeRectangle"/> structure.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    Public NotInheritable Class NativeRectangleTests

        ''' <summary>The constructor stores the four edges.</summary>
        <TestMethod>
        Public Sub New_StoresEdges()

            Dim rect As New NativeRectangle(10, 20, 110, 70)

            Assert.AreEqual(10, rect.Left)
            Assert.AreEqual(20, rect.Top)
            Assert.AreEqual(110, rect.Right)
            Assert.AreEqual(70, rect.Bottom)

        End Sub

        ''' <summary>The constructor that takes a <see cref="Rectangle"/> converts width/height into right/bottom edges.</summary>
        <TestMethod>
        Public Sub New_FromRectangle_ConvertsToEdges()

            Dim rect As New NativeRectangle(New Rectangle(10, 20, 100, 50))

            Assert.AreEqual(10, rect.Left)
            Assert.AreEqual(20, rect.Top)
            Assert.AreEqual(110, rect.Right)
            Assert.AreEqual(70, rect.Bottom)

        End Sub

        ''' <summary>The conversion to <see cref="Rectangle"/> computes the width and height.</summary>
        <TestMethod>
        Public Sub WideningToRectangle_ComputesSize()

            Dim rect As Rectangle = New NativeRectangle(-50, -20, 150, 80)

            Assert.AreEqual(New Rectangle(-50, -20, 200, 100), rect)

        End Sub

        ''' <summary>Converting back and forth preserves the rectangle.</summary>
        <TestMethod>
        Public Sub Conversions_RoundTrip()

            Dim original As New Rectangle(5, 6, 7, 8)

            Dim nativeRect As NativeRectangle = original
            Dim roundTrip As Rectangle = nativeRect

            Assert.AreEqual(original, roundTrip)

        End Sub

        ''' <summary>The structure has the same size as the Win32 <c>RECT</c> structure (four 32-bit integers).</summary>
        <TestMethod>
        Public Sub StructureSize_MatchesWin32Rect()

            Assert.AreEqual(16, Marshal.SizeOf(GetType(NativeRectangle)))

        End Sub

    End Class

End Namespace

#End Region
