' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="StartupCommand.vb" company="Elektro Studios">
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
Imports System.IO
Imports System.Linq

#End Region

#Region " StartupCommand "

Namespace Core

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Represents the command-line of a Windows startup entry
    ''' (the data of a value stored in a <c>...\CurrentVersion\Run</c> registry key),
    ''' split into the target file path and its optional arguments.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' This type is immutable.
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    <DebuggerDisplay("{ToCommandLine(),nq}")>
    Public NotInheritable Class StartupCommand

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The file name extensions that are considered executable (or launchable) by the unquoted command-line parser
        ''' when the target file of the command-line does not exist on the current system.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared ReadOnly LaunchableExtensions As String() = {
            ".exe", ".com", ".bat", ".cmd", ".lnk", ".pif", ".scr", ".cpl",
            ".msc", ".vbs", ".vbe", ".js", ".jse", ".wsf", ".wsh", ".ps1", ".hta"
        }

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The characters that are not allowed in a Windows file path:
        ''' the double quote, <c>&lt;</c>, <c>&gt;</c>, <c>|</c> and the control characters (0 to 31).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' This set is defined explicitly instead of using <see cref="Path.GetInvalidPathChars()"/>,
        ''' because the result of that method differs between .NET Framework, modern .NET and the operating system.
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared ReadOnly InvalidPathChars As Char() =
            $"{ControlChars.Quote}<>|".ToCharArray().
            Concat(Enumerable.Range(0, 32).Select(Function(code As Integer) ChrW(code))).
            ToArray()

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the target file path of this command-line,
        ''' exactly as it is stored (environment variables are not expanded).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property FilePath As String

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the arguments of this command-line, or <see cref="String.Empty"/> if it has no arguments.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Arguments As String

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value indicating whether this command-line has arguments.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property HasArguments As Boolean
            Get
                Return Me.Arguments.Length <> 0
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="StartupCommand"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filePath">
        ''' The target file path. Leading and trailing white-spaces are removed.
        ''' </param>
        '''
        ''' <param name="arguments">
        ''' The arguments, or <see cref="String.Empty"/> for none. Leading and trailing white-spaces are removed.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="filePath"/> is <see langword="Nothing"/>, empty or white-spaced,
        ''' or <paramref name="arguments"/> is <see langword="Nothing"/>.
        ''' </exception>
        '''
        ''' <exception cref="ArgumentException">
        ''' <paramref name="filePath"/> contains invalid path characters (for example a quotation mark).
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(filePath As String, arguments As String)

            If String.IsNullOrWhiteSpace(filePath) Then
                Throw New ArgumentNullException(NameOf(filePath))
            End If

            If arguments Is Nothing Then
                Throw New ArgumentNullException(NameOf(arguments))
            End If

            Dim trimmedFilePath As String = filePath.Trim()
            If Not StartupCommand.IsValidFilePath(trimmedFilePath) Then
                Throw New ArgumentException($"The file path contains invalid path characters: {trimmedFilePath}", NameOf(filePath))
            End If

            Me.FilePath = trimmedFilePath
            Me.Arguments = arguments.Trim()

        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Builds the command-line string that represents this instance,
        ''' in the format used to store Windows startup entries: <c>"FilePath" Arguments</c>.
        ''' <para></para>
        ''' The file path is always enclosed in double quotes,
        ''' and no trailing white-space is added when there are no arguments.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The resulting command-line string.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Function ToCommandLine() As String

            Dim quotedFilePath As String = $"{ControlChars.Quote}{Me.FilePath}{ControlChars.Quote}"
            Return If(Me.HasArguments, $"{quotedFilePath} {Me.Arguments}", quotedFilePath)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the target file path with any environment variable (e.g. <c>%WINDIR%</c>) expanded.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The expanded file path.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Function GetExpandedFilePath() As String

            Return Environment.ExpandEnvironmentVariables(Me.FilePath)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Returns the command-line string that represents this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The same value as <see cref="StartupCommand.ToCommandLine()"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overrides Function ToString() As String

            Return Me.ToCommandLine()

        End Function

#End Region

#Region " Public Shared Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Parses the command-line of a Windows startup entry,
        ''' checking the existence of candidate file paths on the local file system.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="commandLine">
        ''' The command-line to parse.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The resulting <see cref="StartupCommand"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="commandLine"/> is <see langword="Nothing"/>.
        ''' </exception>
        '''
        ''' <exception cref="FormatException">
        ''' The file path could not be parsed from <paramref name="commandLine"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function Parse(commandLine As String) As StartupCommand

            Return StartupCommand.Parse(commandLine, AddressOf File.Exists)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Parses the command-line of a Windows startup entry.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' Parsing rules:
        ''' <list type="number">
        '''   <item><description>
        '''   If the command-line starts with a double quote,
        '''   the file path is the text enclosed by the first pair of double quotes,
        '''   and the arguments are the rest of the text (preserving any quote on them).
        '''   </description></item>
        '''   <item><description>
        '''   Otherwise, the white-space separated prefixes of the command-line are evaluated from left to right
        '''   (the same way Windows resolves an unquoted command-line), and the first one that exists as a file is the file path.
        '''   </description></item>
        '''   <item><description>
        '''   Otherwise, the first prefix that ends with a launchable file name extension (<c>.exe</c>, <c>.bat</c>, etc.) is the file path.
        '''   </description></item>
        '''   <item><description>
        '''   Otherwise, the whole command-line is the file path.
        '''   </description></item>
        ''' </list>
        ''' Environment variables are expanded only to check the file existence; the resulting file path keeps them unexpanded.
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="commandLine">
        ''' The command-line to parse.
        ''' </param>
        '''
        ''' <param name="fileExists">
        ''' A function that determines whether a (environment-expanded) file path exists.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The resulting <see cref="StartupCommand"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="commandLine"/> or <paramref name="fileExists"/> is <see langword="Nothing"/>.
        ''' </exception>
        '''
        ''' <exception cref="FormatException">
        ''' The file path could not be parsed from <paramref name="commandLine"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function Parse(commandLine As String, fileExists As Func(Of String, Boolean)) As StartupCommand

            If commandLine Is Nothing Then
                Throw New ArgumentNullException(NameOf(commandLine))
            End If

            If fileExists Is Nothing Then
                Throw New ArgumentNullException(NameOf(fileExists))
            End If

            Dim errorMessage As String = Nothing
            Dim result As StartupCommand = StartupCommand.ParseCore(commandLine, fileExists, errorMessage)
            If result Is Nothing Then
                Throw New FormatException(errorMessage)
            End If

            Return result

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Tries to parse the command-line of a Windows startup entry,
        ''' checking the existence of candidate file paths on the local file system.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="commandLine">
        ''' The command-line to parse.
        ''' </param>
        '''
        ''' <param name="result">
        ''' When this method returns <see langword="True"/>, contains the resulting <see cref="StartupCommand"/>;
        ''' otherwise, <see langword="Nothing"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the command-line was successfully parsed; otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="commandLine"/> is <see langword="Nothing"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function TryParse(commandLine As String, ByRef result As StartupCommand) As Boolean

            Return StartupCommand.TryParse(commandLine, AddressOf File.Exists, result)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Tries to parse the command-line of a Windows startup entry.
        ''' <para></para>
        ''' See <see cref="StartupCommand.Parse(String, Func(Of String, Boolean))"/> for the parsing rules.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="commandLine">
        ''' The command-line to parse.
        ''' </param>
        '''
        ''' <param name="fileExists">
        ''' A function that determines whether a (environment-expanded) file path exists.
        ''' </param>
        '''
        ''' <param name="result">
        ''' When this method returns <see langword="True"/>, contains the resulting <see cref="StartupCommand"/>;
        ''' otherwise, <see langword="Nothing"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the command-line was successfully parsed; otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="commandLine"/> or <paramref name="fileExists"/> is <see langword="Nothing"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function TryParse(commandLine As String, fileExists As Func(Of String, Boolean), ByRef result As StartupCommand) As Boolean

            If commandLine Is Nothing Then
                Throw New ArgumentNullException(NameOf(commandLine))
            End If

            If fileExists Is Nothing Then
                Throw New ArgumentNullException(NameOf(fileExists))
            End If

            Dim errorMessage As String = Nothing
            result = StartupCommand.ParseCore(commandLine, fileExists, errorMessage)
            Return result IsNot Nothing

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Determines whether the specified file path does not contain invalid Windows path characters.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filePath">
        ''' The file path to validate.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if <paramref name="filePath"/> is not empty and has no invalid path characters;
        ''' otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="filePath"/> is <see langword="Nothing"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function IsValidFilePath(filePath As String) As Boolean

            If filePath Is Nothing Then
                Throw New ArgumentNullException(NameOf(filePath))
            End If

            Return Not String.IsNullOrWhiteSpace(filePath) AndAlso
                   filePath.IndexOfAny(StartupCommand.InvalidPathChars) = -1

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes the leading and trailing white-spaces of a file path typed by the user,
        ''' and then a single pair of enclosing double quotes, if any.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="filePath">
        ''' The file path to normalize.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The normalized file path.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentNullException">
        ''' <paramref name="filePath"/> is <see langword="Nothing"/>.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared Function NormalizeUserFilePath(filePath As String) As String

            If filePath Is Nothing Then
                Throw New ArgumentNullException(NameOf(filePath))
            End If

            Dim trimmed As String = filePath.Trim()
            If trimmed.Length >= 2 AndAlso
               trimmed.StartsWith(ControlChars.Quote, StringComparison.Ordinal) AndAlso
               trimmed.EndsWith(ControlChars.Quote, StringComparison.Ordinal) Then

                trimmed = trimmed.Substring(1, trimmed.Length - 2).Trim()
            End If

            Return trimmed

        End Function

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Implements the parsing logic shared by <see cref="StartupCommand.Parse(String, Func(Of String, Boolean))"/>
        ''' and <see cref="StartupCommand.TryParse(String, Func(Of String, Boolean), ByRef StartupCommand)"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="commandLine">
        ''' The command-line to parse. Must not be <see langword="Nothing"/>.
        ''' </param>
        '''
        ''' <param name="fileExists">
        ''' A function that determines whether a (environment-expanded) file path exists. Must not be <see langword="Nothing"/>.
        ''' </param>
        '''
        ''' <param name="errorMessage">
        ''' When this method returns <see langword="Nothing"/>, contains the reason of the failure.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The resulting <see cref="StartupCommand"/>, or <see langword="Nothing"/> if the command-line could not be parsed.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function ParseCore(commandLine As String, fileExists As Func(Of String, Boolean),
                                          ByRef errorMessage As String) As StartupCommand

            Dim trimmed As String = commandLine.Trim()
            If trimmed.Length = 0 Then
                errorMessage = "The command-line is empty."
                Return Nothing
            End If

            Dim filePath As String
            Dim arguments As String

            If trimmed.StartsWith(ControlChars.Quote, StringComparison.Ordinal) Then
                ' Quoted file path: "C:\Dir\File.exe" Arguments
                Dim closingQuoteIndex As Integer = trimmed.IndexOf(ControlChars.Quote, 1)
                If closingQuoteIndex = -1 Then
                    errorMessage = $"The command-line has an unterminated quoted file path: {trimmed}"
                    Return Nothing
                End If

                filePath = trimmed.Substring(1, closingQuoteIndex - 1).Trim()
                arguments = trimmed.Substring(closingQuoteIndex + 1).Trim()

            Else
                ' Unquoted file path: C:\Dir\File.exe Arguments
                Dim splitIndex As Integer = StartupCommand.FindUnquotedFilePathEnd(trimmed, fileExists)
                filePath = trimmed.Substring(0, splitIndex).Trim()
                arguments = trimmed.Substring(splitIndex).Trim()

            End If

            If Not StartupCommand.IsValidFilePath(filePath) Then
                errorMessage = $"The command-line does not contain a valid file path: {trimmed}"
                Return Nothing
            End If

            errorMessage = Nothing
            Return New StartupCommand(filePath, arguments)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Finds the character index where the file path of an unquoted (and already trimmed) command-line ends.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="commandLine">
        ''' The trimmed, unquoted command-line.
        ''' </param>
        '''
        ''' <param name="fileExists">
        ''' A function that determines whether a (environment-expanded) file path exists.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The length of the file path portion of <paramref name="commandLine"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function FindUnquotedFilePathEnd(commandLine As String, fileExists As Func(Of String, Boolean)) As Integer

            Dim candidateEnds As List(Of Integer) = StartupCommand.GetCandidateEnds(commandLine)

            ' 1st pass: the first prefix that exists as a file (same resolution order as Windows).
            For Each candidateEnd As Integer In candidateEnds
                Dim candidate As String = commandLine.Substring(0, candidateEnd)
                If StartupCommand.IsValidFilePath(candidate) AndAlso
                   fileExists(Environment.ExpandEnvironmentVariables(candidate)) Then

                    Return candidateEnd
                End If
            Next candidateEnd

            ' 2nd pass: the first prefix that ends with a launchable file name extension.
            For Each candidateEnd As Integer In candidateEnds
                Dim candidate As String = commandLine.Substring(0, candidateEnd)
                Dim hasLaunchableExtension As Boolean =
                    StartupCommand.LaunchableExtensions.Any(
                        Function(ext As String) candidate.EndsWith(ext, StringComparison.OrdinalIgnoreCase) AndAlso
                                                candidate.Length > ext.Length)

                If hasLaunchableExtension AndAlso StartupCommand.IsValidFilePath(candidate) Then
                    Return candidateEnd
                End If
            Next candidateEnd

            ' Fallback: the whole command-line.
            Return commandLine.Length

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the candidate end indices of the file path of an unquoted command-line:
        ''' the index of every white-space that follows a non white-space character, plus the command-line length.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="commandLine">
        ''' The trimmed, unquoted command-line.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The candidate end indices, in ascending order.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function GetCandidateEnds(commandLine As String) As List(Of Integer)

            Dim candidateEnds As New List(Of Integer)
            For index As Integer = 1 To commandLine.Length - 1
                If Char.IsWhiteSpace(commandLine(index)) AndAlso Not Char.IsWhiteSpace(commandLine(index - 1)) Then
                    candidateEnds.Add(index)
                End If
            Next index
            candidateEnds.Add(commandLine.Length)

            Return candidateEnds

        End Function

#End Region

    End Class

End Namespace

#End Region
