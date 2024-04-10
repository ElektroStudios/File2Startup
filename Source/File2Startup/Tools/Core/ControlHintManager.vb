' ***********************************************************************
' Author   : ElektroStudios
' Modified : 17-July-2021
' ***********************************************************************


' UNUSED CODE HAS BEEN PRUNED. 06/APRIL/2024




' THIS OPEN-SOURCE APPLICATION IS POWERED BY DEVCASE CLASS LIBRARY, CREATED BY ELEKTRO STUDIOS.

' WHAT YOU SEE HERE IS FREE CUTTED CONTENT OF THIS FRAMEWORK.

' IF YOU LIKED THIS FREE APPLICATION, THEN PLEASE CONSIDER TO BUY DEVCASE CLASS LIBRARY FOR .NET AT:
' https://codecanyon.net/item/DevCase-class-library-for-net/19260282

' YOU CAN FIND THESE HELPER METHODS AND A MASSIVE AMOUNT MORE!, 
' +850 EXTENSION METHODS FOR ALL KIND OF TYPES, CUSTOM USER-CONTROLS, 
' EVERYTHING FOR THE NEWBIE And THE ADVANCED USER, FOR VB.NET AND C#. 

' DevCase is a utility framework containing new APIs and extensions to the core .NET Framework 
' to help complete your developer toolbox.
' It Is a set of general purpose classes provided as easy to consume packages.
' These utility classes and components provide productivity in day to day software development 
' mainly focused To WindowsForms. 

' UPDATES OF DevCase ARE MAINTAINED AND RELEASED EVERY MONTH.





#Region " Public Members Summary "

#Region " Properties "

' ControlHints As Dictionary(Of Control, ControlHintInfo)

#End Region

#Region " Methods "

' SetHint(Control, ControlHintInfo)
' SetHint(Control(), ControlHintInfo)

' RemoveHint(Control)
' RemoveHint(Control())

#End Region

#End Region

#Region " Usage Examples "

'Dim hintInfo1 As New ControlHintInfo("I'm a persistent hint.", Nothing,
'                                     Color.Gray, ControlHintType.Normal)
'
'Dim hintInfo2 As New ControlHintInfo("I've set this hint on multiple controls at once!", Nothing,
'                                     Color.Gray, ControlHintType.Normal)
'
'Dim hintInfo3 As New ControlHintInfo("Write something here...", New Font("lucida console", 15),
'                                     Color.YellowGreen, ControlHintType.Normal)
'
'SetHint(TextBox1, hintInfo1)
'SetHint({TextBox2, TextBox3}, hintInfo2)
'SetHint(RichTextBox1, hintInfo3)

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Reflection

Imports DevCase.Core.Application.UserInterface.Enums
Imports DevCase.Core.Application.UserInterface.Types

Imports DevCase.Core.Diagnostics.Assembly.Reflection

#End Region

#Region " ControlHint Manager "


' ReSharper disable once CheckNamespace

Namespace DevCase.Core.Application.Forms

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Manages the control-hints of the edit-controls of a <see cref="Form"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code language="VB.NET">
    ''' Dim hintInfo1 As New ControlHintInfo("I'm a persistent hint.", Nothing,
    '''                                      Color.Gray, ControlHintType.Normal)
    ''' 
    ''' Dim hintInfo2 As New ControlHintInfo("I've set this hint on multiple controls at once!", Nothing,
    '''                                      Color.Gray, ControlHintType.Normal)
    ''' 
    ''' Dim hintInfo3 As New ControlHintInfo("Write something here...", New Font("lucida console", 15),
    '''                                      Color.YellowGreen, ControlHintType.Normal)
    ''' 
    ''' SetHint(TextBox1, hintInfo1)
    ''' SetHint({TextBox2, TextBox3}, hintInfo2)
    ''' SetHint(RichTextBox1, hintInfo3)
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    <CodeAnalysis.SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification:="Required to migrate this code to .NET Core")>
    Public NotInheritable Class ControlHintManager

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the control-hints that has been created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The control-hints that has been created.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public Shared ReadOnly Property ControlHints As Dictionary(Of Control, ControlHintInfo)
            Get
                Return controlHintsB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' The control-hints that has been created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared controlHintsB As Dictionary(Of Control, ControlHintInfo)

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' ( Backing field )
        ''' Remembers the handled controls and they default property values.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared controlHintsDefaults As Dictionary(Of Control, ControlHintInfo)

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Prevents a default instance of the <see cref="ControlHintManager"/> class from being created.
        ''' </summary>
        <DebuggerNonUserCode>
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets a new control-hint for an specific control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="ctrl">
        ''' The control.
        ''' </param>
        ''' 
        ''' <param name="hintInfo">
        ''' The text-hint properties.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <exception cref="Exception">
        ''' control-hint text can't be null or empty.
        ''' </exception>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub SetHint(ctrl As Control,
                                  hintInfo As ControlHintInfo)

            Dim text As String = hintInfo.Text
            Dim font As Font = hintInfo.Font
            Dim foreColor As Color = hintInfo.ForeColor

            If String.IsNullOrEmpty(text) Then
                Throw New ArgumentNullException(message:="Control-hint text can't be null or empty.", paramName:=NameOf(hintInfo))
            End If

            If font Is Nothing Then
                font = UtilReflection.GetPropertyValue(Of Font)(ctrl, "Font", BindingFlags.Instance Or BindingFlags.Public)
            End If

            If foreColor = Color.Empty Then
                foreColor = UtilReflection.GetPropertyValue(Of Color)(ctrl, "ForeColor", BindingFlags.Instance Or BindingFlags.Public)
            End If

            ' Ready to handle the Control.
            ControlHintManager.InstanceControlHintFields()
            Select Case ControlHintManager.controlHintsB.ContainsKey(ctrl)

                Case True ' Control-hint is already set and handled.
                    ' Just replace the new hint properties in the collection.
                    ControlHintManager.controlHintsB(ctrl) = hintInfo

                Case False ' Control-hint is not set.
                    ' Set the default control property values to restore them when needed.
                    Dim deaults As New ControlHintInfo(String.Empty,
                                                       UtilReflection.GetPropertyValue(Of Font)(ctrl, "Font", BindingFlags.Instance Or BindingFlags.Public),
                                                       UtilReflection.GetPropertyValue(Of Color)(ctrl, "ForeColor", BindingFlags.Instance Or BindingFlags.Public),
                                                       ControlHintType.Normal)

                    ' Add the hint properties into collections.
                    ControlHintManager.controlHintsB.Add(ctrl, hintInfo)
                    ControlHintManager.controlHintsDefaults.Add(ctrl, deaults)

                    ' Remove previously handled events (if any).
                    RemoveHandler ctrl.HandleCreated, AddressOf ControlHintManager.Control_HandleCreated
                    RemoveHandler ctrl.Enter, AddressOf ControlHintManager.Control_Enter
                    RemoveHandler ctrl.Leave, AddressOf ControlHintManager.Control_Leave
                    RemoveHandler ctrl.Disposed, AddressOf ControlHintManager.Control_Disposed
                    RemoveHandler ctrl.MouseDown, AddressOf ControlHintManager.Control_MouseDown
                    RemoveHandler ctrl.KeyDown, AddressOf ControlHintManager.Control_KeyDown
                    RemoveHandler ctrl.CausesValidationChanged, AddressOf ControlHintManager.Control_CausesValidationChanged

                    ' Start handling the control events.
                    AddHandler ctrl.HandleCreated, AddressOf ControlHintManager.Control_HandleCreated
                    AddHandler ctrl.Enter, AddressOf ControlHintManager.Control_Enter
                    AddHandler ctrl.Leave, AddressOf ControlHintManager.Control_Leave
                    AddHandler ctrl.Disposed, AddressOf ControlHintManager.Control_Disposed
                    AddHandler ctrl.MouseDown, AddressOf ControlHintManager.Control_MouseDown
                    AddHandler ctrl.KeyDown, AddressOf ControlHintManager.Control_KeyDown
                    AddHandler ctrl.CausesValidationChanged, AddressOf ControlHintManager.Control_CausesValidationChanged

                    ' Force trigger 'Control_CausesValidationChanged' event-handler to set the text hint on the control.
                    Dim olCausesValidation As Boolean = ctrl.CausesValidation
                    ctrl.CausesValidation = Not olCausesValidation
                    ctrl.CausesValidation = olCausesValidation
            End Select

        End Sub



        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets a new control-hint for multiple controls at once.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="controls">
        ''' The controls.
        ''' </param>
        ''' 
        ''' <param name="hintInfo">
        ''' The control-hint properties.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub SetHint(controls As Control(),
                                  hintInfo As ControlHintInfo)

            For Each control As Control In controls
                ControlHintManager.SetHint(control, hintInfo)
            Next control

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes a control-hint from a control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="ctrl">
        ''' The control.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub RemoveHint(ctrl As Control)

            ControlHintManager.InstanceControlHintFields()
            Select Case controlHintsB.ContainsKey(ctrl)

                Case True ' Control-hint is already set.

                    With ctrl
                        RemoveHandler .HandleCreated, AddressOf ControlHintManager.Control_HandleCreated
                        RemoveHandler .Enter, AddressOf ControlHintManager.Control_Enter
                        RemoveHandler .Leave, AddressOf ControlHintManager.Control_Leave
                        RemoveHandler .Disposed, AddressOf ControlHintManager.Control_Disposed
                        RemoveHandler .MouseDown, AddressOf ControlHintManager.Control_MouseDown
                        RemoveHandler .KeyDown, AddressOf ControlHintManager.Control_KeyDown
                        RemoveHandler .CausesValidationChanged, AddressOf ControlHintManager.Control_CausesValidationChanged
                    End With

                    If Not ctrl.IsDisposed Then

                        Dim ctrlDefaults As ControlHintInfo = controlHintsDefaults(ctrl)

                        If ctrl.Text.Equals(controlHintsB(ctrl).Text, StringComparison.OrdinalIgnoreCase) Then
                            ControlHintManager.RestoreProperties(ctrl, ctrlDefaults)
                        Else
#Disable Warning CA1861 ' Avoid constant arrays as arguments
                            ControlHintManager.RestoreProperties(ctrl, ctrlDefaults, skipProperties:={"Text"})
#Enable Warning CA1861 ' Avoid constant arrays as arguments
                        End If

                    End If

                    ' Remove the control-hints from hints collections.
                    ControlHintManager.controlHintsB.Remove(ctrl)
                    ControlHintManager.controlHintsDefaults.Remove(ctrl)

            End Select

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Removes a control-hint from a control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="ctrls">
        ''' The controls.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Shared Sub RemoveHint(ctrls As Control())

            For Each ctrl As Control In ctrls
                ControlHintManager.RemoveHint(ctrl)
            Next ctrl

        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Instances the <see cref="controlHintsB"/> 
        ''' and/or <see cref="controlHintsDefaults"/> members.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub InstanceControlHintFields()

            If controlHintsB Is Nothing Then
                ControlHintManager.controlHintsB = New Dictionary(Of Control, ControlHintInfo)
            End If

            If controlHintsDefaults Is Nothing Then
                ControlHintManager.controlHintsDefaults = New Dictionary(Of Control, ControlHintInfo)
            End If

        End Sub

        ' ReSharper disable once SuggestBaseTypeForParameter
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Sets the properties of an specific control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="ctrl">
        ''' The Control.
        ''' </param>
        ''' 
        ''' <param name="hintInfo">
        ''' The properties to set it's values.
        ''' </param>
        ''' 
        ''' <param name="skipProperties">
        ''' The properties to skip.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub SetProperties(ctrl As Object,
                                         hintInfo As ControlHintInfo,
                                         Optional skipProperties As String() = Nothing)

            ' Get the control type.
            Dim ctrlType As Type = ctrl.GetType
            Dim excludeProperties As String() = If(skipProperties, {""})

            ' Loop through the 'ControlHintInfo' properties to 
            ' determine whether exist all the needed properties in the Control.
            For Each hintProp As PropertyInfo In hintInfo.GetType().GetProperties

                Dim prop As PropertyInfo = ctrlType.GetProperty(hintProp.Name)

                If prop IsNot Nothing AndAlso Not excludeProperties.Contains(prop.Name) Then
                    prop.SetValue(ctrl, hintProp.GetValue(hintInfo, Nothing), Nothing)
                End If

            Next hintProp

        End Sub

        ' ReSharper disable once SuggestBaseTypeForParameter
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Restores the properties of the specified control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="ctrl">
        ''' The Control.
        ''' </param>
        ''' 
        ''' <param name="defaultProperties">
        ''' The properties to reset it's values.
        ''' </param>
        ''' 
        ''' <param name="skipProperties">
        ''' The properties to skip.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub RestoreProperties(ctrl As Object,
                                             defaultProperties As ControlHintInfo,
                                             Optional skipProperties As String() = Nothing)

            Dim excludeProperties As String() = If(skipProperties, {""})

            ' Get the control type.
            Dim ctrlType As Type = ctrl.GetType

            ' Loop through the 'DefaultProperties' properties to 
            ' determine whether exist all the needed properties in the Control.
            For Each defaultProp As PropertyInfo In defaultProperties.GetType().GetProperties.Reverse

                Dim prop As PropertyInfo = ctrlType.GetProperty(defaultProp.Name)

                If prop IsNot Nothing AndAlso Not excludeProperties.Contains(prop.Name) Then
                    prop.SetValue(ctrl, defaultProp.GetValue(defaultProperties, Nothing), Nothing)
                End If

            Next defaultProp

        End Sub

#End Region

#Region "Event Handlers "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.HandleCreated"/> event of the Control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub Control_HandleCreated(sender As Object, e As EventArgs)

            ControlHintManager.InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim hintInfo As ControlHintInfo = ControlHintManager.controlHintsB(ctrl)

            ControlHintManager.SetProperties(ctrl, hintInfo)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.Enter"/> event of the Control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub Control_Enter(sender As Object, e As EventArgs)

            ControlHintManager.InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim ctrlText As String = ctrl.Text
            Dim ctrlDefaults As ControlHintInfo = ControlHintManager.controlHintsDefaults(ctrl)
            Dim hintInfo As ControlHintInfo = ControlHintManager.controlHintsB(ctrl)

            Select Case hintInfo.HintType

                Case ControlHintType.Normal
                    If ctrlText.Equals(hintInfo.Text, StringComparison.OrdinalIgnoreCase) Then
                        RestoreProperties(ctrl, ctrlDefaults)
                    End If

            End Select

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.Leave"/> event of the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub Control_Leave(sender As Object, e As EventArgs)

            ControlHintManager.InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim ctrlText As String = ctrl.Text
            Dim ctrlDefaults As ControlHintInfo = ControlHintManager.controlHintsDefaults(ctrl)
            Dim hintInfo As ControlHintInfo = ControlHintManager.controlHintsB(ctrl)

            Select Case hintInfo.HintType

                Case ControlHintType.Normal
                    If ctrlText.Equals(hintInfo.Text, StringComparison.OrdinalIgnoreCase) Then
                        ControlHintManager.RestoreProperties(ctrl, ctrlDefaults)

                    ElseIf String.IsNullOrEmpty(ctrlText) Then
                        ControlHintManager.SetProperties(ctrl, hintInfo)

                    End If

                Case ControlHintType.Persistent
                    If String.IsNullOrEmpty(ctrlText) Then
                        ControlHintManager.SetProperties(ctrl, hintInfo)
                    End If

            End Select

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.MouseDown"/> event of the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="MouseEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub Control_MouseDown(sender As Object, e As MouseEventArgs)

            ControlHintManager.InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim ctrlText As String = ctrl.Text
            Dim hintInfo As ControlHintInfo = ControlHintManager.controlHintsB(ctrl)

            Select Case hintInfo.HintType

                Case ControlHintType.Persistent

                    If ctrlText.Equals(hintInfo.Text, StringComparison.OrdinalIgnoreCase) Then

                        ' Get the 'Select' control's method (if exist).
                        Dim method As MethodInfo =
                            sender.GetType().GetMethod("Select",
                                                     BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.InvokeMethod,
                                                     Nothing,
                                                     {GetType(Integer), GetType(Integer)},
                                                     Nothing)

                        ' Select the zero length.
                        method?.Invoke(ctrl, New Object() {0I, 0I})

                    End If

            End Select

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.KeyDown"/> event of the Control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="KeyEventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub Control_KeyDown(sender As Object, e As KeyEventArgs)

            ControlHintManager.InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim ctrlText As String = ctrl.Text
            Dim ctrlDefaults As ControlHintInfo = ControlHintManager.controlHintsDefaults(ctrl)
            Dim hintInfo As ControlHintInfo = ControlHintManager.controlHintsB(ctrl)

            Select Case hintInfo.HintType

                Case ControlHintType.Persistent
                    If ctrlText.Equals(hintInfo.Text, StringComparison.OrdinalIgnoreCase) Then
                        ControlHintManager.RestoreProperties(ctrl, ctrlDefaults)

                    ElseIf String.IsNullOrEmpty(ctrlText) Then
#Disable Warning CA1861 ' Avoid constant arrays as arguments
                        ControlHintManager.RestoreProperties(ctrl, ctrlDefaults, skipProperties:={"Text"})
#Enable Warning CA1861 ' Avoid constant arrays as arguments

                    End If

                Case ControlHintType.Normal
                    If String.IsNullOrEmpty(ctrlText) Then
                        ControlHintManager.RestoreProperties(ctrl, ctrlDefaults)
                    End If

            End Select

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.CausesValidationChanged"/> event of the Control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.
        ''' </param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub Control_CausesValidationChanged(sender As Object, e As EventArgs)

            ControlHintManager.InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim ctrlText As String = ctrl.Text
            Dim ctrlDefaults As ControlHintInfo = ControlHintManager.controlHintsDefaults(ctrl)
            Dim hintInfo As ControlHintInfo = ControlHintManager.controlHintsB(ctrl)


            Select Case hintInfo.HintType

                Case ControlHintType.Normal
                    If ctrl.GetContainerControl.ActiveControl.Equals(ctrl) AndAlso ctrlText.Equals(hintInfo.Text, StringComparison.OrdinalIgnoreCase) Then
                        ControlHintManager.RestoreProperties(ctrl, ctrlDefaults)

                    ElseIf String.IsNullOrEmpty(ctrlText) Then
                        ControlHintManager.SetProperties(ctrl, hintInfo)

                    End If

                Case ControlHintType.Persistent
                    If String.IsNullOrEmpty(ctrlText) Then
                        ControlHintManager.SetProperties(ctrl, hintInfo)
                    End If

            End Select


        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Control.Disposed"/> event of the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="sender">
        ''' The source of the event.</param>
        ''' 
        ''' <param name="e">
        ''' The <see cref="EventArgs"/> instance containing the event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Sub Control_Disposed(sender As Object, e As EventArgs)

            ControlHintManager.RemoveHint(DirectCast(sender, Control))

        End Sub

#End Region

    End Class

End Namespace

#End Region
