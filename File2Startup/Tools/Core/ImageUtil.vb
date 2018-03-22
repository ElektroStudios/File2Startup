




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

Imports System.Runtime.InteropServices

Imports ElektroKit.Interop.Win32
Imports ElektroKit.Interop.Win32.Enums

#End Region

#Region " Image Util "

Namespace ElektroKit.Core.Imaging.Tools

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains image related utilities.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------   
    Public NotInheritable Class ImageUtil

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="ImageUtil"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Extracts a icon stored in the specified file.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code>
        ''' Dim ico As Icon = ExtractIconFromFile("C:\Windows\Explorer.exe", 0)
        ''' Dim bmp As Bitmap = ico.ToBitmap()
        ''' PictureBox1.BackgroundImage = bmp
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filepath">
        ''' The source filepath.
        ''' </param>
        ''' 
        ''' <param name="iconIndex">
        ''' The index of the icon to be extracted.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ExtractIconFromFile(ByVal filepath As String,
                                                   ByVal iconIndex As Integer) As Global.System.Drawing.Icon

            Return ImageUtil.ExtractIconFromFile(filepath, iconIndex, 0)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Extracts a icon stored in the specified file.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code>
        ''' Dim ico As Icon = ExtractIconFromFile("C:\Windows\Explorer.exe", 0, 256)
        ''' Dim bmp As Bitmap = ico.ToBitmap()
        ''' PictureBox1.BackgroundImage = bmp
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filepath">
        ''' The source filepath.
        ''' </param>
        ''' 
        ''' <param name="iconIndex">
        ''' The index of the icon to be extracted.
        ''' </param>
        ''' 
        ''' <param name="iconSize">
        ''' The icon size, in pixels.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ExtractIconFromFile(ByVal filepath As String,
                                                   ByVal iconIndex As Integer,
                                                   ByVal iconSize As Integer) As Global.System.Drawing.Icon

            Dim hiconLarge As IntPtr
            Dim result As Integer = NativeMethods.SHDefExtractIcon(filepath, iconIndex, 0, hiconLarge, Nothing, CUInt(iconSize))

            If (CType(result, HResult) <> HResult.S_OK) Then
                Marshal.ThrowExceptionForHR(result)
                Return Nothing

            Else
                Dim ico As Global.System.Drawing.Icon = Global.System.Drawing.Icon.FromHandle(hiconLarge)
                ' NativeMethods.DestroyIcon(hiconLarge)
                Return ico

            End If

        End Function

#End Region

    End Class

End Namespace

#End Region
