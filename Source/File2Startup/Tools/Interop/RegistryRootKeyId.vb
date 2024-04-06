




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

#Region " Registry Root-Key Id "

Namespace DevCase.Interop.Win32.Enums

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Specifies a registry root-key when calling <see cref="NativeMethods.RegLoadKey"/> 
    ''' or <see cref="NativeMethods.RegUnloadKey"/> functions.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/ms724889%28v=vs.85%29.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    Public Enum RegistryRootKeyId As UInteger

        ''' <summary>
        ''' The <c>HKEY_LOCAL_MACHINE</c> registry root key.
        ''' </summary>
        HKEY_LOCAL_MACHINE = &H80000002UI

        ''' <summary>
        ''' The <c>HKEY_USERS</c> registry root key.
        ''' </summary>
        HKEY_USERS = &H80000003UI

    End Enum

End Namespace

#End Region
