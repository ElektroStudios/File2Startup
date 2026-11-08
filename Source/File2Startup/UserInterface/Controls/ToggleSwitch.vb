' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ToggleSwitch.vb" company="Elektro Studios">
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
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

#End Region

#Region " ToggleSwitch "

Namespace UserInterface.Controls

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' An owner-drawn on/off switch (like the toggle switches of the Windows Settings app),
    ''' with an animated thumb and optional sun/moon glyphs to switch between a light and a dark theme.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <remarks>
    ''' The switch can be toggled with the mouse, with the Space key, and through UI Automation (default action).
    ''' </remarks>
    ''' ----------------------------------------------------------------------------------------------------
    <DefaultEvent(NameOf(ToggleSwitch.CheckedChanged))>
    <DefaultProperty(NameOf(ToggleSwitch.Checked))>
    Public Class ToggleSwitch : Inherits Control

#Region " Constants "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The duration, in milliseconds, of the thumb animation.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Const AnimationDurationMs As Double = 120.0R

#End Region

#Region " Fields "

        ''' <summary>The backing field of <see cref="ToggleSwitch.Checked"/>.</summary>
        Private checkedValue As Boolean

        ''' <summary>The backing field of <see cref="ToggleSwitch.OnColor"/>.</summary>
        Private onColorValue As Color = SystemColors.Highlight

        ''' <summary>The backing field of <see cref="ToggleSwitch.OffColor"/>.</summary>
        Private offColorValue As Color = SystemColors.Window

        ''' <summary>The backing field of <see cref="ToggleSwitch.BorderColor"/>.</summary>
        Private borderColorValue As Color = SystemColors.ControlText

        ''' <summary>The backing field of <see cref="ToggleSwitch.ThumbOnColor"/>.</summary>
        Private thumbOnColorValue As Color = SystemColors.HighlightText

        ''' <summary>The backing field of <see cref="ToggleSwitch.ThumbOffColor"/>.</summary>
        Private thumbOffColorValue As Color = SystemColors.ControlText

        ''' <summary>The backing field of <see cref="ToggleSwitch.Glyphs"/>.</summary>
        Private glyphsValue As ToggleSwitchGlyphs = ToggleSwitchGlyphs.SunAndMoon

        ''' <summary>The current thumb position: 0 is the off position and 1 is the on position.</summary>
        Private thumbPosition As Double

        ''' <summary>The thumb position when the current animation started.</summary>
        Private animationStartPosition As Double

        ''' <summary>The time when the current animation started.</summary>
        Private animationStartTime As Date

        ''' <summary>The timer that drives the thumb animation.</summary>
        Private ReadOnly animationTimer As Timer

        ''' <summary>A value indicating whether the mouse pointer is over the control.</summary>
        Private isMouseOver As Boolean

#End Region

#Region " Events "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Occurs when the value of the <see cref="ToggleSwitch.Checked"/> property changes.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Action")>
        <Description("Occurs when the value of the Checked property changes.")>
        Public Event CheckedChanged As EventHandler

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets a value indicating whether the switch is on.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Appearance")>
        <DefaultValue(False)>
        <Bindable(True)>
        <Description("Indicates whether the switch is on.")>
        Public Property Checked As Boolean
            Get
                Return Me.checkedValue
            End Get
            Set(value As Boolean)
                If value = Me.checkedValue Then
                    Exit Property
                End If

                Me.checkedValue = value
                Me.StartThumbAnimation()
                Me.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1)
                Me.OnCheckedChanged(EventArgs.Empty)
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the track color when the switch is on.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Appearance")>
        <Description("The track color when the switch is on.")>
        Public Property OnColor As Color
            Get
                Return Me.onColorValue
            End Get
            Set(value As Color)
                Me.onColorValue = value
                Me.Invalidate()
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the track color when the switch is off.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Appearance")>
        <Description("The track color when the switch is off.")>
        Public Property OffColor As Color
            Get
                Return Me.offColorValue
            End Get
            Set(value As Color)
                Me.offColorValue = value
                Me.Invalidate()
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the track border color when the switch is off.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Appearance")>
        <Description("The track border color when the switch is off.")>
        Public Property BorderColor As Color
            Get
                Return Me.borderColorValue
            End Get
            Set(value As Color)
                Me.borderColorValue = value
                Me.Invalidate()
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the thumb color when the switch is on.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Appearance")>
        <Description("The thumb color when the switch is on.")>
        Public Property ThumbOnColor As Color
            Get
                Return Me.thumbOnColorValue
            End Get
            Set(value As Color)
                Me.thumbOnColorValue = value
                Me.Invalidate()
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the thumb color when the switch is off.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Appearance")>
        <Description("The thumb color when the switch is off.")>
        Public Property ThumbOffColor As Color
            Get
                Return Me.thumbOffColorValue
            End Get
            Set(value As Color)
                Me.thumbOffColorValue = value
                Me.Invalidate()
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the glyphs drawn inside the thumb.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Category("Appearance")>
        <DefaultValue(GetType(ToggleSwitchGlyphs), "SunAndMoon")>
        <Description("The glyphs drawn inside the thumb.")>
        Public Property Glyphs As ToggleSwitchGlyphs
            Get
                Return Me.glyphsValue
            End Get
            Set(value As ToggleSwitchGlyphs)
                Me.glyphsValue = value
                Me.Invalidate()
            End Set
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets a value indicating whether the thumb animation is running.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <Browsable(False)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public ReadOnly Property IsAnimating As Boolean
            Get
                Return Me.animationTimer.Enabled
            End Get
        End Property

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the default size of the control.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides ReadOnly Property DefaultSize As Size
            Get
                Return New Size(44, 22)
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="ToggleSwitch"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New()

            Me.SetStyle(ControlStyles.UserPaint Or
                        ControlStyles.AllPaintingInWmPaint Or
                        ControlStyles.OptimizedDoubleBuffer Or
                        ControlStyles.ResizeRedraw Or
                        ControlStyles.Selectable Or
                        ControlStyles.SupportsTransparentBackColor, True)

            ' A fast double click must toggle the switch twice, not raise a double click.
            Me.SetStyle(ControlStyles.StandardDoubleClick, False)

            Me.animationTimer = New Timer() With {.Interval = 15}
            AddHandler Me.animationTimer.Tick, AddressOf Me.AnimationTimer_Tick

            Me.Cursor = Cursors.Hand
            Me.TabStop = True
            Me.AccessibleRole = AccessibleRole.CheckButton

        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Inverts the value of the <see cref="ToggleSwitch.Checked"/> property.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub Toggle()

            Me.Checked = Not Me.Checked

        End Sub

#End Region

#Region " Overridden Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Raises the <see cref="ToggleSwitch.CheckedChanged"/> event.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overridable Sub OnCheckedChanged(e As EventArgs)

            RaiseEvent CheckedChanged(Me, e)

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Toggles the switch when it is clicked with the left mouse button.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnMouseClick(e As MouseEventArgs)

            MyBase.OnMouseClick(e)
            If e.Button = MouseButtons.Left Then
                Me.Focus()
                Me.Toggle()
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Toggles the switch when the Space key is released.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnKeyUp(e As KeyEventArgs)

            MyBase.OnKeyUp(e)
            If e.KeyCode = Keys.Space AndAlso e.Modifiers = Keys.None Then
                Me.Toggle()
                e.Handled = True
            End If

        End Sub

        ''' <inheritdoc/>
        Protected Overrides Sub OnMouseEnter(e As EventArgs)

            MyBase.OnMouseEnter(e)
            Me.isMouseOver = True
            Me.Invalidate()

        End Sub

        ''' <inheritdoc/>
        Protected Overrides Sub OnMouseLeave(e As EventArgs)

            MyBase.OnMouseLeave(e)
            Me.isMouseOver = False
            Me.Invalidate()

        End Sub

        ''' <inheritdoc/>
        Protected Overrides Sub OnGotFocus(e As EventArgs)

            MyBase.OnGotFocus(e)
            Me.Invalidate()

        End Sub

        ''' <inheritdoc/>
        Protected Overrides Sub OnLostFocus(e As EventArgs)

            MyBase.OnLostFocus(e)
            Me.Invalidate()

        End Sub

        ''' <inheritdoc/>
        Protected Overrides Sub OnEnabledChanged(e As EventArgs)

            MyBase.OnEnabledChanged(e)
            Me.Invalidate()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Paints the switch.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub OnPaint(e As PaintEventArgs)

            MyBase.OnPaint(e)

            Dim g As Graphics = e.Graphics
            g.SmoothingMode = SmoothingMode.AntiAlias
            g.PixelOffsetMode = PixelOffsetMode.HighQuality

            Dim layout As ToggleSwitchLayout = ToggleSwitchLayout.Calculate(Me.ClientSize, Me.thumbPosition)
            If layout.Track.Width <= 0 OrElse layout.Track.Height <= 0 Then
                Exit Sub
            End If

            Dim isOn As Boolean = Me.thumbPosition >= 0.5R
            Dim trackColor As Color = ToggleSwitch.Blend(Me.offColorValue, Me.onColorValue, Me.thumbPosition)
            Dim thumbColor As Color = If(isOn, Me.thumbOnColorValue, Me.thumbOffColorValue)
            If Not Me.Enabled Then
                trackColor = ToggleSwitch.Blend(trackColor, Me.BackColor, 0.5R)
                thumbColor = ToggleSwitch.Blend(thumbColor, Me.BackColor, 0.5R)
            End If

            ' Track.
            Using trackPath As GraphicsPath = ToggleSwitch.CreateCapsule(layout.Track),
                  trackBrush As New SolidBrush(trackColor)

                g.FillPath(trackBrush, trackPath)
                If Not isOn Then
                    Using borderPen As New Pen(Me.borderColorValue, 1.0F)
                        g.DrawPath(borderPen, trackPath)
                    End Using
                End If
            End Using

            ' Thumb (slightly bigger under the mouse pointer, like the Windows 11 switches).
            Dim thumb As RectangleF = layout.Thumb
            If Me.isMouseOver AndAlso Me.Enabled Then
                thumb.Inflate(thumb.Width * 0.08F, thumb.Height * 0.08F)
            End If
            Using thumbBrush As New SolidBrush(thumbColor)
                g.FillEllipse(thumbBrush, thumb)
            End Using

            ' Glyph inside the thumb, painted with the track color so it is "cut" from the thumb.
            If Me.glyphsValue = ToggleSwitchGlyphs.SunAndMoon Then
                If isOn Then
                    ToggleSwitch.DrawMoonGlyph(g, thumb, trackColor, thumbColor)
                Else
                    ToggleSwitch.DrawSunGlyph(g, thumb, trackColor)
                End If
            End If

            ' Focus rectangle.
            If Me.Focused AndAlso Me.ShowFocusCues Then
                Dim focusBounds As Rectangle = Me.ClientRectangle
                focusBounds.Width -= 1
                focusBounds.Height -= 1
                ControlPaint.DrawFocusRectangle(g, focusBounds, Me.ForeColor, Me.BackColor)
            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates the accessibility object of the control, that exposes the checked state and the toggle action.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The accessibility object.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Function CreateAccessibilityInstance() As AccessibleObject

            Return New ToggleSwitchAccessibleObject(Me)

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Releases the unmanaged resources used by the control and optionally releases the managed resources.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="disposing">
        ''' <see langword="True"/> to release both managed and unmanaged resources.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Protected Overrides Sub Dispose(disposing As Boolean)

            If disposing Then
                Me.animationTimer.Dispose()
            End If
            MyBase.Dispose(disposing)

        End Sub

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Starts the thumb animation towards the position of the current <see cref="ToggleSwitch.Checked"/> value,
        ''' or moves the thumb directly if the control is not visible.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub StartThumbAnimation()

            Dim target As Double = If(Me.checkedValue, 1.0R, 0.0R)

            ' The thumb is animated only when the user can see it (the switch is shown on a visible form).
            Dim ownerForm As Form = Me.FindForm()
            Dim isDisplayed As Boolean = Me.IsHandleCreated AndAlso Me.Visible AndAlso ownerForm IsNot Nothing AndAlso ownerForm.Visible

            If Not (isDisplayed AndAlso SystemInformation.UIEffectsEnabled) Then
                Me.animationTimer.Stop()
                Me.thumbPosition = target
                Me.Invalidate()
                Exit Sub
            End If

            Me.animationStartPosition = Me.thumbPosition
            Me.animationStartTime = Date.UtcNow
            Me.animationTimer.Start()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Timer.Tick"/> event of the animation timer.
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
        Private Sub AnimationTimer_Tick(sender As Object, e As EventArgs)

            Dim target As Double = If(Me.checkedValue, 1.0R, 0.0R)
            Dim progress As Double = Math.Min(1.0R, (Date.UtcNow - Me.animationStartTime).TotalMilliseconds / ToggleSwitch.AnimationDurationMs)

            ' Ease-out cubic.
            Dim eased As Double = 1.0R - Math.Pow(1.0R - progress, 3.0R)
            Me.thumbPosition = Me.animationStartPosition + (target - Me.animationStartPosition) * eased

            If progress >= 1.0R Then
                Me.thumbPosition = target
                Me.animationTimer.Stop()
            End If

            Me.Invalidate()

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Linearly interpolates two colors.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="color1">
        ''' The color at amount zero.
        ''' </param>
        '''
        ''' <param name="color2">
        ''' The color at amount one.
        ''' </param>
        '''
        ''' <param name="amount">
        ''' The interpolation amount, from 0 to 1.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The interpolated color.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Friend Shared Function Blend(color1 As Color, color2 As Color, amount As Double) As Color

            Dim t As Double = Math.Max(0.0R, Math.Min(1.0R, amount))
            Dim lerp As Func(Of Byte, Byte, Integer) =
                Function(a As Byte, b As Byte) CInt(Math.Round(a + (CInt(b) - CInt(a)) * t))

            Return Color.FromArgb(lerp(color1.A, color2.A), lerp(color1.R, color2.R), lerp(color1.G, color2.G), lerp(color1.B, color2.B))

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Creates a capsule (rectangle with fully rounded ends) path.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="bounds">
        ''' The bounds of the capsule.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' The path.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Function CreateCapsule(bounds As RectangleF) As GraphicsPath

            Dim diameter As Single = Math.Min(bounds.Width, bounds.Height)
            Dim path As New GraphicsPath()
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 90.0F, 180.0F)
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270.0F, 180.0F)
            path.CloseFigure()
            Return path

        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Draws a sun glyph (a disc with eight rays) inside the thumb.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="g">
        ''' The graphics.
        ''' </param>
        '''
        ''' <param name="thumb">
        ''' The thumb bounds.
        ''' </param>
        '''
        ''' <param name="color">
        ''' The glyph color.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub DrawSunGlyph(g As Graphics, thumb As RectangleF, color As Color)

            Dim center As New PointF(thumb.X + thumb.Width / 2.0F, thumb.Y + thumb.Height / 2.0F)
            Dim size As Single = thumb.Width
            Dim discRadius As Single = size * 0.16F
            Dim rayInner As Single = size * 0.25F
            Dim rayOuter As Single = size * 0.36F

            Using brush As New SolidBrush(color),
                  pen As New Pen(color, Math.Max(1.0F, size * 0.075F)) With {.StartCap = LineCap.Round, .EndCap = LineCap.Round}

                g.FillEllipse(brush, center.X - discRadius, center.Y - discRadius, discRadius * 2.0F, discRadius * 2.0F)

                For ray As Integer = 0 To 7
                    Dim angle As Double = ray * Math.PI / 4.0R
                    Dim cos As Single = CSng(Math.Cos(angle))
                    Dim sin As Single = CSng(Math.Sin(angle))
                    g.DrawLine(pen,
                               center.X + cos * rayInner, center.Y + sin * rayInner,
                               center.X + cos * rayOuter, center.Y + sin * rayOuter)
                Next ray
            End Using

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Draws a crescent moon glyph inside the thumb.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="g">
        ''' The graphics.
        ''' </param>
        '''
        ''' <param name="thumb">
        ''' The thumb bounds.
        ''' </param>
        '''
        ''' <param name="color">
        ''' The glyph color.
        ''' </param>
        '''
        ''' <param name="thumbColor">
        ''' The thumb color, used to cut the crescent.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Private Shared Sub DrawMoonGlyph(g As Graphics, thumb As RectangleF, color As Color, thumbColor As Color)

            Dim size As Single = thumb.Width
            Dim moonDiameter As Single = size * 0.62F
            Dim moon As New RectangleF(thumb.X + (size - moonDiameter) / 2.0F, thumb.Y + (size - moonDiameter) / 2.0F, moonDiameter, moonDiameter)

            Dim cut As RectangleF = moon
            cut.Offset(moonDiameter * 0.34F, -moonDiameter * 0.22F)

            ' The cut circle is clipped to the thumb, so it never paints over the track.
            Dim state As GraphicsState = g.Save()
            Try
                Using thumbPath As New GraphicsPath(),
                      moonBrush As New SolidBrush(color),
                      cutBrush As New SolidBrush(thumbColor)

                    thumbPath.AddEllipse(thumb)
                    g.SetClip(thumbPath, CombineMode.Intersect)
                    g.FillEllipse(moonBrush, moon)
                    g.FillEllipse(cutBrush, cut)
                End Using

            Finally
                g.Restore(state)

            End Try

        End Sub

#End Region

#Region " ToggleSwitchAccessibleObject "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The accessibility object of a <see cref="ToggleSwitch"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private NotInheritable Class ToggleSwitchAccessibleObject : Inherits ControlAccessibleObject

            ''' <summary>The owner switch.</summary>
            Private ReadOnly toggleSwitch As ToggleSwitch

            ''' <summary>Initializes a new instance of the <see cref="ToggleSwitchAccessibleObject"/> class.</summary>
            ''' <param name="owner">The owner switch.</param>
            Public Sub New(owner As ToggleSwitch)
                MyBase.New(owner)
                Me.toggleSwitch = owner
            End Sub

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property State As AccessibleStates
                Get
                    Dim result As AccessibleStates = MyBase.State
                    If Me.toggleSwitch.Checked Then
                        result = result Or AccessibleStates.Checked
                    End If
                    Return result
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides ReadOnly Property DefaultAction As String
                Get
                    Return "Toggle"
                End Get
            End Property

            ''' <inheritdoc/>
            Public Overrides Sub DoDefaultAction()
                Me.toggleSwitch.Toggle()
            End Sub

        End Class

#End Region

    End Class

End Namespace

#End Region
