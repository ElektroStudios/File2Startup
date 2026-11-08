' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ToolStripToggleSwitch.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

#End Region

#Region " ToolStripToggleSwitch "

Namespace UserInterface.Controls

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' A tool strip item that hosts a <see cref="ToggleSwitch"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    <ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip Or ToolStripItemDesignerAvailability.StatusStrip)>
    <DefaultEvent(NameOf(ToolStripToggleSwitch.CheckedChanged))>
    Public Class ToolStripToggleSwitch : Inherits ToolStripControlHost

#Region " Events "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when the value of the <see cref="ToolStripToggleSwitch.Checked"/> property changes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Action")>
        Public Event CheckedChanged As EventHandler

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the hosted <see cref="ToggleSwitch"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(False)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public ReadOnly Property ToggleSwitch As ToggleSwitch
            Get
                Return DirectCast(Me.Control, ToggleSwitch)
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets a value indicating whether the switch is on.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Appearance")>
        <DefaultValue(False)>
        Public Property Checked As Boolean
            Get
                Return Me.ToggleSwitch.Checked
            End Get
            Set(value As Boolean)
                Me.ToggleSwitch.Checked = value
            End Set
        End Property

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="ToolStripToggleSwitch"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New()

            MyBase.New(New ToggleSwitch())
            Me.AutoSize = False
            Me.Size = Me.ToggleSwitch.Size

        End Sub

#End Region

#Region " Overridden Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Subscribes to the events of the hosted control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="control">
        ''' The hosted control.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnSubscribeControlEvents(control As Control)

            MyBase.OnSubscribeControlEvents(control)
            AddHandler DirectCast(control, ToggleSwitch).CheckedChanged, AddressOf Me.HostedToggleSwitch_CheckedChanged

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Unsubscribes from the events of the hosted control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="control">
        ''' The hosted control.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnUnsubscribeControlEvents(control As Control)

            RemoveHandler DirectCast(control, ToggleSwitch).CheckedChanged, AddressOf Me.HostedToggleSwitch_CheckedChanged
            MyBase.OnUnsubscribeControlEvents(control)

        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="ToggleSwitch.CheckedChanged"/> event of the hosted control.
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
        Private Sub HostedToggleSwitch_CheckedChanged(sender As Object, e As EventArgs)

            RaiseEvent CheckedChanged(Me, e)

        End Sub

#End Region

    End Class

End Namespace

#End Region
