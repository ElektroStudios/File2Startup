' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="WindowThemeUtilTests.vb" company="Elektro Studios">
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
Imports System.Windows.Forms

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UserInterface.Theming

#End Region

#Region " WindowThemeUtilTests "

Namespace UserInterface

    ''' <summary>
    ''' Tests for the <see cref="WindowThemeUtil"/> class.
    ''' </summary>
    <TestClass>
    <TestCategory("UI")>
    Public NotInheritable Class WindowThemeUtilTests

        ''' <summary>Null arguments are rejected.</summary>
        <TestMethod>
        Public Sub NullArguments_ThrowArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() WindowThemeUtil.SetDarkTitleBar(Nothing, True))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() WindowThemeUtil.SetDarkScrollBars(Nothing, True))

        End Sub

        ''' <summary>
        ''' Applying the title bar theme before the window handle exists fails fast
        ''' (it must be applied from <see cref="Form.OnHandleCreated"/>, where it takes effect).
        ''' </summary>
        <STATestMethod>
        Public Sub WithoutHandle_ThrowsInvalidOperationException()

            Using form As New Form(),
                  vScroll As New VScrollBar()

                Assert.ThrowsExactly(Of InvalidOperationException)(Sub() WindowThemeUtil.SetDarkTitleBar(form, True))
                Assert.ThrowsExactly(Of InvalidOperationException)(Sub() WindowThemeUtil.SetDarkScrollBars(vScroll, True))
                Assert.IsFalse(form.IsHandleCreated, "The method must not create the window handle by itself.")
            End Using

        End Sub

        ''' <summary>
        ''' The dark title bar can be enabled and disabled on a hidden window
        ''' (the attribute is supported since Windows 10 version 1809).
        ''' </summary>
        <STATestMethod>
        Public Sub SetDarkTitleBar_WithHandle_TogglesWithoutError()

            Using form As New Form() With {.ShowInTaskbar = False}
                Dim unused As IntPtr = form.Handle ' Forces the handle creation.

                Dim darkApplied As Boolean = WindowThemeUtil.SetDarkTitleBar(form, True)
                Dim lightApplied As Boolean = WindowThemeUtil.SetDarkTitleBar(form, False)

                If Environment.OSVersion.Version.Major >= 10 AndAlso Environment.OSVersion.Version.Build >= 17763 Then
                    Assert.IsTrue(darkApplied)
                    Assert.IsTrue(lightApplied)
                Else
                    Assert.AreEqual(darkApplied, lightApplied)
                End If
            End Using

        End Sub

        ''' <summary>The dark scroll bar visual style can be applied and removed.</summary>
        <STATestMethod>
        Public Sub SetDarkScrollBars_WithHandle_TogglesWithoutError()

            Using vScroll As New VScrollBar()
                Dim unused As IntPtr = vScroll.Handle ' Forces the handle creation.

                Assert.IsTrue(WindowThemeUtil.SetDarkScrollBars(vScroll, True))
                Assert.IsTrue(WindowThemeUtil.SetDarkScrollBars(vScroll, False))
            End Using

        End Sub

    End Class

End Namespace

#End Region
