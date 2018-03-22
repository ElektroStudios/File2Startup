' ***********************************************************************
' Author   : Administrador
' Modified : 15-January-2015
' ***********************************************************************
' <copyright file="Main.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Imports "

Imports Microsoft.Win32

Imports System.ComponentModel

Imports ElektroKit.Core.Application.UserInterface.Enums
Imports ElektroKit.Core.Application.UserInterface.Tools.Graphical
Imports ElektroKit.Core.Application.UserInterface.Types
Imports ElektroKit.Core.Imaging.Tools
Imports ElektroKit.Core.System.Tools

#End Region

Namespace UserInterface

    ''' <summary>
    ''' The Main user interface Form.
    ''' </summary>
    Public NotInheritable Class Main

#Region " Properties & Objects "

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
        ''' Gets or sets the startup type, for current user only or for all users..
        ''' </summary>
        ''' <value>The startup type.</value>
        Private Property SelectedStartupType As String = "CurrentUserKey"

        Private ReadOnly textBoxNameHint As String =
            "The registry value title. (Drag&Drop a file here to auto-assign)"

        Private ReadOnly textBoxFileHint As String =
            "The full file path to add. (Drag&Drop a file here to auto-assign)"

        Private ReadOnly textBoxParametersHint As String =
            "The file arguments (if any)."

        Private mruDelimiter As String = "@@File2Startup@@"

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="Main"/> class.
        ''' </summary>
        Public Sub New()

            ' This call is required by the designer.
            InitializeComponent()

            SetControlHints()

        End Sub

#End Region

#Region " Event Handlers "

        ''' <summary>
        ''' Handles the Load event of the Form.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub Main_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown

            Me.windowSticker = New WindowMagnetizer(window:=Me) With {.Threshold = 35, .AllowOffscreen = True, .Enabled = True}

            If My.Application.CommandLineArgs.Count <> 0 Then

                If IO.File.Exists(My.Application.CommandLineArgs.First) Then

                    Me.LoadDragDropFile(New IO.FileInfo(My.Application.CommandLineArgs.First))

                End If

            End If

            Me.CheckRecentFiles()

        End Sub

        ''' <summary>
        ''' Handles the DragEnter event of the Form.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        Private Sub Form_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) _
        Handles Me.DragEnter

            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                e.Effect = DragDropEffects.All
            End If

        End Sub

        ''' <summary>
        ''' Handles the DragDrop event of the Form.
        ''' </summary>
        ''' <param name="sender">The source of the event.</param>
        ''' <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        Private Sub Form_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) _
        Handles Me.DragDrop

            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Me.LoadDragDropFile(New IO.FileInfo(e.Data.GetData(DataFormats.FileDrop)(0)))
            End If

        End Sub

        ''' <summary>
        ''' Handles the _TextChanged event of the RadioButtonsTextBoxes.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub TextBoxes_TextChanged(ByVal sender As Object, ByVal e As EventArgs) _
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
        Private Sub RadioButtons_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) _
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
        Private Sub Button_AddToStartup_Click(ByVal sender As Object, ByVal e As EventArgs) _
        Handles Button_AddToStartup.Click

            Me.AddFileToRegistryStartup()

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

                    Dim name As String = My.Settings.MRU(index).Split({mruDelimiter}, StringSplitOptions.None)(0)
                    Dim path As String = My.Settings.MRU(index).Split({mruDelimiter}, StringSplitOptions.None)(1)
                    ' Dim parameters As String = My.Settings.MRU(index).Split({mruDelimiter}, StringSplitOptions.None)(2)

                    Dim menuItem As New ToolStripMenuItem
                    With menuItem
                        .Text = name
                        .Tag = index
                        Try
                            .Image = ImageUtil.ExtractIconFromFile(path, 0).ToBitmap
                        Catch ex As Exception
                        End Try
                    End With

                    AddHandler menuItem.Click, AddressOf IToolStripMenuItem_Click

                    sender.Items.Insert(0, menuItem)

                Next index

                e.Cancel = False

            Else
                e.Cancel = True

            End If

        End Sub

        ''' <summary>
        ''' Handles the Click event of the IToolStripMenuItem control.
        ''' </summary>
        ''' <param name="sender">The sender.</param>
        ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        Private Sub IToolStripMenuItem_Click(sender As Object, e As EventArgs)

            Me.RemoveControlHints()

            Dim name As String = My.Settings.MRU(CInt(sender.tag)).Split({mruDelimiter}, StringSplitOptions.None)(0)
            Dim path As String = My.Settings.MRU(CInt(sender.tag)).Split({mruDelimiter}, StringSplitOptions.None)(1)
            Dim parameters As String = My.Settings.MRU(CInt(sender.tag)).Split({mruDelimiter}, StringSplitOptions.None)(2)

            Me.TextBox_Name.Text = name
            Me.TextBox_File.Text = path

            If Not String.IsNullOrEmpty(parameters) Then
                Me.TextBox_Parameters.Text = parameters
            End If

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

            ControlHintManager.RemoveHint(Me.TextBox_File)
            Me.TextBox_File.Text = DirectCast(sender, OpenFileDialog).FileName
            Me.SetControlHints()

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
        End Sub

        ''' <summary>
        ''' Loads the dropped file into the textboxes.
        ''' </summary>
        ''' <param name="File">The file to load.</param>
        Private Sub LoadDragDropFile(ByVal file As IO.FileInfo)

            Me.RemoveControlHints()
            Me.TextBox_Name.Text = file.Name.Substring(0, file.Name.LastIndexOf(file.Extension))
            Me.TextBox_File.Text = file.FullName
            Me.SetControlHints()

        End Sub

        ''' <summary>
        ''' Adds the file to the registry startup.
        ''' </summary>
        Private Sub AddFileToRegistryStartup()

            Dim entry As String = String.Format("{0}" & mruDelimiter & "{1}" & mruDelimiter & "{2}",
                                                Me.TextBox_Name.Text,
                                                Me.TextBox_File.Text,
                                                If(Me.TextBox_Parameters.Text = textBoxParametersHint,
                                                   String.Empty,
                                                   Me.TextBox_Parameters.Text))

            Dim name As String = entry.Split({mruDelimiter}, StringSplitOptions.None)(0)
            Dim path As String = entry.Split({mruDelimiter}, StringSplitOptions.None)(1)
            Dim parameters As String = entry.Split({mruDelimiter}, StringSplitOptions.None)(2)

            RegistryUtil.CreateValue(Of String)(RegistryView.Default, Me.startupType(Me.SelectedStartupType),
                                                name,
                                                String.Format("""{0}"" {1}", path, parameters))

            Select Case RegistryUtil.ExistValue(RegistryView.Default, Me.startupType(Me.SelectedStartupType), name)

                Case True

                    My.Settings.MRU.Add(entry)
                    Me.RenewRecentfiles()

                    Using New CenteredMessageBox(Me)
                        MessageBox.Show("File successfully added to Windows Startup", "File2Startup",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End Using

                Case False
                    Using New CenteredMessageBox(Me, Nothing)
                        MessageBox.Show("Failed to add the file to Windows Startup", "File2Startup",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End Using

            End Select

        End Sub

        ''' <summary>
        ''' Checks whether the recent files object is empty, then instances a new one.
        ''' </summary>
        Private Sub CheckRecentFiles()

            If My.Settings.MRU Is Nothing Then
                My.Settings.MRU = New System.Collections.Specialized.StringCollection
            End If

        End Sub

        ''' <summary>
        ''' Renews the recentfiles to controls the maximum number of entries.
        ''' </summary>
        Private Sub RenewRecentfiles()

            If My.Settings.MRU.Count > 10 Then

                Do Until My.Settings.MRU.Count = 10
                    My.Settings.MRU.RemoveAt(0)
                Loop

            End If

            My.Settings.Save()

        End Sub

#End Region

    End Class

End Namespace