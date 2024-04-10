
' UNUSED CODE HAS BEEN PRUNED. 06/APRIL/2024




' THIS OPEN-SOURCE APPLICATION IS POWERED BY DEVCASE CLASS LIBRARY, CREATED BY ELEKTRO STUDIOS.

' WHAT YOU SEE HERE IS FREE CUTTED CONTENT OF THIS FRAMEWORK.

' IF YOU LIKED THIS FREE APPLICATION, THEN PLEASE CONSIDER TO BUY DEVCASE CLASS LIBRARY FOR .NET AT:
' https://codecanyon.net/item/DevCase-class-library-for-net/19260282

' YOU CAN FIND THESE HELPER METHODS AND A MASSIVE AMOUNT MORE!, 
' +850 EXTENSION METHODS FOR ALL KIND OF TYPES, CUSTOM USER-CONTROLS, 
' EVERYTHING FOR THE NEWBIE And THE ADVANCED USER, FOR VB.NET AND C#. 

' DevCase is a utility framework containing new APIs and extensions to the core .NET Framework 
' to help complete your developer toolbox.
' It Is a set of general purpose classes provided as easy to consume packages.
' These utility classes and components provide productivity in day to day software development 
' mainly focused To WindowsForms. 

' UPDATES OF DevCase ARE MAINTAINED AND RELEASED EVERY MONTH.





#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security
Imports System.Text

Imports DevCase.Interop.Win32.Delegates
Imports DevCase.Interop.Win32.Enums
Imports DevCase.Interop.Win32.Types
Imports DevCase.Win32.Enums
Imports DevCase.Win32.Structures

#End Region

#Region " P/Invoking "

Namespace DevCase.Interop.Win32

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Platform Invocation methods (P/Invoke), access unmanaged code.
    ''' <para></para>
    ''' This class does not suppress stack walks for unmanaged code permission.
    ''' <see cref="Global.System.Security.SuppressUnmanagedCodeSecurityAttribute"/> must not be applied to this class.
    ''' <para></para>
    ''' This class is for methods that can be used anywhere because a stack walk will be performed.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/en-us/library/ms182161.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class NativeMethods

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="NativeMethods"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Kernel32.dll "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the thread identifier of the calling thread.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms683183%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The thread identifier of the calling thread.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        <DllImport("Kernel32.dll", SetLastError:=False)>
        Public Shared Function GetCurrentThreadId(
        ) As UInteger
        End Function

#End Region

#Region " Shell32.dll "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Retrieves information about an object in the file system, 
        ''' such as a file, folder, directory, or drive root.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="https://msdn.microsoft.com/es-es/library/windows/desktop/bb762179(v=vs.85).aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="path">
        ''' A pointer to a null-terminated string of maximum length <c>MAX_PATH</c> that contains the path and file name. 
        ''' Both absolute and relative paths are valid. 
        ''' <para></para>
        ''' If the <paramref name="flags"/> parameter includes the <see cref="SHGetFileInfoFlags.PIDL"/> flag, 
        ''' this parameter must be the address of an <c>ITEMIDLIST</c> (<c>PIDL</c>) structure that contains the 
        ''' list of item identifiers that uniquely identifies the file within the Shell's namespace. 
        ''' The <c>PIDL</c> must be a fully qualified <c>PIDL</c>. Relative <c>PIDLs</c> are not allowed.
        ''' <para></para>
        ''' If the uFlags parameter includes the <see cref="SHGetFileInfoFlags.UseFileAttributes"/> flag, 
        ''' this parameter does not have to be a valid file name. 
        ''' The function will proceed as if the file exists with the specified name and with the 
        ''' file attributes passed in the <paramref name="fileAttribs"/> parameter. 
        ''' This allows you to obtain information about a file type by passing just the extension for 
        ''' <paramref name="path"/> parameter and passing FILE_ATTRIBUTE_NORMAL in <paramref name="fileAttribs"/> parameter
        ''' </param>
        ''' 
        ''' <param name="fileAttribs">
        ''' A combination of one or more file attribute flags. 
        ''' <para></para>
        ''' If <paramref name="flags"/> parameter does not include the 
        ''' <see cref="SHGetFileInfoFlags.UseFileAttributes"/> flag, this parameter is ignored.
        ''' </param>
        ''' 
        ''' <param name="refShellFileInfo">
        ''' Pointer to a <see cref="ShellFileInfo"/> structure to receive the file information.
        ''' </param>
        ''' 
        ''' <param name="size">
        ''' The size, in bytes, of the <see cref="ShellFileInfo"/> structure pointed to by the 
        ''' <paramref name="refShellFileInfo"/> parameter.
        ''' </param>
        ''' 
        ''' <param name="flags">
        ''' The flags that specify the file information to retrieve.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' Returns a value whose meaning depends on the <paramref name="flags"/> parameter.
        ''' <para></para>
        ''' If <paramref name="flags"/> parameter does not contain <see cref="SHGetFileInfoFlags.ExeType"/> 
        ''' or <see cref="SHGetFileInfoFlags.SysIconIndex"/>, the return value is nonzero if successful, or zero otherwise.
        ''' <para></para>
        ''' If <paramref name="flags"/> parameter contains the <see cref="SHGetFileInfoFlags.ExeType"/> flag, 
        ''' the return value specifies the type of the executable.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        <DllImport("Shell32.dll", CharSet:=CharSet.Auto, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Public Shared Function SHGetFileInfo(path As String,
               <MarshalAs(UnmanagedType.U4)> fileAttribs As FileAttributes,
                                             ByRef refShellFileInfo As ShellFileInfo,
               <MarshalAs(UnmanagedType.U4)> size As UInteger,
               <MarshalAs(UnmanagedType.U4)> flags As SHGetFileInfoFlags
        ) As IntPtr
        End Function

#End Region

#Region " User32.dll "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Destroys an icon and frees any memory the icon occupied.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="https://msdn.microsoft.com/en-us/library/windows/desktop/ms648063%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hIcon">
        ''' A handle to the icon to be destroyed.
        ''' <para></para>
        ''' The icon must not be in use. 
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is <see langword="True"/>.
        ''' <para></para>
        ''' If the function fails, the return value is <see langword="False"/>.
        ''' <para></para>
        ''' To get extended error information, call <see cref="Marshal.GetLastWin32Error"/>. 
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        <DllImport("User32.dll", SetLastError:=True)>
        Public Shared Function DestroyIcon(hIcon As IntPtr
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Retrieves the dimensions of the bounding rectangle of the specified window. 
        ''' <para></para>
        ''' The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/es-es/library/windows/desktop/ms633519%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwnd">
        ''' A <see cref="IntPtr"/> handle to the window.
        ''' </param>
        ''' 
        ''' <param name="refRect">
        ''' A pointer to a <see cref="NativeRectangle"/> structure that receives the screen coordinates of the 
        ''' upper-left and lower-right corners of the window.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the function succeeds, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        <DllImport("User32.dll", SetLastError:=True)>
        Public Shared Function GetWindowRect(hwnd As IntPtr,
                                       <Out> ByRef refRect As NativeRectangle
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Enumerates all nonchild windows associated with a thread by passing the handle to each window, 
        ''' in turn, to an application-defined callback function.
        ''' <para></para>
        ''' <see cref="EnumThreadWindows"/> continues until the last window is enumerated 
        ''' or the callback function returns <see langword="False"/>.
        ''' <para></para>
        ''' To enumerate child windows of a particular window, use the EnumChildWindows function.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633495%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="dwThreadId">
        ''' The identifier of the thread whose windows are to be enumerated.
        ''' </param>
        ''' 
        ''' <param name="lpEnumFunc">
        ''' A pointer to an application-defined callback function.
        ''' </param>
        ''' 
        ''' <param name="lParam">
        ''' An application-defined value to be passed to the callback function. 
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the callback function returns <see langword="True"/> for all windows in the thread specified by dwThreadId, 
        ''' the return value is <see langword="True"/>.
        ''' <para></para>
        ''' If the callback function returns <see langword="False"/> on any enumerated window, 
        ''' or if there are no windows found in the thread specified by dwThreadId, 
        ''' the return value is <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        <DllImport("User32.dll", SetLastError:=True)>
        Public Shared Function EnumThreadWindows(dwThreadId As UInteger, lpEnumFunc As EnumThreadWindowsProc, lParam As IntPtr
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Retrieves the name of the class to which the specified window belongs.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633582(v=vs.85).aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwnd">
        ''' A handle to the window and, indirectly, the class to which the window belongs.
        ''' </param>
        ''' 
        ''' <param name="className">
        ''' The class name string. 
        ''' </param>
        ''' 
        ''' <param name="maxCount">
        ''' The length of the <paramref name="className"/> buffer, in characters. 
        ''' <para></para>
        ''' The buffer must be large enough to include the terminating null character; 
        ''' otherwise, the class name string is truncated to <paramref name="maxCount"/>-1 characters. 
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is the number of characters copied to the buffer, 
        ''' not including the terminating null character.
        ''' <para></para>
        ''' If the function fails, the return value is <c>0</c>. 
        ''' <para></para>
        ''' To get extended error information, call <see cref="Marshal.GetLastWin32Error()"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        <DllImport("User32.dll", CharSet:=CharSet.Auto, SetLastError:=True, BestFitMapping:=False, ThrowOnUnmappableChar:=True)>
        Public Shared Function GetClassName(hwnd As IntPtr, className As StringBuilder, maxCount As Integer
        ) As Integer
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Retrieves a handle to a control in the specified dialog box.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms645481%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwnd">
        ''' A handle to the dialog box that contains the control
        ''' .</param>
        ''' 
        ''' <param name="index">
        ''' The index of the item to be retrieved.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is the window handle of the specified control.
        ''' <para></para>
        ''' If the function fails, the return value is <see cref="IntPtr.Zero"/>, 
        ''' indicating an invalid dialog box handle or a nonexistent control.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        <DllImport("user32.dll", SetLastError:=False)>
        Public Shared Function GetDlgItem(hwnd As IntPtr, index As Integer
        ) As IntPtr
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Destroys the specified window.
        ''' The function sends WindowsMessages.WM_Destroy and WindowsMessages.WM_NcDestroy messages to the window 
        ''' to deactivate it and remove the keyboard focus from it.
        ''' <para></para>
        ''' The function also destroys the window's menu, flushes the thread message queue, destroys timers, 
        ''' removes clipboard ownership, and breaks the clipboard viewer chain (if the window is at the top of the viewer chain).
        ''' <para></para>
        ''' If the specified window is a parent or owner window, 
        ''' <see cref="NativeMethods.DestroyWindow"/> automatically destroys the associated child or owned windows when 
        ''' it destroys the parent or owner window.
        ''' <para></para>
        ''' The function first destroys child or owned windows, and then it destroys the parent or owner window.
        ''' <para></para>
        ''' <see cref="NativeMethods.DestroyWindow"/> also destroys modeless dialog boxes created by the <c>CreateDialog</c> function.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms632682%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwnd">
        ''' A handle to the window to be destroyed. 
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is <see cref="IntPtr.Zero"/>.
        ''' <para></para>
        ''' If the function fails, the return value is equal to a handle to the local memory object.
        ''' <para></para>
        ''' To get extended error information, call <see cref="Marshal.GetLastWin32Error()"/>. 
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        <DllImport("User32.dll", EntryPoint:="DestroyWindow", SetLastError:=True,
                   CallingConvention:=CallingConvention.StdCall)>
        Public Shared Function DestroyWindow(hwnd As IntPtr
        ) As IntPtr
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Changes the size, position, and Z order of a child, pop-up, or top-level window.
        ''' <para></para>
        ''' These windows are ordered according to their appearance on the screen.
        ''' <para></para>
        ''' The topmost window receives the highest rank and is the first window in the Z order.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/es-es/library/windows/desktop/ms633545(v=vs.85).aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwnd">
        ''' A handle to the window.
        ''' </param>
        ''' 
        ''' <param name="hwndInsertAfter">
        ''' A handle to the window to precede the positioned window in the Z order.
        ''' </param>
        ''' 
        ''' <param name="x">
        ''' The new position of the left side of the window, in client coordinates.
        ''' </param>
        ''' 
        ''' <param name="y">
        ''' The new position of the top of the window, in client coordinates.
        ''' </param>
        ''' 
        ''' <param name="cx">
        ''' The new width of the window, in pixels.
        ''' </param>
        ''' 
        ''' <param name="cy">
        ''' The new height of the window, in pixels.
        ''' </param>
        ''' 
        ''' <param name="uFlags">
        ''' The window sizing and positioning flags.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the function succeeds, the return value is <see langword="True"/>.
        ''' <para></para>
        ''' If the function fails, the return value is <see langword="False"/>.
        ''' <para></para>
        ''' To get extended error information, call <see cref="Marshal.GetLastWin32Error()"/>. 
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        <DllImport("User32.dll", SetLastError:=True)>
        Public Shared Function SetWindowPos(hwnd As IntPtr, hwndInsertAfter As IntPtr, x As Integer, y As Integer, cx As Integer, cy As Integer, uFlags As SetWindowPosFlags
        ) As <MarshalAs(UnmanagedType.Bool)> Boolean
        End Function

#End Region

    End Class

End Namespace

#End Region
