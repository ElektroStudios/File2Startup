' ***********************************************************************
' Author   : ElektroStudios
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="IconButton.vb" company="Elektro Studios">
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
Imports System.Windows.Forms

#End Region

#Region " IconButton "

Namespace UserInterface.Controls

    ''' <summary>
    ''' A <see cref="Button"/> that draws an icon before its text, separated by a configurable space,
    ''' and centers the icon and the text together as a single block.
    ''' </summary>
    ''' <remarks>
    ''' The standard <see cref="Button"/> places the image right next to the text
    ''' (<see cref="TextImageRelation.ImageBeforeText"/> has no spacing option), and with
    ''' <see cref="FlatStyle.Flat"/> the text bounds are calculated differently, so the image can even overlap the text.
    ''' <para></para>
    ''' This button keeps the native painting of the background, border and focus cues (so it keeps the
    ''' default Windows look with <see cref="FlatStyle.Standard"/>), and draws the icon and the text itself.
    ''' <para></para>
    ''' Use <see cref="IconButton.Icon"/> instead of <see cref="ButtonBase.Image"/>.
    ''' </remarks>
    <DesignerCategory("Code")>
    Public Class IconButton : Inherits Button

#Region " Constants "

        ''' <summary>
        ''' The default space, in pixels at 96 DPI, between the icon and the text.
        ''' </summary>
        Public Const DefaultIconTextSpacing As Integer = 8

#End Region

#Region " Fields "

        ''' <summary>The backing field of <see cref="IconButton.Icon"/>.</summary>
        Private iconValue As Image

        ''' <summary>The backing field of <see cref="IconButton.IconTextSpacing"/>.</summary>
        Private iconTextSpacingValue As Integer = IconButton.DefaultIconTextSpacing

        ''' <summary>The backing field of <see cref="IconButton.DisabledForeColor"/>.</summary>
        Private disabledForeColorValue As Color = SystemColors.GrayText

        ''' <summary>
        ''' A value indicating whether the base class is painting,
        ''' so <see cref="IconButton.Text"/> returns an empty string and the base class does not draw the text.
        ''' </summary>
        Private isBasePainting As Boolean

        ''' <summary>The scale factor of the system DPI relative to 96 DPI.</summary>
        Private Shared ReadOnly dpiScale As Single = IconButton.GetSystemDpiScale()

#End Region

#Region " Properties "

        ''' <summary>
        ''' Gets or sets the icon drawn before the text. It is drawn at its own size.
        ''' </summary>
        <Category("Appearance")>
        <DefaultValue(GetType(Image), Nothing)>
        <Description("The icon drawn before the text.")>
        Public Property Icon As Image
            Get
                Return Me.iconValue
            End Get
            Set(value As Image)
                Me.iconValue = value
                Me.OnContentSizeChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the space, in pixels at 96 DPI, between the icon and the text. It is scaled to the system DPI.
        ''' </summary>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' The value is negative.
        ''' </exception>
        <Category("Appearance")>
        <DefaultValue(IconButton.DefaultIconTextSpacing)>
        <Description("The space, in pixels at 96 DPI, between the icon and the text.")>
        Public Property IconTextSpacing As Integer
            Get
                Return Me.iconTextSpacingValue
            End Get
            Set(value As Integer)
                If value < 0 Then
                    Throw New ArgumentOutOfRangeException(NameOf(value), value, "The spacing cannot be negative.")
                End If
                Me.iconTextSpacingValue = value
                Me.OnContentSizeChanged()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the text color when the button is disabled.
        ''' </summary>
        <Category("Appearance")>
        <Description("The text color when the button is disabled.")>
        Public Property DisabledForeColor As Color
            Get
                Return Me.disabledForeColorValue
            End Get
            Set(value As Color)
                Me.disabledForeColorValue = value
                Me.Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the text of the button.
        ''' </summary>
        ''' <remarks>
        ''' While the base class paints the button, an empty string is returned, so only this class draws the text.
        ''' </remarks>
        <Bindable(True)>
        <Localizable(True)>
        Public Overrides Property Text As String
            Get
                Return If(Me.isBasePainting, String.Empty, MyBase.Text)
            End Get
            Set(value As String)
                MyBase.Text = value
            End Set
        End Property

        ''' <summary>
        ''' Gets the space, in device pixels, between the icon and the text.
        ''' </summary>
        <Browsable(False)>
        <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
        Public ReadOnly Property ScaledIconTextSpacing As Integer
            Get
                Return CInt(Math.Round(Me.iconTextSpacingValue * IconButton.dpiScale))
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="IconButton"/> class.
        ''' </summary>
        ''' <remarks>
        ''' The auto-size mode is <see cref="AutoSizeMode.GrowAndShrink"/>, so the preferred size is calculated 
        ''' from the content only (with <see cref="AutoSizeMode.GrowOnly"/> the current size is part of the preferred size, 
        ''' and adding the icon width to it would grow the button on every layout).
        ''' </remarks>
        Public Sub New()

            Me.AutoSizeMode = AutoSizeMode.GrowAndShrink

        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Calculates the bounds of the icon and the drawing bounds of the text inside the specified client size, 
        ''' measuring the text with the screen device context.
        ''' </summary>
        ''' <param name="clientSize">
        ''' The client size of the button.
        ''' </param>
        ''' <returns>
        ''' The icon bounds (empty if there is no icon) and the text drawing bounds.
        ''' See <see cref="IconButton.GetContentLayout(Size, IDeviceContext)"/>.
        ''' </returns>
        Public Function GetContentLayout(clientSize As Size) As Tuple(Of Rectangle, Rectangle)

            Return Me.GetContentLayout(clientSize, Nothing)

        End Function

        ''' <summary>
        ''' Calculates the bounds of the icon and the drawing bounds of the text inside the specified client size.
        ''' <para></para>
        ''' The icon and the text are centered together horizontally (using the measured text width) 
        ''' within the client area minus the <see cref="Control.Padding"/>, and the icon is centered vertically.
        ''' </summary>
        ''' <remarks>
        ''' The text drawing bounds span the whole height of the content area and extend to its right edge, 
        ''' and the text is drawn vertically centered inside them. 
        ''' So the text is never clipped when the measured size is slightly smaller than the drawn text 
        ''' (for example, with the Windows text scale factor, the screen and the paint device contexts can differ).
        ''' </remarks>
        ''' <param name="clientSize">
        ''' The client size of the button.
        ''' </param>
        ''' 
        ''' <param name="deviceContext">
        ''' The device context used to measure the text (the paint graphics), 
        ''' or <see langword="Nothing"/> to use the screen device context.
        ''' </param>
        ''' <returns>
        ''' The icon bounds (empty if there is no icon) and the text drawing bounds.
        ''' </returns>
        Public Function GetContentLayout(clientSize As Size, deviceContext As IDeviceContext) As Tuple(Of Rectangle, Rectangle)

            Dim textSize As Size = Me.MeasureTextSize(deviceContext)
            Dim iconSize As Size = If(Me.iconValue Is Nothing, Size.Empty, Me.iconValue.Size)
            Dim spacing As Integer = If(Me.iconValue IsNot Nothing AndAlso textSize.Width > 0, Me.ScaledIconTextSpacing, 0)

            Dim area As New Rectangle(Me.Padding.Left, Me.Padding.Top,
                                      Math.Max(0, clientSize.Width - Me.Padding.Horizontal),
                                      Math.Max(0, clientSize.Height - Me.Padding.Vertical))

            Dim contentWidth As Integer = iconSize.Width + spacing + textSize.Width
            Dim x As Integer = area.Left + Math.Max(0, (area.Width - contentWidth) \ 2)
            Dim centerY As Integer = area.Top + area.Height \ 2

            Dim iconBounds As Rectangle = If(Me.iconValue Is Nothing,
                                             Rectangle.Empty,
                                             New Rectangle(x, centerY - iconSize.Height \ 2, iconSize.Width, iconSize.Height))

            Dim textLeft As Integer = x + iconSize.Width + spacing
            Dim textBounds As New Rectangle(textLeft, area.Top, Math.Max(0, area.Right - textLeft), area.Height)

            Return Tuple.Create(iconBounds, textBounds)

        End Function

        ''' <summary>
        ''' Measures the size of the text, with the same format flags used to draw it.
        ''' </summary>
        ''' <param name="deviceContext">
        ''' The device context used to measure the text, or <see langword="Nothing"/> to use the screen device context.
        ''' </param>
        ''' <returns>
        ''' The text size, or <see cref="Size.Empty"/> if the button has no text.
        ''' </returns>
        Public Function MeasureTextSize(deviceContext As IDeviceContext) As Size

            Dim text As String = MyBase.Text
            If String.IsNullOrEmpty(text) Then
                Return Size.Empty
            End If

            Dim flags As TextFormatFlags = Me.GetTextFormatFlags() And Not TextFormatFlags.EndEllipsis
            Return If(deviceContext Is Nothing,
                      TextRenderer.MeasureText(text, Me.Font, Size.Empty, flags),
                      TextRenderer.MeasureText(deviceContext, text, Me.Font, Size.Empty, flags))

        End Function

        ''' <summary>
        ''' Retrieves the size of a rectangular area into which the button can be fitted,
        ''' including the icon and the icon-text spacing.
        ''' </summary>
        ''' <param name="proposedSize">
        ''' The custom-sized area for the button.
        ''' </param>
        ''' <returns>
        ''' The preferred size.
        ''' </returns>
        Public Overrides Function GetPreferredSize(proposedSize As Size) As Size

            Dim preferred As Size = MyBase.GetPreferredSize(proposedSize)
            If Me.iconValue Is Nothing Then
                Return preferred
            End If

            Dim hasText As Boolean = Not String.IsNullOrEmpty(MyBase.Text)
            preferred.Width += Me.iconValue.Width + If(hasText, Me.ScaledIconTextSpacing, 0)
            preferred.Height = Math.Max(preferred.Height, Me.iconValue.Height + Me.Padding.Vertical + 8)
            Return preferred

        End Function

#End Region

#Region " Overridden Methods "

        ''' <summary>
        ''' Paints the button: the base class paints the background, border and focus cues,
        ''' and then the icon and the text are drawn.
        ''' </summary>
        ''' <param name="e">
        ''' The event data.
        ''' </param>
        Protected Overrides Sub OnPaint(e As PaintEventArgs)

            Me.isBasePainting = True
            Try
                MyBase.OnPaint(e)
            Finally
                Me.isBasePainting = False
            End Try

            Dim layout As Tuple(Of Rectangle, Rectangle) = Me.GetContentLayout(Me.ClientSize, e.Graphics)

            If Me.iconValue IsNot Nothing Then
                If Me.Enabled Then
                    e.Graphics.DrawImage(Me.iconValue, layout.Item1)
                Else
                    ControlPaint.DrawImageDisabled(e.Graphics, Me.iconValue, layout.Item1.X, layout.Item1.Y, Me.BackColor)
                End If
            End If

            Dim text As String = MyBase.Text
            If Not String.IsNullOrEmpty(text) Then
                TextRenderer.DrawText(e.Graphics, text, Me.Font, layout.Item2,
                                      If(Me.Enabled, Me.ForeColor, Me.disabledForeColorValue),
                                      Me.GetTextFormatFlags())
            End If

        End Sub

        ''' <inheritdoc/>
        Protected Overrides Sub OnTextChanged(e As EventArgs)

            MyBase.OnTextChanged(e)
            Me.Invalidate()

        End Sub

        ''' <inheritdoc/>
        Protected Overrides Sub OnFontChanged(e As EventArgs)

            MyBase.OnFontChanged(e)
            Me.Invalidate()

        End Sub

        ''' <inheritdoc/>
        Protected Overrides Sub OnPaddingChanged(e As EventArgs)

            MyBase.OnPaddingChanged(e)
            Me.Invalidate()

        End Sub

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Gets the text format flags used to measure and draw the text.
        ''' </summary>
        ''' <returns>
        ''' The flags.
        ''' </returns>
        Private Function GetTextFormatFlags() As TextFormatFlags

            Dim flags As TextFormatFlags = TextFormatFlags.SingleLine Or TextFormatFlags.NoPadding Or
                                           TextFormatFlags.Left Or TextFormatFlags.VerticalCenter Or
                                           TextFormatFlags.EndEllipsis

            If Not Me.UseMnemonic Then
                flags = flags Or TextFormatFlags.NoPrefix
            ElseIf Not Me.ShowKeyboardCues Then
                flags = flags Or TextFormatFlags.HidePrefix
            End If

            Return flags

        End Function

        ''' <summary>
        ''' Recalculates the layout of the parent (for auto-sized buttons) and repaints the button,
        ''' after a change of the content size.
        ''' </summary>
        Private Sub OnContentSizeChanged()

            If Me.Parent IsNot Nothing Then
                Me.Parent.PerformLayout(Me, NameOf(IconButton.Icon))
            End If
            Me.Invalidate()

        End Sub

        ''' <summary>
        ''' Gets the scale factor of the system DPI relative to 96 DPI.
        ''' </summary>
        ''' <returns>
        ''' The scale factor (1 at 96 DPI).
        ''' </returns>
        Private Shared Function GetSystemDpiScale() As Single

            Using g As Graphics = Graphics.FromHwnd(IntPtr.Zero)
                Return g.DpiX / 96.0F
            End Using

        End Function

#End Region

    End Class

End Namespace

#End Region
