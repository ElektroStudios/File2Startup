Namespace UserInterface

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class Main
        Inherits System.Windows.Forms.Form

        'Form overrides dispose to clean up the component list.
        <System.Diagnostics.DebuggerNonUserCode()>
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
            Me.ToolStrip_Header = New System.Windows.Forms.ToolStrip()
            Me.ToolStripButton_ItemBuilder = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripButton_StartupList = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripButton_About = New System.Windows.Forms.ToolStripButton()
            Me.ToolStripToggleSwitch_Theme = New Global.UserInterface.Controls.ToolStripToggleSwitch()
            Me.ToolStripDropDownButton_Language = New System.Windows.Forms.ToolStripDropDownButton()
            Me.EnglishToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SpanishToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.PortugueseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.StatusStrip_Main = New System.Windows.Forms.StatusStrip()
            Me.ToolStripStatusLabel_Elevation = New System.Windows.Forms.ToolStripStatusLabel()
            Me.ToolStripStatusLabel_EntryCount = New System.Windows.Forms.ToolStripStatusLabel()
            Me.Panel_ItemBuilder = New System.Windows.Forms.Panel()
            Me.TableLayoutPanel_ItemBuilder = New System.Windows.Forms.TableLayoutPanel()
            Me.Label_Name = New System.Windows.Forms.Label()
            Me.TextBox_Name = New System.Windows.Forms.TextBox()
            Me.Label_File = New System.Windows.Forms.Label()
            Me.TextBox_File = New System.Windows.Forms.TextBox()
            Me.Button_OpenFile = New System.Windows.Forms.Button()
            Me.Label_Arguments = New System.Windows.Forms.Label()
            Me.TextBox_Parameters = New System.Windows.Forms.TextBox()
            Me.Label_StartupType = New System.Windows.Forms.Label()
            Me.FlowLayoutPanel_StartupType = New System.Windows.Forms.FlowLayoutPanel()
            Me.RadioButton_CurrentUser = New System.Windows.Forms.RadioButton()
            Me.RadioButton_AllUsers = New System.Windows.Forms.RadioButton()
            Me.TableLayoutPanel_StartupType = New System.Windows.Forms.TableLayoutPanel()
            Me.Button_AddToStartup = New Global.UserInterface.Controls.IconButton()
            Me.ToolStrip_ItemBuilder = New System.Windows.Forms.ToolStrip()
            Me.ToolStripDropDownButton_Recent = New System.Windows.Forms.ToolStripDropDownButton()
            Me.Panel_StartupList = New System.Windows.Forms.Panel()
            Me.TableLayoutPanel_StartupList = New System.Windows.Forms.TableLayoutPanel()
            Me.DataGridView1 = New System.Windows.Forms.DataGridView()
            Me.ColumnType = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnIcon = New System.Windows.Forms.DataGridViewImageColumn()
            Me.ColumnName = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.ColumnValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
            Me.TableLayoutPanel_ListActions = New System.Windows.Forms.TableLayoutPanel()
            Me.FlowLayoutPanel_ListActions = New System.Windows.Forms.FlowLayoutPanel()
            Me.Button_DeleteEntry = New Global.UserInterface.Controls.IconButton()
            Me.Button_SendtoItemBuilder = New Global.UserInterface.Controls.IconButton()
            Me.Button_OpenInRegedit = New Global.UserInterface.Controls.IconButton()
            Me.Button_RefreshList = New Global.UserInterface.Controls.IconButton()
            Me.ContextMenuStrip_Recent = New System.Windows.Forms.ContextMenuStrip(Me.components)
            Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
            Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.ToolStrip_Header.SuspendLayout()
            Me.StatusStrip_Main.SuspendLayout()
            Me.Panel_ItemBuilder.SuspendLayout()
            Me.TableLayoutPanel_ItemBuilder.SuspendLayout()
            Me.FlowLayoutPanel_StartupType.SuspendLayout()
            Me.TableLayoutPanel_StartupType.SuspendLayout()
            Me.ToolStrip_ItemBuilder.SuspendLayout()
            Me.Panel_StartupList.SuspendLayout()
            Me.TableLayoutPanel_StartupList.SuspendLayout()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TableLayoutPanel_ListActions.SuspendLayout()
            Me.FlowLayoutPanel_ListActions.SuspendLayout()
            Me.SuspendLayout()
            '
            'ToolStrip_Header
            '
            Me.ToolStrip_Header.AllowMerge = False
            Me.ToolStrip_Header.CanOverflow = False
            Me.ToolStrip_Header.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolStrip_Header.ImageScalingSize = New System.Drawing.Size(16, 16)
            Me.ToolStrip_Header.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton_ItemBuilder, Me.ToolStripButton_StartupList, Me.ToolStripButton_About, Me.ToolStripToggleSwitch_Theme, Me.ToolStripDropDownButton_Language})
            Me.ToolStrip_Header.Location = New System.Drawing.Point(0, 0)
            Me.ToolStrip_Header.Name = "ToolStrip_Header"
            Me.ToolStrip_Header.Padding = New System.Windows.Forms.Padding(8, 0, 8, 0)
            Me.ToolStrip_Header.Size = New System.Drawing.Size(640, 38)
            Me.ToolStrip_Header.TabIndex = 0
            '
            'ToolStripButton_ItemBuilder
            '
            Me.ToolStripButton_ItemBuilder.Image = Global.My.Resources.Resources.pencil
            Me.ToolStripButton_ItemBuilder.Margin = New System.Windows.Forms.Padding(0)
            Me.ToolStripButton_ItemBuilder.Name = "ToolStripButton_ItemBuilder"
            Me.ToolStripButton_ItemBuilder.Padding = New System.Windows.Forms.Padding(10, 8, 10, 8)
            Me.ToolStripButton_ItemBuilder.Size = New System.Drawing.Size(120, 38)
            Me.ToolStripButton_ItemBuilder.Text = "Item Builder"
            '
            'ToolStripButton_StartupList
            '
            Me.ToolStripButton_StartupList.Image = Global.My.Resources.Resources.to_do_list
            Me.ToolStripButton_StartupList.Margin = New System.Windows.Forms.Padding(0)
            Me.ToolStripButton_StartupList.Name = "ToolStripButton_StartupList"
            Me.ToolStripButton_StartupList.Padding = New System.Windows.Forms.Padding(10, 8, 10, 8)
            Me.ToolStripButton_StartupList.Size = New System.Drawing.Size(120, 38)
            Me.ToolStripButton_StartupList.Text = "Startup List"
            '
            'ToolStripButton_About
            '
            Me.ToolStripButton_About.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ToolStripButton_About.Image = Global.My.Resources.Resources.information
            Me.ToolStripButton_About.Margin = New System.Windows.Forms.Padding(2, 6, 0, 6)
            Me.ToolStripButton_About.Name = "ToolStripButton_About"
            Me.ToolStripButton_About.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.ToolStripButton_About.Size = New System.Drawing.Size(80, 26)
            Me.ToolStripButton_About.Text = "About..."
            '
            'ToolStripToggleSwitch_Theme
            '
            Me.ToolStripToggleSwitch_Theme.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ToolStripToggleSwitch_Theme.Margin = New System.Windows.Forms.Padding(8, 8, 8, 8)
            Me.ToolStripToggleSwitch_Theme.Name = "ToolStripToggleSwitch_Theme"
            Me.ToolStripToggleSwitch_Theme.Size = New System.Drawing.Size(44, 22)
            '
            'ToolStripDropDownButton_Language
            '
            Me.ToolStripDropDownButton_Language.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
            Me.ToolStripDropDownButton_Language.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EnglishToolStripMenuItem, Me.SpanishToolStripMenuItem, Me.PortugueseToolStripMenuItem})
            Me.ToolStripDropDownButton_Language.Image = Global.My.Resources.Resources.earth
            Me.ToolStripDropDownButton_Language.Margin = New System.Windows.Forms.Padding(2, 6, 0, 6)
            Me.ToolStripDropDownButton_Language.Name = "ToolStripDropDownButton_Language"
            Me.ToolStripDropDownButton_Language.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.ToolStripDropDownButton_Language.Size = New System.Drawing.Size(100, 26)
            Me.ToolStripDropDownButton_Language.Text = "Language"
            '
            'EnglishToolStripMenuItem
            '
            Me.EnglishToolStripMenuItem.Image = Global.My.Resources.Resources.united_states_of_america
            Me.EnglishToolStripMenuItem.Name = "EnglishToolStripMenuItem"
            Me.EnglishToolStripMenuItem.Size = New System.Drawing.Size(150, 24)
            Me.EnglishToolStripMenuItem.Text = "English"
            '
            'SpanishToolStripMenuItem
            '
            Me.SpanishToolStripMenuItem.Image = Global.My.Resources.Resources.spain
            Me.SpanishToolStripMenuItem.Name = "SpanishToolStripMenuItem"
            Me.SpanishToolStripMenuItem.Size = New System.Drawing.Size(150, 24)
            Me.SpanishToolStripMenuItem.Text = "Spanish"
            '
            'PortugueseToolStripMenuItem
            '
            Me.PortugueseToolStripMenuItem.Image = Global.My.Resources.Resources.portugal
            Me.PortugueseToolStripMenuItem.Name = "PortugueseToolStripMenuItem"
            Me.PortugueseToolStripMenuItem.Size = New System.Drawing.Size(150, 24)
            Me.PortugueseToolStripMenuItem.Text = "Portuguese"
            '
            'StatusStrip_Main
            '
            Me.StatusStrip_Main.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripStatusLabel_Elevation, Me.ToolStripStatusLabel_EntryCount})
            Me.StatusStrip_Main.Location = New System.Drawing.Point(0, 378)
            Me.StatusStrip_Main.Name = "StatusStrip_Main"
            Me.StatusStrip_Main.Padding = New System.Windows.Forms.Padding(8, 0, 12, 0)
            Me.StatusStrip_Main.Size = New System.Drawing.Size(640, 22)
            Me.StatusStrip_Main.TabIndex = 3
            '
            'ToolStripStatusLabel_Elevation
            '
            Me.ToolStripStatusLabel_Elevation.Name = "ToolStripStatusLabel_Elevation"
            Me.ToolStripStatusLabel_Elevation.Size = New System.Drawing.Size(560, 17)
            Me.ToolStripStatusLabel_Elevation.Spring = True
            Me.ToolStripStatusLabel_Elevation.Text = "Running as administrator."
            Me.ToolStripStatusLabel_Elevation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'ToolStripStatusLabel_EntryCount
            '
            Me.ToolStripStatusLabel_EntryCount.Name = "ToolStripStatusLabel_EntryCount"
            Me.ToolStripStatusLabel_EntryCount.Size = New System.Drawing.Size(60, 17)
            Me.ToolStripStatusLabel_EntryCount.Text = "0 entries"
            Me.ToolStripStatusLabel_EntryCount.Visible = False
            '
            'Panel_ItemBuilder
            '
            Me.Panel_ItemBuilder.Controls.Add(Me.TableLayoutPanel_ItemBuilder)
            Me.Panel_ItemBuilder.Controls.Add(Me.ToolStrip_ItemBuilder)
            Me.Panel_ItemBuilder.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel_ItemBuilder.Location = New System.Drawing.Point(0, 38)
            Me.Panel_ItemBuilder.Name = "Panel_ItemBuilder"
            Me.Panel_ItemBuilder.Size = New System.Drawing.Size(640, 340)
            Me.Panel_ItemBuilder.TabIndex = 1
            '
            'TableLayoutPanel_ItemBuilder
            '
            Me.TableLayoutPanel_ItemBuilder.ColumnCount = 2
            Me.TableLayoutPanel_ItemBuilder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel_ItemBuilder.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanel_ItemBuilder.Controls.Add(Me.Label_Name, 0, 0)
            Me.TableLayoutPanel_ItemBuilder.Controls.Add(Me.TextBox_Name, 0, 1)
            Me.TableLayoutPanel_ItemBuilder.Controls.Add(Me.Label_File, 0, 2)
            Me.TableLayoutPanel_ItemBuilder.Controls.Add(Me.TextBox_File, 0, 3)
            Me.TableLayoutPanel_ItemBuilder.Controls.Add(Me.Button_OpenFile, 1, 3)
            Me.TableLayoutPanel_ItemBuilder.Controls.Add(Me.Label_Arguments, 0, 4)
            Me.TableLayoutPanel_ItemBuilder.Controls.Add(Me.TextBox_Parameters, 0, 5)
            Me.TableLayoutPanel_ItemBuilder.Controls.Add(Me.Label_StartupType, 0, 6)
            Me.TableLayoutPanel_ItemBuilder.Controls.Add(Me.TableLayoutPanel_StartupType, 0, 7)
            Me.TableLayoutPanel_ItemBuilder.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel_ItemBuilder.Location = New System.Drawing.Point(0, 25)
            Me.TableLayoutPanel_ItemBuilder.Name = "TableLayoutPanel_ItemBuilder"
            Me.TableLayoutPanel_ItemBuilder.Padding = New System.Windows.Forms.Padding(16, 8, 16, 12)
            Me.TableLayoutPanel_ItemBuilder.RowCount = 9
            Me.TableLayoutPanel_ItemBuilder.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_ItemBuilder.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_ItemBuilder.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_ItemBuilder.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_ItemBuilder.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_ItemBuilder.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_ItemBuilder.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_ItemBuilder.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_ItemBuilder.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel_ItemBuilder.Size = New System.Drawing.Size(640, 287)
            Me.TableLayoutPanel_ItemBuilder.TabIndex = 0
            '
            'Label_Name
            '
            Me.Label_Name.AutoSize = True
            Me.TableLayoutPanel_ItemBuilder.SetColumnSpan(Me.Label_Name, 2)
            Me.Label_Name.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
            Me.Label_Name.Name = "Label_Name"
            Me.Label_Name.TabIndex = 0
            Me.Label_Name.Tag = "Theme:Caption"
            Me.Label_Name.Text = "Name"
            '
            'TextBox_Name
            '
            Me.TextBox_Name.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TableLayoutPanel_ItemBuilder.SetColumnSpan(Me.TextBox_Name, 2)
            Me.TextBox_Name.Margin = New System.Windows.Forms.Padding(3, 0, 3, 12)
            Me.TextBox_Name.Name = "TextBox_Name"
            Me.TextBox_Name.TabIndex = 1
            '
            'Label_File
            '
            Me.Label_File.AutoSize = True
            Me.TableLayoutPanel_ItemBuilder.SetColumnSpan(Me.Label_File, 2)
            Me.Label_File.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
            Me.Label_File.Name = "Label_File"
            Me.Label_File.TabIndex = 2
            Me.Label_File.Tag = "Theme:Caption"
            Me.Label_File.Text = "File"
            '
            'TextBox_File
            '
            Me.TextBox_File.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TextBox_File.Margin = New System.Windows.Forms.Padding(3, 0, 3, 12)
            Me.TextBox_File.Name = "TextBox_File"
            Me.TextBox_File.TabIndex = 3
            '
            'Button_OpenFile
            '
            Me.Button_OpenFile.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button_OpenFile.AutoSize = True
            Me.Button_OpenFile.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Button_OpenFile.Margin = New System.Windows.Forms.Padding(3, 0, 3, 12)
            Me.Button_OpenFile.Name = "Button_OpenFile"
            Me.Button_OpenFile.Padding = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.Button_OpenFile.TabIndex = 4
            Me.Button_OpenFile.Text = "Browse..."
            Me.Button_OpenFile.UseVisualStyleBackColor = True
            '
            'Label_Arguments
            '
            Me.Label_Arguments.AutoSize = True
            Me.TableLayoutPanel_ItemBuilder.SetColumnSpan(Me.Label_Arguments, 2)
            Me.Label_Arguments.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
            Me.Label_Arguments.Name = "Label_Arguments"
            Me.Label_Arguments.TabIndex = 5
            Me.Label_Arguments.Tag = "Theme:Caption"
            Me.Label_Arguments.Text = "Arguments"
            '
            'TextBox_Parameters
            '
            Me.TextBox_Parameters.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.TableLayoutPanel_ItemBuilder.SetColumnSpan(Me.TextBox_Parameters, 2)
            Me.TextBox_Parameters.Margin = New System.Windows.Forms.Padding(3, 0, 3, 12)
            Me.TextBox_Parameters.Name = "TextBox_Parameters"
            Me.TextBox_Parameters.TabIndex = 6
            '
            'Label_StartupType
            '
            Me.Label_StartupType.AutoSize = True
            Me.TableLayoutPanel_ItemBuilder.SetColumnSpan(Me.Label_StartupType, 2)
            Me.Label_StartupType.Margin = New System.Windows.Forms.Padding(3, 0, 3, 3)
            Me.Label_StartupType.Name = "Label_StartupType"
            Me.Label_StartupType.TabIndex = 7
            Me.Label_StartupType.Tag = "Theme:Caption"
            Me.Label_StartupType.Text = "Startup type"
            '
            'FlowLayoutPanel_StartupType
            '
            Me.FlowLayoutPanel_StartupType.AutoSize = True
            Me.FlowLayoutPanel_StartupType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.FlowLayoutPanel_StartupType.Controls.Add(Me.RadioButton_CurrentUser)
            Me.FlowLayoutPanel_StartupType.Controls.Add(Me.RadioButton_AllUsers)
            Me.FlowLayoutPanel_StartupType.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
            Me.FlowLayoutPanel_StartupType.Margin = New System.Windows.Forms.Padding(0)
            Me.FlowLayoutPanel_StartupType.Name = "FlowLayoutPanel_StartupType"
            Me.FlowLayoutPanel_StartupType.TabIndex = 0
            Me.FlowLayoutPanel_StartupType.WrapContents = False
            '
            'RadioButton_CurrentUser
            '
            Me.RadioButton_CurrentUser.AutoSize = True
            Me.RadioButton_CurrentUser.Checked = True
            Me.RadioButton_CurrentUser.Cursor = System.Windows.Forms.Cursors.Hand
            Me.RadioButton_CurrentUser.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.RadioButton_CurrentUser.Name = "RadioButton_CurrentUser"
            Me.RadioButton_CurrentUser.TabIndex = 0
            Me.RadioButton_CurrentUser.TabStop = True
            Me.RadioButton_CurrentUser.Tag = "CurrentUserKey"
            Me.RadioButton_CurrentUser.Text = "Current user"
            Me.RadioButton_CurrentUser.UseVisualStyleBackColor = True
            '
            'RadioButton_AllUsers
            '
            Me.RadioButton_AllUsers.AutoSize = True
            Me.RadioButton_AllUsers.Cursor = System.Windows.Forms.Cursors.Hand
            Me.RadioButton_AllUsers.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
            Me.RadioButton_AllUsers.Name = "RadioButton_AllUsers"
            Me.RadioButton_AllUsers.TabIndex = 1
            Me.RadioButton_AllUsers.Tag = "AllUsersKey"
            Me.RadioButton_AllUsers.Text = "All users"
            Me.RadioButton_AllUsers.UseVisualStyleBackColor = True
            '
            'TableLayoutPanel_StartupType
            '
            Me.TableLayoutPanel_StartupType.AutoSize = True
            Me.TableLayoutPanel_StartupType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel_StartupType.ColumnCount = 2
            Me.TableLayoutPanel_ItemBuilder.SetColumnSpan(Me.TableLayoutPanel_StartupType, 2)
            Me.TableLayoutPanel_StartupType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel_StartupType.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanel_StartupType.Controls.Add(Me.FlowLayoutPanel_StartupType, 0, 0)
            Me.TableLayoutPanel_StartupType.Controls.Add(Me.Button_AddToStartup, 1, 0)
            Me.TableLayoutPanel_StartupType.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel_StartupType.Margin = New System.Windows.Forms.Padding(0)
            Me.TableLayoutPanel_StartupType.Name = "TableLayoutPanel_StartupType"
            Me.TableLayoutPanel_StartupType.RowCount = 1
            Me.TableLayoutPanel_StartupType.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_StartupType.TabIndex = 8
            '
            'Button_AddToStartup
            '
            Me.Button_AddToStartup.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button_AddToStartup.AutoSize = True
            Me.Button_AddToStartup.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Button_AddToStartup.Enabled = False
            Me.Button_AddToStartup.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold)
            Me.Button_AddToStartup.Margin = New System.Windows.Forms.Padding(3, 0, 3, 0)
            Me.Button_AddToStartup.MinimumSize = New System.Drawing.Size(0, 36)
            Me.Button_AddToStartup.Name = "Button_AddToStartup"
            Me.Button_AddToStartup.Padding = New System.Windows.Forms.Padding(12, 0, 12, 0)
            Me.Button_AddToStartup.TabIndex = 1
            Me.Button_AddToStartup.Tag = "Theme:Accent"
            Me.Button_AddToStartup.Text = "Add to Windows startup"
            Me.Button_AddToStartup.UseVisualStyleBackColor = True
            '
            'ToolStrip_ItemBuilder
            '
            Me.ToolStrip_ItemBuilder.AllowMerge = False
            Me.ToolStrip_ItemBuilder.CanOverflow = False
            Me.ToolStrip_ItemBuilder.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
            Me.ToolStrip_ItemBuilder.ImageScalingSize = New System.Drawing.Size(16, 16)
            Me.ToolStrip_ItemBuilder.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripDropDownButton_Recent})
            Me.ToolStrip_ItemBuilder.Location = New System.Drawing.Point(0, 0)
            Me.ToolStrip_ItemBuilder.Name = "ToolStrip_ItemBuilder"
            Me.ToolStrip_ItemBuilder.Padding = New System.Windows.Forms.Padding(16, 4, 16, 0)
            Me.ToolStrip_ItemBuilder.Size = New System.Drawing.Size(640, 25)
            Me.ToolStrip_ItemBuilder.TabIndex = 1
            Me.ToolStrip_ItemBuilder.Tag = "Theme:PageToolBar"
            '
            'ToolStripDropDownButton_Recent
            '
            Me.ToolStripDropDownButton_Recent.AutoToolTip = False
            Me.ToolStripDropDownButton_Recent.DropDown = Me.ContextMenuStrip_Recent
            Me.ToolStripDropDownButton_Recent.Enabled = False
            Me.ToolStripDropDownButton_Recent.Image = Global.My.Resources.Resources.recent_files
            Me.ToolStripDropDownButton_Recent.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.ToolStripDropDownButton_Recent.Name = "ToolStripDropDownButton_Recent"
            Me.ToolStripDropDownButton_Recent.Size = New System.Drawing.Size(114, 22)
            Me.ToolStripDropDownButton_Recent.Text = "Recent Files..."
            '
            'Panel_StartupList
            '
            Me.Panel_StartupList.Controls.Add(Me.TableLayoutPanel_StartupList)
            Me.Panel_StartupList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Panel_StartupList.Location = New System.Drawing.Point(0, 38)
            Me.Panel_StartupList.Name = "Panel_StartupList"
            Me.Panel_StartupList.Size = New System.Drawing.Size(640, 340)
            Me.Panel_StartupList.TabIndex = 2
            Me.Panel_StartupList.Visible = False
            '
            'TableLayoutPanel_StartupList
            '
            Me.TableLayoutPanel_StartupList.ColumnCount = 1
            Me.TableLayoutPanel_StartupList.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel_StartupList.Controls.Add(Me.DataGridView1, 0, 0)
            Me.TableLayoutPanel_StartupList.Controls.Add(Me.TableLayoutPanel_ListActions, 0, 1)
            Me.TableLayoutPanel_StartupList.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel_StartupList.Location = New System.Drawing.Point(0, 0)
            Me.TableLayoutPanel_StartupList.Name = "TableLayoutPanel_StartupList"
            Me.TableLayoutPanel_StartupList.Padding = New System.Windows.Forms.Padding(16, 12, 16, 12)
            Me.TableLayoutPanel_StartupList.RowCount = 2
            Me.TableLayoutPanel_StartupList.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel_StartupList.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_StartupList.Size = New System.Drawing.Size(640, 340)
            Me.TableLayoutPanel_StartupList.TabIndex = 0
            '
            'DataGridView1
            '
            Me.DataGridView1.AllowUserToAddRows = False
            Me.DataGridView1.AllowUserToDeleteRows = False
            Me.DataGridView1.AllowUserToOrderColumns = True
            Me.DataGridView1.AllowUserToResizeRows = False
            Me.DataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal
            Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single
            Me.DataGridView1.ColumnHeadersHeight = 30
            Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
            Me.DataGridView1.ColumnHeadersDefaultCellStyle.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.DataGridView1.DefaultCellStyle.Padding = New System.Windows.Forms.Padding(4, 0, 4, 0)
            Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnType, Me.ColumnIcon, Me.ColumnName, Me.ColumnValue})
            Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.DataGridView1.Margin = New System.Windows.Forms.Padding(3, 0, 3, 12)
            Me.DataGridView1.MultiSelect = False
            Me.DataGridView1.Name = "DataGridView1"
            Me.DataGridView1.ReadOnly = True
            Me.DataGridView1.RowHeadersVisible = False
            Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
            Me.DataGridView1.RowTemplate.Height = 28
            Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
            Me.DataGridView1.ShowCellErrors = False
            Me.DataGridView1.ShowCellToolTips = True
            Me.DataGridView1.ShowEditingIcon = False
            Me.DataGridView1.ShowRowErrors = False
            Me.DataGridView1.TabIndex = 0
            '
            'ColumnType
            '
            Me.ColumnType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.ColumnType.HeaderText = "Type"
            Me.ColumnType.Name = "ColumnType"
            Me.ColumnType.ReadOnly = True
            '
            'ColumnIcon
            '
            Me.ColumnIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
            Me.ColumnIcon.DefaultCellStyle.NullValue = Nothing
            Me.ColumnIcon.HeaderText = "Icon"
            Me.ColumnIcon.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom
            Me.ColumnIcon.Name = "ColumnIcon"
            Me.ColumnIcon.ReadOnly = True
            Me.ColumnIcon.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
            Me.ColumnIcon.Width = 48
            '
            'ColumnName
            '
            Me.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
            Me.ColumnName.HeaderText = "Name"
            Me.ColumnName.Name = "ColumnName"
            Me.ColumnName.ReadOnly = True
            '
            'ColumnValue
            '
            Me.ColumnValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
            Me.ColumnValue.HeaderText = "Value"
            Me.ColumnValue.MinimumWidth = 120
            Me.ColumnValue.Name = "ColumnValue"
            Me.ColumnValue.ReadOnly = True
            '
            'TableLayoutPanel_ListActions
            '
            Me.TableLayoutPanel_ListActions.AutoSize = True
            Me.TableLayoutPanel_ListActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.TableLayoutPanel_ListActions.ColumnCount = 2
            Me.TableLayoutPanel_ListActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
            Me.TableLayoutPanel_ListActions.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
            Me.TableLayoutPanel_ListActions.Controls.Add(Me.FlowLayoutPanel_ListActions, 0, 0)
            Me.TableLayoutPanel_ListActions.Controls.Add(Me.Button_RefreshList, 1, 0)
            Me.TableLayoutPanel_ListActions.Dock = System.Windows.Forms.DockStyle.Fill
            Me.TableLayoutPanel_ListActions.Margin = New System.Windows.Forms.Padding(0)
            Me.TableLayoutPanel_ListActions.Name = "TableLayoutPanel_ListActions"
            Me.TableLayoutPanel_ListActions.RowCount = 1
            Me.TableLayoutPanel_ListActions.RowStyles.Add(New System.Windows.Forms.RowStyle())
            Me.TableLayoutPanel_ListActions.TabIndex = 1
            '
            'FlowLayoutPanel_ListActions
            '
            Me.FlowLayoutPanel_ListActions.AutoSize = True
            Me.FlowLayoutPanel_ListActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
            Me.FlowLayoutPanel_ListActions.Controls.Add(Me.Button_DeleteEntry)
            Me.FlowLayoutPanel_ListActions.Controls.Add(Me.Button_SendtoItemBuilder)
            Me.FlowLayoutPanel_ListActions.Controls.Add(Me.Button_OpenInRegedit)
            Me.FlowLayoutPanel_ListActions.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
            Me.FlowLayoutPanel_ListActions.Margin = New System.Windows.Forms.Padding(0)
            Me.FlowLayoutPanel_ListActions.Name = "FlowLayoutPanel_ListActions"
            Me.FlowLayoutPanel_ListActions.TabIndex = 0
            Me.FlowLayoutPanel_ListActions.WrapContents = False
            '
            'Button_DeleteEntry
            '
            Me.Button_DeleteEntry.AutoSize = True
            Me.Button_DeleteEntry.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Button_DeleteEntry.Enabled = False
            Me.Button_DeleteEntry.MinimumSize = New System.Drawing.Size(0, 32)
            Me.Button_DeleteEntry.Name = "Button_DeleteEntry"
            Me.Button_DeleteEntry.Padding = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.Button_DeleteEntry.TabIndex = 0
            Me.Button_DeleteEntry.Text = "Delete entry"
            Me.Button_DeleteEntry.UseVisualStyleBackColor = True
            '
            'Button_SendtoItemBuilder
            '
            Me.Button_SendtoItemBuilder.AutoSize = True
            Me.Button_SendtoItemBuilder.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Button_SendtoItemBuilder.Enabled = False
            Me.Button_SendtoItemBuilder.MinimumSize = New System.Drawing.Size(0, 32)
            Me.Button_SendtoItemBuilder.Name = "Button_SendtoItemBuilder"
            Me.Button_SendtoItemBuilder.Padding = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.Button_SendtoItemBuilder.TabIndex = 1
            Me.Button_SendtoItemBuilder.Text = "Send to item builder"
            Me.Button_SendtoItemBuilder.UseVisualStyleBackColor = True
            '
            'Button_OpenInRegedit
            '
            Me.Button_OpenInRegedit.AutoSize = True
            Me.Button_OpenInRegedit.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Button_OpenInRegedit.Enabled = False
            Me.Button_OpenInRegedit.MinimumSize = New System.Drawing.Size(0, 32)
            Me.Button_OpenInRegedit.Name = "Button_OpenInRegedit"
            Me.Button_OpenInRegedit.Padding = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.Button_OpenInRegedit.TabIndex = 2
            Me.Button_OpenInRegedit.Text = "Open in Regedit"
            Me.Button_OpenInRegedit.UseVisualStyleBackColor = True
            '
            'Button_RefreshList
            '
            Me.Button_RefreshList.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.Button_RefreshList.AutoSize = True
            Me.Button_RefreshList.Cursor = System.Windows.Forms.Cursors.Hand
            Me.Button_RefreshList.MinimumSize = New System.Drawing.Size(0, 32)
            Me.Button_RefreshList.Name = "Button_RefreshList"
            Me.Button_RefreshList.Padding = New System.Windows.Forms.Padding(6, 0, 6, 0)
            Me.Button_RefreshList.TabIndex = 1
            Me.Button_RefreshList.Text = "Refresh"
            Me.Button_RefreshList.UseVisualStyleBackColor = True
            '
            'ContextMenuStrip_Recent
            '
            Me.ContextMenuStrip_Recent.ImageScalingSize = New System.Drawing.Size(16, 16)
            Me.ContextMenuStrip_Recent.Name = "ContextMenuStrip_Recent"
            Me.ContextMenuStrip_Recent.OwnerItem = Me.ToolStripDropDownButton_Recent
            Me.ContextMenuStrip_Recent.Size = New System.Drawing.Size(61, 4)
            '
            'OpenFileDialog1
            '
            Me.OpenFileDialog1.FileName = ""
            '
            'Main
            '
            Me.AllowDrop = True
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(640, 372)
            Me.Controls.Add(Me.Panel_ItemBuilder)
            Me.Controls.Add(Me.Panel_StartupList)
            Me.Controls.Add(Me.StatusStrip_Main)
            Me.Controls.Add(Me.ToolStrip_Header)
            Me.Font = New System.Drawing.Font("Segoe UI", 9.75!)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "Main"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "File2Startup"
            Me.ToolStrip_Header.ResumeLayout(False)
            Me.ToolStrip_Header.PerformLayout()
            Me.StatusStrip_Main.ResumeLayout(False)
            Me.StatusStrip_Main.PerformLayout()
            Me.Panel_ItemBuilder.ResumeLayout(False)
            Me.Panel_ItemBuilder.PerformLayout()
            Me.TableLayoutPanel_ItemBuilder.ResumeLayout(False)
            Me.TableLayoutPanel_ItemBuilder.PerformLayout()
            Me.FlowLayoutPanel_StartupType.ResumeLayout(False)
            Me.FlowLayoutPanel_StartupType.PerformLayout()
            Me.TableLayoutPanel_StartupType.ResumeLayout(False)
            Me.TableLayoutPanel_StartupType.PerformLayout()
            Me.ToolStrip_ItemBuilder.ResumeLayout(False)
            Me.ToolStrip_ItemBuilder.PerformLayout()
            Me.Panel_StartupList.ResumeLayout(False)
            Me.TableLayoutPanel_StartupList.ResumeLayout(False)
            Me.TableLayoutPanel_StartupList.PerformLayout()
            CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TableLayoutPanel_ListActions.ResumeLayout(False)
            Me.TableLayoutPanel_ListActions.PerformLayout()
            Me.FlowLayoutPanel_ListActions.ResumeLayout(False)
            Me.FlowLayoutPanel_ListActions.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents ToolStrip_Header As System.Windows.Forms.ToolStrip
        Friend WithEvents ToolStripButton_ItemBuilder As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripButton_StartupList As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripButton_About As System.Windows.Forms.ToolStripButton
        Friend WithEvents ToolStripToggleSwitch_Theme As Global.UserInterface.Controls.ToolStripToggleSwitch
        Friend WithEvents ToolStripDropDownButton_Language As System.Windows.Forms.ToolStripDropDownButton
        Friend WithEvents EnglishToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents SpanishToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents PortugueseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
        Friend WithEvents StatusStrip_Main As System.Windows.Forms.StatusStrip
        Friend WithEvents ToolStripStatusLabel_Elevation As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents ToolStripStatusLabel_EntryCount As System.Windows.Forms.ToolStripStatusLabel
        Friend WithEvents Panel_ItemBuilder As System.Windows.Forms.Panel
        Friend WithEvents TableLayoutPanel_ItemBuilder As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents Label_Name As System.Windows.Forms.Label
        Friend WithEvents TextBox_Name As System.Windows.Forms.TextBox
        Friend WithEvents Label_File As System.Windows.Forms.Label
        Friend WithEvents TextBox_File As System.Windows.Forms.TextBox
        Friend WithEvents Button_OpenFile As System.Windows.Forms.Button
        Friend WithEvents Label_Arguments As System.Windows.Forms.Label
        Friend WithEvents TextBox_Parameters As System.Windows.Forms.TextBox
        Friend WithEvents Label_StartupType As System.Windows.Forms.Label
        Friend WithEvents FlowLayoutPanel_StartupType As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents RadioButton_CurrentUser As System.Windows.Forms.RadioButton
        Friend WithEvents RadioButton_AllUsers As System.Windows.Forms.RadioButton
        Friend WithEvents TableLayoutPanel_StartupType As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents ToolStrip_ItemBuilder As System.Windows.Forms.ToolStrip
        Friend WithEvents ToolStripDropDownButton_Recent As System.Windows.Forms.ToolStripDropDownButton
        Friend WithEvents Button_AddToStartup As Global.UserInterface.Controls.IconButton
        Friend WithEvents Panel_StartupList As System.Windows.Forms.Panel
        Friend WithEvents TableLayoutPanel_StartupList As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
        Friend WithEvents ColumnType As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnIcon As System.Windows.Forms.DataGridViewImageColumn
        Friend WithEvents ColumnName As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents ColumnValue As System.Windows.Forms.DataGridViewTextBoxColumn
        Friend WithEvents TableLayoutPanel_ListActions As System.Windows.Forms.TableLayoutPanel
        Friend WithEvents FlowLayoutPanel_ListActions As System.Windows.Forms.FlowLayoutPanel
        Friend WithEvents Button_DeleteEntry As Global.UserInterface.Controls.IconButton
        Friend WithEvents Button_SendtoItemBuilder As Global.UserInterface.Controls.IconButton
        Friend WithEvents Button_OpenInRegedit As Global.UserInterface.Controls.IconButton
        Friend WithEvents Button_RefreshList As Global.UserInterface.Controls.IconButton
        Friend WithEvents ContextMenuStrip_Recent As System.Windows.Forms.ContextMenuStrip
        Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
        Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    End Class

End Namespace
