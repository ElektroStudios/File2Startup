' ***********************************************************************
' Author   : ElektroStudios
' Modified : 07-November-2026
' ***********************************************************************
' <copyright file="ToggleSwitchTests.vb" company="Elektro Studios">
'     Copyright (c) Elektro Studios. All rights reserved.
' </copyright>
' ***********************************************************************

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UserInterface.Controls

#End Region

#Region " ToggleSwitchTests "

Namespace UserInterface

    ''' <summary>
    ''' Tests for the <see cref="ToggleSwitch"/>, <see cref="ToggleSwitchLayout"/> and <see cref="ToolStripToggleSwitch"/> types.
    ''' </summary>
    <TestClass>
    Public NotInheritable Class ToggleSwitchTests

#Region " Testable Switch "

        ''' <summary>
        ''' A <see cref="ToggleSwitch"/> that exposes its input methods to the tests.
        ''' </summary>
        Private NotInheritable Class TestableToggleSwitch : Inherits ToggleSwitch

            ''' <summary>Simulates a mouse click.</summary>
            ''' <param name="button">The mouse button.</param>
            Public Sub SimulateClick(button As MouseButtons)
                Me.OnMouseClick(New MouseEventArgs(button, 1, 5, 5, 0))
            End Sub

            ''' <summary>Simulates the release of a key.</summary>
            ''' <param name="keyData">The key and modifiers.</param>
            Public Sub SimulateKeyUp(keyData As Keys)
                Me.OnKeyUp(New KeyEventArgs(keyData))
            End Sub

            ''' <summary>Gets the accessibility object.</summary>
            ''' <returns>The accessibility object.</returns>
            Public Function GetAccessibilityObject() As AccessibleObject
                Return Me.AccessibilityObject
            End Function

        End Class

#End Region

#Region " ToggleSwitch "

        ''' <summary>The switch has sensible defaults.</summary>
        <STATestMethod>
        <TestCategory("UI")>
        Public Sub New_Defaults()

            Using toggle As New ToggleSwitch()
                Assert.IsFalse(toggle.Checked)
                Assert.AreEqual(New Size(44, 22), toggle.Size)
                Assert.AreEqual(ToggleSwitchGlyphs.SunAndMoon, toggle.Glyphs)
                Assert.IsTrue(toggle.TabStop)
                Assert.AreEqual(AccessibleRole.CheckButton, toggle.AccessibleRole)
                Assert.IsFalse(toggle.IsAnimating)
            End Using

        End Sub

        ''' <summary>Changing <see cref="ToggleSwitch.Checked"/> raises <see cref="ToggleSwitch.CheckedChanged"/> only when the value changes.</summary>
        <STATestMethod>
        <TestCategory("UI")>
        Public Sub Checked_RaisesCheckedChangedOnlyOnChange()

            Using toggle As New ToggleSwitch()
                Dim raised As New List(Of Boolean)
                AddHandler toggle.CheckedChanged, Sub(sender As Object, e As EventArgs) raised.Add(DirectCast(sender, ToggleSwitch).Checked)

                toggle.Checked = True
                toggle.Checked = True
                toggle.Checked = False
                toggle.Toggle()

                CollectionAssert.AreEqual({True, False, True}, raised.ToArray())
            End Using

        End Sub

        ''' <summary>The switch is toggled with the left mouse button and the Space key only.</summary>
        <STATestMethod>
        <TestCategory("UI")>
        Public Sub Input_TogglesWithLeftClickAndSpace()

            Using toggle As New TestableToggleSwitch()
                toggle.SimulateClick(MouseButtons.Left)
                Assert.IsTrue(toggle.Checked)

                toggle.SimulateClick(MouseButtons.Right)
                Assert.IsTrue(toggle.Checked)

                toggle.SimulateKeyUp(Keys.Space)
                Assert.IsFalse(toggle.Checked)

                toggle.SimulateKeyUp(Keys.Enter)
                toggle.SimulateKeyUp(Keys.Space Or Keys.Control)
                Assert.IsFalse(toggle.Checked)
            End Using

        End Sub

        ''' <summary>The accessibility object exposes the checked state and toggles with its default action.</summary>
        <STATestMethod>
        <TestCategory("UI")>
        Public Sub Accessibility_ExposesStateAndToggleAction()

            Using toggle As New TestableToggleSwitch()
                Dim accessible As AccessibleObject = toggle.GetAccessibilityObject()

                Assert.IsFalse(accessible.State.HasFlag(AccessibleStates.Checked))

                accessible.DoDefaultAction()

                Assert.IsTrue(toggle.Checked)
                Assert.IsTrue(accessible.State.HasFlag(AccessibleStates.Checked))
            End Using

        End Sub

        ''' <summary>
        ''' When the switch is not visible the thumb moves directly (no animation),
        ''' and the track is painted with the on/off color.
        ''' </summary>
        <STATestMethod>
        <TestCategory("UI")>
        Public Sub Paint_UsesOnAndOffColors()

            Using toggle As New ToggleSwitch() With {
                    .Size = New Size(88, 44),
                    .BackColor = Color.White,
                    .OnColor = Color.FromArgb(0, 120, 215),
                    .OffColor = Color.FromArgb(200, 200, 200),
                    .Glyphs = ToggleSwitchGlyphs.None
                  },
                  bmp As New Bitmap(88, 44)

                ' Off: the right end of the track (far from the thumb) has the off color.
                toggle.DrawToBitmap(bmp, New Rectangle(Point.Empty, bmp.Size))
                Assert.AreEqual(toggle.OffColor.ToArgb(), bmp.GetPixel(70, 22).ToArgb())

                ' On: the left end of the track (far from the thumb) has the on color.
                toggle.Checked = True
                Assert.IsFalse(toggle.IsAnimating)
                toggle.DrawToBitmap(bmp, New Rectangle(Point.Empty, bmp.Size))
                Assert.AreEqual(toggle.OnColor.ToArgb(), bmp.GetPixel(17, 22).ToArgb())
            End Using

        End Sub

        ''' <summary>The switch paints without errors in every state and glyph mode.</summary>
        <STATestMethod>
        <TestCategory("UI")>
        Public Sub Paint_AllStates_DoNotThrow()

            For Each glyphs As ToggleSwitchGlyphs In {ToggleSwitchGlyphs.None, ToggleSwitchGlyphs.SunAndMoon}
                For Each isChecked As Boolean In {False, True}
                    For Each isEnabled As Boolean In {False, True}
                        Using toggle As New ToggleSwitch() With {.Glyphs = glyphs, .Checked = isChecked, .Enabled = isEnabled},
                              bmp As New Bitmap(toggle.Width, toggle.Height)

                            toggle.DrawToBitmap(bmp, New Rectangle(Point.Empty, bmp.Size))
                        End Using
                    Next isEnabled
                Next isChecked
            Next glyphs

        End Sub

#End Region

#Region " ToggleSwitchLayout "

        ''' <summary>The thumb is inside the track, at the left end when off and at the right end when on.</summary>
        <TestMethod>
        Public Sub Layout_ThumbMovesInsideTrack()

            Dim off As ToggleSwitchLayout = ToggleSwitchLayout.Calculate(New Size(44, 22), 0.0R)
            Dim [on] As ToggleSwitchLayout = ToggleSwitchLayout.Calculate(New Size(44, 22), 1.0R)
            Dim middle As ToggleSwitchLayout = ToggleSwitchLayout.Calculate(New Size(44, 22), 0.5R)

            For Each layout As ToggleSwitchLayout In {off, [on], middle}
                Assert.IsTrue(layout.Track.Contains(layout.Thumb), "The thumb must be inside the track.")
                Assert.AreEqual(layout.Thumb.Width, layout.Thumb.Height, 0.001F)
            Next layout

            Assert.IsTrue(off.Thumb.X < middle.Thumb.X AndAlso middle.Thumb.X < [on].Thumb.X)
            Assert.AreEqual(off.Track.Left + (off.Thumb.Top - off.Track.Top), off.Thumb.Left, 0.001F)
            Assert.AreEqual([on].Track.Right - ([on].Thumb.Top - [on].Track.Top), [on].Thumb.Right, 0.001F)

        End Sub

        ''' <summary>Out of range thumb positions are clamped.</summary>
        <TestMethod>
        Public Sub Layout_ClampsThumbPosition()

            Dim size As New Size(44, 22)

            Assert.AreEqual(ToggleSwitchLayout.Calculate(size, 0.0R).Thumb, ToggleSwitchLayout.Calculate(size, -3.0R).Thumb)
            Assert.AreEqual(ToggleSwitchLayout.Calculate(size, 1.0R).Thumb, ToggleSwitchLayout.Calculate(size, 7.0R).Thumb)

        End Sub

        ''' <summary>The track keeps its minimum aspect ratio, and is vertically centered, in a tall client area.</summary>
        <TestMethod>
        Public Sub Layout_KeepsAspectRatioAndCentersTrack()

            Dim layout As ToggleSwitchLayout = ToggleSwitchLayout.Calculate(New Size(46, 100), 0.0R)

            Assert.AreEqual(ToggleSwitchLayout.TrackAspectRatio, layout.Track.Width / layout.Track.Height, 0.001F)
            Assert.AreEqual(50.0F, layout.Track.Top + layout.Track.Height / 2.0F, 0.001F)

        End Sub

        ''' <summary>A client area too small produces an empty layout.</summary>
        <TestMethod>
        <DataRow(0, 0)>
        <DataRow(2, 20)>
        <DataRow(20, 2)>
        Public Sub Layout_TooSmall_IsEmpty(width As Integer, height As Integer)

            Dim layout As ToggleSwitchLayout = ToggleSwitchLayout.Calculate(New Size(width, height), 0.0R)

            Assert.IsTrue(layout.Track.IsEmpty)
            Assert.IsTrue(layout.Thumb.IsEmpty)

        End Sub

#End Region

#Region " ToolStripToggleSwitch "

        ''' <summary>The tool strip host forwards the checked state and relays the event with itself as sender.</summary>
        <STATestMethod>
        <TestCategory("UI")>
        Public Sub ToolStripHost_ForwardsCheckedAndEvent()

            Using toolBar As New ToolStrip()
                Dim host As New ToolStripToggleSwitch()
                toolBar.Items.Add(host)

                Dim senders As New List(Of Object)
                AddHandler host.CheckedChanged, Sub(sender As Object, e As EventArgs) senders.Add(sender)

                host.Checked = True
                Assert.IsTrue(host.ToggleSwitch.Checked)

                host.ToggleSwitch.Toggle()
                Assert.IsFalse(host.Checked)

                Assert.AreEqual(2, senders.Count)
                Assert.AreSame(host, senders(0))
                Assert.AreSame(host, senders(1))
            End Using

        End Sub

#End Region

    End Class

End Namespace

#End Region
