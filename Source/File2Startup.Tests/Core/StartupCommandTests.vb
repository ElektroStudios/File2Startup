' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="StartupCommandTests.vb" company="Elektro Studios">
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

Imports Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

#End Region

#Region " StartupCommandTests "

Namespace Core

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Tests for the <see cref="StartupCommand"/> class.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <TestClass>
    Public NotInheritable Class StartupCommandTests

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The name of a process environment variable used by the tests.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Const TestEnvironmentVariable As String = "FILE2STARTUP_TESTS_DIR"

#End Region

#Region " Helpers "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates a file existence predicate that only returns <see langword="True"/>
        ''' for the specified file paths (case-insensitive).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="existingFiles">
        ''' The file paths that are considered to exist.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The predicate.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function FilesThatExist(ParamArray existingFiles As String()) As Func(Of String, Boolean)

            Dim fileSet As New HashSet(Of String)(existingFiles, StringComparer.OrdinalIgnoreCase)
            Return Function(path As String) fileSet.Contains(path)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A file existence predicate that always returns <see langword="False"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared ReadOnly NoFileExists As Func(Of String, Boolean) = Function(path As String) False

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes the process environment variable used by the tests.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <TestCleanup>
        Public Sub Cleanup()

            Environment.SetEnvironmentVariable(StartupCommandTests.TestEnvironmentVariable, Nothing)

        End Sub

        ''' <summary>
        ''' Creates a new <see cref="StartupCommand"/> (used by the lambdas of exception assertions).
        ''' </summary>
        ''' <param name="filePath">The file path.</param>
        ''' <param name="arguments">The arguments.</param>
        ''' <returns>The new instance.</returns>
        Private Shared Function Create(filePath As String, arguments As String) As StartupCommand

            Return New StartupCommand(filePath, arguments)

        End Function

#End Region

#Region " Constructor "

        ''' <summary>The constructor trims the file path and the arguments.</summary>
        <TestMethod>
        Public Sub New_TrimsFilePathAndArguments()

            Dim command As New StartupCommand("  C:\App\app.exe  ", "  -minimized  ")

            Assert.AreEqual("C:\App\app.exe", command.FilePath)
            Assert.AreEqual("-minimized", command.Arguments)

        End Sub

        ''' <summary>The constructor rejects a null file path.</summary>
        <TestMethod>
        Public Sub New_NullFilePath_ThrowsArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() StartupCommandTests.Create(Nothing, String.Empty))

        End Sub

        ''' <summary>The constructor rejects an empty or white-spaced file path.</summary>
        <TestMethod>
        <DataRow("")>
        <DataRow("   ")>
        Public Sub New_EmptyFilePath_ThrowsArgumentNullException(filePath As String)

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() StartupCommandTests.Create(filePath, String.Empty))

        End Sub

        ''' <summary>The constructor rejects null arguments.</summary>
        <TestMethod>
        Public Sub New_NullArguments_ThrowsArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() StartupCommandTests.Create("C:\App\app.exe", Nothing))

        End Sub

        ''' <summary>The constructor rejects file paths with invalid path characters.</summary>
        <TestMethod>
        <DataRow("C:\App\""app"".exe")>
        <DataRow("C:\App\app<.exe")>
        <DataRow("C:\App\app>.exe")>
        <DataRow("C:\App\app|.exe")>
        Public Sub New_FilePathWithInvalidChars_ThrowsArgumentException(filePath As String)

            Assert.ThrowsExactly(Of ArgumentException)(Sub() StartupCommandTests.Create(filePath, String.Empty))

        End Sub

#End Region

#Region " ToCommandLine / ToString / HasArguments "

        ''' <summary>The command-line encloses the file path in quotes and appends the arguments.</summary>
        <TestMethod>
        Public Sub ToCommandLine_WithArguments_QuotesFilePathAndAppendsArguments()

            Dim command As New StartupCommand("C:\Program Files\App\app.exe", "-minimized /tray")

            Assert.AreEqual("""C:\Program Files\App\app.exe"" -minimized /tray", command.ToCommandLine())

        End Sub

        ''' <summary>
        ''' Regression test: the command-line has no trailing white-space when there are no arguments
        ''' (v1.5 stored <c>"C:\App\app.exe" </c> in the registry).
        ''' </summary>
        <TestMethod>
        Public Sub ToCommandLine_WithoutArguments_HasNoTrailingWhiteSpace()

            Dim command As New StartupCommand("C:\App\app.exe", String.Empty)

            Assert.AreEqual("""C:\App\app.exe""", command.ToCommandLine())

        End Sub

        ''' <summary><see cref="StartupCommand.ToString()"/> returns the command-line.</summary>
        <TestMethod>
        Public Sub ToString_ReturnsCommandLine()

            Dim command As New StartupCommand("C:\App\app.exe", "-x")

            Assert.AreEqual(command.ToCommandLine(), command.ToString())

        End Sub

        ''' <summary><see cref="StartupCommand.HasArguments"/> reflects whether there are arguments.</summary>
        <TestMethod>
        Public Sub HasArguments_ReflectsArguments()

            Assert.IsTrue(New StartupCommand("C:\App\app.exe", "-x").HasArguments)
            Assert.IsFalse(New StartupCommand("C:\App\app.exe", String.Empty).HasArguments)
            Assert.IsFalse(New StartupCommand("C:\App\app.exe", "   ").HasArguments)

        End Sub

#End Region

#Region " GetExpandedFilePath "

        ''' <summary>The expanded file path resolves environment variables, while the file path keeps them.</summary>
        <TestMethod>
        Public Sub GetExpandedFilePath_ExpandsEnvironmentVariables()

            Environment.SetEnvironmentVariable(StartupCommandTests.TestEnvironmentVariable, "C:\Expanded")
            Dim command As New StartupCommand($"%{StartupCommandTests.TestEnvironmentVariable}%\app.exe", String.Empty)

            Assert.AreEqual($"%{StartupCommandTests.TestEnvironmentVariable}%\app.exe", command.FilePath)
            Assert.AreEqual("C:\Expanded\app.exe", command.GetExpandedFilePath())

        End Sub

#End Region

#Region " Parse: Argument Validation "

        ''' <summary><see cref="StartupCommand.Parse(String)"/> rejects a null command-line.</summary>
        <TestMethod>
        Public Sub Parse_NullCommandLine_ThrowsArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() StartupCommand.Parse(Nothing))

        End Sub

        ''' <summary><see cref="StartupCommand.Parse(String, Func(Of String, Boolean))"/> rejects a null predicate.</summary>
        <TestMethod>
        Public Sub Parse_NullFileExists_ThrowsArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() StartupCommand.Parse("C:\App\app.exe", Nothing))

        End Sub

        ''' <summary><see cref="StartupCommand.TryParse(String, ByRef StartupCommand)"/> rejects a null command-line.</summary>
        <TestMethod>
        Public Sub TryParse_NullCommandLine_ThrowsArgumentNullException()

            Dim result As StartupCommand = Nothing
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() StartupCommand.TryParse(Nothing, result))

        End Sub

        ''' <summary><see cref="StartupCommand.TryParse(String, Func(Of String, Boolean), ByRef StartupCommand)"/> rejects a null predicate.</summary>
        <TestMethod>
        Public Sub TryParse_NullFileExists_ThrowsArgumentNullException()

            Dim result As StartupCommand = Nothing
            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() StartupCommand.TryParse("C:\App\app.exe", Nothing, result))

        End Sub

#End Region

#Region " Parse: Quoted File Path "

        ''' <summary>A quoted file path without arguments.</summary>
        <TestMethod>
        Public Sub Parse_QuotedFilePathWithoutArguments()

            Dim command As StartupCommand = StartupCommand.Parse("""C:\Program Files\App\app.exe""", StartupCommandTests.NoFileExists)

            Assert.AreEqual("C:\Program Files\App\app.exe", command.FilePath)
            Assert.AreEqual(String.Empty, command.Arguments)

        End Sub

        ''' <summary>A quoted file path with arguments, and surrounding white-spaces.</summary>
        <TestMethod>
        <DataRow("""C:\Program Files\App\app.exe"" -minimized", "C:\Program Files\App\app.exe", "-minimized")>
        <DataRow("   ""C:\App\app.exe""    /a /b   ", "C:\App\app.exe", "/a /b")>
        <DataRow("""C:\App\app.exe""/silent", "C:\App\app.exe", "/silent")>
        <DataRow("""  C:\App\app.exe  "" -x", "C:\App\app.exe", "-x")>
        Public Sub Parse_QuotedFilePathWithArguments(commandLine As String, expectedFilePath As String, expectedArguments As String)

            Dim command As StartupCommand = StartupCommand.Parse(commandLine, StartupCommandTests.NoFileExists)

            Assert.AreEqual(expectedFilePath, command.FilePath)
            Assert.AreEqual(expectedArguments, command.Arguments)

        End Sub

        ''' <summary>
        ''' Regression test: the quotes of a quoted argument are preserved
        ''' (v1.5 stripped the leading quote of the first argument).
        ''' </summary>
        <TestMethod>
        Public Sub Parse_QuotedFilePathWithQuotedArgument_PreservesArgumentQuotes()

            Dim command As StartupCommand = StartupCommand.Parse("""C:\App\app.exe"" ""D:\My Documents\file.txt"" -x", StartupCommandTests.NoFileExists)

            Assert.AreEqual("C:\App\app.exe", command.FilePath)
            Assert.AreEqual("""D:\My Documents\file.txt"" -x", command.Arguments)

        End Sub

        ''' <summary>An unterminated quoted file path cannot be parsed.</summary>
        <TestMethod>
        Public Sub Parse_UnterminatedQuotedFilePath_ThrowsFormatException()

            Assert.ThrowsExactly(Of FormatException)(Sub() StartupCommand.Parse("""C:\App\app.exe -x", StartupCommandTests.NoFileExists))

        End Sub

        ''' <summary>An empty quoted file path cannot be parsed.</summary>
        <TestMethod>
        <DataRow("""""")>
        <DataRow("""   "" -x")>
        Public Sub TryParse_EmptyQuotedFilePath_ReturnsFalse(commandLine As String)

            Dim result As StartupCommand = New StartupCommand("C:\placeholder.exe", String.Empty)

            Assert.IsFalse(StartupCommand.TryParse(commandLine, StartupCommandTests.NoFileExists, result))
            Assert.IsNull(result)

        End Sub

#End Region

#Region " Parse: Unquoted File Path "

        ''' <summary>An unquoted file path that exists, with spaces and arguments.</summary>
        <TestMethod>
        Public Sub Parse_UnquotedExistingFilePathWithSpaces_SplitsAtExistingFile()

            Dim fileExists As Func(Of String, Boolean) = StartupCommandTests.FilesThatExist("C:\Program Files\My App\app.exe")

            Dim command As StartupCommand = StartupCommand.Parse("C:\Program Files\My App\app.exe -minimized /tray", fileExists)

            Assert.AreEqual("C:\Program Files\My App\app.exe", command.FilePath)
            Assert.AreEqual("-minimized /tray", command.Arguments)

        End Sub

        ''' <summary>An unquoted file path whose whole command-line is an existing file (no arguments).</summary>
        <TestMethod>
        Public Sub Parse_UnquotedWholeCommandLineExists_HasNoArguments()

            Dim fileExists As Func(Of String, Boolean) = StartupCommandTests.FilesThatExist("C:\Program Files\My App\My Tool")

            Dim command As StartupCommand = StartupCommand.Parse("C:\Program Files\My App\My Tool", fileExists)

            Assert.AreEqual("C:\Program Files\My App\My Tool", command.FilePath)
            Assert.AreEqual(String.Empty, command.Arguments)

        End Sub

        ''' <summary>
        ''' Unquoted prefixes are resolved from left to right, the same way Windows does:
        ''' the first prefix that exists wins.
        ''' </summary>
        <TestMethod>
        Public Sub Parse_UnquotedAmbiguousPath_FirstExistingPrefixWins()

            Dim fileExists As Func(Of String, Boolean) =
                StartupCommandTests.FilesThatExist("C:\Program", "C:\Program Files\App\app.exe")

            Dim command As StartupCommand = StartupCommand.Parse("C:\Program Files\App\app.exe -x", fileExists)

            Assert.AreEqual("C:\Program", command.FilePath)
            Assert.AreEqual("Files\App\app.exe -x", command.Arguments)

        End Sub

        ''' <summary>The file existence check expands environment variables, but the file path keeps them unexpanded.</summary>
        <TestMethod>
        Public Sub Parse_UnquotedPathWithEnvironmentVariable_ChecksExpandedPathAndKeepsVariable()

            Environment.SetEnvironmentVariable(StartupCommandTests.TestEnvironmentVariable, "C:\Some Dir")
            Dim fileExists As Func(Of String, Boolean) = StartupCommandTests.FilesThatExist("C:\Some Dir\tool")

            Dim command As StartupCommand = StartupCommand.Parse($"%{StartupCommandTests.TestEnvironmentVariable}%\tool --start", fileExists)

            Assert.AreEqual($"%{StartupCommandTests.TestEnvironmentVariable}%\tool", command.FilePath)
            Assert.AreEqual("--start", command.Arguments)

        End Sub

        ''' <summary>
        ''' Regression test: when the file does not exist, the file path ends at the first launchable file name extension
        ''' (v1.5 put the whole unquoted command-line, arguments included, into the file path field).
        ''' </summary>
        <TestMethod>
        <DataRow("C:\Program Files\App\app.exe -minimized", "C:\Program Files\App\app.exe", "-minimized")>
        <DataRow("C:\APP\APP.EXE /S", "C:\APP\APP.EXE", "/S")>
        <DataRow("C:\Scripts\run.bat now", "C:\Scripts\run.bat", "now")>
        <DataRow("C:\Scripts\run.cmd", "C:\Scripts\run.cmd", "")>
        <DataRow("rundll32.exe shell32.dll,Control_RunDLL desk.cpl", "rundll32.exe", "shell32.dll,Control_RunDLL desk.cpl")>
        <DataRow("wscript.exe ""C:\Scripts\my script.vbs""", "wscript.exe", """C:\Scripts\my script.vbs""")>
        Public Sub Parse_UnquotedNonExistingPath_SplitsAtLaunchableExtension(commandLine As String, expectedFilePath As String, expectedArguments As String)

            Dim command As StartupCommand = StartupCommand.Parse(commandLine, StartupCommandTests.NoFileExists)

            Assert.AreEqual(expectedFilePath, command.FilePath)
            Assert.AreEqual(expectedArguments, command.Arguments)

        End Sub

        ''' <summary>
        ''' Regression test: an unquoted command-line with a quoted argument is split at the executable
        ''' (v1.5 took the text before the first quote as the file path).
        ''' </summary>
        <TestMethod>
        Public Sub Parse_UnquotedPathWithQuotedArgument_SplitsAtExecutable()

            Dim command As StartupCommand = StartupCommand.Parse("C:\Tools\app.exe -config ""C:\My Config\app.ini""", StartupCommandTests.NoFileExists)

            Assert.AreEqual("C:\Tools\app.exe", command.FilePath)
            Assert.AreEqual("-config ""C:\My Config\app.ini""", command.Arguments)

        End Sub

        ''' <summary>A file existence match takes precedence over the launchable extension heuristic.</summary>
        <TestMethod>
        Public Sub Parse_UnquotedPath_ExistingFileTakesPrecedenceOverExtension()

            Dim fileExists As Func(Of String, Boolean) = StartupCommandTests.FilesThatExist("C:\Tools\launcher.exe tool")

            Dim command As StartupCommand = StartupCommand.Parse("C:\Tools\launcher.exe tool -x", fileExists)

            Assert.AreEqual("C:\Tools\launcher.exe tool", command.FilePath)
            Assert.AreEqual("-x", command.Arguments)

        End Sub

        ''' <summary>Without an existing file nor a launchable extension, the whole command-line is the file path.</summary>
        <TestMethod>
        Public Sub Parse_UnquotedUnknownPath_WholeCommandLineIsFilePath()

            Dim command As StartupCommand = StartupCommand.Parse("C:\Tools\daemon --run", StartupCommandTests.NoFileExists)

            Assert.AreEqual("C:\Tools\daemon --run", command.FilePath)
            Assert.AreEqual(String.Empty, command.Arguments)

        End Sub

        ''' <summary>A command-line that only consists of an extension is not split at that extension.</summary>
        <TestMethod>
        Public Sub Parse_UnquotedBareExtension_IsNotTreatedAsFilePath()

            Dim command As StartupCommand = StartupCommand.Parse(".exe file", StartupCommandTests.NoFileExists)

            Assert.AreEqual(".exe file", command.FilePath)
            Assert.AreEqual(String.Empty, command.Arguments)

        End Sub

        ''' <summary>An unquoted command-line that falls back to the whole text, with an invalid path character, cannot be parsed.</summary>
        <TestMethod>
        Public Sub TryParse_UnquotedUnknownPathWithQuote_ReturnsFalse()

            Dim result As StartupCommand = Nothing

            Assert.IsFalse(StartupCommand.TryParse("C:\Tools\daemon ""arg""", StartupCommandTests.NoFileExists, result))
            Assert.IsNull(result)

        End Sub

        ''' <summary>An empty or white-spaced command-line cannot be parsed.</summary>
        <TestMethod>
        <DataRow("")>
        <DataRow("    ")>
        Public Sub Parse_EmptyCommandLine_ThrowsFormatException(commandLine As String)

            Assert.ThrowsExactly(Of FormatException)(Sub() StartupCommand.Parse(commandLine, StartupCommandTests.NoFileExists))

        End Sub

        ''' <summary>The overload without predicate checks the real file system.</summary>
        <TestMethod>
        Public Sub Parse_RealFileSystem_FindsExistingFile()

            Dim tempFile As String = IO.Path.Combine(IO.Path.GetTempPath(), $"File2Startup Tests {Guid.NewGuid():N}.dat")
            IO.File.WriteAllText(tempFile, String.Empty)
            Try
                Dim command As StartupCommand = StartupCommand.Parse($"{tempFile} -x")

                Assert.AreEqual(tempFile, command.FilePath)
                Assert.AreEqual("-x", command.Arguments)

            Finally
                IO.File.Delete(tempFile)

            End Try

        End Sub

#End Region

#Region " Round-trip "

        ''' <summary>Parsing the command-line built by <see cref="StartupCommand.ToCommandLine()"/> gives back the same values.</summary>
        <TestMethod>
        <DataRow("C:\App\app.exe", "")>
        <DataRow("C:\Program Files\App\app.exe", "-minimized")>
        <DataRow("C:\Program Files\App\app.exe", """D:\Some File.txt"" /x")>
        <DataRow("%ProgramFiles%\App\app.exe", "--tray")>
        Public Sub ToCommandLine_Parse_RoundTrip(filePath As String, arguments As String)

            Dim original As New StartupCommand(filePath, arguments)

            Dim parsed As StartupCommand = StartupCommand.Parse(original.ToCommandLine(), StartupCommandTests.NoFileExists)

            Assert.AreEqual(original.FilePath, parsed.FilePath)
            Assert.AreEqual(original.Arguments, parsed.Arguments)

        End Sub

#End Region

#Region " IsValidFilePath "

        ''' <summary>Valid file paths.</summary>
        <TestMethod>
        <DataRow("C:\App\app.exe")>
        <DataRow("%WINDIR%\notepad.exe")>
        <DataRow("\\server\share\app.exe")>
        <DataRow("app.exe")>
        Public Sub IsValidFilePath_ValidPath_ReturnsTrue(filePath As String)

            Assert.IsTrue(StartupCommand.IsValidFilePath(filePath))

        End Sub

        ''' <summary>Invalid file paths.</summary>
        <TestMethod>
        <DataRow("")>
        <DataRow("   ")>
        <DataRow("C:\App\""app.exe")>
        <DataRow("C:\App\a<b.exe")>
        <DataRow("C:\App\a>b.exe")>
        <DataRow("C:\App\a|b.exe")>
        <DataRow("C:\App\a" & vbTab & "b.exe")>
        Public Sub IsValidFilePath_InvalidPath_ReturnsFalse(filePath As String)

            Assert.IsFalse(StartupCommand.IsValidFilePath(filePath))

        End Sub

        ''' <summary><see cref="StartupCommand.IsValidFilePath(String)"/> rejects a null file path.</summary>
        <TestMethod>
        Public Sub IsValidFilePath_Null_ThrowsArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() StartupCommand.IsValidFilePath(Nothing))

        End Sub

#End Region

#Region " NormalizeUserFilePath "

        ''' <summary>The user file path is trimmed and a single pair of enclosing quotes is removed.</summary>
        <TestMethod>
        <DataRow("C:\App\app.exe", "C:\App\app.exe")>
        <DataRow("   C:\App\app.exe   ", "C:\App\app.exe")>
        <DataRow("""C:\Program Files\App\app.exe""", "C:\Program Files\App\app.exe")>
        <DataRow("  "" C:\App\app.exe ""  ", "C:\App\app.exe")>
        <DataRow("""C:\App\app.exe", """C:\App\app.exe")>
        <DataRow("""", """")>
        <DataRow("", "")>
        Public Sub NormalizeUserFilePath_ReturnsExpectedValue(filePath As String, expected As String)

            Assert.AreEqual(expected, StartupCommand.NormalizeUserFilePath(filePath))

        End Sub

        ''' <summary><see cref="StartupCommand.NormalizeUserFilePath(String)"/> rejects a null file path.</summary>
        <TestMethod>
        Public Sub NormalizeUserFilePath_Null_ThrowsArgumentNullException()

            Assert.ThrowsExactly(Of ArgumentNullException)(Sub() StartupCommand.NormalizeUserFilePath(Nothing))

        End Sub

#End Region

    End Class

End Namespace

#End Region
