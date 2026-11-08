' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="StartupRegistryKeys.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " StartupRegistryKeys "

Namespace Core

    ''' <summary>
    ''' Contains the registry keys used to register Windows startup entries.
    ''' </summary>
    Public NotInheritable Class StartupRegistryKeys

#Region " Constants "

        ''' <summary>
        ''' The identifier of the current user startup type (as stored in the <c>Tag</c> of its radio button).
        ''' </summary>
        Public Const CurrentUserId As String = "CurrentUserKey"

        ''' <summary>
        ''' The identifier of the all users startup type (as stored in the <c>Tag</c> of its radio button).
        ''' </summary>
        Public Const AllUsersId As String = "AllUsersKey"

        ''' <summary>
        ''' The full path of the registry key that contains the startup entries of the current user.
        ''' </summary>
        Public Const CurrentUserRunKeyPath As String = "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"

        ''' <summary>
        ''' The full path of the registry key that contains the startup entries of all users.
        ''' </summary>
        Public Const AllUsersRunKeyPath As String = "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"

        ''' <summary>
        ''' The display name of the current user startup type, as shown in the startup list.
        ''' </summary>
        Public Const CurrentUserDisplayName As String = "Current User"

        ''' <summary>
        ''' The display name of the all users startup type, as shown in the startup list.
        ''' </summary>
        Public Const AllUsersDisplayName As String = "All Users"

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="StartupRegistryKeys"/> class from being created.
        ''' </summary>
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Gets the full path of the startup registry key that corresponds to the specified startup type identifier.
        ''' </summary>
        ''' <param name="startupTypeId">
        ''' <see cref="StartupRegistryKeys.CurrentUserId"/> or <see cref="StartupRegistryKeys.AllUsersId"/>.
        ''' </param>
        ''' <returns>
        ''' The full registry key path.
        ''' </returns>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="startupTypeId"/> is not a known startup type identifier.
        ''' </exception>
        Public Shared Function GetKeyPathFromId(startupTypeId As String) As String

            Select Case startupTypeId
                Case StartupRegistryKeys.CurrentUserId
                    Return StartupRegistryKeys.CurrentUserRunKeyPath
                Case StartupRegistryKeys.AllUsersId
                    Return StartupRegistryKeys.AllUsersRunKeyPath
                Case Else
                    Throw New ArgumentException($"Unknown startup type identifier: '{startupTypeId}'.", NameOf(startupTypeId))
            End Select

        End Function

        ''' <summary>
        ''' Gets the full path of the startup registry key that corresponds to the specified startup type display name.
        ''' </summary>
        ''' <param name="displayName">
        ''' <see cref="StartupRegistryKeys.CurrentUserDisplayName"/> or <see cref="StartupRegistryKeys.AllUsersDisplayName"/>.
        ''' </param>
        ''' <returns>
        ''' The full registry key path.
        ''' </returns>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="displayName"/> is not a known startup type display name.
        ''' </exception>
        Public Shared Function GetKeyPathFromDisplayName(displayName As String) As String

            Select Case displayName
                Case StartupRegistryKeys.CurrentUserDisplayName
                    Return StartupRegistryKeys.CurrentUserRunKeyPath
                Case StartupRegistryKeys.AllUsersDisplayName
                    Return StartupRegistryKeys.AllUsersRunKeyPath
                Case Else
                    Throw New ArgumentException($"Unknown startup type display name: '{displayName}'.", NameOf(displayName))
            End Select

        End Function

        ''' <summary>
        ''' Gets the display name that corresponds to the specified startup type identifier.
        ''' </summary>
        ''' <param name="startupTypeId">
        ''' <see cref="StartupRegistryKeys.CurrentUserId"/> or <see cref="StartupRegistryKeys.AllUsersId"/>.
        ''' </param>
        ''' <returns>
        ''' The display name.
        ''' </returns>
        ''' <exception cref="ArgumentException">
        ''' <paramref name="startupTypeId"/> is not a known startup type identifier.
        ''' </exception>
        Public Shared Function GetDisplayNameFromId(startupTypeId As String) As String

            Select Case startupTypeId
                Case StartupRegistryKeys.CurrentUserId
                    Return StartupRegistryKeys.CurrentUserDisplayName
                Case StartupRegistryKeys.AllUsersId
                    Return StartupRegistryKeys.AllUsersDisplayName
                Case Else
                    Throw New ArgumentException($"Unknown startup type identifier: '{startupTypeId}'.", NameOf(startupTypeId))
            End Select

        End Function

#End Region

    End Class

End Namespace

#End Region
