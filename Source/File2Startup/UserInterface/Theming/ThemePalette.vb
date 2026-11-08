' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ThemePalette.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Drawing

Imports Core

#End Region

#Region " ThemePalette "

Namespace UserInterface.Theming

    ''' <summary>
    ''' Defines the colors of a visual theme.
    ''' <para></para>
    ''' The <see cref="ThemePalette.Light"/> palette uses the default Windows system colors (<see cref="SystemColors"/>),
    ''' so it follows the colors of the user's Windows configuration (including high contrast themes).
    ''' </summary>
    Public NotInheritable Class ThemePalette

#Region " Properties "

        ''' <summary>Gets the theme this palette belongs to.</summary>
        Public ReadOnly Property Theme As AppTheme

        ''' <summary>Gets the background color of forms and pages.</summary>
        Public ReadOnly Property Background As Color

        ''' <summary>Gets the background color of tool bars, status bars and grid headers.</summary>
        Public ReadOnly Property Surface As Color

        ''' <summary>Gets the background color of text boxes and grids.</summary>
        Public ReadOnly Property InputBackground As Color

        ''' <summary>Gets the alternating row background color of grids.</summary>
        Public ReadOnly Property AlternateInputBackground As Color

        ''' <summary>Gets the color of borders and separators.</summary>
        Public ReadOnly Property Border As Color

        ''' <summary>Gets the color of the regular text.</summary>
        Public ReadOnly Property Text As Color

        ''' <summary>Gets the color of the field captions.</summary>
        Public ReadOnly Property SecondaryText As Color

        ''' <summary>Gets the color of the text hints (placeholders) of text boxes.</summary>
        Public ReadOnly Property HintText As Color

        ''' <summary>Gets the color of the text of disabled items.</summary>
        Public ReadOnly Property DisabledText As Color

        ''' <summary>Gets the background color of buttons.</summary>
        Public ReadOnly Property ButtonBackground As Color

        ''' <summary>Gets the background color of buttons and tool bar items under the mouse pointer.</summary>
        Public ReadOnly Property ButtonHoverBackground As Color

        ''' <summary>Gets the background color of pressed buttons and checked tool bar items.</summary>
        Public ReadOnly Property ButtonPressedBackground As Color

        ''' <summary>Gets the accent color (primary button, active page marker, toggle switch).</summary>
        Public ReadOnly Property Accent As Color

        ''' <summary>Gets the accent color under the mouse pointer.</summary>
        Public ReadOnly Property AccentHover As Color

        ''' <summary>Gets the color of the text drawn over the accent color.</summary>
        Public ReadOnly Property AccentText As Color

        ''' <summary>Gets the background color of selected grid rows.</summary>
        Public ReadOnly Property Selection As Color

        ''' <summary>Gets the text color of selected grid rows.</summary>
        Public ReadOnly Property SelectionText As Color

        ''' <summary>Gets the color of the grid lines.</summary>
        Public ReadOnly Property GridLine As Color

        ''' <summary>Gets the color of hyperlinks.</summary>
        Public ReadOnly Property Link As Color

        ''' <summary>Gets the color of warning texts (e.g. the elevation notice of the status bar).</summary>
        Public ReadOnly Property Warning As Color

        ''' <summary>Gets a value indicating whether this is a dark palette.</summary>
        Public ReadOnly Property IsDark As Boolean
            Get
                Return Me.Theme = AppTheme.Dark
            End Get
        End Property

#End Region

#Region " Shared Properties "

        ''' <summary>
        ''' Gets the light palette, built from the current Windows system colors.
        ''' </summary>
        ''' <remarks>
        ''' A new instance is returned on each call, so a change of the system colors is reflected.
        ''' </remarks>
        Public Shared ReadOnly Property Light As ThemePalette
            Get
                Return New ThemePalette(
                    theme:=AppTheme.Light,
                    background:=SystemColors.Control,
                    surface:=SystemColors.Control,
                    inputBackground:=SystemColors.Window,
                    alternateInputBackground:=SystemColors.Window,
                    border:=SystemColors.ControlDark,
                    text:=SystemColors.ControlText,
                    secondaryText:=SystemColors.ControlText,
                    hintText:=SystemColors.GrayText,
                    disabledText:=SystemColors.GrayText,
                    buttonBackground:=SystemColors.Control,
                    buttonHoverBackground:=SystemColors.ControlLight,
                    buttonPressedBackground:=SystemColors.ControlLight,
                    accent:=SystemColors.Highlight,
                    accentHover:=SystemColors.HotTrack,
                    accentText:=SystemColors.HighlightText,
                    selection:=SystemColors.Highlight,
                    selectionText:=SystemColors.HighlightText,
                    gridLine:=SystemColors.ControlLight,
                    link:=Color.FromArgb(0, 0, 255),
                    warning:=Color.FromArgb(157, 93, 0))
            End Get
        End Property

        ''' <summary>
        ''' Gets the dark palette (colors similar to the dark mode of Windows 11).
        ''' </summary>
        Public Shared ReadOnly Property Dark As ThemePalette
            Get
                Return New ThemePalette(
                    theme:=AppTheme.Dark,
                    background:=Color.FromArgb(32, 32, 32),
                    surface:=Color.FromArgb(43, 43, 43),
                    inputBackground:=Color.FromArgb(28, 28, 28),
                    alternateInputBackground:=Color.FromArgb(36, 36, 36),
                    border:=Color.FromArgb(77, 77, 77),
                    text:=Color.FromArgb(242, 242, 242),
                    secondaryText:=Color.FromArgb(204, 204, 204),
                    hintText:=Color.FromArgb(150, 150, 150),
                    disabledText:=Color.FromArgb(120, 120, 120),
                    buttonBackground:=Color.FromArgb(55, 55, 55),
                    buttonHoverBackground:=Color.FromArgb(66, 66, 66),
                    buttonPressedBackground:=Color.FromArgb(77, 77, 77),
                    accent:=Color.FromArgb(0, 103, 192),
                    accentHover:=Color.FromArgb(25, 117, 197),
                    accentText:=Color.White,
                    selection:=Color.FromArgb(0, 84, 153),
                    selectionText:=Color.White,
                    gridLine:=Color.FromArgb(56, 56, 56),
                    link:=Color.FromArgb(96, 205, 255),
                    warning:=Color.FromArgb(252, 225, 0))
            End Get
        End Property

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ThemePalette"/> class.
        ''' </summary>
        Private Sub New(theme As AppTheme,
                        background As Color, surface As Color, inputBackground As Color, alternateInputBackground As Color,
                        border As Color, text As Color, secondaryText As Color, hintText As Color, disabledText As Color,
                        buttonBackground As Color, buttonHoverBackground As Color, buttonPressedBackground As Color,
                        accent As Color, accentHover As Color, accentText As Color,
                        selection As Color, selectionText As Color, gridLine As Color, link As Color, warning As Color)

            Me.Theme = theme
            Me.Background = background
            Me.Surface = surface
            Me.InputBackground = inputBackground
            Me.AlternateInputBackground = alternateInputBackground
            Me.Border = border
            Me.Text = text
            Me.SecondaryText = secondaryText
            Me.HintText = hintText
            Me.DisabledText = disabledText
            Me.ButtonBackground = buttonBackground
            Me.ButtonHoverBackground = buttonHoverBackground
            Me.ButtonPressedBackground = buttonPressedBackground
            Me.Accent = accent
            Me.AccentHover = accentHover
            Me.AccentText = accentText
            Me.Selection = selection
            Me.SelectionText = selectionText
            Me.GridLine = gridLine
            Me.Link = link
            Me.Warning = warning

        End Sub

#End Region

#Region " Public Shared Methods "

        ''' <summary>
        ''' Gets the palette of the specified theme.
        ''' </summary>
        ''' <param name="theme">
        ''' The theme.
        ''' </param>
        ''' <returns>
        ''' The palette.
        ''' </returns>
        ''' <exception cref="ArgumentOutOfRangeException">
        ''' <paramref name="theme"/> is not a defined <see cref="AppTheme"/> value.
        ''' </exception>
        Public Shared Function FromTheme(theme As AppTheme) As ThemePalette

            Select Case theme
                Case AppTheme.Light
                    Return ThemePalette.Light
                Case AppTheme.Dark
                    Return ThemePalette.Dark
                Case Else
                    Throw New ArgumentOutOfRangeException(NameOf(theme), theme, "Undefined theme value.")
            End Select

        End Function

        ''' <summary>
        ''' Calculates the WCAG 2.x contrast ratio between two colors.
        ''' </summary>
        ''' <param name="color1">
        ''' The first color.
        ''' </param>
        '''
        ''' <param name="color2">
        ''' The second color.
        ''' </param>
        ''' <returns>
        ''' The contrast ratio, from 1 (no contrast) to 21 (black on white).
        ''' </returns>
        Public Shared Function GetContrastRatio(color1 As Color, color2 As Color) As Double

            Dim luminance1 As Double = ThemePalette.GetRelativeLuminance(color1)
            Dim luminance2 As Double = ThemePalette.GetRelativeLuminance(color2)
            Dim lighter As Double = Math.Max(luminance1, luminance2)
            Dim darker As Double = Math.Min(luminance1, luminance2)

            Return (lighter + 0.05R) / (darker + 0.05R)

        End Function

#End Region

#Region " Private Methods "

        ''' <summary>
        ''' Calculates the WCAG 2.x relative luminance of a color.
        ''' </summary>
        ''' <param name="color">
        ''' The color.
        ''' </param>
        ''' <returns>
        ''' The relative luminance, from 0 (black) to 1 (white).
        ''' </returns>
        Private Shared Function GetRelativeLuminance(color As Color) As Double

            Dim linearize As Func(Of Byte, Double) =
                Function(channel As Byte)
                    Dim value As Double = channel / 255.0R
                    Return If(value <= 0.03928R, value / 12.92R, Math.Pow((value + 0.055R) / 1.055R, 2.4R))
                End Function

            Return 0.2126R * linearize(color.R) + 0.7152R * linearize(color.G) + 0.0722R * linearize(color.B)

        End Function

#End Region

    End Class

End Namespace

#End Region
