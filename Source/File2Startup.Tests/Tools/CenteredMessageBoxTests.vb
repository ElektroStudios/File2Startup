' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="CenteredMessageBoxTests.vb" company="Elektro Studios">
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
Imports System.Threading
Imports System.Windows.Forms

Imports DevCase.Core.Application.UserInterface.Types

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " CenteredMessageBoxTests "

Namespace Tools

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="CenteredMessageBox"/> class.
    ''' <para></para>
    ''' The message box itself is never shown, because it would block the test run.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    <TestCategory("UI")>
    Public NotInheritable Class CenteredMessageBoxTests

        ''' <summary>
        ''' Creates a form with a created handle (required by <see cref="CenteredMessageBox"/>, which posts a callback to its owner).
        ''' </summary>
        ''' <returns>The new <see cref="Form"/>.</returns>
        Private Shared Function CreateFormWithHandle() As Form

            Dim form As New Form() With {.ShowInTaskbar = False}
            Dim unused As IntPtr = form.Handle ' Forces the handle creation.
            Return form

        End Function

        ''' <summary>The constructor stores the owner, and the timeout is infinite by default.</summary>
        <STATestMethod>
        Public Sub New_StoresOwnerAndInfiniteTimeout()

            Using form As Form = CenteredMessageBoxTests.CreateFormWithHandle(),
                  msgbox As New CenteredMessageBox(form)

                Assert.AreSame(form, msgbox.Owner)
                Assert.AreEqual(Timeout.Infinite, msgbox.TimeOut)
            End Using

        End Sub

        ''' <summary>The constructor stores the specified timeout.</summary>
        <STATestMethod>
        Public Sub New_WithTimeout_StoresTimeout()

            Using form As Form = CenteredMessageBoxTests.CreateFormWithHandle(),
                  msgbox As New CenteredMessageBox(form, 5000)

                Assert.AreEqual(5000, msgbox.TimeOut)
            End Using

        End Sub

        ''' <summary>The owner must have a window handle, otherwise the constructor fails fast.</summary>
        <STATestMethod>
        Public Sub New_OwnerWithoutHandle_ThrowsInvalidOperationException()

            Using form As New Form()
                Assert.ThrowsExactly(Of InvalidOperationException)(
                    Sub()
                        Using msgbox As New CenteredMessageBox(form)
                        End Using
                    End Sub)
            End Using

        End Sub

        ''' <summary>Disposing the message box releases the owner reference.</summary>
        <STATestMethod>
        Public Sub Dispose_ReleasesOwner()

            Using form As Form = CenteredMessageBoxTests.CreateFormWithHandle()
                Dim msgbox As New CenteredMessageBox(form)

                msgbox.Dispose()

                Assert.IsNull(msgbox.Owner)
                Assert.IsFalse(form.IsDisposed)

                ' The pending callback posted by the constructor must do nothing after disposal.
                Application.DoEvents()
            End Using

        End Sub

    End Class

End Namespace

#End Region
