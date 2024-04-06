




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

Imports System.ComponentModel
Imports System.Security

#End Region

#Region " P/Invoking "

Namespace DevCase.Interop.Win32

    Public NotInheritable Class Delegates

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="Delegates"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Delegates "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' An application-defined callback function used with the NativeMethods.EnumWindows 
        ''' or NativeMethods.EnumDesktopWindows function.
        ''' <para></para>
        ''' It receives top-level window handles.
        ''' <para></para>
        ''' The <c>WNDENUMPROC</c> type defines a pointer to this callback function.
        ''' <para></para>
        ''' <see cref="EnumWindowsProc"/> is a placeholder for the application-defined function name. 
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633498%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwnd">
        ''' A handle to a top-level window.
        ''' </param>
        ''' 
        ''' <param name="lParam">
        ''' The application-defined value given in NativeMethods.EnumWindows 
        ''' or NativeMethods.EnumDesktopWindows functions.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' To continue enumeration, the callback function must return <see langword="True"/>; 
        ''' to stop enumeration, it must return <see langword="False"/>. 
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        Public Delegate Function EnumWindowsProc(hwnd As IntPtr,
lParam As IntPtr) As Boolean

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' An application-defined callback function used with the NativeMethods.EnumChildWindows function.
        ''' <para></para>
        ''' It receives the child window handles.
        ''' <para></para>
        ''' The <c>WNDENUMPROC</c> type defines a pointer to this callback function.
        ''' <para></para>
        ''' <see cref="EnumChildWindowsProc"/> is a placeholder for the application-defined function name. 
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633493%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwnd">
        ''' A handle to a child window of the parent window specified in NativeMethods.EnumChildWindows function. 
        ''' </param>
        ''' 
        ''' <param name="lParam">
        ''' The application-defined value given in NativeMethods.EnumChildWindows function.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' To continue enumeration, the callback function must return <see langword="True"/>; 
        ''' to stop enumeration, it must return <see langword="False"/>. 
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        Public Delegate Function EnumChildWindowsProc(hwnd As IntPtr,
lParam As IntPtr) As Boolean

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' An application-defined callback function used with the <see cref="NativeMethods.EnumThreadWindows"/> function.
        ''' <para></para>
        ''' It receives the window handles associated with a thread.
        ''' <para></para>
        ''' The <c>WNDENUMPROC</c> type defines a pointer to this callback function.
        ''' <para></para>
        ''' <see cref="EnumThreadWindowsProc"/> is a placeholder for the application-defined function name. 
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms633496%28v=vs.85%29.aspx"/>
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwnd">
        ''' A handle to a window associated with the thread specified in the <see cref="NativeMethods.EnumThreadWindows"/> function. 
        ''' </param>
        ''' 
        ''' <param name="lParam">
        ''' The application-defined value given in <see cref="NativeMethods.EnumThreadWindows"/> functions.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' To continue enumeration, the callback function must return <see langword="True"/>; 
        ''' to stop enumeration, it must return <see langword="False"/>. 
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <SuppressUnmanagedCodeSecurity>
        Public Delegate Function EnumThreadWindowsProc(hwnd As IntPtr,
lParam As IntPtr) As Boolean

#End Region

#Region " Hidden Base Members "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Serves as a hash function for a particular type.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        <DebuggerNonUserCode>
        Public Shadows Function GetHashCode() As Integer
            Return MyBase.GetHashCode()
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the <see cref="Type"/> of the current instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The exact runtime type of the current instance.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        <DebuggerNonUserCode>
        Public Shadows Function [GetType]() As Type
            Return MyBase.GetType()
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified <see cref="Object"/> is equal to this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="obj">
        ''' Another object to compare to.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the specified <see cref="Object"/> is equal to this instance; 
        ''' otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        <DebuggerNonUserCode>
        Public Shadows Function Equals(obj As Object) As Boolean
            Return MyBase.Equals(obj)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified <see cref="Object"/> instances are considered equal.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="objA">
        ''' The first object to compare.
        ''' </param>
        ''' 
        ''' <param name="objB">
        ''' The second object to compare.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the objects are considered equal; otherwise, <see langword="False"/>.
        ''' <para></para>
        ''' If both <paramref name="objA"/> and <paramref name="objB"/> are <see langword="Nothing"/>, 
        ''' the method returns <see langword="True"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        <DebuggerNonUserCode>
        Public Shared Shadows Function Equals(objA As Object, objB As Object) As Boolean
            Return Object.Equals(objA, objB)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified <see cref="Object"/> instances are the same instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="objA">
        ''' The first object to compare.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="objB">
        ''' The second object to compare.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if <paramref name="objA"/> is the same instance as <paramref name="objB"/> 
        ''' or if both are <see langword="Nothing"/>; otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        <DebuggerNonUserCode>
        Public Shared Shadows Function ReferenceEquals(objA As Object, objB As Object) As Boolean
            Return Object.ReferenceEquals(objA, objB)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Returns a String that represents the current object.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' A string that represents the current object.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <EditorBrowsable(EditorBrowsableState.Never)>
        <DebuggerNonUserCode>
        Public Overrides Function ToString() As String
            Return MyBase.ToString()
        End Function

#End Region

    End Class

End Namespace

#End Region
