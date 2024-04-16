﻿




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

#Region " SetWindowPos Flags "

Namespace DevCase.Interop.Win32.Enums

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' The window sizing and positioning flags.
    ''' <para></para>
    ''' Flags combination for <c>uFlags</c> parameter of <see cref="NativeMethods.SetWindowPos"/> function.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/es-es/library/windows/desktop/ms633545%28v=vs.85%29.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    <Flags>
    Public Enum SetWindowPosFlags As UInteger

        ''' <summary>
        ''' Indicates any flag.
        ''' </summary>
        None = &H0UI

        ''' <summary>
        ''' If the calling thread and the thread that owns the window are attached to different input queues, 
        ''' the system posts the request to the thread that owns the window.
        ''' <para></para>
        ''' This prevents the calling thread from blocking its execution while other threads process the request.
        ''' </summary>
        AsyncWindowPos = &H4000

        ''' <summary>
        ''' Prevents generation of the <c>WM_SYNCPAINT</c> message.
        ''' </summary>
        DeferErase = &H2000

        ''' <summary>
        ''' Draws a frame (defined in the window's class description) around the window.
        ''' </summary>
        DrawFrame = &H20

        ''' <summary>
        ''' Applies new frame styles set using the SetWindowLong function.
        ''' <para></para>
        ''' Sends a <c>WM_NCCALCSIZE</c> message to the window, even if the window's size is not being changed.
        ''' <para></para>
        ''' If this flag is not specified, <c>WM_NCCALCSIZE</c> is sent only when the window's size is being changed.
        ''' </summary>
        FrameChanged = &H20

        ''' <summary>
        ''' Hides the window.
        ''' </summary>
        HideWindow = &H80

        ''' <summary>
        ''' Does not activate the window.
        ''' If this flag is not set, the window is activated and moved to the top of
        ''' either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
        ''' </summary>
        ''' <remarks>SWP_NOACTIVATE</remarks>
        DontActivate = &H10

        ''' <summary>
        ''' Discards the entire contents of the client area.
        ''' <para></para>
        ''' If this flag is not specified, the valid contents of the client area are saved and copied back 
        ''' into the client area after the window is sized or repositioned.
        ''' </summary>
        DontCopyBits = &H100

        ''' <summary>
        ''' Retains the current position (ignores X and Y parameters).
        ''' </summary>
        IgnoreMove = &H2

        ''' <summary>
        ''' Does not change the owner window's position in the Z order.
        ''' </summary>
        DontChangeOwnerZOrder = &H200

        ''' <summary>
        ''' Does not redraw changes.
        ''' <para></para>
        ''' If this flag is set, no repainting of any kind occurs.
        ''' <para></para>
        ''' This applies to the client area, the nonclient area (including the title bar and scroll bars), 
        ''' and any part of the parent window uncovered as a result of the window being moved.
        ''' <para></para>
        ''' When this flag is set, the application must explicitly invalidate or redraw any parts of 
        ''' the window and parent window that need redrawing.
        ''' </summary>
        DontRedraw = &H8

        ''' <summary>
        ''' Same as the <see cref="SetWindowPosFlags.DontChangeOwnerZOrder"/> flag.
        ''' </summary>
        DontReposition = SetWindowPosFlags.DontChangeOwnerZOrder

        ''' <summary>
        ''' Prevents the window from receiving the <c>WM_WINDOWPOSCHANGING</c> message.
        ''' </summary>
        DontSendChangingEvent = &H400

        ''' <summary>
        ''' Retains the current size (ignores the cx and cy parameters).
        ''' </summary>
        IgnoreResize = &H1

        ''' <summary>
        ''' Retains the current Z order (ignores the <c>hwndInsertAfter</c> parameter).
        ''' </summary>
        IgnoreZOrder = &H4

        ''' <summary>
        ''' Displays the window.
        ''' </summary>
        ShowWindow = &H40

    End Enum

End Namespace

#End Region
