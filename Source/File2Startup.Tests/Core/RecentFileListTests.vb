' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="RecentFileListTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.Linq

Imports Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " RecentFileListTests "

Namespace Core

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="RecentFileList"/> class.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    Public NotInheritable Class RecentFileListTests

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The same delimiter used by the application (<c>@@{ProductName}@@</c>).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Const Delimiter As String = "@@File2Startup@@"

#End Region

#Region " Helpers "

        ''' <summary>
        ''' Creates a recent file list with the specified number of entries, named from <c>Item0</c> to <c>Item{count - 1}</c>.
        ''' </summary>
        ''' <param name="count">The number of entries.</param>
        ''' <returns>The recent file list.</returns>
        Private Shared Function CreateList(count As Integer) As StringCollection

            Dim list As New StringCollection()
            For index As Integer = 0 To count - 1
                list.Add(New RecentFileEntry($"Item{index}", $"C:\Item{index}.exe", String.Empty).Serialize(RecentFileListTests.Delimiter))
            Next index
            Return list

        End Function

#End Region

#Region " MaxCount "

        ''' <summary>The application keeps up to 10 recent entries, as in previous versions.</summary>
        <TestMethod>
        Public Sub MaxCount_IsTen()

            Dim maxCount As Integer = RecentFileList.MaxCount

            Assert.AreEqual(10, maxCount)

        End Sub

#End Region

#Region " TrimToMaxCount "

        ''' <summary>Trimming removes the oldest entries (lowest indices) and keeps the newest ones.</summary>
        <TestMethod>
        Public Sub TrimToMaxCount_AboveMax_RemovesOldestEntries()

            Dim list As StringCollection = RecentFileListTests.CreateList(13)

            Dim removed As Boolean = RecentFileList.TrimToMaxCount(list, 10)

            Assert.IsTrue(removed)
            Assert.AreEqual(10, list.Count)
            Assert.IsTrue(list(0).StartsWith("Item3", StringComparison.Ordinal))
            Assert.IsTrue(list(9).StartsWith("Item12", StringComparison.Ordinal))

        End Sub

        ''' <summary>Trimming a list that does not exceed the maximum does nothing.</summary>
        <TestMethod>
        <DataRow(0)>
        <DataRow(5)>
        <DataRow(10)>
        Public Sub TrimToMaxCount_NotAboveMax_DoesNothing(count As Integer)

            Dim list As StringCollection = RecentFileListTests.CreateList(count)

            Dim removed As Boolean = RecentFileList.TrimToMaxCount(list, 10)

            Assert.IsFalse(removed)
            Assert.AreEqual(count, list.Count)

        End Sub

        ''' <summary>A maximum of zero clears the list.</summary>
        <TestMethod>
        Public Sub TrimToMaxCount_Zero_ClearsList()

            Dim list As StringCollection = RecentFileListTests.CreateList(3)

            Assert.IsTrue(RecentFileList.TrimToMaxCount(list, 0))
            Assert.AreEqual(0, list.Count)

        End Sub

        ''' <summary>Trimming rejects invalid arguments.</summary>
        <TestMethod>
        Public Sub TrimToMaxCount_InvalidArguments_Throw()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileList.TrimToMaxCount(Nothing, 10))
            Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() RecentFileList.TrimToMaxCount(New StringCollection(), -1))

        End Sub

#End Region

#Region " AddIfMissing "

        ''' <summary>A new entry is appended (as the newest one) in serialized form.</summary>
        <TestMethod>
        Public Sub AddIfMissing_NewEntry_IsAppended()

            Dim list As StringCollection = RecentFileListTests.CreateList(2)
            Dim entry As New RecentFileEntry("New", "C:\New.exe", "-x")

            Dim added As Boolean = RecentFileList.AddIfMissing(list, entry, RecentFileListTests.Delimiter)

            Assert.IsTrue(added)
            Assert.AreEqual(3, list.Count)
            Assert.AreEqual(entry.Serialize(RecentFileListTests.Delimiter), list(2))

        End Sub

        ''' <summary>An entry that already exists (case-insensitive) is not added again.</summary>
        <TestMethod>
        Public Sub AddIfMissing_ExistingEntryDifferentCase_IsNotAdded()

            Dim list As New StringCollection()
            RecentFileList.AddIfMissing(list, New RecentFileEntry("My App", "C:\App\app.exe", "-x"), RecentFileListTests.Delimiter)

            Dim added As Boolean = RecentFileList.AddIfMissing(list, New RecentFileEntry("MY APP", "c:\app\app.exe", "-X"), RecentFileListTests.Delimiter)

            Assert.IsFalse(added)
            Assert.AreEqual(1, list.Count)

        End Sub

        ''' <summary>The same name with different arguments is a different entry.</summary>
        <TestMethod>
        Public Sub AddIfMissing_SameNameDifferentArguments_IsAdded()

            Dim list As New StringCollection()
            RecentFileList.AddIfMissing(list, New RecentFileEntry("My App", "C:\App\app.exe", "-x"), RecentFileListTests.Delimiter)

            Assert.IsTrue(RecentFileList.AddIfMissing(list, New RecentFileEntry("My App", "C:\App\app.exe", "-y"), RecentFileListTests.Delimiter))
            Assert.AreEqual(2, list.Count)

        End Sub

        ''' <summary>Malformed stored entries do not prevent adding a new entry.</summary>
        <TestMethod>
        Public Sub AddIfMissing_WithMalformedEntries_AddsNewEntry()

            Dim list As New StringCollection From {"corrupted", String.Empty}

            Assert.IsTrue(RecentFileList.AddIfMissing(list, New RecentFileEntry("My App", "C:\App\app.exe", ""), RecentFileListTests.Delimiter))
            Assert.AreEqual(3, list.Count)

        End Sub

        ''' <summary>Adding rejects null arguments.</summary>
        <TestMethod>
        Public Sub AddIfMissing_NullArguments_Throw()

            Dim entry As New RecentFileEntry("My App", "C:\App\app.exe", "")
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileList.AddIfMissing(Nothing, entry, RecentFileListTests.Delimiter))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileList.AddIfMissing(New StringCollection(), Nothing, RecentFileListTests.Delimiter))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileList.AddIfMissing(New StringCollection(), entry, String.Empty))

        End Sub

#End Region

#Region " GetEntries "

        ''' <summary>The entries are returned in the stored order.</summary>
        <TestMethod>
        Public Sub GetEntries_ReturnsEntriesInStoredOrder()

            Dim list As StringCollection = RecentFileListTests.CreateList(3)

            Dim entries As List(Of RecentFileEntry) = RecentFileList.GetEntries(list, RecentFileListTests.Delimiter)

            CollectionAssert.AreEqual({"Item0", "Item1", "Item2"}, entries.Select(Function(entry As RecentFileEntry) entry.Name).ToArray())

        End Sub

        ''' <summary>
        ''' Regression test: malformed (corrupted) stored entries are skipped
        ''' (v1.5 crashed with an <see cref="IndexOutOfRangeException"/> when opening the recent files menu).
        ''' </summary>
        <TestMethod>
        Public Sub GetEntries_SkipsMalformedEntries()

            Dim list As StringCollection = RecentFileListTests.CreateList(2)
            list.Insert(1, "corrupted entry")
            list.Add(Nothing)

            Dim entries As List(Of RecentFileEntry) = RecentFileList.GetEntries(list, RecentFileListTests.Delimiter)

            CollectionAssert.AreEqual({"Item0", "Item1"}, entries.Select(Function(entry As RecentFileEntry) entry.Name).ToArray())

        End Sub

        ''' <summary>Getting entries rejects null arguments.</summary>
        <TestMethod>
        Public Sub GetEntries_NullArguments_Throw()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileList.GetEntries(Nothing, RecentFileListTests.Delimiter))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileList.GetEntries(New StringCollection(), Nothing))

        End Sub

#End Region

    End Class

End Namespace

#End Region
