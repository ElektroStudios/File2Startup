




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

#End Region

#Region " ControlHint Manager "


Namespace DevCase.Core.Application.UserInterface.Tools.Graphical

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Manages the control-hints of the edit-controls of a <see cref="Form"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
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
        Public Shared Sub SetHint(ctrl As Control, hintInfo As ControlHintInfo)

            Dim text As String = hintInfo.Text
            Dim font As Font = hintInfo.Font
            Dim foreColor As Color = hintInfo.ForeColor

            If String.IsNullOrEmpty(text) Then
                Throw New ArgumentNullException(paramName:="hintInfo", message:="Control-hint text can't be null or empty.")
                Exit Sub
            End If

            If (font Is Nothing) Then
                font = GetPropertyValue(Of Font)(ctrl, "Font")
            End If

            If (foreColor = Color.Empty) Then
                foreColor = GetPropertyValue(Of Color)(ctrl, "ForeColor")
            End If

            ' Ready to handle the Control.
            InstanceControlHintFields()
            Select Case controlHintsB.ContainsKey(ctrl)

                Case True ' Control-hint is already set and handled.
                    ' Just replace the new hint properties in the collection.
                    controlHintsB(ctrl) = hintInfo

                Case False ' Control-hint is not set.
                    ' Set the default control property values to restore them when needed.
                    Dim deaults As New ControlHintInfo(String.Empty,
                                                       GetPropertyValue(Of Font)(ctrl, "Font"),
                                                       GetPropertyValue(Of Color)(ctrl, "ForeColor"),
                                                       ControlHintType.Normal)

                    ' Add the hint properties into collections.
                    controlHintsB.Add(ctrl, hintInfo)
                    controlHintsDefaults.Add(ctrl, deaults)

                    With ctrl

                        ' Remove previouslly handled events (if any).
                        RemoveHandler .HandleCreated, AddressOf Control_HandleCreated
                        RemoveHandler .Enter, AddressOf Control_Enter
                        RemoveHandler .Leave, AddressOf Control_Leave
                        RemoveHandler .Disposed, AddressOf Control_Disposed
                        RemoveHandler .MouseDown, AddressOf Control_MouseDown
                        RemoveHandler .KeyDown, AddressOf Control_KeyDown

                        ' Start handling the control events.
                        AddHandler .HandleCreated, AddressOf Control_HandleCreated
                        AddHandler .Enter, AddressOf Control_Enter
                        AddHandler .Leave, AddressOf Control_Leave
                        AddHandler .Disposed, AddressOf Control_Disposed
                        AddHandler .MouseDown, AddressOf Control_MouseDown
                        AddHandler .KeyDown, AddressOf Control_KeyDown

                    End With

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
        Public Shared Sub SetHint(controls As Control(), hintInfo As ControlHintInfo)

            For Each control As Control In controls
                SetHint(control, hintInfo)
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

            InstanceControlHintFields()
            Select Case controlHintsB.ContainsKey(ctrl)

                Case True ' Control-hint is already set.

                    With ctrl
                        RemoveHandler .HandleCreated, AddressOf Control_HandleCreated
                        RemoveHandler .Enter, AddressOf Control_Enter
                        RemoveHandler .Leave, AddressOf Control_Leave
                        RemoveHandler .Disposed, AddressOf Control_Disposed
                        RemoveHandler .MouseDown, AddressOf Control_MouseDown
                        RemoveHandler .KeyDown, AddressOf Control_KeyDown
                    End With

                    If Not (ctrl.IsDisposed) Then

                        Dim ctrlDefaults As ControlHintInfo = controlHintsDefaults(ctrl)

                        If (ctrl.Text.Equals(controlHintsB(ctrl).Text, StringComparison.OrdinalIgnoreCase)) Then
                            RestoreProperties(ctrl, ctrlDefaults)
                        Else
                            RestoreProperties(ctrl, ctrlDefaults, skipProperties:={"Text"})
                        End If

                    End If

                    ' Remove the control-hints from hints collections.
                    controlHintsB.Remove(ctrl)
                    controlHintsDefaults.Remove(ctrl)

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
                RemoveHint(ctrl)
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

            If (controlHintsB Is Nothing) Then
                controlHintsB = New Dictionary(Of Control, ControlHintInfo)
            End If

            If (controlHintsDefaults Is Nothing) Then
                controlHintsDefaults = New Dictionary(Of Control, ControlHintInfo)
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the property value of an specific control through reflection.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="ctrl">
        ''' The Control.
        ''' </param>
        ''' 
        ''' <param name="propName">
        ''' The name of the property to get it's value.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' If the property is not found on the Control, the return value is <see langword="Nothing"/>,
        ''' Otherwise, the return value is the control's property value.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Private Shared Function GetPropertyValue(Of T)(ctrl As Control,
propName As String) As T

            Dim prop As PropertyInfo = ctrl.GetType().GetProperty(propName)

            Select Case (prop Is Nothing)

                Case False
                    Return DirectCast(prop.GetValue(ctrl, Nothing), T)

                Case Else
                    Return Nothing

            End Select

        End Function

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
        Private Shared Sub SetProperties(ctrl As Object, hintInfo As ControlHintInfo,
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

                If Not (prop Is Nothing) AndAlso Not (excludeProperties.Contains(prop.Name)) Then
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

            InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim hintInfo As ControlHintInfo = controlHintsB(ctrl)

            SetProperties(ctrl, hintInfo)

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

            InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim ctrlText As String = ctrl.Text
            Dim ctrlDefaults As ControlHintInfo = controlHintsDefaults(ctrl)
            Dim hintInfo As ControlHintInfo = controlHintsB(ctrl)

            Select Case hintInfo.HintType

                Case ControlHintType.Normal
                    If (ctrlText.Equals(hintInfo.Text, StringComparison.OrdinalIgnoreCase)) Then
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

            InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim ctrlText As String = ctrl.Text
            Dim ctrlDefaults As ControlHintInfo = controlHintsDefaults(ctrl)
            Dim hintInfo As ControlHintInfo = controlHintsB(ctrl)

            Select Case hintInfo.HintType

                Case ControlHintType.Normal
                    If (ctrlText.Equals(hintInfo.Text, StringComparison.OrdinalIgnoreCase)) Then
                        RestoreProperties(ctrl, ctrlDefaults)

                    ElseIf String.IsNullOrEmpty(ctrlText) Then
                        SetProperties(ctrl, hintInfo)

                    End If

                Case ControlHintType.Persistent
                    If String.IsNullOrEmpty(ctrlText) Then
                        SetProperties(ctrl, hintInfo)
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

            InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim ctrlText As String = ctrl.Text
            Dim hintInfo As ControlHintInfo = controlHintsB(ctrl)

            Select Case hintInfo.HintType

                Case ControlHintType.Persistent

                    If (ctrlText.Equals(hintInfo.Text, StringComparison.OrdinalIgnoreCase)) Then

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

            InstanceControlHintFields()

            Dim ctrl As Control = DirectCast(sender, Control)
            Dim ctrlText As String = ctrl.Text
            Dim ctrlDefaults As ControlHintInfo = controlHintsDefaults(ctrl)
            Dim hintInfo As ControlHintInfo = controlHintsB(ctrl)

            Select Case hintInfo.HintType

                Case ControlHintType.Persistent
                    If (ctrlText.Equals(hintInfo.Text, StringComparison.OrdinalIgnoreCase)) Then
                        RestoreProperties(ctrl, ctrlDefaults)

                    ElseIf String.IsNullOrEmpty(ctrlText) Then
                        RestoreProperties(ctrl, ctrlDefaults, skipProperties:={"Text"})

                    End If

                Case ControlHintType.Normal
                    If String.IsNullOrEmpty(ctrlText) Then
                        RestoreProperties(ctrl, ctrlDefaults)
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

            RemoveHint(DirectCast(sender, Control))

        End Sub

#End Region

    End Class

End Namespace

#End Region
