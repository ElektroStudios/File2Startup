' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="RecentFileEntryTests.vb" company="Elektro Studios">
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

Imports Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " RecentFileEntryTests "

Namespace Core

    ''' <summary>
    ''' Tests for the <see cref="RecentFileEntry"/> class.
    ''' </summary>
    <TestClass>
    Public NotInheritable Class RecentFileEntryTests

#Region " Constants "

        ''' <summary>
        ''' The same delimiter used by the application (<c>@@{ProductName}@@</c>).
        ''' </summary>
        Private Const Delimiter As String = "@@File2Startup@@"

#End Region

#Region " Helpers "

        ''' <summary>
        ''' Creates a new <see cref="RecentFileEntry"/> (used by the lambdas of exception assertions).
        ''' </summary>
        ''' <param name="name">The name.</param>
        ''' <param name="filePath">The file path.</param>
        ''' <param name="arguments">The arguments.</param>
        ''' <returns>The new instance.</returns>
        Private Shared Function Create(name As String, filePath As String, arguments As String) As RecentFileEntry

            Return New RecentFileEntry(name, filePath, arguments)

        End Function

#End Region

#Region " Constructor "

        ''' <summary>The constructor stores the values as they are.</summary>
        <TestMethod>
        Public Sub New_StoresValues()

            Dim entry As New RecentFileEntry("My App", "C:\App\app.exe", "-x")

            Assert.AreEqual("My App", entry.Name)
            Assert.AreEqual("C:\App\app.exe", entry.FilePath)
            Assert.AreEqual("-x", entry.Arguments)

        End Sub

        ''' <summary>The constructor rejects null values.</summary>
        <TestMethod>
        Public Sub New_NullValues_ThrowArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileEntryTests.Create(Nothing, "C:\a.exe", ""))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileEntryTests.Create("a", Nothing, ""))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileEntryTests.Create("a", "C:\a.exe", Nothing))

        End Sub

#End Region

#Region " Serialize "

        ''' <summary>The serialized format is <c>Name{delimiter}FilePath{delimiter}Arguments</c>.</summary>
        <TestMethod>
        Public Sub Serialize_UsesDelimitedFormat()

            Dim entry As New RecentFileEntry("My App", "C:\App\app.exe", "-x")

            Assert.AreEqual("My App@@File2Startup@@C:\App\app.exe@@File2Startup@@-x", entry.Serialize(RecentFileEntryTests.Delimiter))

        End Sub

        ''' <summary>Empty arguments produce a trailing delimiter, as in v1.5.</summary>
        <TestMethod>
        Public Sub Serialize_EmptyArguments_EndsWithDelimiter()

            Dim entry As New RecentFileEntry("My App", "C:\App\app.exe", String.Empty)

            Assert.AreEqual("My App@@File2Startup@@C:\App\app.exe@@File2Startup@@", entry.Serialize(RecentFileEntryTests.Delimiter))

        End Sub

        ''' <summary>Serialization rejects a null or empty delimiter.</summary>
        <TestMethod>
        <DataRow(Nothing)>
        <DataRow("")>
        Public Sub Serialize_NullOrEmptyDelimiter_ThrowsArgumentNullException(delimiter As String)

            Dim entry As New RecentFileEntry("My App", "C:\App\app.exe", String.Empty)

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() entry.Serialize(delimiter))

        End Sub

        ''' <summary>Serialization fails if a field contains the delimiter, because it could not be deserialized back.</summary>
        <TestMethod>
        Public Sub Serialize_FieldContainsDelimiter_ThrowsInvalidOperationException()

            Dim entry As New RecentFileEntry($"My{RecentFileEntryTests.Delimiter}App", "C:\App\app.exe", String.Empty)

            Assert.ThrowsExactly(Of InvalidOperationException)(Sub() entry.Serialize(RecentFileEntryTests.Delimiter))

        End Sub

#End Region

#Region " Deserialize / TryDeserialize "

        ''' <summary>Deserialization gives back the serialized entry.</summary>
        <TestMethod>
        <DataRow("My App", "C:\App\app.exe", "-x")>
        <DataRow("My App", "C:\App\app.exe", "")>
        <DataRow("", "", "")>
        <DataRow("Name with @ char", "C:\Dir@\app.exe", """quoted arg""")>
        Public Sub Serialize_Deserialize_RoundTrip(name As String, filePath As String, arguments As String)

            Dim original As New RecentFileEntry(name, filePath, arguments)

            Dim deserialized As RecentFileEntry = RecentFileEntry.Deserialize(original.Serialize(RecentFileEntryTests.Delimiter), RecentFileEntryTests.Delimiter)

            Assert.AreEqual(name, deserialized.Name)
            Assert.AreEqual(filePath, deserialized.FilePath)
            Assert.AreEqual(arguments, deserialized.Arguments)

        End Sub

        ''' <summary>Entries stored in the user settings by v1.5 are still readable.</summary>
        <TestMethod>
        Public Sub Deserialize_LegacyFormat_IsCompatible()

            ' Same expression used by v1.5 to build the entries.
            Dim legacy As String = String.Format("{0}" & RecentFileEntryTests.Delimiter & "{1}" & RecentFileEntryTests.Delimiter & "{2}",
                                                 "Legacy", "C:\Legacy\app.exe", "/a /b")

            Dim entry As RecentFileEntry = RecentFileEntry.Deserialize(legacy, RecentFileEntryTests.Delimiter)

            Assert.AreEqual("Legacy", entry.Name)
            Assert.AreEqual("C:\Legacy\app.exe", entry.FilePath)
            Assert.AreEqual("/a /b", entry.Arguments)

        End Sub

        ''' <summary>A serialized entry without exactly three fields is malformed.</summary>
        <TestMethod>
        <DataRow("")>
        <DataRow("OnlyName")>
        <DataRow("Name@@File2Startup@@C:\a.exe")>
        <DataRow("Name@@File2Startup@@C:\a.exe@@File2Startup@@-x@@File2Startup@@extra")>
        Public Sub Deserialize_Malformed_ThrowsFormatException(serialized As String)

            Assert.ThrowsExactly(Of FormatException)(Sub() RecentFileEntry.Deserialize(serialized, RecentFileEntryTests.Delimiter))

        End Sub

        ''' <summary><see cref="RecentFileEntry.TryDeserialize(String, String, ByRef RecentFileEntry)"/> returns False for a malformed entry.</summary>
        <TestMethod>
        Public Sub TryDeserialize_Malformed_ReturnsFalseAndNothing()

            Dim result As RecentFileEntry = New RecentFileEntry("placeholder", "", "")

            Assert.IsFalse(RecentFileEntry.TryDeserialize("Name@@File2Startup@@C:\a.exe", RecentFileEntryTests.Delimiter, result))
            Assert.IsNull(result)

        End Sub

        ''' <summary>Deserialization rejects null arguments.</summary>
        <TestMethod>
        Public Sub TryDeserialize_NullArguments_ThrowArgumentNullException()

            Dim result As RecentFileEntry = Nothing
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileEntry.TryDeserialize(Nothing, RecentFileEntryTests.Delimiter, result))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileEntry.TryDeserialize("a", Nothing, result))
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() RecentFileEntry.TryDeserialize("a", String.Empty, result))

        End Sub

#End Region

#Region " Equality "

        ''' <summary>Equality is case-insensitive on every field.</summary>
        <TestMethod>
        Public Sub Equals_DifferentCase_AreEqual()

            Dim entry1 As New RecentFileEntry("My App", "C:\App\app.exe", "-X")
            Dim entry2 As New RecentFileEntry("MY APP", "c:\app\APP.EXE", "-x")

            Assert.IsTrue(entry1.Equals(entry2))
            Assert.IsTrue(entry1.Equals(DirectCast(entry2, Object)))
            Assert.AreEqual(entry1.GetHashCode(), entry2.GetHashCode())

        End Sub

        ''' <summary>Entries that differ in any field are not equal.</summary>
        <TestMethod>
        <DataRow("Other", "C:\App\app.exe", "-x")>
        <DataRow("My App", "C:\App\other.exe", "-x")>
        <DataRow("My App", "C:\App\app.exe", "-y")>
        <DataRow("My App", "C:\App\app.exe", "")>
        Public Sub Equals_DifferentField_AreNotEqual(name As String, filePath As String, arguments As String)

            Dim entry1 As New RecentFileEntry("My App", "C:\App\app.exe", "-x")
            Dim entry2 As New RecentFileEntry(name, filePath, arguments)

            Assert.IsFalse(entry1.Equals(entry2))

        End Sub

        ''' <summary>An entry is not equal to Nothing nor to an object of other type.</summary>
        <TestMethod>
        Public Sub Equals_NothingOrOtherType_IsFalse()

            Dim entry As New RecentFileEntry("My App", "C:\App\app.exe", "-x")

            Assert.IsFalse(entry.Equals(DirectCast(Nothing, RecentFileEntry)))
            Assert.IsFalse(entry.Equals(DirectCast(Nothing, Object)))
            Assert.IsFalse(entry.Equals("My App"))

        End Sub

        ''' <summary><see cref="RecentFileEntry.ToString()"/> returns the name.</summary>
        <TestMethod>
        Public Sub ToString_ReturnsName()

            Assert.AreEqual("My App", New RecentFileEntry("My App", "C:\App\app.exe", "-x").ToString())

        End Sub

#End Region

    End Class

End Namespace

#End Region
