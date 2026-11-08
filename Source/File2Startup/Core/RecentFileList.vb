' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="RecentFileList.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Collections.Generic
Imports System.Collections.Specialized

#End Region

#Region " RecentFileList "

Namespace Core

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Contains the operations performed on the recent file list (MRU)
    ''' that is persisted in the user settings as a <see cref="StringCollection"/> of serialized <see cref="RecentFileEntry"/>.
    ''' <para></para>
    ''' The oldest entry is at index zero; new entries are appended at the end.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class RecentFileList

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The maximum number of entries kept in the recent file list.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Const MaxCount As Integer = 10

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="RecentFileList"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes the oldest entries of the recent file list until it contains, at most, the specified number of entries.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="list">
        ''' The recent file list.
        ''' </param>
        '''
        ''' <param name="maxCount">
        ''' The maximum number of entries to keep.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if any entry was removed; otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="list"/> is <see langword="Nothing"/>.
        ''' </exception>
        '''
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' <paramref name="maxCount"/> is negative.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function TrimToMaxCount(list As StringCollection, maxCount As Integer) As Boolean

            If list Is Nothing Then
                Throw New ArgumentNullException(NameOf(list))
            End If

            If maxCount < 0 Then
                Throw New ArgumentOutOfRangeException(NameOf(maxCount), maxCount, "The maximum count cannot be negative.")
            End If

            Dim removedAny As Boolean = False
            Do While list.Count > maxCount
                list.RemoveAt(0)
                removedAny = True
            Loop

            Return removedAny

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Appends an entry to the recent file list, unless an equal entry (case-insensitive) already exists.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="list">
        ''' The recent file list.
        ''' </param>
        '''
        ''' <param name="entry">
        ''' The entry to add.
        ''' </param>
        '''
        ''' <param name="delimiter">
        ''' The field delimiter used to serialize the entries.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the entry was added; <see langword="False"/> if it already existed.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="list"/> or <paramref name="entry"/> is <see langword="Nothing"/>,
        ''' or <paramref name="delimiter"/> is <see langword="Nothing"/> or empty.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function AddIfMissing(list As StringCollection, entry As RecentFileEntry, delimiter As String) As Boolean

            If list Is Nothing Then
                Throw New ArgumentNullException(NameOf(list))
            End If

            If entry Is Nothing Then
                Throw New ArgumentNullException(NameOf(entry))
            End If

            If String.IsNullOrEmpty(delimiter) Then
                Throw New ArgumentNullException(NameOf(delimiter))
            End If

            For Each serialized As String In list
                Dim existing As RecentFileEntry = Nothing
                If RecentFileEntry.TryDeserialize(serialized, delimiter, existing) AndAlso existing.Equals(entry) Then
                    Return False
                End If
            Next serialized

            list.Add(entry.Serialize(delimiter))
            Return True

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the well-formed entries of the recent file list, in the same order as they are stored.
        ''' Malformed (corrupted) serialized entries are not returned.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="list">
        ''' The recent file list.
        ''' </param>
        '''
        ''' <param name="delimiter">
        ''' The field delimiter used to serialize the entries.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The deserialized entries.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="list"/> is <see langword="Nothing"/>,
        ''' or <paramref name="delimiter"/> is <see langword="Nothing"/> or empty.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function GetEntries(list As StringCollection, delimiter As String) As List(Of RecentFileEntry)

            If list Is Nothing Then
                Throw New ArgumentNullException(NameOf(list))
            End If

            If String.IsNullOrEmpty(delimiter) Then
                Throw New ArgumentNullException(NameOf(delimiter))
            End If

            Dim entries As New List(Of RecentFileEntry)(list.Count)
            For Each serialized As String In list
                Dim entry As RecentFileEntry = Nothing
                If serialized IsNot Nothing AndAlso RecentFileEntry.TryDeserialize(serialized, delimiter, entry) Then
                    entries.Add(entry)
                End If
            Next serialized

            Return entries

        End Function

#End Region

    End Class

End Namespace

#End Region
