




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

Imports System.Xml.Serialization

Imports ElektroKit.Core.Application.UserInterface.Enums

#End Region

#Region " Control-Hint Info "

Namespace ElektroKit.Core.Application.UserInterface.Types

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Represents a control-hint and it's personalization.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerStepThrough>
    <Serializable>
    <XmlRoot("HintInfo")>
    Public NotInheritable Class ControlHintInfo

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the control-hint type.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The control-hint type.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property HintType As ControlHintType
            <DebuggerStepThrough>
            Get
                Return Me.hintTypeB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Baking field )
        ''' The control-hint type.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly hintTypeB As ControlHintType

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the hint text.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The hint text.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Text As String
            <DebuggerStepThrough>
            Get
                Return Me.textB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Baking field )
        ''' The hint text.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly textB As String

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the text font.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The text font.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Font As Font
            <DebuggerStepThrough>
            Get
                Return Me.fontB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Baking field )
        ''' The hint text.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly fontB As Font

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the text color.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The text color.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property ForeColor As Color
            <DebuggerStepThrough>
            Get
                Return Me.forecolorB
            End Get
        End Property
        Private ReadOnly forecolorB As Color

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="ControlHintInfo"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New()
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="ControlHintInfo"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The hint text.
        ''' </param>
        ''' 
        ''' <param name="font">
        ''' The text font.
        ''' </param>
        ''' 
        ''' <param name="forecolor">
        ''' The text forecolor.
        ''' </param>
        ''' 
        ''' <param name="hintType">
        ''' The control-hint type.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub New(ByVal text As String,
                       ByVal font As Font,
                       ByVal forecolor As Color,
                       ByVal hintType As ControlHintType)

            Me.textB = text
            Me.fontB = font
            Me.forecolorB = forecolor
            Me.hintTypeB = hintType

        End Sub

#End Region

    End Class

End Namespace

#End Region
