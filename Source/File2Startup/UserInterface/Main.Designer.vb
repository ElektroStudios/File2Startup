
Namespace UserInterface

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class Main
    Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()> _
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> _
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
            Me.TextBox_Parameters = New System.Windows.Forms.TextBox()
            Me.TextBox_File = New System.Windows.Forms.TextBox()
            Me.TextBox_Name = New System.Windows.Forms.TextBox()
            Me.Label1 = New System.Windows.Forms.Label()
            Me.Label2 = New System.Windows.Forms.Label()
            Me.Label3 = New System.Windows.Forms.Label()
            Me.Button_AddToStartup = New System.Windows.Forms.Button()
            Me.RadioButton_CurrentUser = New System.Windows.Forms.RadioButton()
            Me.RadioButton_AllUsers = New System.Windows.Forms.RadioButton()
            Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
            Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
            Me.ContextMenuStrip_Recent = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.Button_OpenFile = New System.Windows.Forms.Button()
            Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
            Me.ToolStrip1.SuspendLayout()
            Me.SuspendLayout()
            '
            'TextBox_Parameters
            '
            Me.TextBox_Parameters.Location = New System.Drawing.Point(53, 85)
            Me.TextBox_Parameters.Name = "TextBox_Parameters"
            Me.TextBox_Parameters.Size = New System.Drawing.Size(347, 20)
            Me.TextBox_Parameters.TabIndex = 7
            '
            'TextBox_File
            '
            Me.TextBox_File.Location = New System.Drawing.Point(53, 59)
            Me.TextBox_File.Name = "TextBox_File"
            Me.TextBox_File.Size = New System.Drawing.Size(317, 20)
            Me.TextBox_File.TabIndex = 5
            '
            'TextBox_Name
            '
            Me.TextBox_Name.Location = New System.Drawing.Point(53, 33)
            Me.TextBox_Name.Name = "TextBox_Name"
            Me.TextBox_Name.Size = New System.Drawing.Size(347, 20)
            Me.TextBox_Name.TabIndex = 3
            '
            'Label1
            '
            Me.Label1.Location = New System.Drawing.Point(12, 38)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(35, 13)
            Me.Label1.TabIndex = 2
            Me.Label1.Tag = ""
            Me.Label1.Text = "Title"
            '
            'Label2
            '
            Me.Label2.Location = New System.Drawing.Point(12, 64)
            Me.Label2.Name = "Label2"
            Me.Label2.Size = New System.Drawing.Size(35, 13)
            Me.Label2.TabIndex = 4
            Me.Label2.Text = "File"
            '
            'Label3
            '
            Me.Label3.Location = New System.Drawing.Point(12, 90)
            Me.Label3.Name = "Label3"
            Me.Label3.Size = New System.Drawing.Size(35, 13)
            Me.Label3.TabIndex = 6
            Me.Label3.Text = "Args."
            '
            'Button_AddToStartup
            '
            Me.Button_AddToStartup.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Button_AddToStartup.Enabled = False
            Me.Button_AddToStartup.Location = New System.Drawing.Point(120, 116)
            Me.Button_AddToStartup.Name = "Button_AddToStartup"
            Me.Button_AddToStartup.Size = New System.Drawing.Size(280, 40)
            Me.Button_AddToStartup.TabIndex = 8
            Me.Button_AddToStartup.Text = "Add to Windows Startup"
            Me.Button_AddToStartup.UseVisualStyleBackColor = True
            '
            'RadioButton_CurrentUser
            '
            Me.RadioButton_CurrentUser.AutoSize = True
            Me.RadioButton_CurrentUser.Checked = True
            Me.RadioButton_CurrentUser.Cursor = System.Windows.Forms.Cursors.Hand
            Me.RadioButton_CurrentUser.Location = New System.Drawing.Point(12, 116)
            Me.RadioButton_CurrentUser.Name = "RadioButton_CurrentUser"
            Me.RadioButton_CurrentUser.Size = New System.Drawing.Size(102, 17)
            Me.RadioButton_CurrentUser.TabIndex = 0
            Me.RadioButton_CurrentUser.TabStop = True
            Me.RadioButton_CurrentUser.Tag = "CurrentUserKey"
            Me.RadioButton_CurrentUser.Text = "For Current User"
            Me.RadioButton_CurrentUser.UseVisualStyleBackColor = True
            '
            'RadioButton_AllUsers
            '
            Me.RadioButton_AllUsers.AutoSize = True
            Me.RadioButton_AllUsers.Cursor = System.Windows.Forms.Cursors.Hand
            Me.RadioButton_AllUsers.Location = New System.Drawing.Point(12, 139)
            Me.RadioButton_AllUsers.Name = "RadioButton_AllUsers"
            Me.RadioButton_AllUsers.Size = New System.Drawing.Size(84, 17)
            Me.RadioButton_AllUsers.TabIndex = 1
            Me.RadioButton_AllUsers.Tag = "AllUsersKey"
            Me.RadioButton_AllUsers.Text = "For All Users"
            Me.RadioButton_AllUsers.UseVisualStyleBackColor = True
            '
            'ToolStrip1
            '
            Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton1})
            Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
            Me.ToolStrip1.Name = "ToolStrip1"
            Me.ToolStrip1.Size = New System.Drawing.Size(412, 25)
            Me.ToolStrip1.TabIndex = 9
            Me.ToolStrip1.Text = "ToolStrip1"
            '
            'ToolStripDropDownButton1
            '
            Me.ToolStripDropDownButton1.AutoToolTip = False
            Me.ToolStripDropDownButton1.DropDown = Me.ContextMenuStrip_Recent
            Me.ToolStripDropDownButton1.Image = CType(resources.GetObject("ToolStripDropDownButton1.Image"), System.Drawing.Image)
            Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
            Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(81, 22)
            Me.ToolStripDropDownButton1.Text = "Recent..."
            '
            'ContextMenuStrip_Recent
            '
            Me.ContextMenuStrip_Recent.Name = "ContextMenuStrip_Recent"
            Me.ContextMenuStrip_Recent.OwnerItem = Me.ToolStripDropDownButton1
            Me.ContextMenuStrip_Recent.Size = New System.Drawing.Size(61, 4)
            '
            'Button_OpenFile
            '
            Me.Button_OpenFile.Location = New System.Drawing.Point(376, 60)
            Me.Button_OpenFile.Name = "Button_OpenFile"
            Me.Button_OpenFile.Size = New System.Drawing.Size(24, 20)
            Me.Button_OpenFile.TabIndex = 10
            Me.Button_OpenFile.Text = "..."
            Me.Button_OpenFile.UseVisualStyleBackColor = True
            '
            'OpenFileDialog1
            '
            Me.OpenFileDialog1.FileName = "OpenFileDialog1"
            '
            'Main
            '
            Me.AllowDrop = True
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(412, 169)
            Me.Controls.Add(Me.Button_OpenFile)
            Me.Controls.Add(Me.ToolStrip1)
            Me.Controls.Add(Me.RadioButton_AllUsers)
            Me.Controls.Add(Me.RadioButton_CurrentUser)
            Me.Controls.Add(Me.Button_AddToStartup)
            Me.Controls.Add(Me.Label3)
            Me.Controls.Add(Me.Label2)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.TextBox_Name)
            Me.Controls.Add(Me.TextBox_File)
            Me.Controls.Add(Me.TextBox_Parameters)
            Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "Main"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "File 2 Startup"
            Me.ToolStrip1.ResumeLayout(False)
            Me.ToolStrip1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents TextBox_Parameters As System.Windows.Forms.TextBox
        Friend WithEvents TextBox_File As System.Windows.Forms.TextBox
        Friend WithEvents TextBox_Name As System.Windows.Forms.TextBox
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents Label2 As System.Windows.Forms.Label
        Friend WithEvents Label3 As System.Windows.Forms.Label
        Friend WithEvents Button_AddToStartup As System.Windows.Forms.Button
        Friend WithEvents RadioButton_CurrentUser As System.Windows.Forms.RadioButton
        Friend WithEvents RadioButton_AllUsers As System.Windows.Forms.RadioButton
        Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
        Friend WithEvents ContextMenuStrip_Recent As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
        Friend WithEvents Button_OpenFile As System.Windows.Forms.Button
        Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog

    End Class

End Namespace
