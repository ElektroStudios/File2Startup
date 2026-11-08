' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="WindowThemeUtil.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.InteropServices
Imports System.Windows.Forms

Imports DevCase.Interop.Win32
Imports DevCase.Interop.Win32.Enums

#End Region

#Region " WindowThemeUtil "

Namespace UserInterface.Theming

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Applies the dark/light mode to the native parts of the windows
    ''' that cannot be colored through Windows Forms: the title bar and the scroll bars.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class WindowThemeUtil

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The visual style application name that provides the dark scroll bars of the Windows Explorer.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Const DarkVisualStyleName As String = "DarkMode_Explorer"

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The visual style application name that provides the default (light) scroll bars of the Windows Explorer.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Const LightVisualStyleName As String = "Explorer"

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="WindowThemeUtil"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Enables or disables the dark title bar (and window frame) of the specified form,
        ''' using the <c>DWMWA_USE_IMMERSIVE_DARK_MODE</c> attribute of <c>DwmSetWindowAttribute</c>.
        ''' <para></para>
        ''' If the form is already visible, its non-client area is repainted so the change takes effect immediately.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' This method must be called once the window handle exists, that is, from <see cref="Form.OnHandleCreated"/>
        ''' (calling it from the <see cref="Form.Load"/> event has no effect on the first paint of the title bar),
        ''' and again each time the theme changes.
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="form">
        ''' The form.
        ''' </param>
        '''
        ''' <param name="useDarkMode">
        ''' <see langword="True"/> to use the dark title bar; <see langword="False"/> to use the default (light) title bar.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the attribute was applied;
        ''' <see langword="False"/> if the operating system does not support it (Windows versions prior to Windows 10 1809).
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="form"/> is <see langword="Nothing"/>.
        ''' </exception>
        '''
        ''' <exception cref="InvalidOperationException">
        ''' The window handle of <paramref name="form"/> is not created yet.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function SetDarkTitleBar(form As Form, useDarkMode As Boolean) As Boolean

            If form Is Nothing Then
                Throw New ArgumentNullException(NameOf(form))
            End If

            If Not form.IsHandleCreated Then
                Throw New InvalidOperationException("The window handle of the form must be created before applying the title bar theme.")
            End If

            Dim value As Integer = If(useDarkMode, 1, 0)
            Dim applied As Boolean
            Try
                applied = NativeMethods.DwmSetWindowAttribute(form.Handle, DwmWindowAttribute.UseImmersiveDarkMode, value, Marshal.SizeOf(GetType(Integer))) = 0 OrElse
                          NativeMethods.DwmSetWindowAttribute(form.Handle, DwmWindowAttribute.UseImmersiveDarkModeBefore20H1, value, Marshal.SizeOf(GetType(Integer))) = 0

            Catch ex As DllNotFoundException
                ' Desktop Window Manager is not available (non-Windows runtime).
                Return False

            Catch ex As EntryPointNotFoundException
                ' Desktop Window Manager is not available (non-Windows runtime).
                Return False

            End Try

            If applied AndAlso form.Visible Then
                WindowThemeUtil.RepaintNonClientArea(form)
            End If

            Return applied

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the dark or the light visual style to the scroll bars of the specified control
        ''' (for example, the scroll bars of a <see cref="DataGridView"/>).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="control">
        ''' The control.
        ''' </param>
        '''
        ''' <param name="useDarkMode">
        ''' <see langword="True"/> to use the dark visual style; <see langword="False"/> to use the default visual style.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the visual style was applied;
        ''' <see langword="False"/> if the operating system does not support it.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="control"/> is <see langword="Nothing"/>.
        ''' </exception>
        '''
        ''' <exception cref="InvalidOperationException">
        ''' The window handle of <paramref name="control"/> is not created yet.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function SetDarkScrollBars(control As Control, useDarkMode As Boolean) As Boolean

            If control Is Nothing Then
                Throw New ArgumentNullException(NameOf(control))
            End If

            If Not control.IsHandleCreated Then
                Throw New InvalidOperationException("The window handle of the control must be created before applying the visual style.")
            End If

            Try
                Dim visualStyleName As String = If(useDarkMode, WindowThemeUtil.DarkVisualStyleName, WindowThemeUtil.LightVisualStyleName)
                Return NativeMethods.SetWindowTheme(control.Handle, visualStyleName, Nothing) = 0

            Catch ex As DllNotFoundException
                ' Visual styles are not available (non-Windows runtime).
                Return False

            Catch ex As EntryPointNotFoundException
                ' Visual styles are not available (non-Windows runtime).
                Return False

            End Try

        End Function

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Forces the repaint of the non-client area (title bar and frame) of a visible form.
        ''' <para></para>
        ''' On Windows 10, the title bar does not reflect a change of the dark mode attribute
        ''' until the window activation state changes, so the activation is toggled to repaint it.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="window">
        ''' The form.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub RepaintNonClientArea(window As Form)

            Dim isActive As Boolean = Form.ActiveForm Is window
            NativeMethods.SendMessage(window.Handle, WindowsMessages.WM_NCActivate, New IntPtr(If(isActive, 0, 1)), IntPtr.Zero)
            NativeMethods.SendMessage(window.Handle, WindowsMessages.WM_NCActivate, New IntPtr(If(isActive, 1, 0)), IntPtr.Zero)

            Const flags As SetWindowPosFlags = SetWindowPosFlags.FrameChanged Or SetWindowPosFlags.IgnoreMove Or
                                               SetWindowPosFlags.IgnoreResize Or SetWindowPosFlags.IgnoreZOrder Or
                                               SetWindowPosFlags.DontActivate
            NativeMethods.SetWindowPos(window.Handle, IntPtr.Zero, 0, 0, 0, 0, flags)

        End Sub

#End Region

    End Class

End Namespace

#End Region
