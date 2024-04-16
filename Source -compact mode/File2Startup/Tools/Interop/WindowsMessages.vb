




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

#Region " Windows Messages "

Namespace DevCase.Interop.Win32.Enums

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' The system sends or posts a system-defined message when it communicates with an application. 
    ''' <para></para>
    ''' It uses these messages to control the operations of applications and to provide input and other information for applications to process. 
    ''' <para></para>
    ''' An application can also send or post system-defined messages.
    ''' <para></para>
    ''' Applications generally use these messages to control the operation of control windows created by using preregistered window classes.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms644927%28v=vs.85%29.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    Public Enum WindowsMessages As Integer

        ' *****************************************************************************
        '                            WARNING!, NEED TO KNOW...
        '
        '  THIS ENUMERATION IS PARTIALLY DEFINED TO MEET THE PURPOSES OF THIS API
        ' *****************************************************************************

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The <see cref="Null"/> message performs no operation.
        ''' <para></para>
        ''' An application sends the <see cref="Null"/> message if it wants to 
        ''' post a message that the recipient window will ignore.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Null = &H0

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sent to a window that the user is resizing.
        ''' <para></para>
        ''' By processing this message, an application can monitor the size and position of the drag rectangle
        ''' and, if needed, change its size or position.
        ''' <para></para>
        ''' 
        ''' <c>wParam</c> 
        ''' The edge of the window that is being sized.
        ''' <para></para>
        ''' 
        ''' <c>lParam</c> 
        ''' A pointer to a <see cref="Win32.Types.NativeRectangle"/> structure with the screen coordinates of the drag rectangle.
        ''' <para></para>
        ''' To change the size or position of the drag rectangle, an application must change the members of this structure.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms632647%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        WM_Sizing = &H214

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sent one time to a window, after it has exited the moving or sizing modal loop.
        ''' <para></para>
        ''' The window enters the moving or sizing modal loop when the user clicks the window's title bar or sizing border,
        ''' <para></para>
        ''' or when the window passes the WindowsMessages.WM_SysCommand message to the 
        ''' <c>DefWindowProc</c> function and the <c>wParam</c> parameter of the message
        ''' specifies the Wparams.SC_Move or Wparams.SC_Size value.
        ''' <para></para>
        ''' The operation is complete when <c>DefWindowProc</c> returns.
        ''' <para></para>
        ''' 
        ''' <c>wParam</c> 
        ''' This parameter is not used.
        ''' <para></para>
        ''' 
        ''' <c>lParam</c> 
        ''' This parameter is not used.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms632623%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        WM_ExitSizeMove = &H232

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sent to a window whose size, position, or place in the Z order is about to change as a 
        ''' result of a call to the <see cref="NativeMethods.SetWindowPos"/> function or another window-management function.
        ''' <para></para>
        ''' 
        ''' <c>wParam</c> 
        ''' This parameter is not used.
        ''' <para></para>
        ''' 
        ''' <c>lParam</c> 
        ''' A pointer to a <c>WINDOWPOS</c> structure that contains information about the window's new size and position.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms632653%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        WM_WindowPosChanging = &H46

    End Enum

End Namespace

#End Region
