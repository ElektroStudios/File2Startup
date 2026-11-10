' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="RecentFileEntry.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " RecentFileEntry "

Namespace Core

    ''' <summary>
    ''' Represents an item of the recent file list (MRU),
    ''' which is persisted in the user settings as a single delimited string: <c>Name{delimiter}FilePath{delimiter}Arguments</c>.
    ''' </summary>
    ''' <remarks>
    ''' This type is immutable. Equality is case-insensitive on all its fields.
    ''' </remarks>
    <DebuggerDisplay("{Name,nq}")>
    Public NotInheritable Class RecentFileEntry : Implements IEquatable(Of RecentFileEntry)

#Region " Constants "

        ''' <summary>
        ''' The number of fields of a serialized entry.
        ''' </summary>
        Private Const FieldCount As Integer = 3

#End Region

#Region " Properties "

        ''' <summary>
        ''' Gets the name of the startup entry.
        ''' </summary>
        Public ReadOnly Property Name As String

        ''' <summary>
        ''' Gets the file path of the startup entry.
        ''' </summary>
        Public ReadOnly Property FilePath As String

        ''' <summary>
        ''' Gets the arguments of the startup entry, or <see cref="String.Empty"/> for none.
        ''' </summary>
        Public ReadOnly Property Arguments As String

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="RecentFileEntry"/> class.
        ''' </summary>
        ''' <param name="name">
        ''' The name of the startup entry.
        ''' </param>
        '''
        ''' <param name="filePath">
        ''' The file path of the startup entry.
        ''' </param>
        '''
        ''' <param name="arguments">
        ''' The arguments of the startup entry, or <see cref="String.Empty"/> for none.
        ''' </param>
        ''' <exception cref="ArgumentNullException">
        ''' Any of the arguments is <see langword="Nothing"/>.
        ''' </exception>
        Public Sub New(name As String, filePath As String, arguments As String)

            If name Is Nothing Then
                Throw New ArgumentNullException(NameOf(name))
            End If

            If filePath Is Nothing Then
                Throw New ArgumentNullException(NameOf(filePath))
            End If

            If arguments Is Nothing Then
                Throw New ArgumentNullException(NameOf(arguments))
            End If

            Me.Name = name
            Me.FilePath = filePath
            Me.Arguments = arguments

        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Serializes this entry to a delimited string.
        ''' </summary>
        ''' <param name="delimiter">
        ''' The field delimiter.
        ''' </param>
        ''' <returns>
        ''' The serialized entry.
        ''' </returns>
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="delimiter"/> is <see langword="Nothing"/> or empty.
        ''' </exception>
        '''
        ''' <exception cref="InvalidOperationException">
        ''' A field of this entry contains the delimiter, so it could not be deserialized back.
        ''' </exception>
        Public Function Serialize(delimiter As String) As String

            If String.IsNullOrEmpty(delimiter) Then
                Throw New ArgumentNullException(NameOf(delimiter))
            End If

            For Each field As String In {Me.Name, Me.FilePath, Me.Arguments}
                If field.Contains(delimiter) Then
                    Throw New InvalidOperationException($"The field value '{field}' contains the delimiter '{delimiter}'.")
                End If
            Next field

            Return $"{Me.Name}{delimiter}{Me.FilePath}{delimiter}{Me.Arguments}"

        End Function

        ''' <summary>
        ''' Determines whether this entry is equal to another one (case-insensitive comparison of all fields).
        ''' </summary>
        ''' <param name="other">
        ''' The other entry.
        ''' </param>
        ''' <returns>
        ''' <see langword="True"/> if both entries are equal; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Overloads Function Equals(other As RecentFileEntry) As Boolean Implements IEquatable(Of RecentFileEntry).Equals

            If other Is Nothing Then
                Return False
            End If

            Return String.Equals(Me.Name, other.Name, StringComparison.OrdinalIgnoreCase) AndAlso
                   String.Equals(Me.FilePath, other.FilePath, StringComparison.OrdinalIgnoreCase) AndAlso
                   String.Equals(Me.Arguments, other.Arguments, StringComparison.OrdinalIgnoreCase)

        End Function

        ''' <summary>
        ''' Determines whether this entry is equal to the specified object.
        ''' </summary>
        ''' <param name="obj">
        ''' The object to compare.
        ''' </param>
        ''' <returns>
        ''' <see langword="True"/> if <paramref name="obj"/> is an equal <see cref="RecentFileEntry"/>; otherwise, <see langword="False"/>.
        ''' </returns>
        Public Overrides Function Equals(obj As Object) As Boolean

            Return Me.Equals(TryCast(obj, RecentFileEntry))

        End Function

        ''' <summary>
        ''' Returns a case-insensitive hash code for this entry.
        ''' </summary>
        ''' <returns>
        ''' The hash code.
        ''' </returns>
        Public Overrides Function GetHashCode() As Integer

            Dim comparer As StringComparer = StringComparer.OrdinalIgnoreCase
            Return System.HashCode.Combine(comparer.GetHashCode(Me.Name),
                                           comparer.GetHashCode(Me.FilePath),
                                           comparer.GetHashCode(Me.Arguments))

        End Function

        ''' <summary>
        ''' Returns the name of this entry.
        ''' </summary>
        ''' <returns>
        ''' The value of <see cref="RecentFileEntry.Name"/>.
        ''' </returns>
        Public Overrides Function ToString() As String

            Return Me.Name

        End Function

#End Region

#Region " Public Shared Methods "

        ''' <summary>
        ''' Deserializes an entry from a delimited string.
        ''' </summary>
        ''' <param name="serialized">
        ''' The serialized entry.
        ''' </param>
        '''
        ''' <param name="delimiter">
        ''' The field delimiter.
        ''' </param>
        ''' <returns>
        ''' The resulting <see cref="RecentFileEntry"/>.
        ''' </returns>
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="serialized"/> is <see langword="Nothing"/>, or <paramref name="delimiter"/> is <see langword="Nothing"/> or empty.
        ''' </exception>
        '''
        ''' <exception cref="FormatException">
        ''' <paramref name="serialized"/> does not contain exactly three fields.
        ''' </exception>
        Public Shared Function Deserialize(serialized As String, delimiter As String) As RecentFileEntry

            Dim result As RecentFileEntry = Nothing
            If Not RecentFileEntry.TryDeserialize(serialized, delimiter, result) Then
                Throw New FormatException($"The serialized recent file entry does not contain {RecentFileEntry.FieldCount} fields: {serialized}")
            End If

            Return result

        End Function

        ''' <summary>
        ''' Tries to deserialize an entry from a delimited string.
        ''' </summary>
        ''' <param name="serialized">
        ''' The serialized entry.
        ''' </param>
        '''
        ''' <param name="delimiter">
        ''' The field delimiter.
        ''' </param>
        '''
        ''' <param name="result">
        ''' When this method returns <see langword="True"/>, contains the resulting <see cref="RecentFileEntry"/>;
        ''' otherwise, <see langword="Nothing"/>.
        ''' </param>
        ''' <returns>
        ''' <see langword="True"/> if <paramref name="serialized"/> contains exactly three fields; otherwise, <see langword="False"/>.
        ''' </returns>
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="serialized"/> is <see langword="Nothing"/>, or <paramref name="delimiter"/> is <see langword="Nothing"/> or empty.
        ''' </exception>
        Public Shared Function TryDeserialize(serialized As String, delimiter As String, ByRef result As RecentFileEntry) As Boolean

            If serialized Is Nothing Then
                Throw New ArgumentNullException(NameOf(serialized))
            End If

            If String.IsNullOrEmpty(delimiter) Then
                Throw New ArgumentNullException(NameOf(delimiter))
            End If

            Dim fields As String() = serialized.Split({delimiter}, StringSplitOptions.None)
            If fields.Length <> RecentFileEntry.FieldCount Then
                result = Nothing
                Return False
            End If

            result = New RecentFileEntry(fields(0), fields(1), fields(2))
            Return True

        End Function

#End Region

    End Class

End Namespace

#End Region
