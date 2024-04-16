' ***********************************************************************
' Author   : ElektroStudios
' Modified : 11-April-2024
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Windows.Forms.Design

#End Region

#Region " ToolStripCheckBox "

' ReSharper disable once CheckNamespace

Namespace DevCase.UI.Components

    ''' <summary>
    ''' Represents a selectable <see cref="ToolStripItem"/> that when clicked, toggles a checkmark.
    ''' </summary>
    ''' <seealso cref="ToolStripControlHost"/>
    <
        ComVisible(True),
        DebuggerStepThrough,
        DefaultEvent(NameOf(ToolStripCheckBox.CheckedChanged)),
        DefaultProperty(NameOf(ToolStripCheckBox.Text)),
        Description("Represents a selectable ToolStripItem that when clicked, toggles a checkmark."),
        Designer("System.Windows.Forms.Design.ToolStripItemDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
        DesignerCategory(NameOf(DesignerCategoryAttribute.Generic)),
        DesignTimeVisible(False),
        DisplayName(NameOf(ToolStripCheckBox)),
        Localizable(True),
        ToolboxBitmap(GetType(CheckBox), "CheckBox.bmp"),
        ToolboxItem(False),
        ToolboxItemFilter("System.Windows.Forms", ToolboxItemFilterType.Allow),
        ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip Or ToolStripItemDesignerAvailability.StatusStrip)
    >
    Public Class ToolStripCheckBox : Inherits ToolStripControlHost

#Region " Properties "

        ''' <summary>
        ''' Gets the <see cref="CheckBox"/> control that is hosted by this <see cref="ToolStripCheckBox"/>.
        ''' </summary>
        <
            Browsable(True), EditorBrowsable(EditorBrowsableState.Advanced),
            DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
            Category("Hosted"), Description("The CheckBox control that is hosted by this control.")
        >
        Public Shadows ReadOnly Property Control As CheckBox
            Get
                Return DirectCast(MyBase.Control, CheckBox)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether this <see cref="ToolStripCheckBox"/> is in the checked state.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' <see langword="True"/> if checked; otherwise, <see langword="False"/>.
        ''' </returns>
        <
            Bindable(True), SettingsBindable(True),
            DefaultValue(False),
            RefreshProperties(RefreshProperties.All),
            Category("Appearance"), Description("Specifies whether this control is in the checked state.")
        >
        Public Property Checked As Boolean
            Get
                Return Me.Control.Checked
            End Get
            Set(value As Boolean)
                Me.Control.Checked = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the checked state of this <see cref="ToolStripCheckBox"/>.
        ''' </summary>
        ''' 
        ''' <returns>
        ''' One of the <see cref="System.Windows.Forms.CheckState"/> enumeration values. 
        ''' <para></para>
        ''' The default value is <see cref="System.Windows.Forms.CheckState.Unchecked"/>.
        ''' </returns>
        ''' 
        ''' <exception cref="System.ComponentModel.InvalidEnumArgumentException">
        ''' The value assigned is not one of the <see cref="System.Windows.Forms.CheckState"/> enumeration values.
        ''' </exception>
        <
            Bindable(True),
            DefaultValue(CheckState.Unchecked),
            RefreshProperties(RefreshProperties.All),
            Category("Appearance"), Description("Specifies the checked state of this control.")
        >
        Public Property CheckState As CheckState
            Get
                Return Me.Control.CheckState
            End Get
            Set(value As CheckState)
                Me.Control.CheckState = value
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value indicating whether this <see cref="ToolStripCheckBox"/> 
        ''' will allow three check states rather than two.
        ''' </summary>
        ''' 
        ''' <remarks>
        ''' If the <see cref="ToolStripCheckBox.ThreeState"/> property is set to <see langword="False"/>, 
        ''' the <see cref="ToolStripCheckBox.CheckState"/> property value can only be set to 
        ''' the <see cref="System.Windows.Forms.CheckState.Indeterminate"/> value in code, 
        ''' and not by user interaction doing click on the control.
        ''' </remarks>
        ''' 
        ''' <returns>
        ''' <see langword="True"/> if this <see cref="ToolStripCheckBox"/> 
        ''' is able to display three check states; otherwise, <see langword="False"/>.
        ''' <para></para>
        ''' The default value is <see langword="False"/>.
        ''' </returns>
        <
            DefaultValue(False),
            Category("Behavior"), Description("Specifies whether this control will allow three check states rather than two.")
        >
        Public Property ThreeState As Boolean
            Get
                Return Me.Control.ThreeState
            End Get
            Set(value As Boolean)
                Me.Control.ThreeState = value
            End Set
        End Property

#End Region

#Region " Events "

        ''' <summary>
        ''' Occurs whenever the <see cref="ToolStripCheckBox.Checked"/> property is changed.
        ''' </summary>
        Public Event CheckedChanged As EventHandler

        ''' <summary>
        ''' Occurs whenever the <see cref="ToolStripCheckBox.CheckState"/> property is changed.
        ''' </summary>
        Public Event CheckStateChanged As EventHandler

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ToolStripCheckBox"/> class.
        ''' </summary>
        Public Sub New()

            MyBase.New(New CheckBox())
            Me.Control.BackColor = Color.Transparent
        End Sub

#End Region

#Region " Event Invocators "

        ''' <summary>
        ''' Raises the <see cref="ToolStripCheckBox.CheckedChanged"/> event.
        ''' </summary>
        ''' 
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArgs"/> instance containing the event data.
        ''' </param>
        Private Sub OnCheckedChanged(sender As Object, e As EventArgs)
            If Me.CheckedChangedEvent IsNot Nothing Then
                RaiseEvent CheckedChanged(Me, e)
            End If
        End Sub

        ''' <summary>
        ''' Raises the <see cref="ToolStripCheckBox.CheckStateChanged"/> event.
        ''' </summary>
        ''' 
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArgs"/> instance containing the event data.
        ''' </param>
        Private Sub OnCheckStateChanged(sender As Object, e As EventArgs)
            If Me.CheckStateChangedEvent IsNot Nothing Then
                RaiseEvent CheckStateChanged(Me, e)
            End If
        End Sub

#End Region

#Region " Event Invocators (Overriden) "

        ''' <summary>
        ''' Subscribes events from the hosted control
        ''' </summary>
        ''' 
        ''' <param name="control">
        ''' The control from which to subscribe events.
        ''' </param>
        Protected Overrides Sub OnSubscribeControlEvents(control As Control)
            MyBase.OnSubscribeControlEvents(control)
            AddHandler DirectCast(control, CheckBox).CheckedChanged, AddressOf Me.OnCheckedChanged
        End Sub

        ''' <summary>
        ''' Unsubscribes events from the hosted control
        ''' </summary>
        ''' 
        ''' <param name="control">
        ''' The control from which to unsubscribe events.
        ''' </param>
        Protected Overrides Sub OnUnsubscribeControlEvents(control As Control)
            MyBase.OnUnsubscribeControlEvents(control)
            RemoveHandler DirectCast(control, CheckBox).CheckedChanged, AddressOf Me.OnCheckedChanged
        End Sub

        ''' <summary>
        ''' Raises the <see cref="Windows.Forms.Control.ParentChanged"/> event.
        ''' </summary>
        ''' 
        ''' <param name="oldParent">
        ''' The original parent of the item.
        ''' </param>
        ''' 
        ''' <param name="newParent">
        ''' The new parent of the item.
        ''' </param>
        Protected Overrides Sub OnParentChanged(oldParent As ToolStrip, newParent As ToolStrip)
            MyBase.OnParentChanged(oldParent, newParent)
        End Sub

        ''' <summary>
        ''' Raises the <see cref="ToolStripItem.OwnerChanged"/> event.
        ''' </summary>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArgs"/> instance containing the event data.
        ''' </param>
        Protected Overrides Sub OnOwnerChanged(e As EventArgs)
            MyBase.OnOwnerChanged(e)
        End Sub

#End Region

    End Class

End Namespace

#End Region
