




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

Imports System.Runtime.InteropServices

#End Region

#Region " NativeRectangle (RECT) "

Namespace DevCase.Interop.Win32.Types

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Defines the coordinates of the upper-left and lower-right corners of a rectangle.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/dd162897%28v=vs.85%29.aspx"/>
    ''' <para></para>
    ''' <see href="http://www.pinvoke.net/default.aspx/Structures/rect.html"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <StructLayout(LayoutKind.Sequential)>
    Public Structure NativeRectangle

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the x-coordinate of the upper-left corner of the rectangle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The x-coordinate of the upper-left corner of the rectangle.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property Left As Integer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the y-coordinate of the upper-left corner of the rectangle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The y-coordinate of the upper-left corner of the rectangle.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property Top As Integer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the x-coordinate of the lower-right corner of the rectangle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The x-coordinate of the lower-right corner of the rectangle.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property Right As Integer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the y-coordinate of the lower-right corner of the rectangle.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The y-coordinate of the lower-right corner of the rectangle.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property Bottom As Integer

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="NativeRectangle"/> struct.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="left">
        ''' The x-coordinate of the upper-left corner of the rectangle.
        ''' </param>
        ''' 
        ''' <param name="top">
        ''' The y-coordinate of the upper-left corner of the rectangle.
        ''' </param>
        ''' 
        ''' <param name="right">
        ''' The x-coordinate of the lower-right corner of the rectangle.
        ''' </param>
        ''' 
        ''' <param name="bottom">
        ''' The y-coordinate of the lower-right corner of the rectangle.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(left As Integer,
top As Integer,
right As Integer,
bottom As Integer)

            Me.Left = left
            Me.Top = top
            Me.Right = right
            Me.Bottom = bottom

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="NativeRectangle"/> struct.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="rect">
        ''' The <see cref="Rectangle"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(rect As Rectangle)

            Me.New(rect.Left, rect.Top, rect.Right, rect.Bottom)

        End Sub

#End Region

#Region " Operator Conversions "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Performs an implicit conversion from <see cref="NativeRectangle"/> to <see cref="Rectangle"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="rect">
        ''' The <see cref="NativeRectangle"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The resulting <see cref="Rectangle"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Widening Operator CType(rect As NativeRectangle) As Rectangle

            Return New Rectangle(rect.Left, rect.Top, (rect.Right - rect.Left), (rect.Bottom - rect.Top))

        End Operator

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Performs an implicit conversion from <see cref="Rectangle"/> to <see cref="NativeRectangle"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="rect">
        ''' The <see cref="Rectangle"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The resulting <see cref="NativeRectangle"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Widening Operator CType(rect As Rectangle) As NativeRectangle

            Return New NativeRectangle(rect)

        End Operator

#End Region

    End Structure

End Namespace

#End Region
