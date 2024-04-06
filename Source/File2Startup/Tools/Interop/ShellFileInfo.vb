' ***********************************************************************
' Author   : ElektroStudios
' Modified : 26-March-2017
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Runtime.InteropServices

#End Region

#Region " ShellFileInfo "

' ReSharper disable once CheckNamespace

Namespace DevCase.Win32.Structures

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains information about a file object.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="https://msdn.microsoft.com/es-es/library/windows/desktop/bb759792(v=vs.85).aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)>
    Public Structure ShellFileInfo

#Region " Fields "

        ''' <summary>
        ''' A handle to the icon that represents the file. 
        ''' <para></para>
        ''' You are responsible for destroying this handle with <see cref="NativeMethods.DestroyIcon"/> 
        ''' when you no longer need it.
        ''' </summary>
        Public IconHandle As IntPtr

        ''' <summary>
        ''' The index of the icon image within the system image list.
        ''' </summary>
        Public IconIndex As Integer

        ''' <summary>
        ''' An array of values that indicates the attributes of the file object. 
        ''' </summary>
        Public Attributes As UInteger

        ''' <summary>
        ''' A string that contains the name of the file as it appears in the Windows Shell, 
        ''' or the path and file name of the file that contains the icon representing the file.
        ''' </summary>
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)>
        Public DisplayName As String

        ''' <summary>
        ''' A string that describes the type of file.
        ''' </summary>
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)>
        Public TypeName As String

#End Region

    End Structure

End Namespace

#End Region
