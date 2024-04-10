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

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO
Imports System.Security.Permissions

Imports Microsoft.Win32

#End Region

#Region " Reg Util "

Namespace DevCase.Core.System.Tools

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains registry related utilities.
    ''' </summary>
    <RegistryPermission(SecurityAction.Demand, Unrestricted:=True)>
    Public NotInheritable Class RegistryUtil

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="RegistryUtil"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

#Region " Create Value "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates or replaces a registry value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <typeparam name="T">
        ''' The type.
        ''' </typeparam>
        ''' 
        ''' <param name="rootKeyName">
        ''' The rootkey name.
        ''' </param>
        ''' 
        ''' <param name="subKeyPath">
        ''' The subkey path.
        ''' </param>
        ''' 
        ''' <param name="valueName">
        ''' The value name.
        ''' </param>
        ''' 
        ''' <param name="valueData">
        ''' The value data.
        ''' </param>
        ''' 
        ''' <param name="valueType">
        ''' The registry value type.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath or valueName
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub CreateValue(Of T)(view As RegistryView, rootKeyName As String, subKeyPath As String, valueName As String, valueData As T,
                                            Optional valueType As RegistryValueKind = RegistryValueKind.String)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)
                    reg.CreateSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), permissionCheck:=RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue(valueName, valueData, valueType)
                End Using

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates or replaces a registry value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <typeparam name="T">
        ''' The type.
        ''' </typeparam>
        ''' 
        ''' <param name="fullKeyPath">
        ''' The registry key full path.
        ''' </param>
        ''' 
        ''' <param name="valueName">
        ''' The value name.
        ''' </param>
        ''' 
        ''' <param name="valueData">
        ''' The value data.
        ''' </param>
        ''' 
        ''' <param name="valueType">
        ''' The registry value type.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub CreateValue(Of T)(view As RegistryView, fullKeyPath As String, valueName As String, valueData As T,
                                            Optional valueType As RegistryValueKind = RegistryValueKind.String)

            RegistryUtil.CreateValue(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, valueData, valueType)

        End Sub

#End Region

#Region " Delete Value "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Deletes a registry subkey.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="rootKeyName">
        ''' The rootkey name.
        ''' </param>
        ''' 
        ''' <param name="subKeyPath">
        ''' The subkey path.
        ''' </param>
        ''' 
        ''' <param name="valueName">
        ''' The value name.
        ''' </param>
        ''' 
        ''' <param name="throwOnMissingValue">
        ''' If set to <see langword="True"/>, throws an exception on missing value.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath or valueName
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub DeleteValue(view As RegistryView, rootKeyName As String, subKeyPath As String, valueName As String,
                                      Optional throwOnMissingValue As Boolean = False)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(valueName) OrElse String.IsNullOrWhiteSpace(valueName) Then
                Throw New ArgumentNullException("valueName")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)
                    reg.OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=True).DeleteValue(valueName, throwOnMissingValue)
                End Using

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Deletes a registry subkey.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="fullKeyPath">
        ''' The registry key full path.
        ''' </param>
        ''' 
        ''' <param name="valueName">
        ''' The value name.
        ''' </param>
        ''' 
        ''' <param name="throwOnMissingValue">
        ''' If set to <see langword="True"/>, throws an exception on missing value.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub DeleteValue(view As RegistryView, fullKeyPath As String, valueName As String,
                                      Optional throwOnMissingValue As Boolean = False)

            RegistryUtil.DeleteValue(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, throwOnMissingValue)

        End Sub

#End Region

#Region " Get ValueData "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the data of a registry value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <typeparam name="T">
        ''' The type.
        ''' </typeparam>
        ''' 
        ''' <param name="rootKeyName">
        ''' The rootkey name.
        ''' </param>
        ''' 
        ''' <param name="subKeyPath">
        ''' The subkey path.
        ''' </param>
        ''' 
        ''' <param name="valueName">
        ''' The value name.
        ''' </param>
        ''' 
        ''' <param name="defaultIfEmpty">
        ''' A default value to return if the data is empty.
        ''' </param>
        ''' 
        ''' <param name="registryValueOptions">
        ''' The registry value options.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The value data.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetValueData(Of T)(view As RegistryView, rootKeyName As String,
                                                  subKeyPath As String,
                                                  valueName As String,
                                                  defaultIfEmpty As T,
                                                  Optional registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As T

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)
                    Return If(RegistryUtil.ExistSubKey(view, rootKeyName, subKeyPath),
                        DirectCast(reg.OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=False).
                                          GetValue(valueName, defaultValue:=defaultIfEmpty, options:=registryValueOptions), T),
                        defaultIfEmpty)
                End Using

            End If

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the data of a registry value.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <typeparam name="T">
        ''' The type.
        ''' </typeparam>
        ''' 
        ''' <param name="fullKeyPath">
        ''' The registry key full path.
        ''' </param>
        ''' 
        ''' <param name="valueName">
        ''' The value name.
        ''' </param>
        ''' 
        ''' <param name="registryValueOptions">
        ''' The registry value options.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The value data.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetValueData(Of T)(view As RegistryView, fullKeyPath As String, valueName As String,
                                                  Optional registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As T

            Return RegistryUtil.GetValueData(Of T)(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, Nothing, registryValueOptions)

        End Function

#End Region

#Region " Validation "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether a registry subkey exists.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="rootKeyName">
        ''' The rootkey name.
        ''' </param>
        ''' 
        ''' <param name="subKeyPath">
        ''' The subkey path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if subkey exist, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ExistSubKey(view As RegistryView, rootKeyName As String, subKeyPath As String) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)
                    Return reg.OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=False) IsNot Nothing
                End Using

            End If

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether a registry subkey exists.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="fullKeyPath">
        ''' The registry key full path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if subkey exist, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ExistSubKey(view As RegistryView, fullKeyPath As String) As Boolean

            Return RegistryUtil.ExistSubKey(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath))

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether a registry value exists.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="rootKeyName">
        ''' The rootkey name.
        ''' </param>
        ''' 
        ''' <param name="subKeyPath">
        ''' The subkey path.
        ''' </param>
        ''' 
        ''' <param name="valueName">
        ''' The value name.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if value exist, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath or valueName
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ExistValue(view As RegistryView, rootKeyName As String,
                                          subKeyPath As String,
                                          valueName As String) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)

                    Return RegistryUtil.ExistSubKey(view, rootKeyName, subKeyPath) AndAlso reg.OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=False).
                                   GetValue(valueName, defaultValue:=Nothing) IsNot Nothing

                End Using

            End If

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether a registry subkey exists.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="fullKeyPath">
        ''' The registry key full path.
        ''' </param>
        ''' 
        ''' <param name="valueName">
        ''' The value name.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if subkey exist, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ExistValue(view As RegistryView, fullKeyPath As String, valueName As String) As Boolean

            Return RegistryUtil.ExistValue(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName)

        End Function

#End Region

#Region " Registry Path Formatting "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the root <see cref="RegistryKey"/> of a registry path.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The root <see cref="RegistryKey"/> of a registry path.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetRootKey(registryPath As String, view As RegistryView) As RegistryKey

            Select Case registryPath.ToUpper.Split("\"c).First()

                Case "HKCR", "HKEY_CLASSES_ROOT"
                    Return RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, view)

                Case "HKCC", "HKEY_CURRENT_CONFIG"
                    Return RegistryKey.OpenBaseKey(RegistryHive.CurrentConfig, view)

                Case "HKCU", "HKEY_CURRENT_USER"
                    Return RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, view)

                Case "HKLM", "HKEY_LOCAL_MACHINE"
                    Return RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, view)

                Case "HKEY_DYN_DATA"
                    Return RegistryKey.OpenBaseKey(RegistryHive.DynData, view)

                Case "HKEY_PERFORMANCE_DATA"
                    Return RegistryKey.OpenBaseKey(RegistryHive.PerformanceData, view)

                Case "HKU", "HKEY_USERS"
                    Return RegistryKey.OpenBaseKey(RegistryHive.Users, view)

                Case Else
                    Return Nothing

            End Select

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the root key name of a registry path.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The root key name of a registry path.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetRootKeyName(registryPath As String) As String

            Select Case registryPath.ToUpper.Split("\"c).FirstOrDefault

                Case "HKCR", "HKEY_CLASSES_ROOT"
                    Return "HKEY_CLASSES_ROOT"

                Case "HKCC", "HKEY_CURRENT_CONFIG"
                    Return "HKEY_CURRENT_CONFIG"

                Case "HKCU", "HKEY_CURRENT_USER"
                    Return "HKEY_CURRENT_USER"

                Case "HKLM", "HKEY_LOCAL_MACHINE"
                    Return "HKEY_LOCAL_MACHINE"

                Case "HKU", "HKEY_USERS"
                    Return "HKEY_USERS"

                Case "HKEY_DYN_DATA"
                    Return "HKEY_DYN_DATA"

                Case "HKEY_PERFORMANCE_DATA"
                    Return "HKEY_PERFORMANCE_DATA"

                Case Else
                    Return String.Empty

            End Select

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the subkey path of a registry path.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The subkey path of a registry path.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetSubKeyPath(registryPath As String) As String

            Select Case String.IsNullOrEmpty(RegistryUtil.GetRootKeyName(registryPath))

                Case True
                    Return registryPath.TrimStart("\"c).TrimEnd("\"c)

                Case Else
                    Return registryPath.Substring(registryPath.IndexOf("\"c)).TrimStart("\"c).TrimEnd("\"c)

            End Select

        End Function

#End Region

#End Region

#Region " Private Methods "

#Region " Collectors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Collects the subkeys on the specified registry path.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcRegistryKey">
        ''' The source registry key.
        ''' </param>
        ''' 
        ''' <param name="searchOption">
        ''' The search mode.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see cref="IEnumerable(Of String)"/>
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Iterator Function CollectSubKeys(srcRegistryKey As RegistryKey, searchOption As SearchOption) As IEnumerable(Of String)

            For Each subKeyName As String In srcRegistryKey.GetSubKeyNames

                Yield String.Format("{0}\{1}", srcRegistryKey.Name, subKeyName)

                If searchOption = SearchOption.AllDirectories Then

                    For Each registryPath As String In RegistryUtil.CollectSubKeys(srcRegistryKey.OpenSubKey(subKeyName, writable:=False), SearchOption.AllDirectories)
                        Yield registryPath
                    Next registryPath

                End If

            Next subKeyName

        End Function

#End Region

#End Region

    End Class

End Namespace

#End Region
