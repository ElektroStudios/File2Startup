' https://www.vbforums.com/showthread.php?614155-ToolStripCheckBox

Imports System.Windows.Forms
Imports System.Windows.Forms.Design

<ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip), DebuggerStepThrough()>
Public Class ToolStripCheckBox
    Inherits ToolStripControlHost

    Public Sub New()
        MyBase.New(New CheckBox(), NameOf(ToolStripCheckBox))
        Me.CheckBoxControl.BackColor = Color.Transparent
    End Sub

    Public ReadOnly Property CheckBoxControl() As CheckBox
        Get
            Return DirectCast(Me.Control, CheckBox)
        End Get
    End Property

    Public Property Checked() As Boolean
        Get
            Return Me.CheckBoxControl.Checked
        End Get
        Set(value As Boolean)
            Me.CheckBoxControl.Checked = value
        End Set
    End Property

    Protected Overrides Sub OnSubscribeControlEvents(c As Control)
        MyBase.OnSubscribeControlEvents(c)
        AddHandler DirectCast(c, CheckBox).CheckedChanged, AddressOf Me.OnCheckedChanged
    End Sub

    Protected Overrides Sub OnUnsubscribeControlEvents(c As Control)
        MyBase.OnUnsubscribeControlEvents(c)
        RemoveHandler DirectCast(c, CheckBox).CheckedChanged, AddressOf Me.OnCheckedChanged
    End Sub

    Public Event CheckedChanged As EventHandler

    Private Sub OnCheckedChanged(sender As Object, e As EventArgs)
        RaiseEvent CheckedChanged(Me, e)
    End Sub

End Class