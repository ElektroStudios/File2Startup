' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="Win32DefinitionsTests.vb" company="Elektro Studios">
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
Imports System.Runtime.InteropServices

Imports DevCase.Core.Application.UserInterface.Enums
Imports DevCase.Interop.Win32.Enums
Imports DevCase.Interop.Win32.Types
Imports DevCase.Win32.Enums
Imports DevCase.Win32.Structures

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " Win32DefinitionsTests "

Namespace Interop

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Verifies the Win32 enumeration values and structure layouts against the values documented in the Windows SDK,
    ''' so a typo in a P/Invoke definition is detected.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    Public NotInheritable Class Win32DefinitionsTests

#Region " Enumerations "

        ''' <summary><see cref="WindowsMessages"/> values match <c>WinUser.h</c>.</summary>
        <TestMethod>
        Public Sub WindowsMessages_MatchWinUserH()

            Dim actual As Integer() = {
                CInt(WindowsMessages.Null),
                CInt(WindowsMessages.WM_WindowPosChanging),
                CInt(WindowsMessages.WM_Sizing),
                CInt(WindowsMessages.WM_ExitSizeMove),
                CInt(WindowsMessages.WM_NCActivate)
            }

            CollectionAssert.AreEqual({&H0, &H46, &H214, &H232, &H86}, actual)

        End Sub

        ''' <summary><see cref="SetWindowPosFlags"/> values match the <c>SWP_*</c> constants of <c>WinUser.h</c>.</summary>
        <TestMethod>
        Public Sub SetWindowPosFlags_MatchSwpConstants()

            Dim actual As UInteger() = {
                SetWindowPosFlags.IgnoreResize, SetWindowPosFlags.IgnoreMove, SetWindowPosFlags.IgnoreZOrder,
                SetWindowPosFlags.DontRedraw, SetWindowPosFlags.DontActivate, SetWindowPosFlags.FrameChanged,
                SetWindowPosFlags.DrawFrame, SetWindowPosFlags.ShowWindow, SetWindowPosFlags.HideWindow,
                SetWindowPosFlags.DontCopyBits, SetWindowPosFlags.DontChangeOwnerZOrder, SetWindowPosFlags.DontSendChangingEvent,
                SetWindowPosFlags.DeferErase, SetWindowPosFlags.AsyncWindowPos
            }

            CollectionAssert.AreEqual({&H1UI, &H2UI, &H4UI, &H8UI, &H10UI, &H20UI, &H20UI, &H40UI, &H80UI,
                                       &H100UI, &H200UI, &H400UI, &H2000UI, &H4000UI}, actual)

        End Sub

        ''' <summary><see cref="SHGetFileInfoFlags"/> values match the <c>SHGFI_*</c> constants of <c>ShellAPI.h</c>.</summary>
        <TestMethod>
        Public Sub SHGetFileInfoFlags_MatchShgfiConstants()

            Dim actual As Integer() = {
                SHGetFileInfoFlags.IconSizeLarge, SHGetFileInfoFlags.IconSizeSmall, SHGetFileInfoFlags.PIDL,
                SHGetFileInfoFlags.UseFileAttributes, SHGetFileInfoFlags.AddOverlays, SHGetFileInfoFlags.OverlayIndex,
                SHGetFileInfoFlags.Icon, SHGetFileInfoFlags.DisplayName, SHGetFileInfoFlags.TypeName,
                SHGetFileInfoFlags.Attributes, SHGetFileInfoFlags.IconLocation, SHGetFileInfoFlags.ExeType,
                SHGetFileInfoFlags.SysIconIndex, SHGetFileInfoFlags.LinkOverlay, SHGetFileInfoFlags.Selected,
                SHGetFileInfoFlags.AttributeSpecified
            }

            CollectionAssert.AreEqual({&H0, &H1, &H8, &H10, &H20, &H40, &H100, &H200, &H400,
                                       &H800, &H1000, &H2000, &H4000, &H8000, &H10000, &H20000}, actual)

        End Sub

        ''' <summary><see cref="DwmWindowAttribute"/> values match <c>DWMWA_USE_IMMERSIVE_DARK_MODE</c> (20) and its pre-20H1 value (19).</summary>
        <TestMethod>
        Public Sub DwmWindowAttribute_MatchDwmApiH()

            Dim actual As Integer() = {DwmWindowAttribute.UseImmersiveDarkModeBefore20H1, DwmWindowAttribute.UseImmersiveDarkMode}

            CollectionAssert.AreEqual({19, 20}, actual)

        End Sub

        ''' <summary><see cref="ControlHintType"/> values are stable.</summary>
        <TestMethod>
        Public Sub ControlHintType_Values()

            Dim actual As Integer() = {ControlHintType.Normal, ControlHintType.Persistent}

            CollectionAssert.AreEqual({1, 2}, actual)

        End Sub

#End Region

#Region " Structures "

        ''' <summary><see cref="WindowPos"/> has the same size as the Win32 <c>WINDOWPOS</c> structure.</summary>
        <TestMethod>
        Public Sub WindowPos_SizeMatchesWin32()

            ' Two handles plus five 32-bit integers, padded to the pointer alignment: 28 bytes (x86) or 40 bytes (x64).
            Dim expected As Integer = If(IntPtr.Size = 8, 40, 28)

            Assert.AreEqual(expected, Marshal.SizeOf(GetType(WindowPos)))

        End Sub

        ''' <summary><see cref="ShellFileInfo"/> has the same size as the Win32 <c>SHFILEINFOW</c> structure.</summary>
        <TestMethod>
        Public Sub ShellFileInfo_SizeMatchesSHFileInfoW()

            ' HICON + int + DWORD + WCHAR[260] + WCHAR[80]: 692 bytes (x86) or 696 bytes (x64).
            Dim expected As Integer = If(IntPtr.Size = 8, 696, 692)

            Assert.AreEqual(expected, Marshal.SizeOf(GetType(ShellFileInfo)))

        End Sub

#End Region

    End Class

End Namespace

#End Region
