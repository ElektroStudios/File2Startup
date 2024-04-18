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

#End Region

#Region " File-system Util "

Namespace DevCase.Core.Imaging.Tools

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains file-system related utilities.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------   
    Public NotInheritable Class FileSystemUtil

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="FileSystemUtil"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Determines whether the specified directory path contains invalid Windows path characters.
        ''' </summary>
        '''
        ''' <param name="directoryPath">
        ''' The item path.
        ''' </param>
        '''
        ''' <returns>
        ''' <see langword="True"/> if the specified directory path does not contain invalid Windows path characters, 
        ''' <see langword="False"/> otherwise.
        ''' </returns>
        <DebuggerStepThrough>
        Public Shared Function IsValidWindowsDirectoryPath(directoryPath As String) As Boolean

            Dim invalidChars As Char() = Path.GetInvalidPathChars()
            Return Not directoryPath.Any(Function(c As Char) invalidChars.Contains(c))

        End Function

        ''' <summary>
        ''' Determines whether the specified filename contains invalid Windows path characters.
        ''' </summary>
        '''
        ''' <param name="fileName">
        ''' The item name.
        ''' </param>
        '''
        ''' <returns>
        ''' <see langword="True"/> if the specified filename does not contain invalid Windows path characters, 
        ''' <see langword="False"/> otherwise.
        ''' </returns>
        <DebuggerStepThrough>
        Public Shared Function IsValidWindowsFileName(fileName As String) As Boolean

            Dim invalidChars As Char() = Path.GetInvalidFileNameChars()
            Return Not fileName.Any(Function(c As Char) invalidChars.Contains(c))

        End Function
#End Region

    End Class

End Namespace

#End Region
