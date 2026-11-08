' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="WindowMagnetizerTests.vb" company="Elektro Studios">
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
Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Imports DevCase.Core.Application.UserInterface.Types
Imports DevCase.Interop.Win32.Enums
Imports DevCase.Interop.Win32.Types

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " WindowMagnetizerTests "

Namespace Tools

    ''' <summary>
    ''' Tests for the <see cref="WindowMagnetizer"/> class.
    ''' </summary>
    <TestClass>
    <TestCategory("UI")>
    Public NotInheritable Class WindowMagnetizerTests

#Region " Testable Magnetizer "

        ''' <summary>
        ''' A <see cref="WindowMagnetizer"/> that exposes its window procedure and resizing state to the tests.
        ''' </summary>
        Private NotInheritable Class TestableWindowMagnetizer : Inherits WindowMagnetizer

            ''' <summary>Initializes a new instance of the <see cref="TestableWindowMagnetizer"/> class.</summary>
            ''' <param name="window">The window to magnetize.</param>
            Public Sub New(window As IWin32Window)
                MyBase.New(window)
            End Sub

            ''' <summary>Gets a value indicating whether the window is being resized.</summary>
            Public ReadOnly Property IsResizingWindow As Boolean
                Get
                    Return Me.isResizing
                End Get
            End Property

            ''' <summary>Sends a message to the window procedure of the magnetizer.</summary>
            ''' <param name="m">The message.</param>
            Public Sub SendMessage(ByRef m As Message)
                Me.WndProc(m)
            End Sub

        End Class

#End Region

#Region " Helpers "

        ''' <summary>
        ''' Creates a form with a created handle, at a fixed size.
        ''' </summary>
        ''' <returns>The new <see cref="Form"/>.</returns>
        Private Shared Function CreateFormWithHandle() As Form

            Dim form As New Form() With {
                .StartPosition = FormStartPosition.Manual,
                .FormBorderStyle = FormBorderStyle.FixedSingle,
                .Size = New Size(300, 200),
                .ShowInTaskbar = False
            }
            Dim unused As IntPtr = form.Handle ' Forces the handle creation.
            Return form

        End Function

        ''' <summary>
        ''' Sends a <see cref="WindowsMessages.WM_WindowPosChanging"/> message with the specified position
        ''' and returns the resulting position.
        ''' </summary>
        ''' <param name="magnetizer">The magnetizer.</param>
        ''' <param name="form">The magnetized form.</param>
        ''' <param name="x">The proposed X coordinate.</param>
        ''' <param name="y">The proposed Y coordinate.</param>
        ''' <param name="flags">The <see cref="SetWindowPosFlags"/> of the change.</param>
        ''' <returns>The resulting position.</returns>
        Private Shared Function SendWindowPosChanging(magnetizer As TestableWindowMagnetizer, form As Form,
                                                      x As Integer, y As Integer, flags As SetWindowPosFlags) As Point

            Dim windowPos As New WindowPos With {
                .Hwnd = form.Handle,
                .HwndInsertAfter = IntPtr.Zero,
                .X = x,
                .Y = y,
                .Width = form.Width,
                .Height = form.Height,
                .Flags = CInt(flags Or SetWindowPosFlags.IgnoreResize Or SetWindowPosFlags.IgnoreZOrder Or SetWindowPosFlags.DontActivate)
            }

            Dim windowPosHandle As IntPtr = Marshal.AllocHGlobal(Marshal.SizeOf(GetType(WindowPos)))
            Try
                Marshal.StructureToPtr(windowPos, windowPosHandle, fDeleteOld:=False)

                Dim m As Message = Message.Create(form.Handle, WindowsMessages.WM_WindowPosChanging, IntPtr.Zero, windowPosHandle)
                magnetizer.SendMessage(m)

                Dim result As WindowPos = DirectCast(Marshal.PtrToStructure(windowPosHandle, GetType(WindowPos)), WindowPos)
                Return New Point(result.X, result.Y)

            Finally
                Marshal.FreeHGlobal(windowPosHandle)

            End Try

        End Function

        ''' <summary>
        ''' Sends a parameterless message to the window procedure of the magnetizer.
        ''' </summary>
        ''' <param name="magnetizer">The magnetizer.</param>
        ''' <param name="form">The magnetized form.</param>
        ''' <param name="msg">The message identifier.</param>
        Private Shared Sub SendSimpleMessage(magnetizer As TestableWindowMagnetizer, form As Form, msg As WindowsMessages)

            Dim m As Message = Message.Create(form.Handle, msg, IntPtr.Zero, IntPtr.Zero)
            magnetizer.SendMessage(m)

        End Sub

        ''' <summary>
        ''' Gets a proposed window position that is 5 pixels away from the left border of the working area,
        ''' and far from the other borders.
        ''' </summary>
        ''' <param name="form">The magnetized form.</param>
        ''' <returns>The proposed position.</returns>
        Private Shared Function GetPositionNearLeftBorder(form As Form) As Point

            Dim workingArea As Rectangle = Screen.FromHandle(form.Handle).WorkingArea
            Return New Point(workingArea.X + 5, workingArea.Y + 200)

        End Function

#End Region

#Region " Constructor / Properties / Dispose "

        ''' <summary>The constructor assigns the window handle and the default property values.</summary>
        <STATestMethod>
        Public Sub New_AssignsHandleAndDefaults()

            Using form As Form = WindowMagnetizerTests.CreateFormWithHandle(),
                  magnetizer As New WindowMagnetizer(form)

                Assert.AreSame(form, magnetizer.OwnerWindow)
                Assert.AreEqual(form.Handle, magnetizer.Handle)
                Assert.AreEqual(20, magnetizer.Threshold)
                Assert.IsTrue(magnetizer.AllowOffscreen)
                Assert.IsFalse(magnetizer.Enabled)
            End Using

        End Sub

        ''' <summary>The properties can be changed, as the main form does.</summary>
        <STATestMethod>
        Public Sub Properties_CanBeChanged()

            Using form As Form = WindowMagnetizerTests.CreateFormWithHandle(),
                  magnetizer As New WindowMagnetizer(form) With {.Threshold = 35, .AllowOffscreen = False, .Enabled = True}

                Assert.AreEqual(35, magnetizer.Threshold)
                Assert.IsFalse(magnetizer.AllowOffscreen)
                Assert.IsTrue(magnetizer.Enabled)
            End Using

        End Sub

        ''' <summary>Disposing the magnetizer disables it and releases the window handle.</summary>
        <STATestMethod>
        Public Sub Dispose_DisablesAndReleasesHandle()

            Using form As Form = WindowMagnetizerTests.CreateFormWithHandle()
                Dim magnetizer As New WindowMagnetizer(form) With {.Threshold = 35, .Enabled = True}

                magnetizer.Dispose()

                Assert.IsFalse(magnetizer.Enabled)
                Assert.AreEqual(0, magnetizer.Threshold)
                Assert.AreEqual(IntPtr.Zero, magnetizer.Handle)
                Assert.IsFalse(form.IsDisposed)
            End Using

        End Sub

#End Region

#Region " Window Procedure "

        ''' <summary>
        ''' Regression test: an enabled magnetizer docks a window that is moved near a screen border
        ''' (the window procedure had been pruned, so the window was never docked).
        ''' </summary>
        <STATestMethod>
        Public Sub WndProc_Enabled_DocksWindowNearLeftBorder()

            Using form As Form = WindowMagnetizerTests.CreateFormWithHandle(),
                  magnetizer As New TestableWindowMagnetizer(form) With {.Threshold = 35, .Enabled = True}

                Dim proposed As Point = WindowMagnetizerTests.GetPositionNearLeftBorder(form)
                Dim workingArea As Rectangle = Screen.FromHandle(form.Handle).WorkingArea

                Dim result As Point = WindowMagnetizerTests.SendWindowPosChanging(magnetizer, form, proposed.X, proposed.Y, SetWindowPosFlags.None)

                Assert.AreEqual(workingArea.X, result.X)
                Assert.AreEqual(proposed.Y, result.Y)
            End Using

        End Sub

        ''' <summary>A disabled magnetizer does not change the window position.</summary>
        <STATestMethod>
        Public Sub WndProc_Disabled_DoesNotChangePosition()

            Using form As Form = WindowMagnetizerTests.CreateFormWithHandle(),
                  magnetizer As New TestableWindowMagnetizer(form) With {.Threshold = 35, .Enabled = False}

                Dim proposed As Point = WindowMagnetizerTests.GetPositionNearLeftBorder(form)

                Dim result As Point = WindowMagnetizerTests.SendWindowPosChanging(magnetizer, form, proposed.X, proposed.Y, SetWindowPosFlags.None)

                Assert.AreEqual(proposed, result)
            End Using

        End Sub

        ''' <summary>A position change flagged with <see cref="SetWindowPosFlags.IgnoreMove"/> is not altered.</summary>
        <STATestMethod>
        Public Sub WndProc_IgnoreMoveFlag_DoesNotChangePosition()

            Using form As Form = WindowMagnetizerTests.CreateFormWithHandle(),
                  magnetizer As New TestableWindowMagnetizer(form) With {.Threshold = 35, .Enabled = True}

                Dim proposed As Point = WindowMagnetizerTests.GetPositionNearLeftBorder(form)

                Dim result As Point = WindowMagnetizerTests.SendWindowPosChanging(magnetizer, form, proposed.X, proposed.Y, SetWindowPosFlags.IgnoreMove)

                Assert.AreEqual(proposed, result)
            End Using

        End Sub

        ''' <summary>While the window is being resized it is not docked; docking resumes when the resizing ends.</summary>
        <STATestMethod>
        Public Sub WndProc_WhileResizing_DoesNotDockUntilResizingEnds()

            Using form As Form = WindowMagnetizerTests.CreateFormWithHandle(),
                  magnetizer As New TestableWindowMagnetizer(form) With {.Threshold = 35, .Enabled = True}

                Dim proposed As Point = WindowMagnetizerTests.GetPositionNearLeftBorder(form)
                Dim workingArea As Rectangle = Screen.FromHandle(form.Handle).WorkingArea

                WindowMagnetizerTests.SendSimpleMessage(magnetizer, form, WindowsMessages.WM_Sizing)
                Assert.IsTrue(magnetizer.IsResizingWindow)
                Dim whileResizing As Point = WindowMagnetizerTests.SendWindowPosChanging(magnetizer, form, proposed.X, proposed.Y, SetWindowPosFlags.None)

                WindowMagnetizerTests.SendSimpleMessage(magnetizer, form, WindowsMessages.WM_ExitSizeMove)
                Assert.IsFalse(magnetizer.IsResizingWindow)
                Dim afterResizing As Point = WindowMagnetizerTests.SendWindowPosChanging(magnetizer, form, proposed.X, proposed.Y, SetWindowPosFlags.None)

                Assert.AreEqual(proposed.X, whileResizing.X)
                Assert.AreEqual(workingArea.X, afterResizing.X)
            End Using

        End Sub

        ''' <summary>A window that is far from every border is not moved.</summary>
        <STATestMethod>
        Public Sub WndProc_FarFromBorders_DoesNotChangePosition()

            Using form As Form = WindowMagnetizerTests.CreateFormWithHandle(),
                  magnetizer As New TestableWindowMagnetizer(form) With {.Threshold = 35, .Enabled = True}

                Dim workingArea As Rectangle = Screen.FromHandle(form.Handle).WorkingArea
                Dim proposed As New Point(workingArea.X + 150, workingArea.Y + 150)

                Dim result As Point = WindowMagnetizerTests.SendWindowPosChanging(magnetizer, form, proposed.X, proposed.Y, SetWindowPosFlags.None)

                Assert.AreEqual(proposed, result)
            End Using

        End Sub

#End Region

    End Class

End Namespace

#End Region
