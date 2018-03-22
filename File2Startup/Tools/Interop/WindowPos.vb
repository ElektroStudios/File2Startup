




' THIS OPEN-SOURCE APPLICATION IS POWERED BY ELEKTROKIT FRAMEWORK, CREATED BY ELEKTRO STUDIOS.

' WHAT YOU SEE HERE IS FREE CUTTED CONTENT OF THIS FRAMEWORK.

' IF YOU LIKED THIS FREE APPLICATION, THEN PLEASE CONSIDER TO BUY ELEKTROKIT FRAMEWORK FOR .NET AT:
' https://codecanyon.net/item/elektrokit-class-library-for-net/19260282

' YOU CAN FIND THESE HELPER METHODS AND A MASSIVE AMOUNT MORE!, 
' +850 EXTENSION METHODS FOR ALL KIND OF TYPES, CUSTOM USER-CONTROLS, 
' EVERYTHING FOR THE NEWBIE And THE ADVANCED USER, FOR VB.NET AND C#. 

' ElektroKit is a utility framework containing new APIs and extensions to the core .NET Framework 
' to help complete your developer toolbox.
' It Is a set of general purpose classes provided as easy to consume packages.
' These utility classes and components provide productivity in day to day software development 
' mainly focused To WindowsForms. 

' UPDATES OF ELEKTROKIT ARE MAINTAINED AND RELEASED EVERY MONTH.





#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Diagnostics.CodeAnalysis
Imports System.Runtime.InteropServices

#End Region

#Region " Window Pos "

Namespace ElektroKit.Interop.Win32.Types

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains information about the size and position of a window.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms632612%28v=vs.85%29.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure WindowPos

#Region " Fields "

        ''' <summary>
        ''' A handle to the window.
        ''' </summary>
        <SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible", Justification:="Visible for API")>
        Public Hwnd As IntPtr

        ''' <summary>
        ''' The position of the window in Z order (front-to-back position). 
        ''' This member can be a handle to the window behind which this window is placed, 
        ''' or can be one of the special values listed with the 'SetWindowPos' function. 
        ''' </summary>
        <SuppressMessage("Microsoft.Security", "CA2111:PointersShouldNotBeVisible", Justification:="Visible for API")>
        Public HwndInsertAfter As IntPtr

        ''' <summary>
        ''' The position of the left edge of the window.
        ''' </summary>
        Public X As Integer

        ''' <summary>
        ''' The position of the top edge of the window.
        ''' </summary>
        Public Y As Integer

        ''' <summary>
        ''' The window width, in pixels.
        ''' </summary>
        Public Width As Integer

        ''' <summary>
        ''' The window height, in pixels.
        ''' </summary>
        Public Height As Integer

        ''' <summary>
        ''' Flag containing the window position.
        ''' </summary>
        Public Flags As Integer

#End Region

    End Structure

End Namespace

#End Region
