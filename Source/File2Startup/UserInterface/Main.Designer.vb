
Imports DevCase.UI.Components

Namespace UserInterface

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
            Me.ToolStripDropDownButton_Recent = New System.Windows.Forms.ToolStripDropDownButton()
            Me.ContextMenuStrip_Recent = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.Button_OpenFile = New System.Windows.Forms.Button()
            Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
            Me.TabControl1 = New System.Windows.Forms.TabControl()
            Me.TabPage1 = New System.Windows.Forms.TabPage()
            Me.Label4 = New System.Windows.Forms.Label()
            Me.Panel1 = New System.Windows.Forms.Panel()
            Me.TabPage2 = New System.Windows.Forms.TabPage()
            Me.Button_OpenInRegedit = New System.Windows.Forms.Button()
            Me.Button_RefreshList = New System.Windows.Forms.Button()
            Me.Button_SendtoItemBuilder = New System.Windows.Forms.Button()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.ColumnType = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnIcon = New System.Windows.Forms.DataGridViewImageColumn()
            Me.ColumnName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.Button_DeleteEntry = New System.Windows.Forms.Button()
            Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
            Me.ToolStripButton_About = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripDropDownButton2 = New System.Windows.Forms.ToolStripDropDownButton()
            Me.EnglishToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SpanishToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.PortugueseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ToolStripButton_ItemBuilder = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripButton_StartupList = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripCheckBox_CompactMode = New ToolStripCheckBox()
            Me.ToolStrip1.SuspendLayout()
            Me.TabControl1.SuspendLayout()
            Me.TabPage1.SuspendLayout()
            Me.Panel1.SuspendLayout()
            Me.TabPage2.SuspendLayout()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.ToolStrip2.SuspendLayout()
            Me.SuspendLayout()
            '
            'TextBox_Parameters
            '
            resources.ApplyResources(Me.TextBox_Parameters, "TextBox_Parameters")
            Me.TextBox_Parameters.Name = "TextBox_Parameters"
            '
            'TextBox_File
            '
            resources.ApplyResources(Me.TextBox_File, "TextBox_File")
            Me.TextBox_File.Name = "TextBox_File"
            '
            'TextBox_Name
            '
            resources.ApplyResources(Me.TextBox_Name, "TextBox_Name")
            Me.TextBox_Name.Name = "TextBox_Name"
            '
            'Label1
            '
            resources.ApplyResources(Me.Label1, "Label1")
            Me.Label1.Name = "Label1"
            Me.Label1.Tag = ""
            '
            'Label2
            '
            resources.ApplyResources(Me.Label2, "Label2")
            Me.Label2.Name = "Label2"
            '
            'Label3
            '
            resources.ApplyResources(Me.Label3, "Label3")
            Me.Label3.Name = "Label3"
            '
            'Button_AddToStartup
            '
            resources.ApplyResources(Me.Button_AddToStartup, "Button_AddToStartup")
            Me.Button_AddToStartup.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Button_AddToStartup.Name = "Button_AddToStartup"
            Me.Button_AddToStartup.UseVisualStyleBackColor = True
            '
            'RadioButton_CurrentUser
            '
            resources.ApplyResources(Me.RadioButton_CurrentUser, "RadioButton_CurrentUser")
            Me.RadioButton_CurrentUser.Checked = True
            Me.RadioButton_CurrentUser.Cursor = System.Windows.Forms.Cursors.Hand
            Me.RadioButton_CurrentUser.Name = "RadioButton_CurrentUser"
            Me.RadioButton_CurrentUser.TabStop = True
            Me.RadioButton_CurrentUser.Tag = "CurrentUserKey"
            Me.RadioButton_CurrentUser.UseVisualStyleBackColor = True
            '
            'RadioButton_AllUsers
            '
            resources.ApplyResources(Me.RadioButton_AllUsers, "RadioButton_AllUsers")
            Me.RadioButton_AllUsers.Cursor = System.Windows.Forms.Cursors.Hand
            Me.RadioButton_AllUsers.Name = "RadioButton_AllUsers"
            Me.RadioButton_AllUsers.Tag = "AllUsersKey"
            Me.RadioButton_AllUsers.UseVisualStyleBackColor = True
            '
            'ToolStrip1
            '
            resources.ApplyResources(Me.ToolStrip1, "ToolStrip1")
            Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton_Recent})
            Me.ToolStrip1.Name = "ToolStrip1"
            '
            'ToolStripDropDownButton_Recent
            '
            resources.ApplyResources(Me.ToolStripDropDownButton_Recent, "ToolStripDropDownButton_Recent")
            Me.ToolStripDropDownButton_Recent.AutoToolTip = False
            Me.ToolStripDropDownButton_Recent.DropDown = Me.ContextMenuStrip_Recent
            Me.ToolStripDropDownButton_Recent.Name = "ToolStripDropDownButton_Recent"
            '
            'ContextMenuStrip_Recent
            '
            resources.ApplyResources(Me.ContextMenuStrip_Recent, "ContextMenuStrip_Recent")
            Me.ContextMenuStrip_Recent.Name = "ContextMenuStrip_Recent"
            Me.ContextMenuStrip_Recent.OwnerItem = Me.ToolStripDropDownButton_Recent
            '
            'Button_OpenFile
            '
            resources.ApplyResources(Me.Button_OpenFile, "Button_OpenFile")
            Me.Button_OpenFile.Name = "Button_OpenFile"
            Me.Button_OpenFile.UseVisualStyleBackColor = True
            '
            'OpenFileDialog1
            '
            Me.OpenFileDialog1.FileName = "OpenFileDialog1"
            resources.ApplyResources(Me.OpenFileDialog1, "OpenFileDialog1")
            '
            'TabControl1
            '
            resources.ApplyResources(Me.TabControl1, "TabControl1")
            Me.TabControl1.Controls.Add(Me.TabPage1)
            Me.TabControl1.Controls.Add(Me.TabPage2)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
            '
            'TabPage1
            '
            resources.ApplyResources(Me.TabPage1, "TabPage1")
            Me.TabPage1.Controls.Add(Me.ToolStrip1)
            Me.TabPage1.Controls.Add(Me.Label4)
            Me.TabPage1.Controls.Add(Me.Panel1)
            Me.TabPage1.Controls.Add(Me.TextBox_Parameters)
            Me.TabPage1.Controls.Add(Me.Button_OpenFile)
            Me.TabPage1.Controls.Add(Me.TextBox_File)
            Me.TabPage1.Controls.Add(Me.TextBox_Name)
            Me.TabPage1.Controls.Add(Me.Label1)
            Me.TabPage1.Controls.Add(Me.Label2)
            Me.TabPage1.Controls.Add(Me.Button_AddToStartup)
            Me.TabPage1.Controls.Add(Me.Label3)
            Me.TabPage1.Name = "TabPage1"
            Me.TabPage1.UseVisualStyleBackColor = True
            '
            'Label4
            '
            resources.ApplyResources(Me.Label4, "Label4")
            Me.Label4.Name = "Label4"
            '
            'Panel1
            '
            resources.ApplyResources(Me.Panel1, "Panel1")
            Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            Me.Panel1.Controls.Add(Me.RadioButton_CurrentUser)
            Me.Panel1.Controls.Add(Me.RadioButton_AllUsers)
            Me.Panel1.Name = "Panel1"
            '
            'TabPage2
            '
            resources.ApplyResources(Me.TabPage2, "TabPage2")
            Me.TabPage2.Controls.Add(Me.Button_OpenInRegedit)
            Me.TabPage2.Controls.Add(Me.Button_RefreshList)
            Me.TabPage2.Controls.Add(Me.Button_SendtoItemBuilder)
            Me.TabPage2.Controls.Add(Me.DataGridView1)
            Me.TabPage2.Controls.Add(Me.Button_DeleteEntry)
            Me.TabPage2.Name = "TabPage2"
            Me.TabPage2.UseVisualStyleBackColor = True
            '
            'Button_OpenInRegedit
            '
            resources.ApplyResources(Me.Button_OpenInRegedit, "Button_OpenInRegedit")
            Me.Button_OpenInRegedit.Name = "Button_OpenInRegedit"
            Me.Button_OpenInRegedit.UseVisualStyleBackColor = True
            '
            'Button_RefreshList
            '
            resources.ApplyResources(Me.Button_RefreshList, "Button_RefreshList")
            Me.Button_RefreshList.Name = "Button_RefreshList"
            Me.Button_RefreshList.UseVisualStyleBackColor = True
            '
            'Button_SendtoItemBuilder
            '
            resources.ApplyResources(Me.Button_SendtoItemBuilder, "Button_SendtoItemBuilder")
            Me.Button_SendtoItemBuilder.Name = "Button_SendtoItemBuilder"
            Me.Button_SendtoItemBuilder.UseVisualStyleBackColor = True
            '
            'DataGridView1
            '
            resources.ApplyResources(Me.DataGridView1, "DataGridView1")
            Me.DataGridView1.AllowUserToAddRows = False
            Me.DataGridView1.AllowUserToDeleteRows = False
            Me.DataGridView1.AllowUserToOrderColumns = True
            Me.DataGridView1.AllowUserToResizeRows = False
            Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnType, Me.ColumnIcon, Me.ColumnName, Me.ColumnValue})
            Me.DataGridView1.MultiSelect = False
            Me.DataGridView1.Name = "DataGridView1"
            Me.DataGridView1.ReadOnly = True
            Me.DataGridView1.RowHeadersVisible = False
            Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
            Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DataGridView1.ShowCellErrors = False
            Me.DataGridView1.ShowCellToolTips = False
            Me.DataGridView1.ShowEditingIcon = False
            Me.DataGridView1.ShowRowErrors = False
            '
            'ColumnType
            '
            resources.ApplyResources(Me.ColumnType, "ColumnType")
            Me.ColumnType.Name = "ColumnType"
            Me.ColumnType.ReadOnly = True
            '
            'ColumnIcon
            '
            Me.ColumnIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader
            resources.ApplyResources(Me.ColumnIcon, "ColumnIcon")
            Me.ColumnIcon.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
            Me.ColumnIcon.Name = "ColumnIcon"
            Me.ColumnIcon.ReadOnly = True
            Me.ColumnIcon.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            '
            'ColumnName
            '
            resources.ApplyResources(Me.ColumnName, "ColumnName")
            Me.ColumnName.Name = "ColumnName"
            Me.ColumnName.ReadOnly = True
            '
            'ColumnValue
            '
            resources.ApplyResources(Me.ColumnValue, "ColumnValue")
            Me.ColumnValue.Name = "ColumnValue"
            Me.ColumnValue.ReadOnly = True
            '
            'Button_DeleteEntry
            '
            resources.ApplyResources(Me.Button_DeleteEntry, "Button_DeleteEntry")
            Me.Button_DeleteEntry.Name = "Button_DeleteEntry"
            Me.Button_DeleteEntry.UseVisualStyleBackColor = True
            '
            'ToolStrip2
            '
            resources.ApplyResources(Me.ToolStrip2, "ToolStrip2")
            Me.ToolStrip2.AllowMerge = False
            Me.ToolStrip2.BackColor = System.Drawing.SystemColors.Control
            Me.ToolStrip2.CanOverflow = False
            Me.ToolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_About, Me.ToolStripDropDownButton2, Me.ToolStripButton_ItemBuilder, Me.ToolStripButton_StartupList, Me.ToolStripCheckBox_CompactMode})
            Me.ToolStrip2.Name = "ToolStrip2"
            '
            'ToolStripButton_About
            '
            resources.ApplyResources(Me.ToolStripButton_About, "ToolStripButton_About")
            Me.ToolStripButton_About.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ToolStripButton_About.Image = Global.My.Resources.Resources.communications
            Me.ToolStripButton_About.Name = "ToolStripButton_About"
            '
            'ToolStripDropDownButton2
            '
            resources.ApplyResources(Me.ToolStripDropDownButton2, "ToolStripDropDownButton2")
            Me.ToolStripDropDownButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ToolStripDropDownButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnglishToolStripMenuItem, Me.SpanishToolStripMenuItem, Me.PortugueseToolStripMenuItem})
            Me.ToolStripDropDownButton2.Image = Global.My.Resources.Resources.languages
            Me.ToolStripDropDownButton2.Name = "ToolStripDropDownButton2"
            '
            'EnglishToolStripMenuItem
            '
            resources.ApplyResources(Me.EnglishToolStripMenuItem, "EnglishToolStripMenuItem")
            Me.EnglishToolStripMenuItem.Image = Global.My.Resources.Resources.united_states_of_america
            Me.EnglishToolStripMenuItem.Name = "EnglishToolStripMenuItem"
            '
            'SpanishToolStripMenuItem
            '
            resources.ApplyResources(Me.SpanishToolStripMenuItem, "SpanishToolStripMenuItem")
            Me.SpanishToolStripMenuItem.Image = Global.My.Resources.Resources.spain
            Me.SpanishToolStripMenuItem.Name = "SpanishToolStripMenuItem"
            '
            'PortugueseToolStripMenuItem
            '
            resources.ApplyResources(Me.PortugueseToolStripMenuItem, "PortugueseToolStripMenuItem")
            Me.PortugueseToolStripMenuItem.Image = Global.My.Resources.Resources.portugal
            Me.PortugueseToolStripMenuItem.Name = "PortugueseToolStripMenuItem"
            '
            'ToolStripButton_ItemBuilder
            '
            resources.ApplyResources(Me.ToolStripButton_ItemBuilder, "ToolStripButton_ItemBuilder")
            Me.ToolStripButton_ItemBuilder.Image = Global.My.Resources.Resources.plus
            Me.ToolStripButton_ItemBuilder.Name = "ToolStripButton_ItemBuilder"
            '
            'ToolStripButton_StartupList
            '
            resources.ApplyResources(Me.ToolStripButton_StartupList, "ToolStripButton_StartupList")
            Me.ToolStripButton_StartupList.Image = Global.My.Resources.Resources.pencil
            Me.ToolStripButton_StartupList.Name = "ToolStripButton_StartupList"
            '
            'ToolStripCheckBox_CompactMode
            '
            resources.ApplyResources(Me.ToolStripCheckBox_CompactMode, "ToolStripCheckBox_CompactMode")
            Me.ToolStripCheckBox_CompactMode.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ToolStripCheckBox_CompactMode.BackColor = System.Drawing.Color.Transparent
            Me.ToolStripCheckBox_CompactMode.Checked = True
            Me.ToolStripCheckBox_CompactMode.Name = "ToolStripCheckBox"
            '
            'Main
            '
            resources.ApplyResources(Me, "$this")
            Me.AllowDrop = True
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.Controls.Add(Me.TabControl1)
            Me.Controls.Add(Me.ToolStrip2)
            Me.Name = "Main"
            Me.ToolStrip1.ResumeLayout(False)
            Me.ToolStrip1.PerformLayout()
            Me.TabControl1.ResumeLayout(False)
            Me.TabPage1.ResumeLayout(False)
            Me.TabPage1.PerformLayout()
            Me.Panel1.ResumeLayout(False)
            Me.Panel1.PerformLayout()
            Me.TabPage2.ResumeLayout(False)
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ToolStrip2.ResumeLayout(False)
            Me.ToolStrip2.PerformLayout()
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
        Friend WithEvents ToolStripDropDownButton_Recent As System.Windows.Forms.ToolStripDropDownButton
        Friend WithEvents Button_OpenFile As System.Windows.Forms.Button
        Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
        Friend WithEvents TabControl1 As TabControl
        Friend WithEvents TabPage1 As TabPage
        Friend WithEvents TabPage2 As TabPage
        Friend WithEvents Panel1 As Panel
        Friend WithEvents Button_DeleteEntry As Button
        Friend WithEvents Label4 As Label
        Friend WithEvents DataGridView1 As DataGridView
        Friend WithEvents ColumnType As DataGridViewTextBoxColumn
        Friend WithEvents ColumnIcon As DataGridViewImageColumn
        Friend WithEvents ColumnName As DataGridViewTextBoxColumn
        Friend WithEvents ColumnValue As DataGridViewTextBoxColumn
        Friend WithEvents ToolStrip2 As ToolStrip
        Friend WithEvents ToolStripButton_About As ToolStripButton
        Friend WithEvents ToolStripDropDownButton2 As ToolStripDropDownButton
        Friend WithEvents EnglishToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents SpanishToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents PortugueseToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents ToolStripButton_ItemBuilder As ToolStripButton
        Friend WithEvents ToolStripButton_StartupList As ToolStripButton
        Friend WithEvents Button_SendtoItemBuilder As Button
        Friend WithEvents Button_RefreshList As Button
        Friend WithEvents Button_OpenInRegedit As Button
        Friend WithEvents ToolStripCheckBox_CompactMode As ToolStripCheckBox
    End Class

End Namespace
