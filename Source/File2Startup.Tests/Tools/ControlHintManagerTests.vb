' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ControlHintManagerTests.vb" company="Elektro Studios">
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
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Imports DevCase.Core.Application.Forms
Imports DevCase.Core.Application.UserInterface.Enums
Imports DevCase.Core.Application.UserInterface.Types

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " ControlHintManagerTests "

Namespace Tools

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="ControlHintManager"/> and <see cref="ControlHintInfo"/> classes.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    <TestCategory("UI")>
    Public NotInheritable Class ControlHintManagerTests

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The hint text used by the tests.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Const HintText As String = "Full path to the file."

#End Region

#Region " Fields "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The controls created by the current test, to remove their hints and dispose them after the test.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly createdControls As New List(Of Control)

#End Region

#Region " Helpers / Cleanup "

        ''' <summary>
        ''' Creates a <see cref="TextBox"/> that is tracked for cleanup.
        ''' </summary>
        ''' <returns>The new <see cref="TextBox"/>.</returns>
        Private Function CreateTextBox() As TextBox

            Dim tb As New TextBox() With {.ForeColor = Color.Black}
            Me.createdControls.Add(tb)
            Return tb

        End Function

        ''' <summary>
        ''' Creates a hint info with the test hint text and a gray color.
        ''' </summary>
        ''' <param name="hintType">The hint type.</param>
        ''' <param name="font">The hint font.</param>
        ''' <returns>The new <see cref="ControlHintInfo"/>.</returns>
        Private Shared Function CreateHintInfo(hintType As ControlHintType, font As Font) As ControlHintInfo

            Return New ControlHintInfo(ControlHintManagerTests.HintText, font, Color.Gray, hintType)

        End Function

        ''' <summary>Removes the hints of the controls created by the test and disposes them.</summary>
        <TestCleanup>
        Public Sub Cleanup()

            For Each ctrl As Control In Me.createdControls
                ControlHintManager.RemoveHint(ctrl)
                ctrl.Dispose()
            Next ctrl
            Me.createdControls.Clear()

        End Sub

#End Region

#Region " ControlHintInfo "

        ''' <summary><see cref="ControlHintInfo"/> stores the values passed to its constructor.</summary>
        <TestMethod>
        Public Sub ControlHintInfo_StoresValues()

            Using font As New Font(FontFamily.GenericSansSerif, 9.0F)
                Dim info As New ControlHintInfo("hint", font, Color.Red, ControlHintType.Persistent)

                Assert.AreEqual("hint", info.Text)
                Assert.AreSame(font, info.Font)
                Assert.AreEqual(Color.Red, info.ForeColor)
                Assert.AreEqual(ControlHintType.Persistent, info.HintType)
            End Using

        End Sub

#End Region

#Region " SetHint "

        ''' <summary>A persistent hint is shown in an empty control, with the hint color.</summary>
        <STATestMethod>
        Public Sub SetHint_PersistentOnEmptyControl_ShowsHint()

            Dim tb As TextBox = Me.CreateTextBox()

            ControlHintManager.SetHint(tb, ControlHintManagerTests.CreateHintInfo(ControlHintType.Persistent, tb.Font))

            Assert.AreEqual(ControlHintManagerTests.HintText, tb.Text)
            Assert.AreEqual(Color.Gray, tb.ForeColor)
            Assert.IsTrue(ControlHintManager.ControlHints.ContainsKey(tb))

        End Sub

        ''' <summary>A hint is not shown in a control that already has text.</summary>
        <STATestMethod>
        Public Sub SetHint_OnControlWithText_KeepsText()

            Dim tb As TextBox = Me.CreateTextBox()
            tb.Text = "C:\App\app.exe"

            ControlHintManager.SetHint(tb, ControlHintManagerTests.CreateHintInfo(ControlHintType.Persistent, tb.Font))

            Assert.AreEqual("C:\App\app.exe", tb.Text)
            Assert.AreEqual(Color.Black, tb.ForeColor)

        End Sub

        ''' <summary>Setting a hint again on the same control replaces the hint info.</summary>
        <STATestMethod>
        Public Sub SetHint_Twice_ReplacesHintInfo()

            Dim tb As TextBox = Me.CreateTextBox()
            Dim info1 As ControlHintInfo = ControlHintManagerTests.CreateHintInfo(ControlHintType.Persistent, tb.Font)
            Dim info2 As New ControlHintInfo("Other hint", tb.Font, Color.Gray, ControlHintType.Persistent)

            ControlHintManager.SetHint(tb, info1)
            ControlHintManager.SetHint(tb, info2)

            Assert.AreSame(info2, ControlHintManager.ControlHints(tb))

        End Sub

        ''' <summary>A hint with empty text is rejected.</summary>
        <STATestMethod>
        Public Sub SetHint_EmptyText_ThrowsArgumentNullException()

            Dim tb As TextBox = Me.CreateTextBox()
            Dim info As New ControlHintInfo(String.Empty, tb.Font, Color.Gray, ControlHintType.Persistent)

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() ControlHintManager.SetHint(tb, info))

        End Sub

        ''' <summary>The array overload sets the hint on every control.</summary>
        <STATestMethod>
        Public Sub SetHint_Array_SetsHintOnEveryControl()

            Dim tb1 As TextBox = Me.CreateTextBox()
            Dim tb2 As TextBox = Me.CreateTextBox()

            ControlHintManager.SetHint({tb1, tb2}, ControlHintManagerTests.CreateHintInfo(ControlHintType.Persistent, tb1.Font))

            Assert.AreEqual(ControlHintManagerTests.HintText, tb1.Text)
            Assert.AreEqual(ControlHintManagerTests.HintText, tb2.Text)

        End Sub

        ''' <summary>
        ''' Regression test: a normal hint on a control that is not hosted in a container control does not throw
        ''' (it threw <see cref="NullReferenceException"/> before).
        ''' </summary>
        <STATestMethod>
        Public Sub SetHint_NormalOnControlWithoutContainer_DoesNotThrow()

            Dim tb As TextBox = Me.CreateTextBox()

            ControlHintManager.SetHint(tb, ControlHintManagerTests.CreateHintInfo(ControlHintType.Normal, tb.Font))

            Assert.AreEqual(ControlHintManagerTests.HintText, tb.Text)

        End Sub

        ''' <summary>
        ''' Regression test: a normal hint on a control hosted in a form without active control does not throw
        ''' (it threw <see cref="NullReferenceException"/> before).
        ''' </summary>
        <STATestMethod>
        Public Sub SetHint_NormalInFormWithoutActiveControl_DoesNotThrow()

            Using form As New Form()
                Dim tb As TextBox = Me.CreateTextBox()
                form.Controls.Add(tb)

                ControlHintManager.SetHint(tb, ControlHintManagerTests.CreateHintInfo(ControlHintType.Normal, tb.Font))

                Assert.AreEqual(ControlHintManagerTests.HintText, tb.Text)

                ControlHintManager.RemoveHint(tb)
                form.Controls.Remove(tb)
            End Using

        End Sub

#End Region

#Region " RemoveHint "

        ''' <summary>Removing a shown hint restores the original (empty) text and color.</summary>
        <STATestMethod>
        Public Sub RemoveHint_ShownHint_RestoresOriginalProperties()

            Dim tb As TextBox = Me.CreateTextBox()
            ControlHintManager.SetHint(tb, ControlHintManagerTests.CreateHintInfo(ControlHintType.Persistent, tb.Font))

            ControlHintManager.RemoveHint(tb)

            Assert.AreEqual(String.Empty, tb.Text)
            Assert.AreEqual(Color.Black, tb.ForeColor)
            Assert.IsFalse(ControlHintManager.ControlHints.ContainsKey(tb))

        End Sub

        ''' <summary>Removing a hint after the user typed text keeps the typed text.</summary>
        <STATestMethod>
        Public Sub RemoveHint_AfterUserText_KeepsUserText()

            Dim tb As TextBox = Me.CreateTextBox()
            ControlHintManager.SetHint(tb, ControlHintManagerTests.CreateHintInfo(ControlHintType.Persistent, tb.Font))
            tb.Text = "C:\App\app.exe"

            ControlHintManager.RemoveHint(tb)

            Assert.AreEqual("C:\App\app.exe", tb.Text)
            Assert.AreEqual(Color.Black, tb.ForeColor)

        End Sub

        ''' <summary>Removing the hint of a control without hint does nothing.</summary>
        <STATestMethod>
        Public Sub RemoveHint_ControlWithoutHint_DoesNothing()

            Dim tb As TextBox = Me.CreateTextBox()
            tb.Text = "text"

            ControlHintManager.RemoveHint(tb)

            Assert.AreEqual("text", tb.Text)

        End Sub

        ''' <summary>The array overload removes the hint from every control.</summary>
        <STATestMethod>
        Public Sub RemoveHint_Array_RemovesHintFromEveryControl()

            Dim tb1 As TextBox = Me.CreateTextBox()
            Dim tb2 As TextBox = Me.CreateTextBox()
            ControlHintManager.SetHint({tb1, tb2}, ControlHintManagerTests.CreateHintInfo(ControlHintType.Persistent, tb1.Font))

            ControlHintManager.RemoveHint({tb1, tb2})

            Assert.AreEqual(String.Empty, tb1.Text)
            Assert.AreEqual(String.Empty, tb2.Text)

        End Sub

        ''' <summary>Disposing a control removes its hint from the manager.</summary>
        <STATestMethod>
        Public Sub DisposingControl_RemovesHint()

            Dim tb As New TextBox()
            ControlHintManager.SetHint(tb, ControlHintManagerTests.CreateHintInfo(ControlHintType.Persistent, tb.Font))

            tb.Dispose()

            Assert.IsFalse(ControlHintManager.ControlHints.ContainsKey(tb))

        End Sub

#End Region

    End Class

End Namespace

#End Region
