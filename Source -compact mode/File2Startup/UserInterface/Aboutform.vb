#Region " Option Statements "

Option Explicit On
Option Strict On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Reflection

#End Region

#Region " AboutForm "

''' <summary>
''' Tthe 'About' form, responsible for displaying application information.
''' </summary>
Partial Friend NotInheritable Class AboutForm : Inherits Form

#Region " Event Handlers "

    ''' <summary>
    ''' Handles the <see cref="Form.Load"/> event of the <see cref="AboutForm"/> form.
    ''' </summary>
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="EventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepperBoundary>
    Private Sub AboutForm_Load(sender As Object, e As EventArgs) _
    Handles MyBase.Load

        Me.Text = $"About {My.Application.Info.Title}"
        Me.InitializeControlValues()
    End Sub

    ''' <summary>
    ''' Handles the <see cref="LinkLabel.LinkClicked"/> event 
    ''' of the <see cref="AboutForm.LinkLabelProductUrl"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepperBoundary>
    Private Sub LinkLabelProductUrl_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) _
    Handles LinkLabelProductUrl.LinkClicked

        Dim lbl As LinkLabel = DirectCast(sender, LinkLabel)
        Try
            Using pr As New Process
                pr.StartInfo.FileName = lbl.Text
                pr.StartInfo.UseShellExecute = True
                pr.Start()
            End Using

        Catch ex As Exception
            MessageBox.Show(Me, "Error opening the URL:" & Environment.NewLine & Environment.NewLine & ex.Message,
                            My.Application.Info.Title, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

        End Try
    End Sub

    Private Sub DataGridViewLoadedAssemblies_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) _
        Handles DataGridViewLoadedAssemblies.CellFormatting

        If e.ColumnIndex = Me.DataGridViewLoadedAssemblies.Columns("ProcessorArchitecture")?.Index AndAlso e.Value IsNot Nothing Then
            e.Value = Me.FormatArchitecture(CType(e.Value, ProcessorArchitecture))
            e.FormattingApplied = True
        End If
    End Sub


    ''' <summary>
    ''' Handles the <see cref="DataGridView.DataError"/> event 
    ''' of the <see cref="AboutForm.DataGridViewLoadedAssemblies"/> control.
    ''' </summary>
    ''' 
    ''' <param name="sender">
    ''' The source of the event.
    ''' </param>
    ''' 
    ''' <param name="e">
    ''' The <see cref="DataGridViewDataErrorEventArgs"/> instance containing the event data.
    ''' </param>
    <DebuggerStepperBoundary>
    Private Sub DataGridViewLoadedAssemblies_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) _
        Handles DataGridViewLoadedAssemblies.DataError

        e.ThrowException = False
    End Sub

#End Region

#Region " Private Methods "

    ''' <summary>
    ''' Sets the values for the controls in the <see cref="AboutForm"/> form.
    ''' </summary>
    <DebuggerStepperBoundary>
    Private Sub InitializeControlValues()

        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelDescription.Text = My.Application.Info.Description
        Me.LabelVersion.Text = $"v{My.Application.Info.Version}"
        Me.LabelArchitecture.Text = Me.FormatArchitecture(Assembly.GetExecutingAssembly.GetName().ProcessorArchitecture)
        Me.LabelCompany.Text = My.Application.Info.CompanyName
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LinkLabelProductUrl.Text = "https://github.com/ElektroStudios/File2Startup"

        With Me.DataGridViewLoadedAssemblies
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            .DataSource = New BindingList(Of AssemblyName)(
                            (From ass As Assembly In My.Application.Info.LoadedAssemblies
                             Order By ass.GetName.Name
                             Select ass.GetName
                            ).ToList())

            .Columns("CodeBase").Visible = False
            .Columns("ContentType").Visible = False
            .Columns("CultureInfo").Visible = False
            .Columns("CultureName").Visible = False
            .Columns("EscapedCodeBase").Visible = False
            .Columns("Flags").Visible = False
            .Columns("HashAlgorithm").Visible = False
            .Columns("KeyPair").Visible = False
            .Columns("VersionCompatibility").Visible = False

            .Columns("ProcessorArchitecture").HeaderText = "Architecture"
            ' Adjust "ProcessorArchitecture" column width after header name changed,
            ' and make the column not resizable, 
            .Columns("ProcessorArchitecture").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

            ' Make "Version" column not resizable.
            .Columns("Version").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells

            ' Allow to resize "Name" and "FullName" columns width.
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        End With

    End Sub

    ''' <summary>
    ''' Formats the input <see cref="ProcessorArchitecture"/> value to a human readable string.
    ''' <para></para>
    ''' Possible values are: 
    ''' "32-Bit", "64-Bit", "Neutral (x86/x64)",
    ''' "ARM" and "Undefined Process Architecture".
    ''' </summary>
    ''' 
    ''' <param name="architecture">
    ''' The input <see cref="ProcessorArchitecture"/> value.
    ''' </param>
    ''' 
    ''' <returns>
    ''' A human readable string representing the current process architecture.
    ''' </returns>
    <DebuggerStepThrough>
    Private Function FormatArchitecture(architecture As ProcessorArchitecture) As String

        Select Case architecture
            Case ProcessorArchitecture.X86
                Return "32-Bit"

            Case ProcessorArchitecture.Amd64, ProcessorArchitecture.IA64
                Return "64-Bit"

            Case ProcessorArchitecture.MSIL
                Return "Neutral (x86/x64)"

            Case ProcessorArchitecture.Arm
                Return "ARM"

            Case ProcessorArchitecture.None
                Return "Undefined"

            Case Else
                Return architecture.ToString()
        End Select

    End Function

#End Region

End Class

#End Region