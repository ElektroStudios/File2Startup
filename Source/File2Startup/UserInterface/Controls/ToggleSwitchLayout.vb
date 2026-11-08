' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ToggleSwitchLayout.vb" company="Elektro Studios">
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

#End Region

#Region " ToggleSwitchLayout "

Namespace UserInterface.Controls

    ''' <summary>
    ''' Calculates the geometry (track and thumb bounds) of a <see cref="ToggleSwitch"/>.
    ''' </summary>
    Public Structure ToggleSwitchLayout

#Region " Constants "

        ''' <summary>
        ''' The minimum width/height ratio of the track.
        ''' </summary>
        Public Const TrackAspectRatio As Single = 1.8F

        ''' <summary>
        ''' The space between the track edge and the thumb, relative to the track height.
        ''' </summary>
        Public Const ThumbInsetRatio As Single = 0.16F

        ''' <summary>
        ''' The margin, in pixels, between the client area edge and the track (room for the focus rectangle).
        ''' </summary>
        Public Const Margin As Single = 1.0F

#End Region

#Region " Properties "

        ''' <summary>Gets the bounds of the track.</summary>
        Public ReadOnly Property Track As RectangleF

        ''' <summary>Gets the bounds of the thumb.</summary>
        Public ReadOnly Property Thumb As RectangleF

#End Region

#Region " Constructors "

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ToggleSwitchLayout"/> structure.
        ''' </summary>
        ''' <param name="track">
        ''' The bounds of the track.
        ''' </param>
        '''
        ''' <param name="thumb">
        ''' The bounds of the thumb.
        ''' </param>
        Public Sub New(track As RectangleF, thumb As RectangleF)

            Me.Track = track
            Me.Thumb = thumb

        End Sub

#End Region

#Region " Public Methods "

        ''' <summary>
        ''' Calculates the layout of a switch.
        ''' <para></para>
        ''' The track is vertically centered and uses the whole width of the client area
        ''' (its height is limited so the track is never narrower than <see cref="ToggleSwitchLayout.TrackAspectRatio"/>).
        ''' The thumb is a circle that moves from the left end (off) to the right end (on) of the track.
        ''' </summary>
        ''' <param name="clientSize">
        ''' The size of the client area of the switch.
        ''' </param>
        '''
        ''' <param name="thumbPosition">
        ''' The thumb position: 0 is the off position and 1 is the on position. Values out of range are clamped.
        ''' </param>
        ''' <returns>
        ''' The layout. The rectangles are empty if the client area is too small.
        ''' </returns>
        Public Shared Function Calculate(clientSize As Size, thumbPosition As Double) As ToggleSwitchLayout

            Dim availableWidth As Single = clientSize.Width - ToggleSwitchLayout.Margin * 2.0F
            Dim availableHeight As Single = clientSize.Height - ToggleSwitchLayout.Margin * 2.0F
            If availableWidth <= 0.0F OrElse availableHeight <= 0.0F Then
                Return New ToggleSwitchLayout(RectangleF.Empty, RectangleF.Empty)
            End If

            Dim trackHeight As Single = Math.Min(availableHeight, availableWidth / ToggleSwitchLayout.TrackAspectRatio)
            Dim track As New RectangleF(ToggleSwitchLayout.Margin,
                                        (clientSize.Height - trackHeight) / 2.0F,
                                        availableWidth,
                                        trackHeight)

            Dim inset As Single = trackHeight * ToggleSwitchLayout.ThumbInsetRatio
            Dim thumbDiameter As Single = trackHeight - inset * 2.0F
            Dim travel As Single = track.Width - inset * 2.0F - thumbDiameter
            Dim position As Single = CSng(Math.Max(0.0R, Math.Min(1.0R, thumbPosition)))

            Dim thumb As New RectangleF(track.X + inset + travel * position,
                                        track.Y + inset,
                                        thumbDiameter,
                                        thumbDiameter)

            Return New ToggleSwitchLayout(track, thumb)

        End Function

#End Region

    End Structure

End Namespace

#End Region
