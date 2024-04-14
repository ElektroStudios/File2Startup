<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AboutForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AboutForm))
        Me.LinkLabelProductUrl = New System.Windows.Forms.LinkLabel()
        Me.DataGridViewLoadedAssemblies = New System.Windows.Forms.DataGridView()
        Me.LabelProductName = New System.Windows.Forms.Label()
        Me.LabelVersion = New System.Windows.Forms.Label()
        Me.LabelArchitecture = New System.Windows.Forms.Label()
        Me.LabelCompany = New System.Windows.Forms.Label()
        Me.LabelCopyright = New System.Windows.Forms.Label()
        Me.LabelDescription = New System.Windows.Forms.Label()
        Me.LabelLoadedAssemblies = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.DataGridViewLoadedAssemblies, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LinkLabelProductUrl
        '
        resources.ApplyResources(Me.LinkLabelProductUrl, "LinkLabelProductUrl")
        Me.LinkLabelProductUrl.Name = "LinkLabelProductUrl"
        Me.LinkLabelProductUrl.TabStop = True
        '
        'DataGridViewLoadedAssemblies
        '
        resources.ApplyResources(Me.DataGridViewLoadedAssemblies, "DataGridViewLoadedAssemblies")
        Me.DataGridViewLoadedAssemblies.AllowUserToAddRows = False
        Me.DataGridViewLoadedAssemblies.AllowUserToDeleteRows = False
        Me.DataGridViewLoadedAssemblies.AllowUserToOrderColumns = True
        Me.DataGridViewLoadedAssemblies.AllowUserToResizeRows = False
        Me.DataGridViewLoadedAssemblies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewLoadedAssemblies.MultiSelect = False
        Me.DataGridViewLoadedAssemblies.Name = "DataGridViewLoadedAssemblies"
        Me.DataGridViewLoadedAssemblies.ReadOnly = True
        Me.DataGridViewLoadedAssemblies.RowHeadersVisible = False
        Me.DataGridViewLoadedAssemblies.RowTemplate.Height = 25
        Me.DataGridViewLoadedAssemblies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        '
        'LabelProductName
        '
        resources.ApplyResources(Me.LabelProductName, "LabelProductName")
        Me.LabelProductName.Name = "LabelProductName"
        '
        'LabelVersion
        '
        resources.ApplyResources(Me.LabelVersion, "LabelVersion")
        Me.LabelVersion.Name = "LabelVersion"
        '
        'LabelArchitecture
        '
        resources.ApplyResources(Me.LabelArchitecture, "LabelArchitecture")
        Me.LabelArchitecture.Name = "LabelArchitecture"
        '
        'LabelCompany
        '
        resources.ApplyResources(Me.LabelCompany, "LabelCompany")
        Me.LabelCompany.Name = "LabelCompany"
        '
        'LabelCopyright
        '
        resources.ApplyResources(Me.LabelCopyright, "LabelCopyright")
        Me.LabelCopyright.Name = "LabelCopyright"
        '
        'LabelDescription
        '
        resources.ApplyResources(Me.LabelDescription, "LabelDescription")
        Me.LabelDescription.Name = "LabelDescription"
        '
        'LabelLoadedAssemblies
        '
        resources.ApplyResources(Me.LabelLoadedAssemblies, "LabelLoadedAssemblies")
        Me.LabelLoadedAssemblies.Name = "LabelLoadedAssemblies"
        '
        'PictureBox1
        '
        resources.ApplyResources(Me.PictureBox1, "PictureBox1")
        Me.PictureBox1.BackgroundImage = Global.My.Resources.Resources.Elektro
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.TabStop = False
        '
        'AboutForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.LabelLoadedAssemblies)
        Me.Controls.Add(Me.LabelDescription)
        Me.Controls.Add(Me.LabelCopyright)
        Me.Controls.Add(Me.LabelCompany)
        Me.Controls.Add(Me.LabelArchitecture)
        Me.Controls.Add(Me.LabelVersion)
        Me.Controls.Add(Me.LabelProductName)
        Me.Controls.Add(Me.DataGridViewLoadedAssemblies)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.LinkLabelProductUrl)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutForm"
        Me.ShowInTaskbar = False
        CType(Me.DataGridViewLoadedAssemblies, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents LinkLabelProductUrl As LinkLabel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents DataGridViewLoadedAssemblies As DataGridView
    Friend WithEvents LabelProductName As Label
    Friend WithEvents LabelVersion As Label
    Friend WithEvents LabelArchitecture As Label
    Friend WithEvents LabelCompany As Label
    Friend WithEvents LabelCopyright As Label
    Friend WithEvents LabelDescription As Label
    Friend WithEvents LabelLoadedAssemblies As Label
End Class
