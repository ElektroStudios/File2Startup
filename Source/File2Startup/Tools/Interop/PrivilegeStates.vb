




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

#Region " Privilege State "

Namespace DevCase.Interop.Win32.Enums

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Specifies a privilege state.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' <see href="http://msdn.microsoft.com/en-us/library/windows/desktop/aa379630%28v=vs.85%29.aspx"/>
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    <Flags>
    Public Enum PrivilegeStates As UInteger

        ''' <summary>
        ''' The privilege is disabled.
        ''' </summary>        
        ''' <remarks>
        ''' <see href="http://referencesource.microsoft.com/system.servicemodel/System/ServiceModel/ComIntegration/SafeNativeMethods.cs.html#"/>
        ''' </remarks>
        PrivilegeDisabled = &H0UI

        ''' <summary>
        ''' The privilege is enabled by default.
        ''' </summary>
        PrivilegeEnabledByDefault = &H1UI

        ''' <summary>
        ''' The privilege is enabled.
        ''' </summary>
        PrivilegeEnabled = &H2UI

        ''' <summary>
        ''' Used to remove a privilege.
        ''' </summary>
        PrivilegeRemoved = &H4UI

        ''' <summary>
        ''' The privilege was used to gain access to an object or service.
        ''' <para></para>
        ''' This flag is used to identify the relevant privileges 
        ''' in a set passed by a client application that may contain unnecessary privileges
        ''' </summary>
        PrivilegeUsedForAccess = &H80000000UI

    End Enum

End Namespace

#End Region
