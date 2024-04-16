' ***********************************************************************
' Author   : Administrador
' Modified : 06-April-2024
' ***********************************************************************
' <copyright file="Main.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Imports "

Imports Microsoft.Win32

Imports System.ComponentModel

Imports DevCase.Core.Application.UserInterface.Enums
Imports DevCase.Core.Application.UserInterface.Types
Imports DevCase.Core.Imaging.Tools
Imports DevCase.Core.System.Tools
Imports System.IO
Imports System.Globalization
Imports System.Threading
Imports DevCase.Core.Application.Forms
Imports System.Security.Principal


#End Region

Namespace UserInterface

    ''' <summary>
    ''' The Main user interface Form.
    ''' </summary>
    Public NotInheritable Class Main

#Region " Properties "

        ''' <summary>
        ''' A <see cref="Boolean" /> value indicating if the current process has full administrative rights
        ''' </summary>
        Public Shared ReadOnly Property IsProcessElevated As Boolean
            Get
                Dim owner As WindowsIdentity = WindowsIdentity.GetCurrent()
                Return owner IsNot Nothing AndAlso New WindowsPrincipal(owner).IsInRole(WindowsBuiltInRole.Administrator)
            End Get
        End Property

#End Region

#Region " Localizable Fields "

        Private textBoxNameHint As String =
            "Unique startup entry name for the file (Drop a file here to auto-assign)."

        Private textBoxFileHint As String =
            "Full path to the file (Drop a file here to auto-assign)."

        Private textBoxParametersHint As String =
            "Optional arguments (for an executable file only)."

        Private msgq1 As String = "Do you really want to delete the following entry?:"
        Private msgq2 As String = "This action cannot be undone."
        Private msgq3 As String = "An item with the same name already exists in Windows startup."
        Private msgq4 As String = "Do you really want to overwrite the item?."
        Private msgq5 As String = "Item successfully added to Windows startup"
        Private msgq6 As String = "Failed to add the item to Windows startup"
        Private msgq7 As String = "Program is not running elevated, some features are disabled."
        Private msgq8 As String = "Clear recent file list"

#End Region

#Region " Fields "

        ''' <summary>
        ''' The <see cref="WindowSticker"/> instance that sticks the Form on the Desktop screen.
        ''' </summary>
        Private windowSticker As WindowMagnetizer

        ''' <summary>
        ''' Indicates the startup type, for current user only or for all users.
        ''' </summary>
        Private ReadOnly startupType As New Dictionary(Of String, String) From
        {
            {"CurrentUserKey", "HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"},
            {"AllUsersKey", "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"}
        }

        ''' <summary>
        ''' The startup type ("CurrentUserKey" or "AllUsersKey").
        ''' </summary>
        ''' <value>The startup type.</value>
        Private SelectedStartupType As String

        Private ReadOnly mruDelimiter As String = $"@@{My.Application.Info.ProductName}@@"

        Private ClearRecentFilesMenuItem As New ToolStripMenuItem(
            Me.msgq8, Nothing, Sub()
                                   My.Settings.MRU?.Clear()
                                   Me.RenewRecentfiles()
                               End Sub) With {
                               .DisplayStyle = ToolStripItemDisplayStyle.ImageAndText,
                               .Image = My.Resources.delete
        }

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Main"/> class.
        ''' </summary>
        Public Sub New()

            ' This call is required by the designer.
            Me.InitializeComponent()

            Me.ApplyFormLanguage(My.Settings.Culture)

        End Sub

#End Region

#Region " Event Handlers "

        ''' <summary>
        ''' Handles the Load event of the Form.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            Me.MinimumSize = Me.Size
            Me.CheckRecentFiles()

            Dim isElevated As Boolean = IsProcessElevated
            Me.RadioButton_AllUsers.Enabled = isElevated

        End Sub

        ''' <summary>
        ''' Handles the Shown event of the Form.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub Main_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown

            Me.windowSticker = New WindowMagnetizer(window:=Me) With {.Threshold = 35, .AllowOffscreen = True, .Enabled = True}

            If My.Application.CommandLineArgs.Count <> 0 Then

                If IO.File.Exists(My.Application.CommandLineArgs.First) Then
                    Me.LoadDragDropFile(New IO.FileInfo(My.Application.CommandLineArgs.First))
                End If

            End If

        End Sub

        ''' <summary>
        ''' Handles the DragEnter event of the Form.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        Private Sub Form_DragEnter(sender As Object, e As DragEventArgs) _
        Handles Me.DragEnter

            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim item As String = e.Data.GetData(DataFormats.FileDrop)(0)
                If File.Exists(item) Then
                    e.Effect = DragDropEffects.All
                End If
            End If

        End Sub

        ''' <summary>
        ''' Handles the DragDrop event of the Form.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        Private Sub Form_DragDrop(sender As Object, e As DragEventArgs) _
        Handles Me.DragDrop

            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim item As String = e.Data.GetData(DataFormats.FileDrop)(0)
                Me.LoadDragDropFile(New IO.FileInfo(item))
            End If

        End Sub

        ''' <summary>
        ''' Handles the _TextChanged event of the RadioButtonsTextBoxes.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub TextBoxes_TextChanged(sender As Object, e As EventArgs) _
        Handles TextBox_Name.TextChanged, TextBox_File.TextChanged

            Me.Button_AddToStartup.Enabled = (Not String.IsNullOrEmpty(Me.TextBox_File.Text) AndAlso
            Not String.IsNullOrEmpty(Me.TextBox_Name.Text) AndAlso
            Not Me.TextBox_File.Text = Me.textBoxFileHint AndAlso
            Not Me.TextBox_Name.Text = Me.textBoxNameHint)

        End Sub

        ''' <summary>
        ''' Handles the CheckedChanged event of the RadioButtons.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub RadioButtons_CheckedChanged(sender As Object, e As EventArgs) _
        Handles RadioButton_CurrentUser.CheckedChanged, RadioButton_AllUsers.CheckedChanged

            Me.SelectedStartupType = CStr(sender.tag)

            If String.IsNullOrEmpty(Me.SelectedStartupType) Then
                Me.SelectedStartupType = "CurrentUserKey"
            End If

        End Sub

        ''' <summary>
        ''' Handles the Click event of the Button_AddToStartup control.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        Private Sub Button_AddToStartup_Click(sender As Object, e As EventArgs) _
        Handles Button_AddToStartup.Click

            Dim success As Boolean = Me.AddFileToWindowsStartup()
            If success Then
                Me.RemoveControlHints()
                Me.TextBox_File.Clear()
                Me.TextBox_Name.Clear()
                Me.TextBox_Parameters.Clear()
                Me.SetControlHints()

                Me.RenewRecentfiles()
            End If

        End Sub

        ''' <summary>
        ''' Handles the Opening event of the ContextMenuStrip_Recent control.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        Private Sub ContextMenuStrip_Recent_Opening(sender As Object, e As CancelEventArgs) _
        Handles ContextMenuStrip_Recent.Opening

            sender.Items.Clear()

            If My.Settings.MRU.Count <> 0I Then

                For index As Integer = 0 To (My.Settings.MRU.Count - 1)

                    Dim name As String = My.Settings.MRU(index).Split({Me.mruDelimiter}, StringSplitOptions.None)(0)
                    Dim path As String = My.Settings.MRU(index).Split({Me.mruDelimiter}, StringSplitOptions.None)(1)
                    Dim parameters As String = My.Settings.MRU(index).Split({Me.mruDelimiter}, StringSplitOptions.None)(2)

                    Dim menuItem As New ToolStripMenuItem
                    With menuItem
                        .Text = name
                        .Tag = index
                        Try
                            Dim ico As Icon = ImageUtil.ExtractIconFromFile(path)
                            .Image = ico.ToBitmap()
                            ico?.Dispose()
                        Catch ex As Exception
                        End Try
                    End With

                    AddHandler menuItem.Click, AddressOf Me.IToolStripMenuItem_Click

                    sender.Items.Insert(0, menuItem)

                Next index

                e.Cancel = False

            Else
                e.Cancel = True

            End If

            sender.Items.Insert(0, Me.ClearRecentFilesMenuItem)
        End Sub

        ''' <summary>
        ''' Handles the Click event of the IToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub IToolStripMenuItem_Click(sender As Object, e As EventArgs)

            Me.RemoveControlHints()

            Dim name As String = My.Settings.MRU(CInt(sender.tag)).Split({Me.mruDelimiter}, StringSplitOptions.None)(0)
            Dim path As String = My.Settings.MRU(CInt(sender.tag)).Split({Me.mruDelimiter}, StringSplitOptions.None)(1)
            Dim parameters As String = My.Settings.MRU(CInt(sender.tag)).Split({Me.mruDelimiter}, StringSplitOptions.None)(2)

            Me.TextBox_Name.Text = name
            Me.TextBox_File.Text = path
            Me.TextBox_Parameters.Text = parameters

        End Sub

        ''' <summary>
        ''' Handles the Click event of the Button_OpenFile control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        Private Sub Button_OpenFile_Click(sender As Object, e As EventArgs) _
        Handles Button_OpenFile.Click

            Me.OpenFileDialog1.ShowDialog(Me)

        End Sub

        ''' <summary>
        ''' Handles the FileOk event of the OpenFileDialog1 control.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
        Private Sub OpenFileDialog1_FileOk(sender As Object, e As CancelEventArgs) _
        Handles OpenFileDialog1.FileOk

            Dim filePath As String = DirectCast(sender, OpenFileDialog).FileName

            ControlHintManager.RemoveHint(Me.TextBox_File)
            Me.TextBox_File.Text = filePath

            If String.IsNullOrWhiteSpace(Me.TextBox_Name.Text) OrElse Me.TextBox_Name.Text = Me.textBoxNameHint Then
                ControlHintManager.RemoveHint(Me.TextBox_Name)
                Me.TextBox_Name.Text = IO.Path.GetFileNameWithoutExtension(filePath)
            End If

            Me.SetControlHints()

        End Sub

        Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) _
        Handles TabControl1.SelectedIndexChanged

            If Me.TabControl1.SelectedTab.Equals(Me.TabPage2) Then
                Try
                    Me.LoadRegistryItemsToDataGridView()
                Catch ex As Exception
                    Using msgbox As New CenteredMessageBox(Me)
                        msgbox.Show("Error trying to parse registry values. Error message:" &
                                    Environment.NewLine & Environment.NewLine & ex.Message,
                                    My.Application.Info.Title,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Using

                End Try

                Me.TabPage2.Show()
            Else
                Me.TabPage1.Show()

            End If

        End Sub

        Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton_ItemBuilder.Click

            ' Tries to ensure that the tab page is shown.
            ' https://github.com/ElektroStudios/File2Startup/issues/2

            Me.TabControl1.Show()
            Me.TabControl1.SelectTab(Me.TabPage1)
            Me.TabPage1.Show()

        End Sub

        Private Sub ToolStripButton3_Click(sender As Object, e As EventArgs) Handles ToolStripButton_StartupList.Click

            Me.TabControl1.Show()
            Me.TabControl1.SelectTab(Me.TabPage2)
            Me.TabPage2.Show()

        End Sub

        Private Sub Button_DeleteEntry_Click(sender As Object, e As EventArgs) Handles Button_DeleteEntry.Click
            Try
                If Me.DataGridView1.SelectedRows IsNot Nothing Then

                    Dim selectedRow As DataGridViewRow = Me.DataGridView1.SelectedRows(0)
                    Dim startupType As String = selectedRow.Cells(0).Value
                    Dim regKeyFullPath As String = If(startupType = "Current User", Me.startupType("CurrentUserKey"), Me.startupType("AllUsersKey"))
                    Dim name As String = selectedRow.Cells(2).Value

                    Using msgbox As New CenteredMessageBox(Me)

                        Dim result As DialogResult = msgbox.Show(Me.msgq1 &
                                                                     Environment.NewLine & Environment.NewLine & $"{startupType}\{name}" &
                                                                     Environment.NewLine & Environment.NewLine & Me.msgq2,
                                                                     My.Application.Info.Title,
                                                                     MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                                     MessageBoxDefaultButton.Button2)
                        If result = DialogResult.No Then
                            Exit Sub
                        End If
                    End Using

                    Dim regValueExists As Boolean = RegistryUtil.ExistValue(RegistryView.Default, regKeyFullPath, name)
                    If regValueExists Then
                        RegistryUtil.DeleteValue(RegistryView.Default, regKeyFullPath, name, throwOnMissingValue:=False)
                    End If

                    Try
                        Me.LoadRegistryItemsToDataGridView()

                    Catch ex As Exception
                        Using msgbox As New CenteredMessageBox(Me)
                            msgbox.Show("Error trying to parse registry values. Error message:" &
                                        Environment.NewLine & Environment.NewLine & ex.Message,
                                        My.Application.Info.Title,
                                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End Using

                    End Try
                End If

            Catch ex As Exception
                Using msgbox As New CenteredMessageBox(Me)
                    msgbox.Show("Error trying to parse registry key. Error message:" &
                                Environment.NewLine & Environment.NewLine & ex.Message,
                                My.Application.Info.Title,
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Using

            End Try

        End Sub

        Private Sub Button_SendtoItemBuilder_Click(sender As Object, e As EventArgs) Handles Button_SendtoItemBuilder.Click

            Try
                If Me.DataGridView1.SelectedRows IsNot Nothing Then
                    Dim selectedRow As DataGridViewRow = Me.DataGridView1.SelectedRows(0)
                    Dim startupType As String = selectedRow.Cells(0).Value
                    Dim name As String = selectedRow.Cells(2).Value
                    Dim value As String = selectedRow.Cells(3).Value

                    Dim filePath As String = value
                    If value.Contains(ControlChars.Quote) Then
                        Try
                            filePath = IO.Path.GetFullPath(value.TrimStart({ControlChars.Quote, " "c}).Substring(0, value.TrimStart({ControlChars.Quote, " "c}).IndexOf(ControlChars.Quote)).TrimEnd(ControlChars.Quote))
                        Catch ex As Exception
                        End Try
                    End If

                    Dim parameters As String =
                        If(value.Contains(ControlChars.Quote),
                           value?.TrimStart({ControlChars.Quote, " "c}).Substring(filePath.Length).TrimStart({ControlChars.Quote, " "c}),
                           value?.Substring(value.IndexOf(filePath) + filePath.Length))

                    Me.RemoveControlHints()
                    Me.TextBox_Name.Text = name
                    Me.TextBox_File.Text = filePath
                    Me.TextBox_Parameters.Text = parameters
                    Me.SetControlHints()

                    If startupType = "Current User" Then
                        Me.RadioButton_CurrentUser.Checked = True
                    Else
                        Me.RadioButton_AllUsers.Checked = True
                    End If

                    Me.TabControl1.Show()
                    Me.TabControl1.SelectTab(Me.TabPage1)
                    Me.TabPage1.Show()

                End If

            Catch ex As Exception
                Using msgbox As New CenteredMessageBox(Me)
                    msgbox.Show("Error trying to parse registry key. Error message:" &
                                Environment.NewLine & Environment.NewLine & ex.Message,
                                My.Application.Info.Title,
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Using

            End Try

        End Sub

        Private Sub Button_OpenInRegedit_Click(sender As Object, e As EventArgs) Handles Button_OpenInRegedit.Click

            Try
                If Me.DataGridView1.SelectedRows IsNot Nothing Then
                    Dim selectedRow As DataGridViewRow = Me.DataGridView1.SelectedRows(0)
                    Dim startupType As String = selectedRow.Cells(0).Value
                    Dim regKeyFullPath As String = If(startupType = "Current User", Me.startupType("CurrentUserKey"), Me.startupType("AllUsersKey"))
                    Dim name As String = selectedRow.Cells(2).Value

                    Dim registryLastKey As String = "HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Applets\Regedit"

                    Registry.SetValue(registryLastKey, "LastKey", regKeyFullPath) ' Establecer el valor LastKey que Regedit abrirá directamente
                    Process.Start("regedit.exe")
                End If

            Catch ex As Exception
                Using msgbox As New CenteredMessageBox(Me)
                    msgbox.Show("Error trying to parse registry key. Error message:" &
                                Environment.NewLine & Environment.NewLine & ex.Message,
                                My.Application.Info.Title,
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Using

            End Try

        End Sub

        Private Sub Button_RefreshList_Click(sender As Object, e As EventArgs) Handles Button_RefreshList.Click

            Try
                Me.LoadRegistryItemsToDataGridView()

            Catch ex As Exception
                Using msgbox As New CenteredMessageBox(Me)
                    msgbox.Show("Error trying to parse registry values. Error message:" &
                                Environment.NewLine & Environment.NewLine & ex.Message,
                                My.Application.Info.Title,
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Using

            End Try

        End Sub

        Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged

            Dim dgv As DataGridView = DirectCast(sender, DataGridView)
            Dim hasSelectedrow As Boolean = dgv.SelectedRows IsNot Nothing

            Me.Button_DeleteEntry.Enabled = hasSelectedrow
            Me.Button_SendtoItemBuilder.Enabled = hasSelectedrow
            Me.Button_OpenInRegedit.Enabled = hasSelectedrow

        End Sub

        Private Sub ToolStripButton_About_Click(sender As Object, e As EventArgs) Handles ToolStripButton_About.Click

            Using aboutBox As New AboutForm()
                aboutBox.ShowDialog()
            End Using

        End Sub

        Private Sub EnglishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnglishToolStripMenuItem.Click

            Me.ApplyFormLanguage("en")

        End Sub

        Private Sub SpanishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpanishToolStripMenuItem.Click

            Me.ApplyFormLanguage("es")

        End Sub

        Private Sub PortugueseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PortugueseToolStripMenuItem.Click

            Me.ApplyFormLanguage("pt")

        End Sub

#End Region

#Region " Methods "

        ''' <summary>
        ''' Sets the control hints.
        ''' </summary>
        Private Sub SetControlHints()
            ControlHintManager.SetHint(Me.TextBox_Name, New ControlHintInfo(Me.textBoxNameHint, Me.TextBox_Name.Font, SystemColors.GrayText, ControlHintType.Persistent))
            ControlHintManager.SetHint(Me.TextBox_File, New ControlHintInfo(Me.textBoxFileHint, Me.TextBox_File.Font, SystemColors.GrayText, ControlHintType.Persistent))
            ControlHintManager.SetHint(Me.TextBox_Parameters, New ControlHintInfo(Me.textBoxParametersHint, Me.TextBox_Parameters.Font, SystemColors.GrayText, ControlHintType.Persistent))
        End Sub

        ''' <summary>
        ''' Removes the control hints.
        ''' </summary>
        Private Sub RemoveControlHints()
            ControlHintManager.RemoveHint(Me.TextBox_Name)
            ControlHintManager.RemoveHint(Me.TextBox_File)
            ControlHintManager.RemoveHint(Me.TextBox_Parameters)
        End Sub

        ''' <summary>
        ''' Loads the dropped file into the textboxes.
        ''' </summary>
        ''' <param name="File">The file to load.</param>
        Private Sub LoadDragDropFile(file As IO.FileInfo)

            Me.RemoveControlHints()
            Me.TextBox_Name.Text = Path.GetFileNameWithoutExtension(file.Name)
            Me.TextBox_File.Text = file.FullName
            Me.SetControlHints()

        End Sub

        ''' <summary>
        ''' Adds the file to the registry startup.
        ''' </summary>
        Private Function AddFileToWindowsStartup() As Boolean

            Dim entry As String = String.Format("{0}" & Me.mruDelimiter & "{1}" & Me.mruDelimiter & "{2}",
                                                Me.TextBox_Name.Text,
                                                Me.TextBox_File.Text,
                                                If(Me.TextBox_Parameters.Text = Me.textBoxParametersHint,
                                                   String.Empty,
                                                   Me.TextBox_Parameters.Text))

            Dim name As String = entry.Split({Me.mruDelimiter}, StringSplitOptions.None)(0)
            Dim path As String = entry.Split({Me.mruDelimiter}, StringSplitOptions.None)(1)
            Dim parameters As String = entry.Split({Me.mruDelimiter}, StringSplitOptions.None)(2)

            Dim regValueExists As Boolean =
                RegistryUtil.ExistValue(RegistryView.Default, Me.startupType(Me.SelectedStartupType), name)

            If regValueExists Then
                Using msgbox As New CenteredMessageBox(Me)
                    Dim result As DialogResult = msgbox.Show(Me.msgq3 & Environment.NewLine & Environment.NewLine & Me.msgq4,
                                                                 My.Application.Info.Title,
                                                                 MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                                                 MessageBoxDefaultButton.Button2)
                    If result = DialogResult.No Then
                        Return False
                    End If
                End Using
            End If

            RegistryUtil.CreateValue(Of String)(RegistryView.Default, Me.startupType(Me.SelectedStartupType),
                                                name,
                                                String.Format("""{0}"" {1}", path, parameters))

            Select Case RegistryUtil.ExistValue(RegistryView.Default, Me.startupType(Me.SelectedStartupType), name)

                Case True
                    Dim itemExists As Boolean = My.Settings.MRU.Cast(Of String).Any(Function(item As String) item.Equals(entry, StringComparison.OrdinalIgnoreCase))
                    If Not itemExists Then
                        My.Settings.MRU.Add(entry)
                    End If

                    Using msgbox As New CenteredMessageBox(Me)
                        msgbox.Show(Me.msgq5, My.Application.Info.Title,
                                    MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using

                Case False
                    Using msgbox As New CenteredMessageBox(Me)
                        msgbox.Show(Me.msgq6, My.Application.Info.Title,
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Using
                    Return False

            End Select

            Return True

        End Function

        ''' <summary>
        ''' Checks whether the recent files object is empty, then instances a new one.
        ''' </summary>
        Private Sub CheckRecentFiles()

            If My.Settings.MRU Is Nothing Then
                My.Settings.MRU = New System.Collections.Specialized.StringCollection
            End If

            Me.RenewRecentfiles()

        End Sub

        ''' <summary>
        ''' Renews the recentfiles to controls the maximum number of entries.
        ''' </summary>
        Private Sub RenewRecentfiles()

            Dim count As Integer = My.Settings.MRU.Count
            If count > 10 Then
                Do Until My.Settings.MRU.Count = 10
                    My.Settings.MRU.RemoveAt(0)
                Loop
                My.Settings.Save()
            End If

            Me.ToolStripDropDownButton_Recent.Enabled = count > 0

        End Sub

        Private Sub LoadRegistryItemsToDataGridView()

            ' Tries to solve issue: https://github.com/ElektroStudios/File2Startup/issues/2
            ' Me.DataGridView1.SuspendLayout()
            Me.DataGridView1.Rows.Clear()

            Dim view As RegistryView = RegistryView.Default

            For i As Integer = 0 To Me.startupType.Values.Count - 1

                Dim startupType As String = If(Me.startupType.Keys(i) = "CurrentUserKey", "Current User", "All Users")
                If startupType = "All Users" AndAlso Not IsProcessElevated Then

                    Continue For
                End If

                Dim fullKeyPath As String = Me.startupType.Values(i)

                If RegistryUtil.ExistSubKey(view, fullKeyPath) Then

                    Using root As RegistryKey = RegistryUtil.GetRootKey(fullKeyPath, view),
                        subKey As RegistryKey = root.OpenSubKey(RegistryUtil.GetSubKeyPath(fullKeyPath), writable:=True)

                        Dim valueNames() As String = subKey.GetValueNames()

                        For Each valueName As String In valueNames
                            Dim data As String = RegistryUtil.GetValueData(Of String)(view, fullKeyPath, valueName, RegistryValueOptions.DoNotExpandEnvironmentNames)

                            Dim fileIcon As Icon = Nothing
                            Try
                                Dim filePath As String = data
                                If filePath.Contains(ControlChars.Quote) Then
                                    filePath = Path.GetFullPath(filePath.TrimStart({ControlChars.Quote, " "c}).Substring(0, filePath.TrimStart({ControlChars.Quote, " "c}).IndexOf(ControlChars.Quote)).TrimEnd(ControlChars.Quote))
                                End If

                                fileIcon = ImageUtil.ExtractIconFromFile(filePath)
                            Catch ex As Exception
                                ' Ignore error on building file path to retrieve icon file,
                                ' as it is not enough reason to abort loading other items in the Data Grid View.

                            End Try

                            Dim dgvRow As New DataGridViewRow()
                            Dim cellType As New DataGridViewTextBoxCell With {.Value = startupType}
                            Dim cellIcon As New DataGridViewImageCell With {.Value = fileIcon, .ImageLayout = DataGridViewImageCellLayout.Zoom}
                            Dim cellName As New DataGridViewTextBoxCell With {.Value = valueName}
                            Dim cellData As New DataGridViewTextBoxCell With {.Value = data}

                            dgvRow.Cells.AddRange(cellType, cellIcon, cellName, cellData)
                            Me.DataGridView1.Rows.Add(dgvRow)

                        Next valueName

                    End Using

                End If

            Next

            ' Tries to solve issue: https://github.com/ElektroStudios/File2Startup/issues/2
            ' Me.DataGridView1.ResumeLayout()
            ' Me.DataGridView1.PerformLayout()

        End Sub

        Private Sub ApplyFormLanguage(cultureName As String)

            My.Settings.Culture = cultureName
            My.Settings.Save()
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureName)

            Dim culture As CultureInfo = Thread.CurrentThread.CurrentUICulture
            Dim resources As New ComponentResourceManager(Me.GetType())

            Dim enabledButton_AddToStartup As Boolean = Me.Button_AddToStartup.Enabled
            Dim enabledButton_DeleteEntry As Boolean = Me.Button_DeleteEntry.Enabled
            Dim enabledButton_SendtoItemBuilder As Boolean = Me.Button_SendtoItemBuilder.Enabled
            Dim enabledButton_OpenInRegedit As Boolean = Me.Button_OpenInRegedit.Enabled
            Dim checkedRadioButton_CurrentUser As Boolean = Me.RadioButton_CurrentUser.Checked
            Dim boundsButton_AddToStartup As Rectangle = Me.Button_AddToStartup.Bounds
            Dim boundsButton_DeleteEntry As Rectangle = Me.Button_DeleteEntry.Bounds
            Dim boundsButton_SendtoItemBuilder As Rectangle = Me.Button_SendtoItemBuilder.Bounds
            Dim boundsButton_OpenInRegedit As Rectangle = Me.Button_OpenInRegedit.Bounds
            Dim boundsButton_RefreshList As Rectangle = Me.Button_RefreshList.Bounds

            resources.ApplyResources(Me, Me.Name, culture)

            resources.ApplyResources(Me.ToolStripButton_ItemBuilder, Me.ToolStripButton_ItemBuilder.Name, culture)
            resources.ApplyResources(Me.ToolStripButton_StartupList, Me.ToolStripButton_StartupList.Name, culture)
            resources.ApplyResources(Me.ToolStripButton_About, Me.ToolStripButton_About.Name, culture)
            resources.ApplyResources(Me.ToolStripDropDownButton2, Me.ToolStripDropDownButton2.Name, culture)
            resources.ApplyResources(Me.ToolStripDropDownButton_Recent, Me.ToolStripDropDownButton_Recent.Name, culture)

            resources.ApplyResources(Me.EnglishToolStripMenuItem, Me.EnglishToolStripMenuItem.Name, culture)
            resources.ApplyResources(Me.SpanishToolStripMenuItem, Me.SpanishToolStripMenuItem.Name, culture)
            resources.ApplyResources(Me.PortugueseToolStripMenuItem, Me.PortugueseToolStripMenuItem.Name, culture)

            resources.ApplyResources(Me.Button_AddToStartup, Me.Button_AddToStartup.Name, culture)
            resources.ApplyResources(Me.Button_DeleteEntry, Me.Button_DeleteEntry.Name, culture)
            resources.ApplyResources(Me.Button_OpenInRegedit, Me.Button_OpenInRegedit.Name, culture)
            resources.ApplyResources(Me.Button_RefreshList, Me.Button_RefreshList.Name, culture)
            resources.ApplyResources(Me.Button_SendtoItemBuilder, Me.Button_SendtoItemBuilder.Name, culture)

            resources.ApplyResources(Me.Label1, Me.Label1.Name, culture)
            resources.ApplyResources(Me.Label2, Me.Label2.Name, culture)
            resources.ApplyResources(Me.Label3, Me.Label3.Name, culture)
            resources.ApplyResources(Me.Label4, Me.Label4.Name, culture)

            resources.ApplyResources(Me.RadioButton_AllUsers, Me.RadioButton_AllUsers.Name, culture)
            resources.ApplyResources(Me.RadioButton_CurrentUser, Me.RadioButton_CurrentUser.Name, culture)

            resources.ApplyResources(AboutForm.LabelLoadedAssemblies, AboutForm.LabelLoadedAssemblies.Name, culture)

            Me.Button_AddToStartup.Enabled = enabledButton_AddToStartup
            Me.Button_DeleteEntry.Enabled = enabledButton_DeleteEntry
            Me.Button_SendtoItemBuilder.Enabled = enabledButton_SendtoItemBuilder
            Me.Button_OpenInRegedit.Enabled = enabledButton_OpenInRegedit
            Me.RadioButton_CurrentUser.Checked = checkedRadioButton_CurrentUser
            Me.Button_AddToStartup.Bounds = boundsButton_AddToStartup
            Me.Button_DeleteEntry.Bounds = boundsButton_DeleteEntry
            Me.Button_SendtoItemBuilder.Bounds = boundsButton_SendtoItemBuilder
            Me.Button_OpenInRegedit.Bounds = boundsButton_OpenInRegedit
            Me.Button_RefreshList.Bounds = boundsButton_RefreshList

            Me.TabPage1.Text = Me.ToolStripButton_ItemBuilder.Text
            Me.TabPage2.Text = Me.ToolStripButton_StartupList.Text

            Me.RemoveControlHints()

            Select Case culture.Name
                Case "en"
                    Me.textBoxNameHint = "Unique startup entry name for the file (Drop a file here to auto-assign)."
                    Me.textBoxFileHint = "Full path to the file (Drop a file here to auto-assign)."
                    Me.textBoxParametersHint = "Optional arguments (for an executable file only)."
                    Me.msgq1 = "Do you really want to delete the following entry?:"
                    Me.msgq2 = "This action cannot be undone."
                    Me.msgq3 = "An entry with the same name already exists in Windows startup."
                    Me.msgq4 = "Do you really want to overwrite the entry?."
                    Me.msgq5 = "Item successfully added to Windows startup."
                    Me.msgq6 = "Failed to add the item to Windows startup."
                    Me.msgq7 = "Program is not running elevated, some features are disabled."
                    Me.msgq8 = "Clear recent file list"

                    Me.DataGridView1.Columns(0).HeaderText = "Type"
                    Me.DataGridView1.Columns(1).HeaderText = "Icon"
                    Me.DataGridView1.Columns(2).HeaderText = "Name"
                    Me.DataGridView1.Columns(3).HeaderText = "Value"

                Case "es"
                    Me.textBoxNameHint = "Nombre único de entrada de inicio para el archivo (arrastre un archivo aquí para asignarlo automáticamente)."
                    Me.textBoxFileHint = "Ruta completa al archivo (arrastre un archivo aquí para asignarlo automáticamente)."
                    Me.textBoxParametersHint = "Argumentos opcionales (sólo para un archivo ejecutable)."
                    Me.msgq1 = "¿Realmente desea eliminar la siguiente entrada?:"
                    Me.msgq2 = "Esta acción no se podrá deshacer."
                    Me.msgq3 = "Ya existe una entrada con el mismo nombre en el inicio de Windows."
                    Me.msgq4 = "¿Realmente desea sobrescribir la entrada?."
                    Me.msgq5 = "Elemento agregado exitosamente al inicio de Windows."
                    Me.msgq6 = "Error al agregar el elemento al inicio de Windows."
                    Me.msgq7 = "El programa no se está ejecutando en modo elevado, algunas funciones están desactivadas."
                    Me.msgq8 = "Borrar lista de archivos recientes"

                    Me.DataGridView1.Columns(0).HeaderText = "Tipo"
                    Me.DataGridView1.Columns(1).HeaderText = "Icono"
                    Me.DataGridView1.Columns(2).HeaderText = "Nombre"
                    Me.DataGridView1.Columns(3).HeaderText = "Valor"

                Case "pt"
                    Me.textBoxNameHint = "Nome exclusivo de entrada de começar para o arquivo (arraste um arquivo aqui para atribuí-lo automaticamente)."
                    Me.textBoxFileHint = "Caminho completo para o arquivo (arraste um arquivo aqui para atribuí-lo automaticamente)."
                    Me.textBoxParametersHint = "Argumentos opcionais (apenas para um arquivo executável)."
                    Me.msgq1 = "Deseja mesmo excluir a seguinte entrada?:"
                    Me.msgq2 = "Esta ação não pode ser desfeita."
                    Me.msgq3 = "Já existe uma entrada com o mesmo nome na começar do Windows."
                    Me.msgq4 = "Deseja realmente substituir a entrada?"
                    Me.msgq5 = "Item adicionado com sucesso na começar do Windows."
                    Me.msgq6 = "Erro ao adicionar item na começar do Windows."
                    Me.msgq7 = "O programa não está sendo executado em modo elevado, algumas funções estão desabilitadas."
                    Me.msgq8 = "Limpar lista de arquivos recentes"

                    Me.DataGridView1.Columns(0).HeaderText = "Tipo"
                    Me.DataGridView1.Columns(1).HeaderText = "Ícone"
                    Me.DataGridView1.Columns(2).HeaderText = "Nome"
                    Me.DataGridView1.Columns(3).HeaderText = "Valor"

            End Select

            Me.Text = If(Not IsProcessElevated, Me.msgq7, My.Application.Info.Title)

            Me.SetControlHints()

            If Me.TabControl1.SelectedTab.Equals(Me.TabPage1) Then
                Me.TextBox_File.Focus()
                Me.TextBox_Name.Focus()
                Me.TextBox_Parameters.Focus()
                Me.ToolStrip1.Focus()
            End If

            Me.Button_AddToStartup.ImageAlign = ContentAlignment.MiddleLeft
            Me.ClearRecentFilesMenuItem.Text = Me.msgq8
            Me.ClearRecentFilesMenuItem.Enabled = True
            Me.ToolStripDropDownButton_Recent.Enabled = My.Settings.MRU.Count > 0

        End Sub

#End Region

    End Class

End Namespace