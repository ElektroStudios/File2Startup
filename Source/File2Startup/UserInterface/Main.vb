' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="Main.vb" company="Elektro Studios">
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
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Security.Principal
Imports System.Threading
Imports System.Windows.Forms

Imports Microsoft.Win32

Imports Core

Imports DevCase.Core.Application.Forms
Imports DevCase.Core.Application.UserInterface.Enums
Imports DevCase.Core.Application.UserInterface.Types
Imports DevCase.Core.Imaging.Tools
Imports DevCase.Core.System.Tools

Imports UserInterface.Localization
Imports UserInterface.Theming

#End Region

#Region " Main "

Namespace UserInterface

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' The main user interface form.
    ''' <para></para>
    ''' It has two pages, selected from the header tool bar: the item builder (to add a file to Windows startup)
    ''' and the startup list (to manage the existing startup entries).
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    Public NotInheritable Class Main

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The registry key where Regedit stores the last key it showed, which it opens on start.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Const RegeditAppletKeyPath As String = "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit"

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The size, in pixels at 96 DPI, of the button images.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Const ButtonImageSize As Integer = 16

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value indicating whether the current process has full administrative rights.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property IsProcessElevated As Boolean
            Get
                Using identity As WindowsIdentity = WindowsIdentity.GetCurrent()
                    Return New WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator)
                End Using
            End Get
        End Property

#End Region

#Region " Fields "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The localized texts of the current language.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private strings As UIStrings = UIStrings.FromCulture(UIStrings.DefaultCultureName)

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The current visual theme.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private currentTheme As AppTheme = AppTheme.Light

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A value indicating whether the theme is being applied,
        ''' to ignore the <see cref="ToolStripToggleSwitch_Theme"/> state changes made by code.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private isApplyingTheme As Boolean

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The <see cref="WindowMagnetizer"/> instance that docks the form to the screen borders.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private windowSticker As WindowMagnetizer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The registry keys of the startup types (current user only, or all users).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly startupType As New Dictionary(Of String, String) From
        {
            {StartupRegistryKeys.CurrentUserId, StartupRegistryKeys.CurrentUserRunKeyPath},
            {StartupRegistryKeys.AllUsersId, StartupRegistryKeys.AllUsersRunKeyPath}
        }

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The selected startup type (<see cref="StartupRegistryKeys.CurrentUserId"/> or <see cref="StartupRegistryKeys.AllUsersId"/>).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private selectedStartupType As String = StartupRegistryKeys.CurrentUserId

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The field delimiter of the serialized recent file list entries.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly mruDelimiter As String = $"@@{My.Application.Info.ProductName}@@"

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The menu item that clears the recent file list.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ReadOnly clearRecentFilesMenuItem As ToolStripMenuItem

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The name and startup type identifier of the last row selected by the user in the startup list.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private lastSelectedRowNameAndStartupType As String()

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A value indicating whether the rows of the startup list are being reloaded.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private isRefreshingDataGridViewItems As Boolean

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="Main"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New()

            ' This call is required by the designer.
            Me.InitializeComponent()

            Me.clearRecentFilesMenuItem = New ToolStripMenuItem(String.Empty, My.Resources.delete, AddressOf Me.ClearRecentFilesMenuItem_Click) With {
                .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
            }

            Me.ApplyButtonImages()
            Me.ApplyFormLanguage(My.Settings.Culture)
            Me.ApplyTheme(Me.LoadThemeSetting())
            Me.ShowPage(Me.Panel_ItemBuilder)

        End Sub

#End Region

#Region " Overridden Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="Control.HandleCreated"/> event, and applies the dark/light title bar.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <remarks>
        ''' The title bar attribute is applied here because the window handle is required,
        ''' and applying it in the <see cref="Form.Load"/> event has no effect on the first paint of the title bar.
        ''' </remarks>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnHandleCreated(e As EventArgs)

            MyBase.OnHandleCreated(e)
            WindowThemeUtil.SetDarkTitleBar(Me, Me.currentTheme = AppTheme.Dark)

        End Sub

#End Region

#Region " Event Handlers "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Form.Load"/> event of the form.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            Me.FitClientHeightToItemBuilder()
            Me.MinimumSize = Me.Size
            Me.CheckRecentFiles()
            Me.RadioButton_AllUsers.Enabled = Main.IsProcessElevated

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Form.Shown"/> event of the form.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Main_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

            Me.windowSticker = New WindowMagnetizer(window:=Me) With {.Threshold = 35, .AllowOffscreen = True, .Enabled = True}

            If My.Application.CommandLineArgs.Count <> 0 Then
                Dim filePath As String = My.Application.CommandLineArgs.First()
                If File.Exists(filePath) Then
                    Me.LoadDragDropFile(New FileInfo(filePath))
                End If
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.DragEnter"/> event of the form.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Form_DragEnter(sender As Object, e As DragEventArgs) Handles Me.DragEnter

            Dim filePath As String = Main.GetDroppedFilePath(e.Data)
            If filePath IsNot Nothing AndAlso File.Exists(filePath) Then
                e.Effect = DragDropEffects.All
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.DragDrop"/> event of the form.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Form_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop

            Dim filePath As String = Main.GetDroppedFilePath(e.Data)
            If filePath IsNot Nothing Then
                Me.ShowPage(Me.Panel_ItemBuilder)
                Me.LoadDragDropFile(New FileInfo(filePath))
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.TextChanged"/> event of the name and file text boxes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub TextBoxes_TextChanged(sender As Object, e As EventArgs) _
        Handles TextBox_Name.TextChanged, TextBox_File.TextChanged

            Me.Button_AddToStartup.Enabled = Not String.IsNullOrEmpty(Me.TextBox_File.Text) AndAlso
                                             Not String.IsNullOrEmpty(Me.TextBox_Name.Text) AndAlso
                                             Me.TextBox_File.Text <> Me.strings.FileHint AndAlso
                                             Me.TextBox_Name.Text <> Me.strings.NameHint

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="RadioButton.CheckedChanged"/> event of the startup type radio buttons.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub RadioButtons_CheckedChanged(sender As Object, e As EventArgs) _
        Handles RadioButton_CurrentUser.CheckedChanged, RadioButton_AllUsers.CheckedChanged

            ' Only the radio button that becomes checked determines the startup type.
            Dim radioButton As RadioButton = DirectCast(sender, RadioButton)
            If Not radioButton.Checked Then
                Exit Sub
            End If

            Me.selectedStartupType = CStr(radioButton.Tag)

            If String.IsNullOrEmpty(Me.selectedStartupType) Then
                Me.selectedStartupType = StartupRegistryKeys.CurrentUserId
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.Click"/> event of the <see cref="Button_AddToStartup"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Button_AddToStartup_Click(sender As Object, e As EventArgs) Handles Button_AddToStartup.Click

            Dim success As Boolean = Me.AddFileToWindowsStartup()
            If success Then
                Me.RemoveControlHints()
                Me.TextBox_File.Clear()
                Me.TextBox_Name.Clear()
                Me.TextBox_Parameters.Clear()
                Me.SetControlHints()

                Me.RenewRecentFiles()
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ToolStripDropDown.Opening"/> event of the <see cref="ContextMenuStrip_Recent"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ContextMenuStrip_Recent_Opening(sender As Object, e As CancelEventArgs) _
        Handles ContextMenuStrip_Recent.Opening

            Dim contextMenu As ContextMenuStrip = DirectCast(sender, ContextMenuStrip)
            contextMenu.Items.Clear()

            ' Malformed (corrupted) entries stored in the user settings are skipped.
            Dim entries As List(Of RecentFileEntry) = RecentFileList.GetEntries(My.Settings.MRU, Me.mruDelimiter)
            If entries.Count = 0 Then
                e.Cancel = True
                Exit Sub
            End If

            For Each entry As RecentFileEntry In entries
                Dim menuItem As New ToolStripMenuItem(entry.Name) With {.Tag = entry}
                Try
                    Using ico As Icon = ImageUtil.ExtractIconFromFile(Environment.ExpandEnvironmentVariables(entry.FilePath))
                        If ico IsNot Nothing Then
                            menuItem.Image = ico.ToBitmap()
                        End If
                    End Using
                Catch ex As Exception
                    ' The icon is merely decorative; a menu item without icon is acceptable.
                End Try

                AddHandler menuItem.Click, AddressOf Me.RecentFileMenuItem_Click
                contextMenu.Items.Insert(0, menuItem)
            Next entry

            contextMenu.Items.Insert(0, New ToolStripSeparator())
            contextMenu.Items.Insert(0, Me.clearRecentFilesMenuItem)

            ThemeManager.ApplyToDropDown(contextMenu, Me.currentTheme)
            e.Cancel = False

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ToolStripItem.Click"/> event of a recent file menu item.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub RecentFileMenuItem_Click(sender As Object, e As EventArgs)

            Dim entry As RecentFileEntry = DirectCast(DirectCast(sender, ToolStripMenuItem).Tag, RecentFileEntry)

            Me.RemoveControlHints()
            Me.TextBox_Name.Text = entry.Name
            Me.TextBox_File.Text = entry.FilePath
            Me.TextBox_Parameters.Text = entry.Arguments
            Me.SetControlHints()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ToolStripItem.Click"/> event of the menu item that clears the recent file list.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ClearRecentFilesMenuItem_Click(sender As Object, e As EventArgs)

            My.Settings.MRU.Clear()
            My.Settings.Save()
            Me.RenewRecentFiles()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.Click"/> event of the <see cref="Button_OpenFile"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Button_OpenFile_Click(sender As Object, e As EventArgs) Handles Button_OpenFile.Click

            Me.OpenFileDialog1.ShowDialog(Me)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="FileDialog.FileOk"/> event of the <see cref="OpenFileDialog1"/> component.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub OpenFileDialog1_FileOk(sender As Object, e As CancelEventArgs) Handles OpenFileDialog1.FileOk

            Dim filePath As String = DirectCast(sender, OpenFileDialog).FileName

            ControlHintManager.RemoveHint(Me.TextBox_File)
            Me.TextBox_File.Text = filePath

            If String.IsNullOrWhiteSpace(Me.TextBox_Name.Text) OrElse Me.TextBox_Name.Text = Me.strings.NameHint Then
                ControlHintManager.RemoveHint(Me.TextBox_Name)
                Me.TextBox_Name.Text = Path.GetFileNameWithoutExtension(filePath)
            End If

            Me.SetControlHints()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ToolStripItem.Click"/> event of the <see cref="ToolStripButton_ItemBuilder"/> item.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ToolStripButton_ItemBuilder_Click(sender As Object, e As EventArgs) Handles ToolStripButton_ItemBuilder.Click

            Me.ShowPage(Me.Panel_ItemBuilder)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ToolStripItem.Click"/> event of the <see cref="ToolStripButton_StartupList"/> item.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ToolStripButton_StartupList_Click(sender As Object, e As EventArgs) Handles ToolStripButton_StartupList.Click

            Me.ShowPage(Me.Panel_StartupList)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.Click"/> event of the <see cref="Button_DeleteEntry"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Button_DeleteEntry_Click(sender As Object, e As EventArgs) Handles Button_DeleteEntry.Click

            If Me.DataGridView1.SelectedRows.Count = 0 Then
                Exit Sub
            End If

            Try
                Dim selectedRow As DataGridViewRow = Me.DataGridView1.SelectedRows(0)
                Dim selectedRowIndex As Integer = selectedRow.Index
                Dim startupTypeName As String = Main.GetCellText(selectedRow, Me.ColumnType)
                Dim regKeyFullPath As String = StartupRegistryKeys.GetKeyPathFromId(Me.GetStartupTypeId(selectedRow))
                Dim name As String = Main.GetCellText(selectedRow, Me.ColumnName)

                Using msgbox As New CenteredMessageBox(Me)
                    Dim result As DialogResult =
                        msgbox.Show($"{Me.strings.DeleteEntryQuestion}{Environment.NewLine}{Environment.NewLine}{startupTypeName}\{name}{Environment.NewLine}{Environment.NewLine}{Me.strings.CannotBeUndone}",
                                    My.Application.Info.Title,
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    If result = DialogResult.No Then
                        Exit Sub
                    End If
                End Using

                If RegistryUtil.ExistValue(RegistryView.Default, regKeyFullPath, name) Then
                    RegistryUtil.DeleteValue(RegistryView.Default, regKeyFullPath, name, throwOnMissingValue:=False)
                End If

                Me.lastSelectedRowNameAndStartupType = Nothing
                If Me.TryLoadRegistryItemsToDataGridView() AndAlso Me.DataGridView1.Rows.Count <> 0 Then
                    ' Selects the row that preceded the deleted one.
                    Dim rowToSelect As DataGridViewRow = Me.DataGridView1.Rows(Math.Max(0, Math.Min(selectedRowIndex - 1, Me.DataGridView1.Rows.Count - 1)))
                    Me.DataGridView1.ClearSelection()
                    rowToSelect.Selected = True
                    Me.DataGridView1.CurrentCell = rowToSelect.Cells(Me.ColumnName.Index)
                    Me.DataGridView1.FirstDisplayedScrollingRowIndex = rowToSelect.Index
                End If

            Catch ex As Exception
                Me.ShowErrorMessage(Me.strings.RegistryKeyError, ex)

            End Try

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.Click"/> event of the <see cref="Button_SendtoItemBuilder"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Button_SendtoItemBuilder_Click(sender As Object, e As EventArgs) Handles Button_SendtoItemBuilder.Click

            If Me.DataGridView1.SelectedRows.Count = 0 Then
                Exit Sub
            End If

            Try
                Dim selectedRow As DataGridViewRow = Me.DataGridView1.SelectedRows(0)
                Dim startupTypeId As String = Me.GetStartupTypeId(selectedRow)
                Dim startupTypeName As String = Main.GetCellText(selectedRow, Me.ColumnType)
                Dim name As String = Main.GetCellText(selectedRow, Me.ColumnName)
                Dim value As String = Main.GetCellText(selectedRow, Me.ColumnValue)

                Dim command As StartupCommand = Nothing
                Dim parsedSuccessfully As Boolean = StartupCommand.TryParse(value, command)

                Me.RemoveControlHints()
                Me.TextBox_Name.Text = name
                If parsedSuccessfully Then
                    Me.TextBox_File.Text = command.FilePath
                    Me.TextBox_Parameters.Text = command.Arguments
                Else
                    Me.TextBox_File.Text = value
                    Me.TextBox_Parameters.Text = String.Empty
                End If
                Me.SetControlHints()

                If startupTypeId = StartupRegistryKeys.CurrentUserId Then
                    Me.RadioButton_CurrentUser.Checked = True
                Else
                    Me.RadioButton_AllUsers.Checked = True
                End If

                If Not parsedSuccessfully Then
                    Using msgbox As New CenteredMessageBox(Me)
                        msgbox.Show($"{Me.strings.ParseFilePathFailed}{Environment.NewLine}{Environment.NewLine}{value}",
                                    My.Application.Info.Title,
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End Using
                End If

                Dim deleteSentEntry As DialogResult
                Using msgbox As New CenteredMessageBox(Me)
                    deleteSentEntry = msgbox.Show($"{Me.strings.DeleteSentEntryQuestion}{Environment.NewLine}{Environment.NewLine}{startupTypeName}\{name}{Environment.NewLine}{Environment.NewLine}{Me.strings.CannotBeUndone}",
                                                  My.Application.Info.Title,
                                                  MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2)
                End Using

                If deleteSentEntry = DialogResult.Yes Then
                    Dim regKeyFullPath As String = StartupRegistryKeys.GetKeyPathFromId(startupTypeId)
                    If RegistryUtil.ExistValue(RegistryView.Default, regKeyFullPath, name) Then
                        RegistryUtil.DeleteValue(RegistryView.Default, regKeyFullPath, name, throwOnMissingValue:=False)
                    End If

                    Me.lastSelectedRowNameAndStartupType = Nothing
                    Me.TryLoadRegistryItemsToDataGridView()
                End If

                Me.ShowPage(Me.Panel_ItemBuilder)

            Catch ex As Exception
                Me.ShowErrorMessage(Me.strings.RegistryKeyError, ex)

            End Try

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.Click"/> event of the <see cref="Button_OpenInRegedit"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Button_OpenInRegedit_Click(sender As Object, e As EventArgs) Handles Button_OpenInRegedit.Click

            If Me.DataGridView1.SelectedRows.Count = 0 Then
                Exit Sub
            End If

            Try
                Dim selectedRow As DataGridViewRow = Me.DataGridView1.SelectedRows(0)
                Dim regKeyFullPath As String = StartupRegistryKeys.GetKeyPathFromId(Me.GetStartupTypeId(selectedRow))

                ' Regedit opens directly the key stored in its "LastKey" value.
                Registry.SetValue(Main.RegeditAppletKeyPath, "LastKey", regKeyFullPath)
                Process.Start("regedit.exe")

            Catch ex As Exception
                Me.ShowErrorMessage(Me.strings.RegistryKeyError, ex)

            End Try

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.Click"/> event of the <see cref="Button_RefreshList"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub Button_RefreshList_Click(sender As Object, e As EventArgs) Handles Button_RefreshList.Click

            Me.TryLoadRegistryItemsToDataGridView()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="DataGridView.SelectionChanged"/> event of the <see cref="DataGridView1"/> control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

            If Me.DataGridView1.SelectedRows.Count > 0 AndAlso Not Me.isRefreshingDataGridViewItems Then
                Dim selectedRow As DataGridViewRow = Me.DataGridView1.SelectedRows(0)
                Me.lastSelectedRowNameAndStartupType = {Main.GetCellText(selectedRow, Me.ColumnName),
                                                        Me.GetStartupTypeId(selectedRow)}
            End If

            Me.UpdateStartupListButtons()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ToolStripItem.Click"/> event of the <see cref="ToolStripButton_About"/> item.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ToolStripButton_About_Click(sender As Object, e As EventArgs) Handles ToolStripButton_About.Click

            Using aboutBox As New AboutForm()
                aboutBox.ShowDialog(Me)
            End Using

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ToolStripItem.Click"/> event of the language menu items.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub LanguageMenuItems_Click(sender As Object, e As EventArgs) _
        Handles EnglishToolStripMenuItem.Click, SpanishToolStripMenuItem.Click, PortugueseToolStripMenuItem.Click

            Dim cultureName As String
            If sender Is Me.SpanishToolStripMenuItem Then
                cultureName = "es"
            ElseIf sender Is Me.PortugueseToolStripMenuItem Then
                cultureName = "pt"
            Else
                cultureName = "en"
            End If

            Me.ApplyFormLanguage(cultureName)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ToolStripToggleSwitch.CheckedChanged"/> event of the theme switch.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        '''
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ToolStripToggleSwitch_Theme_CheckedChanged(sender As Object, e As EventArgs) _
        Handles ToolStripToggleSwitch_Theme.CheckedChanged

            If Me.isApplyingTheme Then
                Exit Sub
            End If

            Dim theme As AppTheme = If(Me.ToolStripToggleSwitch_Theme.Checked, AppTheme.Dark, AppTheme.Light)

            My.Settings.Theme = ThemePreference.ToSettingValue(theme)
            My.Settings.Save()

            Me.ApplyTheme(theme)

        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Shows the specified page (<see cref="Panel_ItemBuilder"/> or <see cref="Panel_StartupList"/>),
        ''' and marks its navigation button as the active one.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="page">
        ''' The page to show.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="ArgumentException">
        ''' <paramref name="page"/> is not one of the pages of the form.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ShowPage(page As Panel)

            Dim isStartupList As Boolean
            If page Is Me.Panel_StartupList Then
                isStartupList = True
            ElseIf page Is Me.Panel_ItemBuilder Then
                isStartupList = False
            Else
                Throw New ArgumentException("Unknown page.", NameOf(page))
            End If

            Me.ToolStripButton_ItemBuilder.Checked = Not isStartupList
            Me.ToolStripButton_StartupList.Checked = isStartupList
            Me.ToolStripStatusLabel_EntryCount.Visible = isStartupList

            Me.Panel_ItemBuilder.Visible = Not isStartupList
            Me.Panel_StartupList.Visible = isStartupList

            If isStartupList Then
                Me.TryLoadRegistryItemsToDataGridView()
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Adjusts the height of the form so the item builder page fits its content exactly, 
        ''' without empty space at the bottom (the form is already scaled to the current DPI and font).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub FitClientHeightToItemBuilder()

            Dim builderHeight As Integer = Me.TableLayoutPanel_ItemBuilder.GetPreferredSize(New Size(Me.TableLayoutPanel_ItemBuilder.Width, 0)).Height

            Dim clientHeight As Integer = Me.ToolStrip_Header.Height +
                                          Me.ToolStrip_ItemBuilder.Height +
                                          builderHeight +
                                          Me.StatusStrip_Main.Height

            Me.ClientSize = New Size(Me.ClientSize.Width, clientHeight)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Loads the theme stored in the user settings (or the Windows theme, if there is no stored theme).
        ''' <para></para>
        ''' A corrupted setting value is discarded.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The theme to apply.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Function LoadThemeSetting() As AppTheme

            Dim isSystemDarkTheme As Boolean = ThemePreference.IsSystemDarkTheme()
            Try
                Return ThemePreference.Resolve(My.Settings.Theme, isSystemDarkTheme)

            Catch ex As FormatException
                ' The user settings file was edited by hand: reset the value, so the Windows theme is followed.
                My.Settings.Theme = String.Empty
                My.Settings.Save()
                Return ThemePreference.Resolve(String.Empty, isSystemDarkTheme)

            End Try

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the specified visual theme to the form, its menus, its title bar and the text hints,
        ''' and updates the state of the theme switch.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="theme">
        ''' The theme to apply.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ApplyTheme(theme As AppTheme)

            Me.isApplyingTheme = True
            Try
                Me.currentTheme = theme

                ' The hints must be removed while the colors change, because they replace the text color of the text boxes.
                Me.RemoveControlHints()

                ThemeManager.Apply(Me, theme)
                ThemeManager.ApplyToDropDown(Me.ContextMenuStrip_Recent, theme)
                Me.ToolStripToggleSwitch_Theme.Checked = (theme = AppTheme.Dark)

                Me.SetControlHints()

            Finally
                Me.isApplyingTheme = False

            End Try

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Assigns the images of the buttons, resized for the current DPI.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ApplyButtonImages()

            ' The form scales by font; the designer font height (17 pixels) corresponds to 96 DPI.
            Dim scale As Single = Me.CurrentAutoScaleDimensions.Height / Me.AutoScaleDimensions.Height
            Dim side As Integer = Math.Max(Main.ButtonImageSize, CInt(Math.Round(Main.ButtonImageSize * scale)))
            Dim size As New Size(side, side)

            Me.Button_AddToStartup.Icon = ImageUtil.ResizeImage(My.Resources.plus, size)
            Me.Button_DeleteEntry.Icon = ImageUtil.ResizeImage(My.Resources.delete, size)
            Me.Button_SendtoItemBuilder.Icon = ImageUtil.ResizeImage(My.Resources.pencil, size)
            Me.Button_OpenInRegedit.Icon = ImageUtil.ResizeImage(My.Resources.registry, size)
            Me.Button_RefreshList.Icon = ImageUtil.ResizeImage(My.Resources.refresh, size)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Applies the texts of the specified language to the user interface, and saves the language in the user settings.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="cultureName">
        ''' The name of the culture (<c>en</c>, <c>es</c> or <c>pt</c>). An unsupported language falls back to English.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ApplyFormLanguage(cultureName As String)

            Dim language As String = UIStrings.GetNeutralLanguage(cultureName)

            My.Settings.Culture = language
            My.Settings.Save()
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language)

            Me.RemoveControlHints()
            Me.strings = UIStrings.FromCulture(language)

            Me.SuspendLayout()
            Try
                Me.Text = My.Application.Info.Title

                ' Header.
                Me.ToolStripButton_ItemBuilder.Text = Me.strings.ItemBuilder
                Me.ToolStripButton_StartupList.Text = Me.strings.StartupList
                Me.ToolStripDropDownButton_Language.Text = Me.strings.Language
                Me.EnglishToolStripMenuItem.Text = Me.strings.English
                Me.SpanishToolStripMenuItem.Text = Me.strings.Spanish
                Me.PortugueseToolStripMenuItem.Text = Me.strings.Portuguese
                Me.EnglishToolStripMenuItem.Checked = (language = "en")
                Me.SpanishToolStripMenuItem.Checked = (language = "es")
                Me.PortugueseToolStripMenuItem.Checked = (language = "pt")
                Me.ToolStripButton_About.Text = Me.strings.About
                Me.ToolTip1.SetToolTip(Me.ToolStripToggleSwitch_Theme.ToggleSwitch, Me.strings.DarkTheme)
                Me.ToolStripToggleSwitch_Theme.ToggleSwitch.AccessibleName = Me.strings.DarkTheme

                ' Item builder page.
                Me.Label_Name.Text = Me.strings.NameCaption
                Me.Label_File.Text = Me.strings.FileCaption
                Me.Label_Arguments.Text = Me.strings.ArgumentsCaption
                Me.Label_StartupType.Text = Me.strings.StartupTypeCaption
                Me.Button_OpenFile.Text = Me.strings.Browse
                Me.RadioButton_CurrentUser.Text = Me.strings.CurrentUser
                Me.RadioButton_AllUsers.Text = Me.strings.AllUsers
                Me.ToolTip1.SetToolTip(Me.RadioButton_AllUsers, If(Main.IsProcessElevated, Nothing, Me.strings.AllUsersRequiresElevation))
                Me.ToolStripDropDownButton_Recent.Text = Me.strings.RecentFiles
                Me.Button_AddToStartup.Text = Me.strings.AddToStartup
                Me.clearRecentFilesMenuItem.Text = Me.strings.ClearRecentFiles

                ' Startup list page.
                Me.ColumnType.HeaderText = Me.strings.TypeColumn
                Me.ColumnIcon.HeaderText = Me.strings.IconColumn
                Me.ColumnName.HeaderText = Me.strings.NameColumn
                Me.ColumnValue.HeaderText = Me.strings.ValueColumn
                Me.Button_DeleteEntry.Text = Me.strings.DeleteEntry
                Me.Button_SendtoItemBuilder.Text = Me.strings.SendToItemBuilder
                Me.Button_OpenInRegedit.Text = Me.strings.OpenInRegedit
                Me.Button_RefreshList.Text = Me.strings.RefreshList

                ' Status bar.
                Me.UpdateElevationStatus()
                Me.UpdateEntryCount()

                ' The startup type column shows localized names.
                If Me.Panel_StartupList.Visible Then
                    Me.TryLoadRegistryItemsToDataGridView()
                End If

            Finally
                Me.ResumeLayout(performLayout:=True)

            End Try

            Me.SetControlHints()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Updates the elevation notice of the status bar.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub UpdateElevationStatus()

            Dim isElevated As Boolean = Main.IsProcessElevated
            Me.ToolStripStatusLabel_Elevation.Text = If(isElevated, Me.strings.RunningElevated, Me.strings.NotRunningElevated)
            Me.ToolStripStatusLabel_Elevation.Tag = If(isElevated, Nothing, ThemeManager.WarningTag)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Updates the number of startup entries shown in the status bar.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub UpdateEntryCount()

            Me.ToolStripStatusLabel_EntryCount.Text = Me.strings.FormatEntryCount(Me.DataGridView1.Rows.Count)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the text hints of the item builder text boxes, with the hint color of the current theme.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub SetControlHints()

            Dim hintColor As Color = ThemePalette.FromTheme(Me.currentTheme).HintText

            ControlHintManager.SetHint(Me.TextBox_Name, New ControlHintInfo(Me.strings.NameHint, Me.TextBox_Name.Font, hintColor, ControlHintType.Persistent))
            ControlHintManager.SetHint(Me.TextBox_File, New ControlHintInfo(Me.strings.FileHint, Me.TextBox_File.Font, hintColor, ControlHintType.Persistent))
            ControlHintManager.SetHint(Me.TextBox_Parameters, New ControlHintInfo(Me.strings.ArgumentsHint, Me.TextBox_Parameters.Font, hintColor, ControlHintType.Persistent))

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes the text hints of the item builder text boxes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub RemoveControlHints()

            ControlHintManager.RemoveHint(Me.TextBox_Name)
            ControlHintManager.RemoveHint(Me.TextBox_File)
            ControlHintManager.RemoveHint(Me.TextBox_Parameters)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Loads the specified (dropped) file into the item builder text boxes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="file">
        ''' The file to load.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub LoadDragDropFile(file As FileInfo)

            Me.RemoveControlHints()
            Me.TextBox_Name.Text = Path.GetFileNameWithoutExtension(file.Name)
            Me.TextBox_File.Text = file.FullName
            Me.SetControlHints()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Adds the file of the item builder to Windows startup.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the entry was added; otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Function AddFileToWindowsStartup() As Boolean

            Dim name As String = Me.TextBox_Name.Text
            Dim parameters As String = If(Me.TextBox_Parameters.Text = Me.strings.ArgumentsHint,
                                          String.Empty,
                                          Me.TextBox_Parameters.Text)

            ' Builds the command-line to store in the registry.
            ' The file path is always quoted, so any quotes typed by the user around the path are removed first.
            Dim command As StartupCommand
            Try
                command = New StartupCommand(StartupCommand.NormalizeUserFilePath(Me.TextBox_File.Text), parameters)

            Catch ex As ArgumentException
                Me.ShowErrorMessage(Me.strings.EntryAddFailed, ex)
                Return False

            End Try

            Dim entry As New RecentFileEntry(name, command.FilePath, command.Arguments)
            Dim regKeyFullPath As String = Me.startupType(Me.selectedStartupType)

            If RegistryUtil.ExistValue(RegistryView.Default, regKeyFullPath, name) Then
                Using msgbox As New CenteredMessageBox(Me)
                    Dim result As DialogResult = msgbox.Show($"{Me.strings.EntryAlreadyExists}{Environment.NewLine}{Environment.NewLine}{Me.strings.OverwriteEntryQuestion}",
                                                             My.Application.Info.Title,
                                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                             MessageBoxDefaultButton.Button2)
                    If result = DialogResult.No Then
                        Return False
                    End If
                End Using
            End If

            RegistryUtil.CreateValue(Of String)(RegistryView.Default, regKeyFullPath, name, command.ToCommandLine())

            If Not RegistryUtil.ExistValue(RegistryView.Default, regKeyFullPath, name) Then
                Using msgbox As New CenteredMessageBox(Me)
                    msgbox.Show(Me.strings.EntryAddFailed, My.Application.Info.Title,
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Using
                Return False
            End If

            RecentFileList.AddIfMissing(My.Settings.MRU, entry, Me.mruDelimiter)

            Using msgbox As New CenteredMessageBox(Me)
                msgbox.Show(Me.strings.EntryAdded, My.Application.Info.Title,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

            Return True

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Ensures that the recent file list of the user settings exists, and renews it.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub CheckRecentFiles()

            If My.Settings.MRU Is Nothing Then
                My.Settings.MRU = New StringCollection()
            End If

            Me.RenewRecentFiles()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Limits the recent file list to its maximum number of entries, and updates the recent files button.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub RenewRecentFiles()

            If RecentFileList.TrimToMaxCount(My.Settings.MRU, RecentFileList.MaxCount) Then
                My.Settings.Save()
            End If

            Me.ToolStripDropDownButton_Recent.Enabled = My.Settings.MRU.Count > 0

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Loads the startup entries into the startup list, showing an error message if the registry cannot be read.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> if the entries were loaded; otherwise, <see langword="False"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Function TryLoadRegistryItemsToDataGridView() As Boolean

            Try
                Me.LoadRegistryItemsToDataGridView()
                Return True

            Catch ex As Exception
                Me.ShowErrorMessage(Me.strings.RegistryValuesError, ex)
                Return False

            End Try

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Loads the startup entries into the startup list.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub LoadRegistryItemsToDataGridView()

            Me.isRefreshingDataGridViewItems = True
            Try
                Me.DataGridView1.Rows.Clear()

                Dim view As RegistryView = RegistryView.Default

                For Each pair As KeyValuePair(Of String, String) In Me.startupType

                    If pair.Key = StartupRegistryKeys.AllUsersId AndAlso Not Main.IsProcessElevated Then
                        Continue For
                    End If
                    Dim startupTypeName As String = If(pair.Key = StartupRegistryKeys.CurrentUserId, Me.strings.CurrentUser, Me.strings.AllUsers)

                    Dim fullKeyPath As String = pair.Value
                    If Not RegistryUtil.ExistSubKey(view, fullKeyPath) Then
                        Continue For
                    End If

                    Using root As RegistryKey = RegistryUtil.GetRootKey(fullKeyPath, view),
                          subKey As RegistryKey = root.OpenSubKey(RegistryUtil.GetSubKeyPath(fullKeyPath), writable:=False)

                        For Each valueName As String In subKey.GetValueNames()
                            ' Values are read as raw objects and converted to string,
                            ' so a non-string value (e.g. REG_DWORD) does not abort the loading of the whole list.
                            Dim rawData As Object = subKey.GetValue(valueName, Nothing, RegistryValueOptions.DoNotExpandEnvironmentNames)
                            Dim data As String = If(TypeOf rawData Is String(),
                                                    String.Join(" ", DirectCast(rawData, String())),
                                                    Convert.ToString(rawData, CultureInfo.InvariantCulture))

                            Dim fileIcon As Icon = Nothing
                            Try
                                Dim command As StartupCommand = Nothing
                                If StartupCommand.TryParse(data, command) Then
                                    fileIcon = ImageUtil.ExtractIconFromFile(command.GetExpandedFilePath())
                                End If
                            Catch ex As Exception
                                ' The icon is merely decorative; it is not a reason to abort loading the other entries.
                            End Try

                            Dim dgvRow As New DataGridViewRow()
                            Dim iconCell As New DataGridViewImageCell With {.Value = fileIcon, .ImageLayout = DataGridViewImageCellLayout.Zoom}
                            iconCell.Style.NullValue = Nothing ' No "missing image" glyph for entries without icon.
                            dgvRow.Cells.AddRange(New DataGridViewTextBoxCell With {.Value = startupTypeName, .Tag = pair.Key},
                                                  iconCell,
                                                  New DataGridViewTextBoxCell With {.Value = valueName},
                                                  New DataGridViewTextBoxCell With {.Value = data, .ToolTipText = data})
                            Me.DataGridView1.Rows.Add(dgvRow)
                        Next valueName

                    End Using

                Next pair

                Me.DataGridView1.Sort(Me.ColumnName, ListSortDirection.Ascending)
                Me.RestoreStartupListSelection()

            Finally
                Me.isRefreshingDataGridViewItems = False
                Me.UpdateEntryCount()
                Me.UpdateStartupListButtons()

            End Try

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Selects the row of the startup list that the user selected before the list was reloaded, 
        ''' or the first row if there is no previous selection (or the previous row no longer exists).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub RestoreStartupListSelection()

            Me.DataGridView1.ClearSelection()
            If Me.DataGridView1.Rows.Count = 0 Then
                Exit Sub
            End If

            Dim rowToSelect As DataGridViewRow = Me.DataGridView1.Rows(0)
            If Me.lastSelectedRowNameAndStartupType IsNot Nothing Then
                For Each row As DataGridViewRow In Me.DataGridView1.Rows
                    If Me.GetStartupTypeId(row) = Me.lastSelectedRowNameAndStartupType(1) AndAlso
                       String.Equals(Main.GetCellText(row, Me.ColumnName), Me.lastSelectedRowNameAndStartupType(0), StringComparison.OrdinalIgnoreCase) Then

                        rowToSelect = row
                        Exit For
                    End If
                Next row
            End If

            rowToSelect.Selected = True
            Me.DataGridView1.CurrentCell = rowToSelect.Cells(Me.ColumnName.Index)
            Me.DataGridView1.FirstDisplayedScrollingRowIndex = rowToSelect.Index

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Shows an error message box.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="message">
        ''' The localized message.
        ''' </param>
        '''
        ''' <param name="ex">
        ''' The exception, whose message is appended.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub ShowErrorMessage(message As String, ex As Exception)

            Using msgbox As New CenteredMessageBox(Me)
                msgbox.Show($"{message}{Environment.NewLine}{Environment.NewLine}{ex.Message}",
                            My.Application.Info.Title,
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Using

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the text of the specified cell of a startup list row.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="row">
        ''' The row.
        ''' </param>
        '''
        ''' <param name="column">
        ''' The column.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The cell text, or <see cref="String.Empty"/> if the cell has no value.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function GetCellText(row As DataGridViewRow, column As DataGridViewColumn) As String

            Return Convert.ToString(row.Cells(column.Index).Value, CultureInfo.InvariantCulture)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the startup type identifier of a startup list row 
        ''' (the type cell shows a localized name, and stores the identifier in its tag).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="row">
        ''' The row.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see cref="StartupRegistryKeys.CurrentUserId"/> or <see cref="StartupRegistryKeys.AllUsersId"/>.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="InvalidOperationException">
        ''' The row has no startup type identifier.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        Private Function GetStartupTypeId(row As DataGridViewRow) As String

            Dim id As String = TryCast(row.Cells(Me.ColumnType.Index).Tag, String)
            If String.IsNullOrEmpty(id) Then
                Throw New InvalidOperationException($"The startup list row {row.Index} has no startup type identifier.")
            End If

            Return id

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Enables the startup list buttons that require a selected row only when there is a selected row.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub UpdateStartupListButtons()

            Dim hasSelectedRow As Boolean = Me.DataGridView1.SelectedRows.Count > 0

            Me.Button_DeleteEntry.Enabled = hasSelectedRow
            Me.Button_SendtoItemBuilder.Enabled = hasSelectedRow
            Me.Button_OpenInRegedit.Enabled = hasSelectedRow

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the path of the first file of a drag and drop operation.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="data">
        ''' The data of the drag and drop operation.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The path of the first dropped file, or <see langword="Nothing"/> if the data does not contain files.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function GetDroppedFilePath(data As IDataObject) As String

            If data Is Nothing OrElse Not data.GetDataPresent(DataFormats.FileDrop) Then
                Return Nothing
            End If

            Dim filePaths As String() = TryCast(data.GetData(DataFormats.FileDrop), String())
            Return If(filePaths IsNot Nothing AndAlso filePaths.Length <> 0, filePaths(0), Nothing)

        End Function

#End Region

    End Class

End Namespace

#End Region
