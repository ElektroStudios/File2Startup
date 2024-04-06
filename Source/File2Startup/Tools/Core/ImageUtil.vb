




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

Imports DevCase.Interop.Win32
Imports DevCase.Win32.Enums
Imports DevCase.Win32.Structures

#End Region

#Region " Image Util "

Namespace DevCase.Core.Imaging.Tools

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
        ''' Extracts the icon associated for the specified file.
        ''' <para></para>
        ''' Note: the maximum size of the returned icon only can be 32x32.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code language="VB.NET">
        ''' Dim path As String = "C:\File.ext"
        ''' Dim ico As Icon = ExtractIconFromFile(path)
        ''' Dim bmp As Bitmap = ico.ToBitmap()
        ''' PictureBox1.BackgroundImage = bmp
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filepath">
        ''' The full path to a file.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The resulting icon.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ExtractIconFromFile(filepath As String) As Icon

            ' Note that the icon returned by "SHGetFileInfo" function 
            ' is limited to "SHGetFileInfoFlags.IconSizeSmall" (16x16) 
            ' and "SHGetFileInfoFlags.IconSizeLarge" (32x32) icon size.

            Dim shInfo As New ShellFileInfo()

            Dim result As IntPtr = NativeMethods.SHGetFileInfo(filepath, FileAttributes.Normal, shInfo, CUInt(Marshal.SizeOf(shInfo)), SHGetFileInfoFlags.UseFileAttributes Or SHGetFileInfoFlags.Icon Or SHGetFileInfoFlags.IconSizeLarge)
            If result = IntPtr.Zero Then
                result = NativeMethods.SHGetFileInfo(filepath, FileAttributes.Normal, shInfo, CUInt(Marshal.SizeOf(shInfo)), SHGetFileInfoFlags.Icon Or SHGetFileInfoFlags.IconSizeLarge)
            End If

            If result = IntPtr.Zero Then
                Return Nothing
            End If

            Dim ico As Icon = TryCast(Drawing.Icon.FromHandle(shInfo.IconHandle).Clone(), Icon)
            NativeMethods.DestroyIcon(shInfo.IconHandle)
            Return ico

        End Function

#End Region

    End Class

End Namespace

#End Region
