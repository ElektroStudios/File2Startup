﻿




' THIS OPEN-SOURCE APPLICATION IS POWERED BY DEVCASE CLASS LIBRARY, CREATED BY ELEKTRO STUDIOS.

' WHAT YOU SEE HERE IS FREE CUTTED CONTENT OF THIS FRAMEWORK.

' IF YOU LIKED THIS FREE APPLICATION, THEN PLEASE CONSIDER TO BUY DEVCASE CLASS LIBRARY FOR .NET AT:
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






#Region " Usage Examples "

'' ----------------
'' Instance RegInfo
'' ----------------
'
'    Dim regInfo As New RegInfo
'    With regInfo
'        .RootKeyName = "HKCU"
'        .SubKeyPath = "Subkey Path"
'        .ValueName = "Value Name"
'        .ValueType = Microsoft.Win32.RegistryValueKind.String
'        .ValueData = "Hello World!"
'    End With
'
'    Dim regInfoByte As New RegInfo(Of Byte())
'    With regInfoByte
'        .RootKeyName = "HKCU"
'        .SubKeyPath = "Subkey Path"
'        .ValueName = "Value Name"
'        .ValueType = Microsoft.Win32.RegistryValueKind.Binary
'        .ValueData = Global.System.Text.Encoding.ASCII.GetBytes("Hello World!")
'    End With

'' ----------------
'' Create SubKey
'' ----------------
'
'    RegistryUtil.CreateSubKey(fullKeyPath:="HKCU\Subkey Path\")
'    RegistryUtil.CreateSubKey(rootKeyName:="HKCU",
'                         subKeyPath:="Subkey Path")
'    RegistryUtil.CreateSubKey(regInfo:=regInfoByte)
'
'    Dim regKey1 As Microsoft.Win32.RegistryKey =
'        RegistryUtil.CreateSubKey(fullKeyPath:="HKCU\Subkey Path\",
'                             registryKeyPermissionCheck:=Microsoft.Win32.RegistryKeyPermissionCheck.Default,
'                             registryOptions:=Microsoft.Win32.RegistryOptions.None)
'
'    Dim regKey2 As Microsoft.Win32.RegistryKey =
'        RegistryUtil.CreateSubKey(rootKeyName:="HKCU",
'                             subKeyPath:="Subkey Path",
'                             registryKeyPermissionCheck:=Microsoft.Win32.RegistryKeyPermissionCheck.Default,
'                             registryOptions:=Microsoft.Win32.RegistryOptions.None)
'
'    Dim regInfo2 As Registry.RegInfo(Of String) = RegistryUtil.CreateSubKey(Of String)(fullKeyPath:="HKCU\Subkey Path\")
'    Dim regInfo3 As Registry.RegInfo(Of String) = RegistryUtil.CreateSubKey(Of String)(rootKeyName:="HKCU",
'                                                                                 subKeyPath:="Subkey Path")

'' ----------------
'' Create Value
'' ----------------
'
'    RegistryUtil.CreateValue(fullKeyPath:="HKCU\Subkey Path\",
'                        valueName:="Value Name",
'                        valueData:="Value Data",
'                        valueType:=Microsoft.Win32.RegistryValueKind.String)
'
'    RegistryUtil.CreateValue(rootKeyName:="HKCU",
'                        subKeyPath:="Subkey Path",
'                        valueName:="Value Name",
'                        valueData:="Value Data",
'                        valueType:=Microsoft.Win32.RegistryValueKind.String)
'
'    RegistryUtil.CreateValue(regInfo:=regInfoByte)
'
'    RegistryUtil.CreateValue(Of String)(fullKeyPath:="HKCU\Subkey Path\",
'                                   valueName:="Value Name",
'                                   valueData:="Value Data",
'                                   valueType:=Microsoft.Win32.RegistryValueKind.String)
'
'    RegistryUtil.CreateValue(Of String)(rootKeyName:="HKCU",
'                                   subKeyPath:="Subkey Path",
'                                   valueName:="Value Name",
'                                   valueData:="Value Data",
'                                   valueType:=Microsoft.Win32.RegistryValueKind.String)
'
'    RegistryUtil.CreateValue(Of Byte())(regInfo:=regInfoByte)

'' ----------------
'' Copy KeyTree
'' ----------------
'
'    RegistryUtil.CopyKeyTree(srcFullKeyPath:="HKCU\Source Subkey Path\",
'                        dstFullKeyPath:="HKCU\Target Subkey Path\")
'
'    RegistryUtil.CopyKeyTree(srcRootKeyName:="HKCU",
'                        srcSubKeyPath:="Source Subkey Path\",
'                        dstRootKeyName:="HKCU",
'                        dstSubKeyPath:="Target Subkey Path\")

'' ----------------
'' Move KeyTree
'' ----------------
'
'    RegistryUtil.MoveKeyTree(srcFullKeyPath:="HKCU\Source Subkey Path\",
'                        dstFullKeyPath:="HKCU\Target Subkey Path\")
'
'    RegistryUtil.MoveKeyTree(srcRootKeyName:="HKCU",
'                        srcSubKeyPath:="Source Subkey Path\",
'                        dstRootKeyName:="HKCU",
'                        dstSubKeyPath:="Target Subkey Path\")

'' ----------------
'' Copy SubKeys
'' ----------------
'
'    RegistryUtil.CopySubKeys(srcFullKeyPath:="HKCU\Source Subkey Path\",
'                        dstFullKeyPath:="HKCU\Target Subkey Path\")
'
'    RegistryUtil.CopySubKeys(srcRootKeyName:="HKCU",
'                        srcSubKeyPath:="Source Subkey Path\",
'                        dstRootKeyName:="HKCU",
'                        dstSubKeyPath:="Target Subkey Path\")

'' ----------------
'' Move SubKeys
'' ----------------
'
'    RegistryUtil.MoveSubKeys(srcFullKeyPath:="HKCU\Source Subkey Path\",
'                        dstFullKeyPath:="HKCU\Target Subkey Path\")
'
'    RegistryUtil.MoveSubKeys(srcRootKeyName:="HKCU",
'                        srcSubKeyPath:="Source Subkey Path\",
'                        dstRootKeyName:="HKCU",
'                        dstSubKeyPath:="Target Subkey Path\")

'' ----------------
'' Copy Value
'' ----------------
'
'    Registry.CopyValue(srcFullKeyPath:="HKCU\Source Subkey Path\",
'                      sourceValueName:="Value Name",
'                      dstFullKeyPath:="HKCU\Target Subkey Path\",
'                      targetValueName:="Value Name")
'
'    Registry.CopyValue(srcRootKeyName:="HKCU",
'                      srcSubKeyPath:="Source Subkey Path\",
'                      sourceValueName:="Value Name",
'                      dstRootKeyName:="HKCU",
'                      dstSubKeyPath:="Target Subkey Path\",
'                      targetValueName:="Value Name")

'' ----------------
'' Move Value
'' ----------------
'
'    Registry.MoveValue(srcFullKeyPath:="HKCU\Source Subkey Path\",
'                      sourceValueName:="Value Name",
'                      dstFullKeyPath:="HKCU\Target Subkey Path\",
'                      targetValueName:="Value Name")
'
'    Registry.MoveValue(srcRootKeyName:="HKCU",
'                      srcSubKeyPath:="Source Subkey Path\",
'                      sourceValueName:="Value Name",
'                      dstRootKeyName:="HKCU",
'                      dstSubKeyPath:="Target Subkey Path\",
'                      targetValueName:="Value Name")

'' ----------------
'' DeleteValue
'' ----------------
'
'    RegistryUtil.DeleteValue(fullKeyPath:="HKCU\Subkey Path\",
'                        valueName:="Value Name",
'                        throwOnMissingValue:=True)
'
'    RegistryUtil.DeleteValue(rootKeyName:="HKCU",
'                        subKeyPath:="Subkey Path",
'                        valueName:="Value Name",
'                        throwOnMissingValue:=True)
'
'    RegistryUtil.DeleteValue(regInfo:=regInfoByte,
'                        throwOnMissingValue:=True)

'' ----------------
'' Delete SubKey
'' ----------------
'
'    RegistryUtil.DeleteSubKey(fullKeyPath:="HKCU\Subkey Path\",
'                         throwOnMissingSubKey:=False)
'
'    RegistryUtil.DeleteSubKey(rootKeyName:="HKCU",
'                         subKeyPath:="Subkey Path",
'                         throwOnMissingSubKey:=False)
'
'    RegistryUtil.DeleteSubKey(regInfo:=regInfoByte,
'                         throwOnMissingSubKey:=False)

'' ----------------
'' Exist SubKey?
'' ----------------
'
'    Dim exist1 As Boolean = RegistryUtil.ExistSubKey(fullKeyPath:="HKCU\Subkey Path\")
'
'    Dim exist2 As Boolean = RegistryUtil.ExistSubKey(rootKeyName:="HKCU",
'                                                subKeyPath:="Subkey Path")

'' ----------------
'' Exist Value?
'' ----------------
'
'    Dim exist3 As Boolean = RegistryUtil.ExistValue(fullKeyPath:="HKCU\Subkey Path\",
'                                               valueName:="Value Name")
'
'    Dim exist4 As Boolean = RegistryUtil.ExistValue(rootKeyName:="HKCU",
'                                               subKeyPath:="Subkey Path",
'                                               valueName:="Value Name")

'' ----------------
'' Value Is Empty?
'' ----------------
'
'    Dim isEmpty1 As Boolean = RegistryUtil.ValueIsEmpty(fullKeyPath:="HKCU\Subkey Path\",
'                                                   valueName:="Value Name")
'
'    Dim isEmpty2 As Boolean = RegistryUtil.ValueIsEmpty(rootKeyName:="HKCU",
'                                                   subKeyPath:="Subkey Path",
'                                                   valueName:="Value Name")

'' ----------------
'' Export Key
'' ----------------
'
'    RegistryUtil.ExportKey(fullKeyPath:="HKCU\Subkey Path\",
'                      outputFile:="C:\Backup.reg")
'
'    RegistryUtil.ExportKey(rootKeyName:="HKCU",
'                      subKeyPath:="Subkey Path",
'                      outputFile:="C:\Backup.reg")

'' ----------------
'' Import RegFile
'' ----------------
'
'    Registry.ImportRegFile(regFilePath:="C:\Backup.reg")

'' ----------------
'' Jump To Key
'' ----------------
'
'    RegistryUtil.JumpToKey(fullKeyPath:="HKCU\Subkey Path\")
'
'    RegistryUtil.JumpToKey(rootKeyName:="HKCU",
'                      subKeyPath:="Subkey Path")

'' ----------------
'' Find SubKey
'' ----------------
'
'    Dim regInfoSubkeyCol As IEnumerable(Of Registry.Reginfo) =
'        RegistryUtil.FindSubKey(rootKeyName:="HKCU",
'                           subKeyPath:="Subkey Path",
'                           subKeyName:="Subkey Name",
'                           matchFullSubKeyName:=False,
'                           ignoreCase:=True,
'                           searchOption:=IO.SearchOption.AllDirectories)
'
'    For Each reg As Registry.RegInfo In regInfoSubkeyCol
'        Debug.WriteLine(reg.RootKeyName)
'        Debug.WriteLine(reg.SubKeyPath)
'        Debug.WriteLine(reg.ValueName)
'        Debug.WriteLine(reg.ValueData.ToString())
'        Debug.WriteLine("")
'    Next reg

'' ----------------
'' Find Value
'' ----------------
'
'    Dim regInfoValueNameCol As IEnumerable(Of Registry.Reginfo) =
'        RegistryUtil.FindValue(rootKeyName:="HKCU",
'                              subKeyPath:="Subkey Path",
'                              valueName:="Value Name",
'                              matchFullValueName:=False,
'                              ignoreCase:=True,
'                              searchOption:=IO.SearchOption.AllDirectories)
'
'    For Each reg As Registry.RegInfo In regInfoValueNameCol
'        Debug.WriteLine(reg.RootKeyName)
'        Debug.WriteLine(reg.SubKeyPath)
'        Debug.WriteLine(reg.ValueName)
'        Debug.WriteLine(reg.ValueData.ToString())
'        Debug.WriteLine("")
'    Next reg

'' ----------------
'' Find Value Data
'' ----------------
'
'    Dim regInfoValueDataCol As IEnumerable(Of Registry.Reginfo) =
'        RegistryUtil.FindValueData(rootKeyName:="HKCU",
'                              subKeyPath:="Subkey Path",
'                              valueData:="Value Data",
'                              matchFullData:=False,
'                              ignoreCase:=True,
'                              searchOption:=IO.SearchOption.AllDirectories)
'
'    For Each reg As Registry.RegInfo In regInfoValueDataCol
'        Debug.WriteLine(reg.RootKeyName)
'        Debug.WriteLine(reg.SubKeyPath)
'        Debug.WriteLine(reg.ValueName)
'        Debug.WriteLine(reg.ValueData.ToString())
'        Debug.WriteLine("")
'    Next reg

'' ----------------
'' Get...
'' ----------------
'
'    Dim rootKeyName As String = RegistryUtil.GetRootKeyName(registryPath:="HKCU\Subkey Path\")
'    Dim subKeyPath As String = RegistryUtil.GetSubKeyPath(registryPath:="HKCU\Subkey Path\")
'    Dim rootKey As Microsoft.Win32.RegistryKey = RegistryUtil.GetRootKey(registryPath:="HKCU\Subkey Path\")

'' ----------------
'' Get Value Data
'' ----------------
'
'    Dim dataObject As Object = RegistryUtil.GetValueData(rootKeyName:="HKCU",
'                                                    subKeyPath:="Subkey Path",
'                                                    valueName:="Value Name")
'
'    Dim dataString As String = RegistryUtil.GetValueData(Of String)(fullKeyPath:="HKCU\Subkey Path\",
'                                                               valueName:="Value Name",
'                                                               registryValueOptions:=Microsoft.Win32.RegistryValueOptions.DoNotExpandEnvironmentNames)
'
'    Dim dataByte As Byte() = RegistryUtil.GetValueData(Of Byte())(regInfo:=regInfoByte,
'                                                             registryValueOptions:=Microsoft.Win32.RegistryValueOptions.None)
'    Debug.WriteLine("dataByte=" & String.Join(",", dataByte))

'' -----------------
' Set UserAccessKey
'' -----------------
'
'Registry.SetUserAccessKey(fullKeyPath:="HKCU\Subkey Path",
'                         userAccess:={Registry.ReginiUserAccess.AdministratorsFullAccess})
'
'Registry.SetUserAccessKey(rootKeyName:="HKCU",
'                         subKeyPath:="Subkey Path",
'                         userAccess:={Registry.ReginiUserAccess.AdministratorsFullAccess,
'                                      Registry.ReginiUserAccess.CreatorFullAccess,
'                                      Registry.ReginiUserAccess.SystemFullAccess})

#End Region

#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.IO
Imports System.Security.Permissions
Imports System.Text
Imports Microsoft.Win32

#End Region

#Region " Reg Util "

Namespace ElektroKit.Core.System.Tools

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains registry related utilities.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example that demonstrates various functionalities.
    ''' <code>
    ''' ' ----------------
    ''' ' Instance RegInfo
    ''' ' ----------------
    ''' 
    ''' Dim regInfo As New RegInfo
    ''' With regInfo
    '''     .RootKeyName = "HKCU"
    '''     .SubKeyPath = "Subkey Path"
    '''     .ValueName = "Value Name"
    '''     .ValueType = Microsoft.Win32.RegistryValueKind.String
    '''     .ValueData = "Hello World!"
    ''' End With
    ''' 
    ''' Dim regInfoByte As New RegInfo(Of Byte())
    ''' With regInfoByte
    '''     .RootKeyName = "HKCU"
    '''     .SubKeyPath = "Subkey Path"
    '''     .ValueName = "Value Name"
    '''     .ValueType = Microsoft.Win32.RegistryValueKind.Binary
    '''     .ValueData = Global.System.Text.Encoding.ASCII.GetBytes("Hello World!")
    ''' End With
    ''' 
    ''' ' ----------------
    ''' ' Create SubKey
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.CreateSubKey(fullKeyPath:="HKCU\Subkey Path\")
    ''' RegistryUtil.CreateSubKey(rootKeyName:="HKCU",
    '''                      subKeyPath:="Subkey Path")
    ''' RegistryUtil.CreateSubKey(regInfo:=regInfoByte)
    ''' 
    ''' Dim regKey1 As Microsoft.Win32.RegistryKey =
    '''     RegistryUtil.CreateSubKey(fullKeyPath:="HKCU\Subkey Path\",
    '''                          registryKeyPermissionCheck:=Microsoft.Win32.RegistryKeyPermissionCheck.Default,
    '''                          registryOptions:=Microsoft.Win32.RegistryOptions.None)
    ''' 
    ''' Dim regKey2 As Microsoft.Win32.RegistryKey =
    '''     RegistryUtil.CreateSubKey(rootKeyName:="HKCU",
    '''                          subKeyPath:="Subkey Path",
    '''                          registryKeyPermissionCheck:=Microsoft.Win32.RegistryKeyPermissionCheck.Default,
    '''                          registryOptions:=Microsoft.Win32.RegistryOptions.None)
    ''' 
    ''' Dim regInfo2 As Registry.RegInfo(Of String) = RegistryUtil.CreateSubKey(Of String)(fullKeyPath:="HKCU\Subkey Path\")
    ''' Dim regInfo3 As Registry.RegInfo(Of String) = RegistryUtil.CreateSubKey(Of String)(rootKeyName:="HKCU",
    '''                                                                              subKeyPath:="Subkey Path")
    ''' 
    ''' ' ----------------
    ''' ' Create Value
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.CreateValue(fullKeyPath:="HKCU\Subkey Path\",
    '''                     valueName:="Value Name",
    '''                     valueData:="Value Data",
    '''                     valueType:=Microsoft.Win32.RegistryValueKind.String)
    ''' 
    ''' RegistryUtil.CreateValue(rootKeyName:="HKCU",
    '''                     subKeyPath:="Subkey Path",
    '''                     valueName:="Value Name",
    '''                     valueData:="Value Data",
    '''                     valueType:=Microsoft.Win32.RegistryValueKind.String)
    ''' 
    ''' RegistryUtil.CreateValue(regInfo:=regInfoByte)
    ''' 
    ''' RegistryUtil.CreateValue(Of String)(fullKeyPath:="HKCU\Subkey Path\",
    '''                                valueName:="Value Name",
    '''                                valueData:="Value Data",
    '''                                valueType:=Microsoft.Win32.RegistryValueKind.String)
    ''' 
    ''' RegistryUtil.CreateValue(Of String)(rootKeyName:="HKCU",
    '''                                subKeyPath:="Subkey Path",
    '''                                valueName:="Value Name",
    '''                                valueData:="Value Data",
    '''                                valueType:=Microsoft.Win32.RegistryValueKind.String)
    ''' 
    ''' RegistryUtil.CreateValue(Of Byte())(regInfo:=regInfoByte)
    ''' 
    ''' ' ----------------
    ''' ' Copy KeyTree
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.CopyKeyTree(srcFullKeyPath:="HKCU\Source Subkey Path\",
    '''                     dstFullKeyPath:="HKCU\Target Subkey Path\")
    ''' 
    ''' RegistryUtil.CopyKeyTree(srcRootKeyName:="HKCU",
    '''                     srcSubKeyPath:="Source Subkey Path\",
    '''                     dstRootKeyName:="HKCU",
    '''                     dstSubKeyPath:="Target Subkey Path\")
    ''' 
    ''' ' ----------------
    ''' ' Move KeyTree
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.MoveKeyTree(srcFullKeyPath:="HKCU\Source Subkey Path\",
    '''                     dstFullKeyPath:="HKCU\Target Subkey Path\")
    ''' 
    ''' RegistryUtil.MoveKeyTree(srcRootKeyName:="HKCU",
    '''                     srcSubKeyPath:="Source Subkey Path\",
    '''                     dstRootKeyName:="HKCU",
    '''                     dstSubKeyPath:="Target Subkey Path\")
    ''' 
    ''' ' ----------------
    ''' ' Copy SubKeys
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.CopySubKeys(srcFullKeyPath:="HKCU\Source Subkey Path\",
    '''                     dstFullKeyPath:="HKCU\Target Subkey Path\")
    ''' 
    ''' RegistryUtil.CopySubKeys(srcRootKeyName:="HKCU",
    '''                     srcSubKeyPath:="Source Subkey Path\",
    '''                     dstRootKeyName:="HKCU",
    '''                     dstSubKeyPath:="Target Subkey Path\")
    ''' 
    ''' ' ----------------
    ''' ' Move SubKeys
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.MoveSubKeys(srcFullKeyPath:="HKCU\Source Subkey Path\",
    '''                     dstFullKeyPath:="HKCU\Target Subkey Path\")
    ''' 
    ''' RegistryUtil.MoveSubKeys(srcRootKeyName:="HKCU",
    '''                     srcSubKeyPath:="Source Subkey Path\",
    '''                     dstRootKeyName:="HKCU",
    '''                     dstSubKeyPath:="Target Subkey Path\")
    ''' 
    ''' ' ----------------
    ''' ' Copy Value
    ''' ' ----------------
    ''' 
    ''' Registry.CopyValue(srcFullKeyPath:="HKCU\Source Subkey Path\",
    '''                   sourceValueName:="Value Name",
    '''                   dstFullKeyPath:="HKCU\Target Subkey Path\",
    '''                   targetValueName:="Value Name")
    ''' 
    ''' Registry.CopyValue(srcRootKeyName:="HKCU",
    '''                   srcSubKeyPath:="Source Subkey Path\",
    '''                   sourceValueName:="Value Name",
    '''                   dstRootKeyName:="HKCU",
    '''                   dstSubKeyPath:="Target Subkey Path\",
    '''                   targetValueName:="Value Name")
    ''' 
    ''' ' ----------------
    ''' ' Move Value
    ''' ' ----------------
    ''' 
    ''' Registry.MoveValue(srcFullKeyPath:="HKCU\Source Subkey Path\",
    '''                   sourceValueName:="Value Name",
    '''                   dstFullKeyPath:="HKCU\Target Subkey Path\",
    '''                   targetValueName:="Value Name")
    ''' 
    ''' Registry.MoveValue(srcRootKeyName:="HKCU",
    '''                   srcSubKeyPath:="Source Subkey Path\",
    '''                   sourceValueName:="Value Name",
    '''                   dstRootKeyName:="HKCU",
    '''                   dstSubKeyPath:="Target Subkey Path\",
    '''                   targetValueName:="Value Name")
    ''' 
    ''' ' ----------------
    ''' ' DeleteValue
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.DeleteValue(fullKeyPath:="HKCU\Subkey Path\",
    '''                     valueName:="Value Name",
    '''                     throwOnMissingValue:=True)
    ''' 
    ''' RegistryUtil.DeleteValue(rootKeyName:="HKCU",
    '''                     subKeyPath:="Subkey Path",
    '''                     valueName:="Value Name",
    '''                     throwOnMissingValue:=True)
    ''' 
    ''' RegistryUtil.DeleteValue(regInfo:=regInfoByte,
    '''                     throwOnMissingValue:=True)
    ''' 
    ''' ' ----------------
    ''' ' Delete SubKey
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.DeleteSubKey(fullKeyPath:="HKCU\Subkey Path\",
    '''                      throwOnMissingSubKey:=False)
    ''' 
    ''' RegistryUtil.DeleteSubKey(rootKeyName:="HKCU",
    '''                      subKeyPath:="Subkey Path",
    '''                      throwOnMissingSubKey:=False)
    ''' 
    ''' RegistryUtil.DeleteSubKey(regInfo:=regInfoByte,
    '''                      throwOnMissingSubKey:=False)
    ''' 
    ''' ' ----------------
    ''' ' Exist SubKey?
    ''' ' ----------------
    ''' 
    ''' Dim exist1 As Boolean = RegistryUtil.ExistSubKey(fullKeyPath:="HKCU\Subkey Path\")
    ''' 
    ''' Dim exist2 As Boolean = RegistryUtil.ExistSubKey(rootKeyName:="HKCU",
    '''                                             subKeyPath:="Subkey Path")
    ''' 
    ''' ' ----------------
    ''' ' Exist Value?
    ''' ' ----------------
    ''' 
    ''' Dim exist3 As Boolean = RegistryUtil.ExistValue(fullKeyPath:="HKCU\Subkey Path\",
    '''                                            valueName:="Value Name")
    ''' 
    ''' Dim exist4 As Boolean = RegistryUtil.ExistValue(rootKeyName:="HKCU",
    '''                                            subKeyPath:="Subkey Path",
    '''                                            valueName:="Value Name")
    ''' 
    ''' ' ----------------
    ''' ' Value Is Empty?
    ''' ' ----------------
    ''' 
    ''' Dim isEmpty1 As Boolean = RegistryUtil.ValueIsEmpty(fullKeyPath:="HKCU\Subkey Path\",
    '''                                                valueName:="Value Name")
    ''' 
    ''' Dim isEmpty2 As Boolean = RegistryUtil.ValueIsEmpty(rootKeyName:="HKCU",
    '''                                                subKeyPath:="Subkey Path",
    '''                                                valueName:="Value Name")
    ''' 
    ''' ' ----------------
    ''' ' Export Key
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.ExportKey(fullKeyPath:="HKCU\Subkey Path\",
    '''                   outputFile:="C:\Backup.reg")
    ''' 
    ''' RegistryUtil.ExportKey(rootKeyName:="HKCU",
    '''                   subKeyPath:="Subkey Path",
    '''                   outputFile:="C:\Backup.reg")
    ''' 
    ''' ' ----------------
    ''' ' Import RegFile
    ''' ' ----------------
    ''' 
    ''' Registry.ImportRegFile(regFilePath:="C:\Backup.reg")
    ''' 
    ''' ' ----------------
    ''' ' Jump To Key
    ''' ' ----------------
    ''' 
    ''' RegistryUtil.JumpToKey(fullKeyPath:="HKCU\Subkey Path\")
    ''' 
    ''' RegistryUtil.JumpToKey(rootKeyName:="HKCU",
    '''                   subKeyPath:="Subkey Path")
    ''' 
    ''' ' ----------------
    ''' ' Find SubKey
    ''' ' ----------------
    ''' 
    ''' Dim regInfoSubkeyCol As IEnumerable(Of Registry.Reginfo) =
    '''     RegistryUtil.FindSubKey(rootKeyName:="HKCU",
    '''                        subKeyPath:="Subkey Path",
    '''                        subKeyName:="Subkey Name",
    '''                        matchFullSubKeyName:=False,
    '''                        ignoreCase:=True,
    '''                        searchOption:=IO.SearchOption.AllDirectories)
    ''' 
    ''' For Each reg As Registry.RegInfo In regInfoSubkeyCol
    '''     Debug.WriteLine(reg.RootKeyName)
    '''     Debug.WriteLine(reg.SubKeyPath)
    '''     Debug.WriteLine(reg.ValueName)
    '''     Debug.WriteLine(reg.ValueData.ToString())
    '''     Debug.WriteLine("")
    ''' Next reg
    ''' 
    ''' ' ----------------
    ''' ' Find Value
    ''' ' ----------------
    ''' 
    ''' Dim regInfoValueNameCol As IEnumerable(Of Registry.Reginfo) =
    '''     RegistryUtil.FindValue(rootKeyName:="HKCU",
    '''                           subKeyPath:="Subkey Path",
    '''                           valueName:="Value Name",
    '''                           matchFullValueName:=False,
    '''                           ignoreCase:=True,
    '''                           searchOption:=IO.SearchOption.AllDirectories)
    ''' 
    ''' For Each reg As Registry.RegInfo In regInfoValueNameCol
    '''     Debug.WriteLine(reg.RootKeyName)
    '''     Debug.WriteLine(reg.SubKeyPath)
    '''     Debug.WriteLine(reg.ValueName)
    '''     Debug.WriteLine(reg.ValueData.ToString())
    '''     Debug.WriteLine("")
    ''' Next reg
    ''' 
    ''' ' ----------------
    ''' ' Find Value Data
    ''' ' ----------------
    ''' 
    ''' Dim regInfoValueDataCol As IEnumerable(Of Registry.Reginfo) =
    '''     RegistryUtil.FindValueData(rootKeyName:="HKCU",
    '''                           subKeyPath:="Subkey Path",
    '''                           valueData:="Value Data",
    '''                           matchFullData:=False,
    '''                           ignoreCase:=True,
    '''                           searchOption:=IO.SearchOption.AllDirectories)
    ''' 
    ''' For Each reg As Registry.RegInfo In regInfoValueDataCol
    '''     Debug.WriteLine(reg.RootKeyName)
    '''     Debug.WriteLine(reg.SubKeyPath)
    '''     Debug.WriteLine(reg.ValueName)
    '''     Debug.WriteLine(reg.ValueData.ToString())
    '''     Debug.WriteLine("")
    ''' Next reg
    ''' 
    ''' ' ----------------
    ''' ' Get...
    ''' ' ----------------
    ''' 
    ''' Dim rootKeyName As String = RegistryUtil.GetRootKeyName(registryPath:="HKCU\Subkey Path\")
    ''' Dim subKeyPath As String = RegistryUtil.GetSubKeyPath(registryPath:="HKCU\Subkey Path\")
    ''' Dim rootKey As Microsoft.Win32.RegistryKey = RegistryUtil.GetRootKey(registryPath:="HKCU\Subkey Path\")
    ''' 
    ''' ' ----------------
    ''' ' Get Value Data
    ''' ' ----------------
    ''' 
    ''' Dim dataObject As Object = RegistryUtil.GetValueData(rootKeyName:="HKCU",
    '''                                                 subKeyPath:="Subkey Path",
    '''                                                 valueName:="Value Name")
    ''' 
    ''' Dim dataString As String = RegistryUtil.GetValueData(Of String)(fullKeyPath:="HKCU\Subkey Path\",
    '''                                                            valueName:="Value Name",
    '''                                                            registryValueOptions:=Microsoft.Win32.RegistryValueOptions.DoNotExpandEnvironmentNames)
    ''' 
    ''' Dim dataByte As Byte() = RegistryUtil.GetValueData(Of Byte())(regInfo:=regInfoByte,
    '''                                                          registryValueOptions:=Microsoft.Win32.RegistryValueOptions.None)
    ''' Debug.WriteLine("dataByte=" &amp; String.Join(",", dataByte))
    ''' 
    ''' ' -----------------
    '''  Set UserAccessKey
    ''' ' -----------------
    ''' 
    ''' Registry.SetUserAccessKey(fullKeyPath:="HKCU\Subkey Path",
    '''                          userAccess:={Registry.ReginiUserAccess.AdministratorsFullAccess})
    ''' 
    ''' Registry.SetUserAccessKey(rootKeyName:="HKCU",
    '''                          subKeyPath:="Subkey Path",
    '''                          userAccess:={Registry.ReginiUserAccess.AdministratorsFullAccess,
    '''                                       Registry.ReginiUserAccess.CreatorFullAccess,
    '''                                       Registry.ReginiUserAccess.SystemFullAccess})
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
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

#Region " Create SubKey "

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Creates a new registry subkey.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="rootKeyName">
        '''' The rootkey name.
        '''' </param>
        '''' 
        '''' <param name="subKeyPath">
        '''' The subkey path.
        '''' </param>
        '''' 
        '''' <param name="registryKeyPermissionCheck">
        '''' The registry key permission check.
        '''' </param>
        '''' 
        '''' <param name="registryOptions">
        '''' The registry options.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' The registry key.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <exception cref="ArgumentNullException">
        '''' rootKeyName or subKeyPath
        '''' </exception>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function CreateSubKey(Of T)(ByVal view As RegistryView, ByVal rootKeyName As String,
        '                                          ByVal subKeyPath As String,
        '                                          Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
        '                                                         RegistryKeyPermissionCheck.Default,
        '                                          Optional ByVal registryOptions As RegistryOptions =
        '                                                         RegistryOptions.None) As RegInfo(Of T)

        '    If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
        '        Throw New ArgumentNullException("rootKeyName")

        '    ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
        '        Throw New ArgumentNullException("subKeyPath")

        '    Else
        '        Dim regInfo As New RegInfo(Of T)
        '        With regInfo
        '            .RootKeyName = RegistryUtil.GetRootKeyName(rootKeyName)
        '            .SubKeyPath = RegistryUtil.GetSubKeyPath(subKeyPath)
        '        End With

        '        Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)
        '            reg.CreateSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), registryKeyPermissionCheck, registryOptions)
        '        End Using

        '        Return regInfo

        '    End If

        'End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Creates a new registry subkey.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="fullKeyPath">
        '''' The registry key full path.
        '''' </param>
        '''' 
        '''' <param name="registryKeyPermissionCheck">
        '''' The registry key permission check.
        '''' </param>
        '''' 
        '''' <param name="registryOptions">
        '''' The registry options.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' The registry key.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function CreateSubKey(Of T)(ByVal view As RegistryView, ByVal fullKeyPath As String,
        '                                          Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
        '                                                         RegistryKeyPermissionCheck.Default,
        '                                          Optional ByVal registryOptions As RegistryOptions =
        '                                                         RegistryOptions.None) As RegInfo(Of T)

        '    Return RegistryUtil.CreateSubKey(Of T)(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), registryKeyPermissionCheck, registryOptions)

        'End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Creates a new registry subkey.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' 
        '''' <param name="registryKeyPermissionCheck">
        '''' The registry key permission check.
        '''' </param>
        '''' 
        '''' <param name="registryOptions">
        '''' The registry options.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' The registry key.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function CreateSubKey(Of T)(ByVal regInfo As RegInfo(Of T),
        '                                          Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
        '                                                         RegistryKeyPermissionCheck.Default,
        '                                          Optional ByVal registryOptions As RegistryOptions =
        '                                                         RegistryOptions.None) As RegInfo(Of T)

        '    Return RegistryUtil.CreateSubKey(Of T)(regInfo.View, regInfo.RootKeyName, regInfo.SubKeyPath, registryKeyPermissionCheck, registryOptions)

        'End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates a new registry subkey.
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
        ''' <param name="registryKeyPermissionCheck">
        ''' The registry key permission check.
        ''' </param>
        ''' 
        ''' <param name="registryOptions">
        ''' The registry options.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The registry key.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function CreateSubKey(ByVal view As RegistryView, ByVal rootKeyName As String,
                                            ByVal subKeyPath As String,
                                            Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
                                                           RegistryKeyPermissionCheck.Default,
                                            Optional ByVal registryOptions As RegistryOptions =
                                                           RegistryOptions.None) As RegistryKey

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)
                    Return reg.CreateSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), registryKeyPermissionCheck, registryOptions)
                End Using

            End If

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates a new registry subkey.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="fullKeyPath">
        ''' The registry key full path.
        ''' </param>
        ''' 
        ''' <param name="registryKeyPermissionCheck">
        ''' The registry key permission check.
        ''' </param>
        ''' 
        ''' <param name="registryOptions">
        ''' The registry options.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The registry key.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function CreateSubKey(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                            Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
                                                           RegistryKeyPermissionCheck.Default,
                                            Optional ByVal registryOptions As RegistryOptions =
                                                           RegistryOptions.None) As RegistryKey

            Return RegistryUtil.CreateSubKey(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), registryKeyPermissionCheck, registryOptions)

        End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Creates a new registry subkey.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' 
        '''' <param name="registryKeyPermissionCheck">T
        '''' he registry key permission check.
        '''' </param>
        '''' 
        '''' <param name="registryOptions">
        '''' The registry options.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' The registry key.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function CreateSubKey(ByVal regInfo As RegInfo,
        '                                    Optional ByVal registryKeyPermissionCheck As RegistryKeyPermissionCheck =
        '                                                   RegistryKeyPermissionCheck.Default,
        '                                    Optional ByVal registryOptions As RegistryOptions =
        '                                                   RegistryOptions.None) As RegistryKey

        '    Return RegistryUtil.CreateSubKey(regInfo.View, regInfo.RootKeyName, regInfo.SubKeyPath, registryKeyPermissionCheck, registryOptions)

        'End Function

#End Region

#Region " Delete SubKey "

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
        ''' <param name="throwOnMissingSubKey">
        ''' If set to <see langword="True"/>, throws an exception on missing subkey.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub DeleteSubKey(ByVal view As RegistryView, ByVal rootKeyName As String,
                                       ByVal subKeyPath As String,
                                       Optional ByVal throwOnMissingSubKey As Boolean = False)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)
                    reg.DeleteSubKeyTree(RegistryUtil.GetSubKeyPath(subKeyPath), throwOnMissingSubKey)
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
        ''' <param name="throwOnMissingSubKey">
        ''' If set to <see langword="True"/>, throws an exception on missing subkey.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub DeleteSubKey(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                       Optional ByVal throwOnMissingSubKey As Boolean = False)

            RegistryUtil.DeleteSubKey(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), throwOnMissingSubKey)

        End Sub

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Deletes a registry subkey.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' 
        '''' <param name="throwOnMissingSubKey">
        '''' If set to <see langword="True"/>, throws an exception on missing subkey.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Sub DeleteSubKey(Of T)(ByVal regInfo As RegInfo(Of T),
        '                                     Optional ByVal throwOnMissingSubKey As Boolean = False)

        '    RegistryUtil.DeleteSubKey(regInfo.View, regInfo.RootKeyName, regInfo.SubKeyPath, throwOnMissingSubKey)

        'End Sub

#End Region

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
        Public Shared Sub CreateValue(Of T)(ByVal view As RegistryView, ByVal rootKeyName As String,
                                            ByVal subKeyPath As String,
                                            ByVal valueName As String,
                                            ByVal valueData As T,
                                            Optional ByVal valueType As RegistryValueKind = RegistryValueKind.String)

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
        Public Shared Sub CreateValue(Of T)(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                            ByVal valueName As String,
                                            ByVal valueData As T,
                                            Optional ByVal valueType As RegistryValueKind = RegistryValueKind.String)

            RegistryUtil.CreateValue(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, valueData, valueType)

        End Sub

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Creates or replaces a registry value.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Sub CreateValue(Of T)(ByVal regInfo As RegInfo(Of T))

        '    RegistryUtil.CreateValue(regInfo.View, regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, regInfo.ValueData, regInfo.ValueType)

        'End Sub

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
        Public Shared Sub DeleteValue(ByVal view As RegistryView, ByVal rootKeyName As String,
                                      ByVal subKeyPath As String,
                                      ByVal valueName As String,
                                      Optional ByVal throwOnMissingValue As Boolean = False)

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
        Public Shared Sub DeleteValue(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                      ByVal valueName As String,
                                      Optional ByVal throwOnMissingValue As Boolean = False)

            RegistryUtil.DeleteValue(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, throwOnMissingValue)

        End Sub

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Deletes a registry subkey.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' 
        '''' <param name="throwOnMissingValue">
        '''' If set to <see langword="True"/>, throws an exception on missing value.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Sub DeleteValue(Of T)(ByVal regInfo As RegInfo(Of T),
        '                                    Optional ByVal throwOnMissingValue As Boolean = False)

        '    RegistryUtil.DeleteValue(regInfo.View, regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, throwOnMissingValue)

        'End Sub

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
        Public Shared Function GetValueData(Of T)(ByVal view As RegistryView, ByVal rootKeyName As String,
                                                  ByVal subKeyPath As String,
                                                  ByVal valueName As String,
                                                  ByVal defaultIfEmpty As T,
                                                  Optional ByVal registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As T

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)
                    If RegistryUtil.ExistSubKey(view, rootKeyName, subKeyPath) Then
                        Return DirectCast(reg.OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=False).
                                          GetValue(valueName, defaultValue:=defaultIfEmpty, options:=registryValueOptions), T)
                    Else
                        Return defaultIfEmpty
                    End If
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
        Public Shared Function GetValueData(Of T)(ByVal view As RegistryView, ByVal rootKeyName As String,
                                                  ByVal subKeyPath As String,
                                                  ByVal valueName As String,
                                                  Optional ByVal registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As T

            Return RegistryUtil.GetValueData(Of T)(view, rootKeyName, subKeyPath, valueName, Nothing, registryValueOptions)

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
        <DebuggerStepThrough>
        Public Shared Function GetValueData(Of T)(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                                  ByVal valueName As String,
                                                  ByVal defaultIfEmpty As T,
                                                  Optional ByVal registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As T

            Return RegistryUtil.GetValueData(Of T)(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, defaultIfEmpty, registryValueOptions)

        End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Gets the data of a registry value.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' 
        '''' <param name="defaultIfEmpty">
        '''' A default value to return if the data is empty.
        '''' </param>
        '''' 
        '''' <param name="registryValueOptions">
        '''' The registry value options.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' The value data.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function GetValueData(Of T)(ByVal regInfo As RegInfo(Of T),
        '                                          ByVal defaultIfEmpty As T,
        '                                          Optional ByVal registryValueOptions As RegistryValueOptions =
        '                                                         RegistryValueOptions.None) As T

        '    Return RegistryUtil.GetValueData(Of T)(regInfo.View, regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, defaultIfEmpty, registryValueOptions)

        'End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the data of a registry value.
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
        ''' rootKeyName or subKeyPath or valueName
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetValueData(ByVal view As RegistryView, ByVal rootKeyName As String,
                                            ByVal subKeyPath As String,
                                            ByVal valueName As String,
                                            ByVal defaultIfEmpty As Object,
                                            Optional ByVal registryValueOptions As RegistryValueOptions =
                                                           RegistryValueOptions.None) As Object

            Return RegistryUtil.GetValueData(Of Object)(view, rootKeyName, subKeyPath, valueName, defaultIfEmpty, registryValueOptions)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the data of a registry value.
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
        <DebuggerStepThrough>
        Public Shared Function GetValueData(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                            ByVal valueName As String,
                                            ByVal defaultIfEmpty As Object,
                                            Optional ByVal registryValueOptions As RegistryValueOptions =
                                                           RegistryValueOptions.None) As Object

            Return RegistryUtil.GetValueData(Of Object)(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, defaultIfEmpty, registryValueOptions)

        End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Gets the data of a registry value.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' 
        '''' <param name="defaultIfEmpty">
        '''' A default value to return if the data is empty.
        '''' </param>
        '''' 
        '''' <param name="registryValueOptions">
        '''' The registry value options.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' The value data.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function GetValueData(ByVal regInfo As RegInfo,
        '                                    ByVal defaultIfEmpty As Object,
        '                                    Optional ByVal registryValueOptions As RegistryValueOptions =
        '                                                   RegistryValueOptions.None) As Object

        '    Return RegistryUtil.GetValueData(Of Object)(regInfo.View, regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, defaultIfEmpty, registryValueOptions)

        'End Function

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
        Public Shared Function GetValueData(Of T)(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                                  ByVal valueName As String,
                                                  Optional ByVal registryValueOptions As RegistryValueOptions =
                                                                 RegistryValueOptions.None) As T

            Return RegistryUtil.GetValueData(Of T)(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, Nothing, registryValueOptions)

        End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Gets the data of a registry value.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' 
        '''' <param name="registryValueOptions">
        '''' The registry value options.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' The value data.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function GetValueData(Of T)(ByVal regInfo As RegInfo(Of T),
        '                                          Optional ByVal registryValueOptions As RegistryValueOptions =
        '                                                         RegistryValueOptions.None) As T

        '    Return RegistryUtil.GetValueData(Of T)(regInfo.View, regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, Nothing, registryValueOptions)

        'End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the data of a registry value.
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
        ''' <param name="registryValueOptions">
        ''' The registry value options.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The value data.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath or valueName
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetValueData(ByVal view As RegistryView, ByVal rootKeyName As String,
                                            ByVal subKeyPath As String,
                                            ByVal valueName As String,
                                            Optional ByVal registryValueOptions As RegistryValueOptions =
                                                           RegistryValueOptions.None) As Object

            Return RegistryUtil.GetValueData(Of Object)(view, rootKeyName, subKeyPath, valueName, String.Empty, registryValueOptions)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the data of a registry value.
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
        ''' <param name="registryValueOptions">
        ''' The registry value options.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The value data.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function GetValueData(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                            ByVal valueName As String,
                                            Optional ByVal registryValueOptions As RegistryValueOptions =
                                                           RegistryValueOptions.None) As Object

            Return RegistryUtil.GetValueData(Of Object)(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, String.Empty, registryValueOptions)

        End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Gets the data of a registry value.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' 
        '''' <param name="registryValueOptions">
        '''' The registry value options.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' The value data.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function GetValueData(ByVal regInfo As RegInfo,
        '                                    Optional ByVal registryValueOptions As RegistryValueOptions =
        '                                                   RegistryValueOptions.None) As Object

        '    Return RegistryUtil.GetValueData(Of Object)(regInfo.View, regInfo.RootKeyName, regInfo.SubKeyPath, regInfo.ValueName, String.Empty, registryValueOptions)

        'End Function

#End Region

#Region " Validation "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether a file is a valid Registry script file.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code>
        ''' Dim isRegFile As Boolean = IsRegistryFile("C:\File.reg")
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filepath">
        ''' The .reg file path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function IsRegistryFile(ByVal filepath As String) As Boolean

            Dim firstLine As String = File.ReadLines(filepath, Encoding.Unicode).FirstOrDefault.Trim

            Return (firstLine.Equals("Windows Registry Editor Version 5.00", StringComparison.OrdinalIgnoreCase)) OrElse
                   (firstLine.Equals("REGEDIT4", StringComparison.OrdinalIgnoreCase))

        End Function

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
        Public Shared Function ExistSubKey(ByVal view As RegistryView, ByVal rootKeyName As String,
                                           ByVal subKeyPath As String) As Boolean

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
        Public Shared Function ExistSubKey(ByVal view As RegistryView, ByVal fullKeyPath As String) As Boolean

            Return RegistryUtil.ExistSubKey(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath))

        End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Determines whether a registry subkey exists.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' <see langword="True"/> if subkey exist, <see langword="False"/> otherwise.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function ExistSubKey(Of T)(ByVal reginfo As RegInfo(Of T)) As Boolean

        '    Return RegistryUtil.ExistSubKey(reginfo.View, reginfo.FullKeyPath)

        'End Function

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
        Public Shared Function ExistValue(ByVal view As RegistryView, ByVal rootKeyName As String,
                                          ByVal subKeyPath As String,
                                          ByVal valueName As String) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)

                    If RegistryUtil.ExistSubKey(view, rootKeyName, subKeyPath) Then
                        Return reg.OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=False).
                                   GetValue(valueName, defaultValue:=Nothing) IsNot Nothing
                    Else
                        Return False
                    End If

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
        Public Shared Function ExistValue(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                          ByVal valueName As String) As Boolean

            Return RegistryUtil.ExistValue(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName)

        End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Determines whether a registry value exists.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <typeparam name="T">
        '''' The type.
        '''' </typeparam>
        '''' 
        '''' <param name="regInfo">
        '''' A <see cref="RegInfo(Of T)"/> instance containing the registry info.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' <see langword="True"/> if value exist, <see langword="False"/> otherwise.
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function ExistValue(Of T)(ByVal reginfo As RegInfo(Of T)) As Boolean

        '    Return RegistryUtil.ExistValue(reginfo.View, reginfo.FullKeyPath, reginfo.ValueName)

        'End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether a registry value is empty.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="rootKeyName">
        ''' The rootkey name.
        ''' 
        ''' </param>
        ''' <param name="subKeyPath">
        ''' The subkey path.
        ''' </param>
        ''' 
        ''' <param name="valueName">
        ''' The value name.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if value is empty, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath or valueName
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ValueIsEmpty(ByVal view As RegistryView, ByVal rootKeyName As String,
                                            ByVal subKeyPath As String,
                                            ByVal valueName As String) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(valueName) OrElse String.IsNullOrWhiteSpace(valueName) Then
                Throw New ArgumentNullException("valueName")

            Else
                Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view)
                    If RegistryUtil.ExistSubKey(view, rootKeyName, subKeyPath) Then
                        Return String.IsNullOrEmpty(reg.OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=False).
                                                    GetValue(valueName, defaultValue:=Nothing).ToString())
                    Else
                        Return True
                    End If
                End Using

            End If

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether a registry value is empty.
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
        ''' <see langword="True"/> if value is empty, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ValueIsEmpty(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                            ByVal valueName As String) As Boolean

            Return RegistryUtil.ValueIsEmpty(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName)

        End Function

#End Region

#Region " Import / Export "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Imports a registry file into the current registry Hive.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="regFilePath">
        ''' The registry filepath.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if operation succeeds, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' regFilePath
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ImportRegFile(ByVal regFilePath As String) As Boolean

            If String.IsNullOrEmpty(regFilePath) OrElse String.IsNullOrWhiteSpace(regFilePath) Then
                Throw New ArgumentNullException("regFilePath")

            Else
                Using proc As New Process With {
                        .StartInfo = New ProcessStartInfo() With {
                              .FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "Reg.exe"),
                              .Arguments = String.Format("Import ""{0}""", regFilePath),
                              .CreateNoWindow = True,
                              .WindowStyle = ProcessWindowStyle.Hidden,
                              .UseShellExecute = False
                            }
                        }

                    With proc
                        .Start()
                        .WaitForExit()
                    End With

                    Return Not CBool(proc.ExitCode)

                End Using

            End If

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Exports a key to a registry file.
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
        ''' <param name="outputFile">
        ''' The output file.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if operation succeeds, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath or outputFile
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ExportKey(ByVal rootKeyName As String,
                                         ByVal subKeyPath As String,
                                         ByVal outputFile As String) As Boolean

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            ElseIf String.IsNullOrEmpty(outputFile) OrElse String.IsNullOrWhiteSpace(outputFile) Then
                Throw New ArgumentNullException("outputFile")

            Else
                Using proc As New Process With {
                        .StartInfo = New ProcessStartInfo() With {
                              .FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "Reg.exe"),
                              .Arguments = String.Format("Export ""{0}\{1}"" ""{2}"" /y",
                                                         RegistryUtil.GetRootKeyName(rootKeyName),
                                                         RegistryUtil.GetSubKeyPath(subKeyPath),
                                                         outputFile),
                              .CreateNoWindow = True,
                              .WindowStyle = ProcessWindowStyle.Hidden,
                              .UseShellExecute = False
                            }
                        }

                    With proc
                        .Start()
                        .WaitForExit()
                    End With

                    Return Not CBool(proc.ExitCode)

                End Using

            End If

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Exports a key to a registry file.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="fullKeyPath">
        ''' The registry key full path.
        ''' </param>
        ''' 
        ''' <param name="outputFile">
        ''' The output file.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if operation succeeds, <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Function ExportKey(ByVal view As RegistryView, ByVal fullKeyPath As String,
                                         ByVal outputFile As String) As Boolean

            Return RegistryUtil.ExportKey(RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), outputFile)

        End Function

#End Region

#Region " Navigation "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Runs Regedit.exe process to jump at the specified key.
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
        ''' <exception cref="ArgumentNullException">
        ''' rootKeyName or subKeyPath
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub JumpToKey(ByVal view As RegistryView, ByVal rootKeyName As String,
                                    ByVal subKeyPath As String)

            If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
                Throw New ArgumentNullException("rootKeyName")

            ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
                Throw New ArgumentNullException("subKeyPath")

            Else
                RegistryUtil.CreateValue(view:=view,
                                     rootKeyName:="HKCU",
                                     subKeyPath:="Software\Microsoft\Windows\CurrentVersion\Applets\Registry",
                                     valueName:="LastKey",
                                     valueData:=String.Format("{0}\{1}", RegistryUtil.GetRootKeyName(rootKeyName), RegistryUtil.GetSubKeyPath(subKeyPath)),
                                     valueType:=RegistryValueKind.String)

                Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Regedit.exe"))

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Runs Registry.exe process to jump at the specified key.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="fullKeyPath">
        ''' The registry key full path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub JumpToKey(ByVal view As RegistryView, ByVal fullKeyPath As String)

            RegistryUtil.JumpToKey(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath))

        End Sub

#End Region

#Region " Copying "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Copies a registry value (with its data) to another subkey.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcFullKeyPath">
        ''' The source registry key full path.
        ''' </param>
        ''' 
        ''' <param name="srcValueName">
        ''' The source registry value name.
        ''' </param>
        ''' 
        ''' <param name="dstFullKeyPath">
        ''' The target registry key full path.
        ''' </param>
        ''' 
        ''' <param name="dstValueName">
        ''' The target registry value name.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub CopyValue(ByVal srcView As RegistryView,
                                    ByVal srcFullKeyPath As String,
                                    ByVal srcValueName As String,
                                    ByVal dstView As RegistryView,
                                    ByVal dstFullKeyPath As String,
                                    ByVal dstValueName As String)

            RegistryUtil.CreateValue(view:=dstView,
                                 fullKeyPath:=dstFullKeyPath,
                                 valueName:=dstValueName,
                                 valueData:=RegistryUtil.GetValueData(srcView, fullKeyPath:=srcFullKeyPath, valueName:=srcValueName),
                                 valueType:=RegistryValueKind.Unknown)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Copies a registry value (with its data) to another subkey.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcRootKeyName">
        ''' The source registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="srcSubKeyPath">
        ''' The source registry subkey path.
        ''' </param>
        ''' 
        ''' <param name="srcValueName">
        ''' The source registry value name.
        ''' </param>
        ''' 
        ''' <param name="dstRootKeyName">
        ''' The target registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="dstSubKeyPath">
        ''' The target registry subkey path.
        ''' </param>
        ''' 
        ''' <param name="dstValueName">
        ''' The target registry value name.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub CopyValue(ByVal srcView As RegistryView,
                                    ByVal srcRootKeyName As String,
                                    ByVal srcSubKeyPath As String,
                                    ByVal srcValueName As String,
                                    ByVal dstView As RegistryView,
                                    ByVal dstRootKeyName As String,
                                    ByVal dstSubKeyPath As String,
                                    ByVal dstValueName As String)

            RegistryUtil.CreateValue(view:=dstView,
                                 rootKeyName:=dstRootKeyName,
                                 subKeyPath:=dstSubKeyPath,
                                 valueName:=dstValueName,
                                 valueData:=RegistryUtil.GetValueData(view:=srcView, rootKeyName:=srcRootKeyName, subKeyPath:=srcSubKeyPath, valueName:=srcValueName),
                                 valueType:=RegistryValueKind.Unknown)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Moves a registry value (with its data) to another subkey.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcFullKeyPath">
        ''' The source registry key full path.
        ''' </param>
        ''' 
        ''' <param name="srcValueName">
        ''' The source registry value name.
        ''' </param>
        ''' 
        ''' <param name="dstFullKeyPath">
        ''' The target registry key full path.
        ''' </param>
        ''' 
        ''' <param name="dstValueName">
        ''' The target registry value name.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub MoveValue(ByVal srcView As RegistryView,
                                    ByVal srcFullKeyPath As String,
                                    ByVal srcValueName As String,
                                    ByVal dstView As RegistryView,
                                    ByVal dstFullKeyPath As String,
                                    ByVal dstValueName As String)

            RegistryUtil.CreateValue(view:=dstView,
                                 fullKeyPath:=dstFullKeyPath,
                                 valueName:=dstValueName,
                                 valueData:=RegistryUtil.GetValueData(view:=srcView, fullKeyPath:=srcFullKeyPath, valueName:=srcValueName),
                                 valueType:=RegistryValueKind.Unknown)

            RegistryUtil.DeleteValue(view:=srcView, fullKeyPath:=srcFullKeyPath, valueName:=srcValueName, throwOnMissingValue:=True)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Moves a registry value (with its data) to another subkey.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcRootKeyName">
        ''' The source registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="srcSubKeyPath">
        ''' The source registry subkey path.
        ''' </param>
        ''' 
        ''' <param name="srcValueName">
        ''' The source registry value name.
        ''' </param>
        ''' 
        ''' <param name="dstRootKeyName">
        ''' The target registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="dstSubKeyPath">
        ''' The target registry subkey path.
        ''' </param>
        ''' 
        ''' <param name="dstValueName">
        ''' The target registry value name.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub MoveValue(ByVal srcView As RegistryView,
                                    ByVal srcRootKeyName As String,
                                    ByVal srcSubKeyPath As String,
                                    ByVal srcValueName As String,
                                    ByVal dstView As RegistryView,
                                    ByVal dstRootKeyName As String,
                                    ByVal dstSubKeyPath As String,
                                    ByVal dstValueName As String)

            RegistryUtil.CreateValue(view:=dstView,
                                 rootKeyName:=dstRootKeyName,
                                 subKeyPath:=dstSubKeyPath,
                                 valueName:=dstValueName,
                                 valueData:=RegistryUtil.GetValueData(view:=srcView, rootKeyName:=srcRootKeyName, subKeyPath:=srcSubKeyPath, valueName:=srcValueName),
                                 valueType:=RegistryValueKind.Unknown)

            RegistryUtil.DeleteValue(view:=srcView, rootKeyName:=srcRootKeyName, subKeyPath:=srcSubKeyPath, valueName:=srcValueName, throwOnMissingValue:=True)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Copies a registry key tree to another registry path.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcRootKeyName">
        ''' The source registry rootkey name.
        ''' 
        ''' </param>
        ''' <param name="srcSubKeyPath">
        ''' The source registry subkey path.
        ''' </param>
        ''' 
        ''' <param name="dstRootKeyName">
        ''' The target registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="dstSubKeyPath">
        ''' The target registry subkey path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' srcRootKeyName or srcSubKeyPath or dstRootKeyName or dstSubKeyPath
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub CopyKeyTree(ByVal srcView As RegistryView,
                                      ByVal srcRootKeyName As String,
                                      ByVal srcSubKeyPath As String,
                                      ByVal dstView As RegistryView,
                                      ByVal dstRootKeyName As String,
                                      ByVal dstSubKeyPath As String)

            If String.IsNullOrEmpty(srcRootKeyName) OrElse String.IsNullOrWhiteSpace(srcRootKeyName) Then
                Throw New ArgumentNullException("srcRootKeyName")

            ElseIf String.IsNullOrEmpty(srcSubKeyPath) OrElse String.IsNullOrWhiteSpace(srcSubKeyPath) Then
                Throw New ArgumentNullException("srcSubKeyPath")

            ElseIf String.IsNullOrEmpty(dstRootKeyName) OrElse String.IsNullOrWhiteSpace(dstRootKeyName) Then
                Throw New ArgumentNullException("dstRootKeyName")

            ElseIf String.IsNullOrEmpty(dstSubKeyPath) OrElse String.IsNullOrWhiteSpace(dstSubKeyPath) Then
                Throw New ArgumentNullException("dstSubKeyPath")

            Else
                If RegistryUtil.ExistSubKey(srcView, srcRootKeyName, srcSubKeyPath) Then

                    Using srcRegistryKey As RegistryKey = RegistryUtil.GetRootKey(srcRootKeyName, srcView).OpenSubKey(RegistryUtil.GetSubKeyPath(srcSubKeyPath), writable:=False)

                        RegistryUtil.CreateSubKey(view:=dstView, rootKeyName:=RegistryUtil.GetRootKeyName(dstRootKeyName), subKeyPath:=RegistryUtil.GetSubKeyPath(dstSubKeyPath))

                        Using dstRegistryKey As RegistryKey = RegistryUtil.GetRootKey(dstRootKeyName, dstView).OpenSubKey(RegistryUtil.GetSubKeyPath(dstSubKeyPath), writable:=True)
                            RegistryUtil.CopySubKeys(srcRegistryKey, dstRegistryKey)
                        End Using

                    End Using
                Else
                    Throw New InvalidOperationException("The source subkey doesn't exists.")
                End If

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Copies a registry key tree to another registry path.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcFullKeyPath">
        ''' The source registry key full path.
        ''' </param>
        ''' 
        ''' <param name="dstFullKeyPath">
        ''' The target registry key full path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub CopyKeyTree(ByVal srcView As RegistryView,
                                      ByVal srcFullKeyPath As String,
                                      ByVal dstView As RegistryView,
                                      ByVal dstFullKeyPath As String)

            RegistryUtil.CopyKeyTree(srcView:=srcView,
                                 srcRootKeyName:=RegistryUtil.GetRootKeyName(srcFullKeyPath),
                                 srcSubKeyPath:=RegistryUtil.GetSubKeyPath(srcFullKeyPath),
                                 dstView:=dstView,
                                 dstRootKeyName:=RegistryUtil.GetRootKeyName(dstFullKeyPath),
                                 dstSubKeyPath:=RegistryUtil.GetSubKeyPath(dstFullKeyPath))

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Copies the sub-keys of the specified registry key.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcRegistryKey">
        ''' The old key.
        ''' </param>
        ''' 
        ''' <param name="dstRegistryKey">
        ''' The new key.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub CopySubKeys(ByVal srcRegistryKey As RegistryKey,
                                       ByVal dstRegistryKey As RegistryKey)

            ' Copy Values.
            For Each valueName As String In srcRegistryKey.GetValueNames()
                dstRegistryKey.SetValue(valueName, srcRegistryKey.GetValue(valueName))
            Next valueName

            ' Copy Subkeys.
            For Each subKeyName As String In srcRegistryKey.GetSubKeyNames

                RegistryUtil.CreateSubKey(view:=dstRegistryKey.View, fullKeyPath:=String.Format("{0}\{1}", dstRegistryKey.Name, subKeyName))
                RegistryUtil.CopySubKeys(srcRegistryKey.OpenSubKey(subKeyName, writable:=False),
                                     dstRegistryKey.OpenSubKey(subKeyName, writable:=True))

            Next subKeyName

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Copies the sub-keys of the specified registry key.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcRootKeyName">
        ''' The source registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="srcSubKeyPath">
        ''' The source registry subkey path.
        ''' </param>
        ''' 
        ''' <param name="dstRootKeyName">
        ''' The target registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="dstSubKeyPath">
        ''' The target registry subkey path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub CopySubKeys(ByVal srcView As RegistryView,
                                      ByVal srcRootKeyName As String,
                                      ByVal srcSubKeyPath As String,
                                      ByVal dstView As RegistryView,
                                      ByVal dstRootKeyName As String,
                                      ByVal dstSubKeyPath As String)

            Dim srcRegistryKey As RegistryKey = Nothing
            Dim dstRegistryKey As RegistryKey = Nothing

            Try
                srcRegistryKey = RegistryUtil.GetRootKey(srcRootKeyName, srcView).OpenSubKey(RegistryUtil.GetSubKeyPath(srcSubKeyPath), writable:=False)
                dstRegistryKey = RegistryUtil.GetRootKey(dstRootKeyName, dstView).OpenSubKey(RegistryUtil.GetSubKeyPath(dstSubKeyPath), writable:=True)

                RegistryUtil.CopySubKeys(srcRegistryKey, dstRegistryKey)

            Catch ex As Exception
                Throw

            Finally
                If srcRegistryKey IsNot Nothing Then
                    srcRegistryKey.Close()
                End If
                If dstRegistryKey IsNot Nothing Then
                    dstRegistryKey.Close()
                End If

            End Try

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Copies the sub-keys of the specified registry key.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcFullKeyPath">
        ''' The source registry key full path.
        ''' </param>
        ''' 
        ''' <param name="dstFullKeyPath">
        ''' The target registry key full path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub CopySubKeys(ByVal srcView As RegistryView,
                                      ByVal srcFullKeyPath As String,
                                      ByVal dstView As RegistryView,
                                      ByVal dstFullKeyPath As String)

            RegistryUtil.CopySubKeys(srcView:=srcView,
                                 srcRootKeyName:=RegistryUtil.GetRootKeyName(srcFullKeyPath),
                                 srcSubKeyPath:=RegistryUtil.GetSubKeyPath(srcFullKeyPath),
                                 dstView:=dstView,
                                 dstRootKeyName:=RegistryUtil.GetRootKeyName(dstFullKeyPath),
                                 dstSubKeyPath:=RegistryUtil.GetSubKeyPath(dstFullKeyPath))

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Moves a registry key tree to another registry path.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcRootKeyName">
        ''' The source registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="srcSubKeyPath">
        ''' The source registry subkey path.
        ''' </param>
        ''' 
        ''' <param name="dstRootKeyName">
        ''' The target registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="dstSubKeyPath">
        ''' The target registry subkey path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' srcRootKeyName or srcSubKeyPath or dstRootKeyName or dstSubKeyPath
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub MoveKeyTree(ByVal srcView As RegistryView,
                                      ByVal srcRootKeyName As String,
                                      ByVal srcSubKeyPath As String,
                                      ByVal dstView As RegistryView,
                                      ByVal dstRootKeyName As String,
                                      ByVal dstSubKeyPath As String)

            If String.IsNullOrEmpty(srcRootKeyName) OrElse String.IsNullOrWhiteSpace(srcRootKeyName) Then
                Throw New ArgumentNullException("srcRootKeyName")

            ElseIf String.IsNullOrEmpty(srcSubKeyPath) OrElse String.IsNullOrWhiteSpace(srcSubKeyPath) Then
                Throw New ArgumentNullException("srcSubKeyPath")

            ElseIf String.IsNullOrEmpty(dstRootKeyName) OrElse String.IsNullOrWhiteSpace(dstRootKeyName) Then
                Throw New ArgumentNullException("dstRootKeyName")

            ElseIf String.IsNullOrEmpty(dstSubKeyPath) OrElse String.IsNullOrWhiteSpace(dstSubKeyPath) Then
                Throw New ArgumentNullException("dstSubKeyPath")

            Else
                If RegistryUtil.ExistSubKey(srcView, srcRootKeyName, srcSubKeyPath) Then

                    Using srcRegistryKey As RegistryKey = RegistryUtil.GetRootKey(srcRootKeyName, srcView).OpenSubKey(RegistryUtil.GetSubKeyPath(srcSubKeyPath), writable:=False)

                        RegistryUtil.CreateSubKey(srcView, rootKeyName:=RegistryUtil.GetRootKeyName(srcRootKeyName), subKeyPath:=RegistryUtil.GetSubKeyPath(srcSubKeyPath))

                        Using dstRegistryKey As RegistryKey = RegistryUtil.GetRootKey(dstRootKeyName, dstView).OpenSubKey(RegistryUtil.GetSubKeyPath(dstSubKeyPath), writable:=True)
                            RegistryUtil.CopySubKeys(srcRegistryKey, dstRegistryKey)
                        End Using

                    End Using

                    RegistryUtil.DeleteSubKey(srcView, RegistryUtil.GetRootKeyName(srcRootKeyName), RegistryUtil.GetSubKeyPath(srcSubKeyPath))

                Else

                    Throw New InvalidOperationException("The source registry subkey doesn't exists.")
                End If
            End If
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Moves a registry key tree to another registry path.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcFullKeyPath">
        ''' The source registry key full path.
        ''' </param>
        ''' 
        ''' <param name="dstFullKeyPath">
        ''' The target registry key full path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub MoveKeyTree(ByVal srcView As RegistryView,
                                      ByVal srcFullKeyPath As String,
                                      ByVal dstView As RegistryView,
                                      ByVal dstFullKeyPath As String)

            RegistryUtil.MoveKeyTree(srcView:=srcView,
                                 srcRootKeyName:=RegistryUtil.GetRootKeyName(srcFullKeyPath),
                                 srcSubKeyPath:=RegistryUtil.GetSubKeyPath(srcFullKeyPath),
                                 dstView:=dstView,
                                 dstRootKeyName:=RegistryUtil.GetRootKeyName(dstFullKeyPath),
                                 dstSubKeyPath:=RegistryUtil.GetSubKeyPath(dstFullKeyPath))

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Moves the sub-keys of the specified registry key.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcRootKeyName">
        ''' The source registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="srcSubKeyPath">
        ''' The source registry subkey path.
        ''' </param>
        ''' 
        ''' <param name="dstRootKeyName">
        ''' The target registry rootkey name.
        ''' </param>
        ''' 
        ''' <param name="dstSubKeyPath">
        ''' The target registry subkey path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub MoveSubKeys(ByVal srcView As RegistryView,
                                      ByVal srcRootKeyName As String,
                                      ByVal srcSubKeyPath As String,
                                      ByVal dstView As RegistryView,
                                      ByVal dstRootKeyName As String,
                                      ByVal dstSubKeyPath As String)

            Dim srcRegistryKey As RegistryKey = Nothing
            Dim dstRegistryKey As RegistryKey = Nothing

            Try
                RegistryUtil.CreateSubKey(dstView, RegistryUtil.GetRootKeyName(dstRootKeyName), RegistryUtil.GetSubKeyPath(dstSubKeyPath))

                srcRegistryKey = RegistryUtil.GetRootKey(srcRootKeyName, srcView).OpenSubKey(RegistryUtil.GetSubKeyPath(srcSubKeyPath), writable:=False)
                dstRegistryKey = RegistryUtil.GetRootKey(dstRootKeyName, dstView).OpenSubKey(RegistryUtil.GetSubKeyPath(dstSubKeyPath), writable:=True)

                RegistryUtil.CopySubKeys(srcRegistryKey, dstRegistryKey)
                RegistryUtil.DeleteSubKey(srcView, RegistryUtil.GetRootKeyName(srcRootKeyName), RegistryUtil.GetSubKeyPath(srcSubKeyPath))

            Catch ex As Exception
                Throw

            Finally
                If srcRegistryKey IsNot Nothing Then
                    srcRegistryKey.Close()
                End If
                If dstRegistryKey IsNot Nothing Then
                    dstRegistryKey.Close()
                End If

            End Try

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Moves the sub-keys of the specified registry key.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="srcFullKeyPath">
        ''' The source registry key full path.
        ''' </param>
        ''' 
        ''' <param name="dstFullKeyPath">
        ''' The target registry key full path.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub MoveSubKeys(ByVal srcView As RegistryView,
                                      ByVal srcFullKeyPath As String,
                                      ByVal dstView As RegistryView,
                                      ByVal dstFullKeyPath As String)

            RegistryUtil.MoveSubKeys(srcView:=srcView,
                                 srcRootKeyName:=RegistryUtil.GetRootKeyName(srcFullKeyPath),
                                 srcSubKeyPath:=RegistryUtil.GetSubKeyPath(srcFullKeyPath),
                                 dstView:=dstView,
                                 dstRootKeyName:=RegistryUtil.GetRootKeyName(dstFullKeyPath),
                                 dstSubKeyPath:=RegistryUtil.GetSubKeyPath(dstFullKeyPath))

        End Sub

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
        Public Shared Function GetRootKey(ByVal registryPath As String, ByVal view As RegistryView) As RegistryKey

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
        Public Shared Function GetRootKeyName(ByVal registryPath As String) As String

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
        Public Shared Function GetSubKeyPath(ByVal registryPath As String) As String

            Select Case String.IsNullOrEmpty(RegistryUtil.GetRootKeyName(registryPath))

                Case True
                    Return registryPath.TrimStart("\"c).TrimEnd("\"c)

                Case Else
                    Return registryPath.Substring(registryPath.IndexOf("\"c)).TrimStart("\"c).TrimEnd("\"c)

            End Select

        End Function

#End Region

#Region " Find "

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Finds on a registry path all the subkey names that matches the specified criteria.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="rootKeyName">
        '''' The rootkey name.
        '''' </param>
        '''' 
        '''' <param name="subKeyPath">
        '''' The subkey path.
        '''' </param>
        '''' 
        '''' <param name="subKeyName">
        '''' The subkey name to find.
        '''' </param>
        '''' 
        '''' <param name="matchFullSubKeyName">
        '''' If set to <see langword="True"/>, matches all the subkey name, otherwise matches a part of the name.
        '''' </param>
        '''' 
        '''' <param name="ignoreCase">
        '''' If set to <see langword="True"/>, performs a non-sensitive stringcase comparison.
        '''' </param>
        '''' 
        '''' <param name="searchOption">
        '''' The search mode.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' <see cref="IEnumerable(Of RegInfo)"/>
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <exception cref="ArgumentNullException">
        '''' rootKeyName or subKeyPath or subKeyName
        '''' </exception>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function FindSubKey(ByVal view As RegistryView,
        '                                  ByVal rootKeyName As String,
        '                                  ByVal subKeyPath As String,
        '                                  ByVal subKeyName As String,
        '                                  ByVal matchFullSubKeyName As Boolean,
        '                                  ByVal ignoreCase As Boolean,
        '                                  ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

        '    If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
        '        Throw New ArgumentNullException("rootKeyName")

        '    ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
        '        Throw New ArgumentNullException("subKeyPath")

        '    ElseIf String.IsNullOrEmpty(subKeyName) OrElse String.IsNullOrWhiteSpace(subKeyName) Then
        '        Throw New ArgumentNullException("subKeyName")

        '    End If

        '    Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view).OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=False)

        '        If matchFullSubKeyName Then

        '            If ignoreCase Then
        '                Return (From registryPath As String In RegistryUtil.CollectSubKeys(reg, searchOption)
        '                        Where RegistryUtil.GetSubKeyPath(registryPath).
        '                                      Remove(0, subKeyPath.Length).
        '                                      ToLower.
        '                                      Split("\"c).
        '                                      Contains(subKeyName.ToLower())
        '                        Select New RegInfo With
        '                               {
        '                                   .rootKeyName = RegistryUtil.GetRootKeyName(registryPath),
        '                                   .subKeyPath = RegistryUtil.GetSubKeyPath(registryPath)
        '                               }).ToArray

        '            Else
        '                Return (From registryPath As String In RegistryUtil.CollectSubKeys(reg, searchOption)
        '                        Where RegistryUtil.GetSubKeyPath(registryPath).
        '                                      Remove(0, subKeyPath.Length).
        '                                      Split("\"c).
        '                                      Contains(subKeyName)
        '                        Select New RegInfo With
        '                               {
        '                                   .rootKeyName = RegistryUtil.GetRootKeyName(registryPath),
        '                                   .subKeyPath = RegistryUtil.GetSubKeyPath(registryPath)
        '                               }).ToArray

        '            End If ' ignoreCase

        '        Else ' not matchFullSubKeyName
        '            If ignoreCase Then
        '                Return (From registryPath As String In RegistryUtil.CollectSubKeys(reg, searchOption)
        '                        Where RegistryUtil.GetSubKeyPath(registryPath).
        '                                      Remove(0, subKeyPath.Length).
        '                                      ToLower.
        '                                      Contains(subKeyName.ToLower())
        '                        Select New RegInfo With
        '                               {
        '                                   .rootKeyName = RegistryUtil.GetRootKeyName(registryPath),
        '                                   .subKeyPath = RegistryUtil.GetSubKeyPath(registryPath)
        '                               }).ToArray

        '            Else
        '                Return (From registryPath As String In RegistryUtil.CollectSubKeys(reg, searchOption)
        '                        Where RegistryUtil.GetSubKeyPath(registryPath).
        '                                      Remove(0, subKeyPath.Length).
        '                                      Contains(subKeyName)
        '                        Select New RegInfo With
        '                               {
        '                                   .rootKeyName = RegistryUtil.GetRootKeyName(registryPath),
        '                                   .subKeyPath = RegistryUtil.GetSubKeyPath(registryPath)
        '                               }).ToArray

        '            End If ' ignoreCase

        '        End If ' matchFullSubKeyName

        '    End Using

        'End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Finds on a registry path all the subkey names that matches the specified criteria.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="fullKeyPath">
        '''' The registry key full path.
        '''' </param>
        '''' 
        '''' <param name="subKeyName">
        '''' The subkey name to find.
        '''' </param>
        '''' 
        '''' <param name="matchFullSubKeyName">
        '''' If set to <see langword="True"/>, matches all the subkey name, otherwise matches a part of the name.
        '''' </param>
        '''' 
        '''' <param name="ignoreCase">
        '''' If set to <see langword="True"/>, performs a non-sensitive stringcase comparison.
        '''' </param>
        '''' 
        '''' <param name="searchOption">
        '''' The search mode.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' <see cref="IEnumerable(Of RegInfo)"/>
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function FindSubKey(ByVal view As RegistryView,
        '                                  ByVal fullKeyPath As String,
        '                                  ByVal subKeyName As String,
        '                                  ByVal matchFullSubKeyName As Boolean,
        '                                  ByVal ignoreCase As Boolean,
        '                                  ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

        '    Return RegistryUtil.FindSubKey(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), subKeyName, matchFullSubKeyName, ignoreCase, searchOption)

        'End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Finds on a registry path all the value names that matches the specified criteria.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="rootKeyName">
        '''' The rootkey name.
        '''' </param>
        '''' 
        '''' <param name="subKeyPath">
        '''' The subkey path.
        '''' </param>
        '''' 
        '''' <param name="valueName">
        '''' The value name to find.
        '''' </param>
        '''' 
        '''' <param name="matchFullValueName">
        '''' If set to <see langword="True"/>, matches all the value name, otherwise matches a part of the name.
        '''' </param>
        '''' 
        '''' <param name="ignoreCase">
        '''' If set to <see langword="True"/>, performs a non-sensitive stringcase comparison.
        '''' </param>
        '''' 
        '''' <param name="searchOption">
        '''' The search mode.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' <see cref="IEnumerable(Of RegInfo)"/>
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <exception cref="ArgumentNullException">
        '''' rootKeyName or subKeyPath or subKeyName
        '''' </exception>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function FindValue(ByVal view As RegistryView,
        '                                 ByVal rootKeyName As String,
        '                                 ByVal subKeyPath As String,
        '                                 ByVal valueName As String,
        '                                 ByVal matchFullValueName As Boolean,
        '                                 ByVal ignoreCase As Boolean,
        '                                 ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

        '    If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
        '        Throw New ArgumentNullException("rootKeyName")

        '    ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
        '        Throw New ArgumentNullException("subKeyPath")

        '    ElseIf String.IsNullOrEmpty(valueName) OrElse String.IsNullOrWhiteSpace(valueName) Then
        '        Throw New ArgumentNullException("valueName")

        '    End If

        '    Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view).OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=False)

        '        If matchFullValueName Then

        '            If ignoreCase Then
        '                Return (From regInfo As RegInfo In RegistryUtil.CollectValues(reg, searchOption)
        '                        Where regInfo.ValueName.ToLower.Equals(valueName.ToLower())).ToArray

        '            Else
        '                Return (From regInfo As RegInfo In RegistryUtil.CollectValues(reg, searchOption)
        '                        Where regInfo.ValueName.Equals(valueName)).ToArray

        '            End If ' ignoreCase

        '        Else ' not matchFullValueName
        '            If ignoreCase Then
        '                Return (From regInfo As RegInfo In RegistryUtil.CollectValues(reg, searchOption)
        '                        Where regInfo.ValueName.ToLower.Contains(valueName.ToLower())).ToArray

        '            Else
        '                Return (From regInfo As RegInfo In RegistryUtil.CollectValues(reg, searchOption)
        '                        Where regInfo.ValueName.Contains(valueName)).ToArray

        '            End If ' ignoreCase

        '        End If ' matchFullValueName

        '    End Using

        'End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Finds on a registry path all the value names that matches the specified criteria.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="fullKeyPath">
        '''' The registry key full path.
        '''' </param>
        '''' 
        '''' <param name="valueName">
        '''' The value name to find.
        '''' </param>
        '''' 
        '''' <param name="matchFullValueName">
        '''' If set to <see langword="True"/>, matches all the value name, otherwise matches a part of the name.
        '''' </param>
        '''' 
        '''' <param name="ignoreCase">
        '''' If set to <see langword="True"/>, performs a non-sensitive stringcase comparison.
        '''' </param>
        '''' 
        '''' <param name="searchOption">
        '''' The search mode.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' <see cref="IEnumerable(Of RegInfo)"/>
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function FindValue(ByVal view As RegistryView,
        '                                 ByVal fullKeyPath As String,
        '                                 ByVal valueName As String,
        '                                 ByVal matchFullValueName As Boolean,
        '                                 ByVal ignoreCase As Boolean,
        '                                 ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

        '    Return RegistryUtil.FindValue(view, RegistryUtil.GetRootKeyName(fullKeyPath), RegistryUtil.GetSubKeyPath(fullKeyPath), valueName, matchFullValueName, ignoreCase, searchOption)

        'End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Finds on a registry path all the values that contains data that matches the specified criteria.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="rootKeyName">
        '''' The rootkey name.
        '''' </param>
        '''' 
        '''' <param name="subKeyPath">
        '''' The subkey path.
        '''' </param>
        '''' 
        '''' <param name="valueData">
        '''' The data to find.
        '''' </param>
        '''' 
        '''' <param name="matchFullData">
        '''' If set to <see langword="True"/>, matches all the data, otherwise matches a part of the data.
        '''' </param>
        '''' 
        '''' <param name="ignoreCase">
        '''' If set to <see langword="True"/>, performs a non-sensitive stringcase comparison (for String data).
        '''' </param>
        '''' 
        '''' <param name="searchOption">
        '''' The search mode.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' <see cref="IEnumerable(Of RegInfo)"/>
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <exception cref="ArgumentNullException">
        '''' rootKeyName or subKeyPath or subKeyName
        '''' </exception>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Public Shared Function FindValueData(ByVal view As RegistryView,
        '                                     ByVal rootKeyName As String,
        '                                     ByVal subKeyPath As String,
        '                                     ByVal valueData As String,
        '                                     ByVal matchFullData As Boolean,
        '                                     ByVal ignoreCase As Boolean,
        '                                     ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

        '    If String.IsNullOrEmpty(rootKeyName) OrElse String.IsNullOrWhiteSpace(rootKeyName) Then
        '        Throw New ArgumentNullException("rootKeyName")

        '    ElseIf String.IsNullOrEmpty(subKeyPath) OrElse String.IsNullOrWhiteSpace(subKeyPath) Then
        '        Throw New ArgumentNullException("subKeyPath")

        '    ElseIf String.IsNullOrEmpty(valueData) OrElse String.IsNullOrWhiteSpace(valueData) Then
        '        Throw New ArgumentNullException("valueData")

        '    End If

        '    Using reg As RegistryKey = RegistryUtil.GetRootKey(rootKeyName, view).OpenSubKey(RegistryUtil.GetSubKeyPath(subKeyPath), writable:=False)

        '        If matchFullData Then

        '            If ignoreCase Then
        '                Return (From regInfo As RegInfo In RegistryUtil.CollectValueDatas(reg, searchOption).ToArray
        '                        Where Not String.IsNullOrEmpty(regInfo.ValueData.ToString()) AndAlso
        '                              regInfo.ValueData.ToString().ToLower.Equals(valueData.ToLower()))

        '            Else
        '                Return (From regInfo As RegInfo In RegistryUtil.CollectValueDatas(reg, searchOption).ToArray
        '                        Where Not String.IsNullOrEmpty(regInfo.ValueData.ToString()) AndAlso
        '                              regInfo.ValueData.ToString().Equals(valueData))

        '            End If ' ignoreCase

        '        Else ' not matchFullData
        '            If ignoreCase Then
        '                Return (From regInfo As RegInfo In RegistryUtil.CollectValueDatas(reg, searchOption).ToArray
        '                        Where Not String.IsNullOrEmpty(regInfo.ValueData.ToString()) AndAlso
        '                              regInfo.ValueData.ToString().ToLower.Contains(valueData.ToLower()))

        '            Else
        '                Return (From regInfo As RegInfo In RegistryUtil.CollectValueDatas(reg, searchOption).ToArray
        '                        Where Not String.IsNullOrEmpty(regInfo.ValueData.ToString()) AndAlso
        '                              regInfo.ValueData.ToString().Contains(valueData))

        '            End If ' ignoreCase

        '        End If ' matchFullData

        '    End Using

        'End Function

#End Region

#Region " Hives "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Loads the subkeys of a hive file, 
        ''' creating a subkey with the specified name into <c>HKEY_LOCAL_MACHINE</c> root key.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code>
        ''' LoadHive("C:\WinMount\Users\Administrador\NTUSER.DAT", "My External Branch")
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filepath">
        ''' The hive filepath.
        ''' </param>
        ''' 
        ''' <param name="subkeyName">
        ''' The name to give to the loaded subkey.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub LoadHive(ByVal filepath As String, ByVal subkeyName As String)

            'ProcessUtil.SetCurrentProcessPrivileges(ProcessPrivileges.RestorePrivilege, PrivilegeStates.PrivilegeEnabled)

            'Dim hr As Integer = NativeMethods.RegLoadKey(RegistryRootKeyId.HKEY_LOCAL_MACHINE, subkeyName, filepath)

            'If (hr <> HResult.S_OK) Then
            '    Marshal.ThrowExceptionForHR(hr)
            'End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Unloads a previouslly hive file that has been loaded into <c>HKEY_LOCAL_MACHINE</c> root key.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <example> This is a code example.
        ''' <code>
        ''' UnloadHive("HKEY_LOCAL_MACHINE\My External Branch")
        ''' or:
        ''' UnloadHive("My External Branch")
        ''' </code>
        ''' </example>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="subkeyName">
        ''' The name of the subkey to unload.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub UnloadHive(ByVal subkeyName As String)

            'ProcessUtil.SetCurrentProcessPrivileges(ProcessPrivileges.RestorePrivilege, PrivilegeStates.PrivilegeEnabled)

            'If subkeyName.StartsWith("HKEY_LOCAL_MACHINE\", StringComparison.OrdinalIgnoreCase) Then
            '    subkeyName = subkeyName.Substring("HKEY_LOCAL_MACHINE\".Length)
            'End If

            'Dim hr As Integer = NativeMethods.RegUnLoadKey(RegistryRootKeyId.HKEY_LOCAL_MACHINE, subkeyName)

            'If (hr <> HResult.S_OK) Then
            '    Marshal.ThrowExceptionForHR(hr)
            'End If

        End Sub

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
        Private Shared Iterator Function CollectSubKeys(ByVal srcRegistryKey As RegistryKey,
                                                        ByVal searchOption As SearchOption) As IEnumerable(Of String)

            For Each subKeyName As String In srcRegistryKey.GetSubKeyNames

                Yield String.Format("{0}\{1}", srcRegistryKey.Name, subKeyName)

                If searchOption = SearchOption.AllDirectories Then

                    For Each registryPath As String In RegistryUtil.CollectSubKeys(srcRegistryKey.OpenSubKey(subKeyName, writable:=False), SearchOption.AllDirectories)
                        Yield registryPath
                    Next registryPath

                End If

            Next subKeyName

        End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Collects the values on the specified registry path.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="srcRegistryKey">
        '''' The source registry key.
        '''' </param>
        '''' 
        '''' <param name="searchOption">
        '''' The search mode.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' <see cref="IEnumerable(Of RegInfo)"/>
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Private Shared Iterator Function CollectValues(ByVal srcRegistryKey As RegistryKey,
        '                                               ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

        '    Select Case searchOption

        '        Case SearchOption.TopDirectoryOnly
        '            For Each valueName As String In srcRegistryKey.GetValueNames

        '                Yield New RegInfo With
        '                    {
        '                        .View = srcRegistryKey.View,
        '                        .RootKeyName = RegistryUtil.GetRootKeyName(srcRegistryKey.Name),
        '                        .SubKeyPath = RegistryUtil.GetSubKeyPath(srcRegistryKey.Name),
        '                        .valueName = valueName
        '                    }

        '            Next valueName

        '        Case SearchOption.AllDirectories
        '            For Each registryPath As String In RegistryUtil.CollectSubKeys(srcRegistryKey, searchOption)

        '                Dim reg As RegistryKey = RegistryUtil.GetRootKey(registryPath, srcRegistryKey.View).OpenSubKey(RegistryUtil.GetSubKeyPath(registryPath), writable:=False)

        '                For Each valueName As String In reg.GetValueNames

        '                    Yield New RegInfo With
        '                        {
        '                            .View = reg.View,
        '                            .RootKeyName = RegistryUtil.GetRootKeyName(reg.Name),
        '                            .SubKeyPath = RegistryUtil.GetSubKeyPath(reg.Name),
        '                            .valueName = valueName
        '                        }

        '                Next valueName

        '                reg.Close()

        '            Next registryPath

        '    End Select

        'End Function

        '''' ----------------------------------------------------------------------------------------------------
        '''' <summary>
        '''' Collects the value datas on the specified registry path.
        '''' </summary>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <param name="srcRegistryKey">
        '''' The source registry key.
        '''' </param>
        '''' 
        '''' <param name="searchOption">The search mode.
        '''' </param>
        '''' ----------------------------------------------------------------------------------------------------
        '''' <returns>
        '''' <see cref="IEnumerable(Of RegInfo)"/>
        '''' </returns>
        '''' ----------------------------------------------------------------------------------------------------
        '<DebuggerStepThrough>
        'Private Shared Iterator Function CollectValueDatas(ByVal srcRegistryKey As RegistryKey,
        '                                                   ByVal searchOption As SearchOption) As IEnumerable(Of RegInfo)

        '    Select Case searchOption

        '        Case SearchOption.TopDirectoryOnly
        '            For Each valueName As String In srcRegistryKey.GetValueNames

        '                Yield New RegInfo With
        '                    {
        '                        .View = srcRegistryKey.View,
        '                        .RootKeyName = RegistryUtil.GetRootKeyName(srcRegistryKey.Name),
        '                        .SubKeyPath = RegistryUtil.GetSubKeyPath(srcRegistryKey.Name),
        '                        .valueName = valueName,
        '                        .ValueData = RegistryUtil.GetValueData(view:= .View,
        '                                                           rootKeyName:= .RootKeyName,
        '                                                           subKeyPath:= .SubKeyPath,
        '                                                           valueName:= .ValueName,
        '                                                           registryValueOptions:=RegistryValueOptions.None)
        '                    }

        '            Next valueName

        '        Case SearchOption.AllDirectories
        '            For Each registryPath As String In RegistryUtil.CollectSubKeys(srcRegistryKey, searchOption)

        '                Dim reg As RegistryKey = RegistryUtil.GetRootKey(registryPath, srcRegistryKey.View).OpenSubKey(RegistryUtil.GetSubKeyPath(registryPath), writable:=False)

        '                For Each valueName As String In reg.GetValueNames

        '                    Yield New RegInfo With
        '                        {
        '                            .View = reg.View,
        '                            .RootKeyName = RegistryUtil.GetRootKeyName(reg.Name),
        '                            .SubKeyPath = RegistryUtil.GetSubKeyPath(reg.Name),
        '                            .valueName = valueName,
        '                            .ValueData = RegistryUtil.GetValueData(view:= .View,
        '                                                               rootKeyName:= .RootKeyName,
        '                                                               subKeyPath:= .SubKeyPath,
        '                                                               valueName:= .ValueName,
        '                                                               registryValueOptions:=RegistryValueOptions.None)
        '                        }
        '                Next valueName

        '                reg.Close()

        '            Next registryPath

        '    End Select

        'End Function

#End Region

#End Region

    End Class

End Namespace

#End Region
