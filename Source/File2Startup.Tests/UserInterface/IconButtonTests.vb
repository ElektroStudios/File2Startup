' ***********************************************************************
' Author   : ElektroStudios
' Modified : 08-November-2026
' ***********************************************************************
' <copyright file="IconButtonTests.vb" company="Elektro Studios">
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
Imports System.Drawing
Imports System.Windows.Forms

Imports Core

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports UserInterface.Controls
Imports UserInterface.Theming

#End Region

#Region " IconButtonTests "

Namespace UserInterface

    ''' <summary>
    ''' Tests for the <see cref="IconButton"/> class.
    ''' </summary>
    <TestClass>
    <TestCategory("UI")>
    Public NotInheritable Class IconButtonTests

#Region " Helpers "

        ''' <summary>Creates a solid color bitmap.</summary>
        ''' <param name="color">The color.</param>
        ''' <returns>The bitmap.</returns>
        Private Shared Function CreateSolidIcon(color As Color) As Bitmap

            Dim bmp As New Bitmap(16, 16)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(color)
            End Using
            Return bmp

        End Function

#End Region

#Region " Properties "

        ''' <summary>The button has sensible defaults.</summary>
        <STATestMethod>
        Public Sub New_Defaults()

            Using button As New IconButton()
                Assert.IsNull(button.Icon)
                Assert.AreEqual(IconButton.DefaultIconTextSpacing, button.IconTextSpacing)
                Assert.AreEqual(AutoSizeMode.GrowAndShrink, button.AutoSizeMode)
                Assert.IsNull(button.Image, "The icon must not be drawn by the base class.")
            End Using

        End Sub

        ''' <summary>A negative spacing is rejected.</summary>
        <STATestMethod>
        Public Sub IconTextSpacing_Negative_ThrowsArgumentOutOfRangeException()

            Using button As New IconButton()
                Assert.ThrowsExactly(Of ArgumentOutOfRangeException)(Sub() button.IconTextSpacing = -1)
            End Using

        End Sub

        ''' <summary>The text is returned normally (it is only hidden from the base class while it paints).</summary>
        <STATestMethod>
        Public Sub Text_IsReturnedNormally()

            Using button As New IconButton() With {.Text = "Delete entry"}
                Assert.AreEqual("Delete entry", button.Text)
            End Using

        End Sub

#End Region

#Region " Preferred Size "

        ''' <summary>The preferred width includes the icon and the icon-text spacing.</summary>
        <STATestMethod>
        Public Sub GetPreferredSize_IncludesIconAndSpacing()

            Using plain As New Button() With {.Text = "Delete entry", .AutoSize = True, .AutoSizeMode = AutoSizeMode.GrowAndShrink},
                  button As New IconButton() With {.Text = "Delete entry", .AutoSize = True},
                  icon As Bitmap = IconButtonTests.CreateSolidIcon(Color.Red)

                button.Icon = icon

                Dim expectedWidth As Integer = plain.GetPreferredSize(Size.Empty).Width + icon.Width + button.ScaledIconTextSpacing
                Assert.AreEqual(expectedWidth, button.GetPreferredSize(Size.Empty).Width)
            End Using

        End Sub

        ''' <summary>
        ''' Regression test: the preferred size is stable across layouts
        ''' (with <see cref="AutoSizeMode.GrowOnly"/> the button grew by the icon width on every layout).
        ''' </summary>
        <STATestMethod>
        Public Sub AutoSize_IsStableAcrossLayouts()

            Using panel As New FlowLayoutPanel() With {.AutoSize = True, .WrapContents = False},
                  button As New IconButton() With {.Text = "Send to item builder", .AutoSize = True},
                  icon As Bitmap = IconButtonTests.CreateSolidIcon(Color.Red)

                panel.Controls.Add(button)
                button.Icon = icon
                panel.PerformLayout()
                Dim firstWidth As Integer = button.Width

                For iteration As Integer = 1 To 5
                    button.Font = New Font(button.Font, button.Font.Style)
                    panel.PerformLayout()
                Next iteration

                Assert.AreEqual(firstWidth, button.Width)
                Assert.AreEqual(button.GetPreferredSize(Size.Empty).Width, button.Width)
            End Using

        End Sub

#End Region

#Region " Layout And Paint "

        ''' <summary>The icon and the text are separated by the scaled spacing and centered together.</summary>
        <STATestMethod>
        Public Sub GetContentLayout_SeparatesAndCentersContent()

            Using button As New IconButton() With {.Text = "Refresh", .Padding = New Padding(6, 0, 6, 0)},
                  icon As Bitmap = IconButtonTests.CreateSolidIcon(Color.Red)

                button.Icon = icon
                Dim clientSize As New Size(200, 32)

                Dim layout As Tuple(Of Rectangle, Rectangle) = button.GetContentLayout(clientSize)
                Dim iconBounds As Rectangle = layout.Item1
                Dim textBounds As Rectangle = layout.Item2

                Assert.AreEqual(icon.Size, iconBounds.Size)
                Assert.AreEqual(button.ScaledIconTextSpacing, textBounds.Left - iconBounds.Right)

                Dim textRight As Integer = textBounds.Left + button.MeasureTextSize(Nothing).Width
                Dim leftSpace As Integer = iconBounds.Left - button.Padding.Left
                Dim rightSpace As Integer = (clientSize.Width - button.Padding.Right) - textRight
                Assert.IsTrue(Math.Abs(leftSpace - rightSpace) <= 1, $"Content is not centered ({leftSpace} vs {rightSpace}).")
                Assert.AreEqual(clientSize.Height \ 2, iconBounds.Top + iconBounds.Height \ 2)
            End Using

        End Sub

        ''' <summary>The content layout does not depend on the flat style (same pixels in the light and dark themes).</summary>
        <STATestMethod>
        Public Sub GetContentLayout_IsIndependentOfFlatStyle()

            Using button As New IconButton() With {.Text = "Add to Windows startup"},
                  icon As Bitmap = IconButtonTests.CreateSolidIcon(Color.Red)

                button.Icon = icon
                button.FlatStyle = FlatStyle.Standard
                Dim standardLayout As Tuple(Of Rectangle, Rectangle) = button.GetContentLayout(New Size(220, 36))
                button.FlatStyle = FlatStyle.Flat
                Dim flatLayout As Tuple(Of Rectangle, Rectangle) = button.GetContentLayout(New Size(220, 36))

                Assert.AreEqual(standardLayout.Item1, flatLayout.Item1)
                Assert.AreEqual(standardLayout.Item2, flatLayout.Item2)
            End Using

        End Sub

        ''' <summary>
        ''' Regression test: in both flat styles, the painted icon never touches the painted text
        ''' (with the standard button, the "+" icon overlapped the text in the dark theme).
        ''' </summary>
        <STATestMethod>
        <DataRow(FlatStyle.Standard)>
        <DataRow(FlatStyle.Flat)>
        Public Sub Paint_IconDoesNotTouchText(flatStyle As FlatStyle)

            Using button As New IconButton() With {
                    .Text = "Add to Windows startup",
                    .FlatStyle = flatStyle,
                    .BackColor = Color.White,
                    .ForeColor = Color.Black,
                    .Size = New Size(240, 36)
                  },
                  icon As Bitmap = IconButtonTests.CreateSolidIcon(Color.FromArgb(255, 0, 0)),
                  bmp As New Bitmap(240, 36)

                button.Icon = icon
                button.DrawToBitmap(bmp, New Rectangle(Point.Empty, bmp.Size))

                Dim layout As Tuple(Of Rectangle, Rectangle) = button.GetContentLayout(button.ClientSize)
                Dim iconRight As Integer = -1
                Dim textLeft As Integer = Integer.MaxValue

                ' Only the rows of the text line are scanned (the button border is outside them).
                Dim textHeight As Integer = button.MeasureTextSize(Nothing).Height
                Dim centerY As Integer = layout.Item2.Top + layout.Item2.Height \ 2
                Dim firstRow As Integer = Math.Max(2, centerY - textHeight \ 2)
                Dim lastRow As Integer = Math.Min(bmp.Height - 3, centerY + textHeight \ 2)

                For x As Integer = 2 To bmp.Width - 3
                    For y As Integer = firstRow To lastRow
                        Dim pixel As Color = bmp.GetPixel(x, y)
                        If pixel.R > 200 AndAlso pixel.G < 60 AndAlso pixel.B < 60 Then
                            iconRight = Math.Max(iconRight, x)
                        ElseIf pixel.R < 100 AndAlso pixel.G < 100 AndAlso pixel.B < 100 AndAlso x > layout.Item1.Right Then
                            textLeft = Math.Min(textLeft, x)
                        End If
                    Next y
                Next x

                Assert.IsTrue(iconRight >= 0, "The icon was not painted.")
                Assert.IsTrue(textLeft < Integer.MaxValue, "The text was not painted.")
                Assert.IsTrue(textLeft - iconRight > button.ScaledIconTextSpacing \ 2,
                              $"The icon (right edge {iconRight}) is too close to the text (left edge {textLeft}).")
            End Using

        End Sub

        ''' <summary>
        ''' Regression test: the text drawing bounds span the whole content area height and reach its right edge, 
        ''' whatever the font size, so the text is never clipped when the measured text size is smaller than the drawn text 
        ''' (the text was cut in half with the Windows text scale factor at 110%).
        ''' </summary>
        <STATestMethod>
        <DataRow(9.75F)>
        <DataRow(10.75F)>
        <DataRow(12.0F)>
        <DataRow(15.0F)>
        Public Sub GetContentLayout_TextBoundsSpanContentArea(fontSize As Single)

            Using button As New IconButton() With {.Text = "Delete entry", .Padding = New Padding(6, 2, 6, 3)},
                  font As New Font("Segoe UI", fontSize),
                  icon As Bitmap = IconButtonTests.CreateSolidIcon(Color.Red)

                button.Font = font
                button.Icon = icon
                Dim clientSize As New Size(220, 40)

                Dim textBounds As Rectangle = button.GetContentLayout(clientSize).Item2

                Assert.AreEqual(button.Padding.Top, textBounds.Top)
                Assert.AreEqual(clientSize.Height - button.Padding.Vertical, textBounds.Height)
                Assert.AreEqual(clientSize.Width - button.Padding.Right, textBounds.Right)
                Assert.IsTrue(textBounds.Width >= button.MeasureTextSize(Nothing).Width)
            End Using

        End Sub

        ''' <summary>
        ''' With a bigger font (as with the Windows text scale factor), the whole glyphs are painted, 
        ''' including the descenders below the baseline (the "y" of "entry").
        ''' </summary>
        <STATestMethod>
        Public Sub Paint_BiggerFont_TextIsNotClipped()

            Using button As New IconButton() With {
                    .Text = "Delete entry",
                    .FlatStyle = FlatStyle.Flat,
                    .BackColor = Color.White,
                    .ForeColor = Color.Black,
                    .Size = New Size(200, 40)
                  },
                  font As New Font("Segoe UI", 10.75F),
                  bmp As New Bitmap(200, 40)

                button.Font = font
                button.FlatAppearance.BorderSize = 0
                button.DrawToBitmap(bmp, New Rectangle(Point.Empty, bmp.Size))

                Dim textSize As Size = button.MeasureTextSize(Nothing)
                Dim topInk As Integer = Integer.MaxValue
                Dim bottomInk As Integer = -1
                For y As Integer = 0 To bmp.Height - 1
                    For x As Integer = 0 To bmp.Width - 1
                        Dim pixel As Color = bmp.GetPixel(x, y)
                        If pixel.R < 110 AndAlso pixel.G < 110 AndAlso pixel.B < 110 Then
                            topInk = Math.Min(topInk, y)
                            bottomInk = Math.Max(bottomInk, y)
                        End If
                    Next x
                Next y

                Assert.IsTrue(bottomInk >= 0, "The text was not painted.")
                ' Cap height plus descender is well over half the line height; a text cut in half would be much shorter.
                Assert.IsTrue(bottomInk - topInk + 1 >= textSize.Height * 0.6R,
                              $"The painted text is {bottomInk - topInk + 1} pixels tall; the line height is {textSize.Height}.")
                Assert.IsTrue(bottomInk > bmp.Height \ 2, "The lower half of the glyphs was not painted.")
            End Using

        End Sub

#End Region

#Region " Theming "

        ''' <summary>The theme manager sets the disabled text color of icon buttons.</summary>
        <STATestMethod>
        Public Sub ThemeManager_SetsDisabledForeColor()

            Using panel As New Panel(),
                  button As New IconButton()

                panel.Controls.Add(button)

                ThemeManager.Apply(panel, AppTheme.Dark)
                Assert.AreEqual(ThemePalette.Dark.DisabledText, button.DisabledForeColor)

                ThemeManager.Apply(panel, AppTheme.Light)
                Assert.AreEqual(ThemePalette.Light.DisabledText, button.DisabledForeColor)
            End Using

        End Sub

#End Region

    End Class

End Namespace

#End Region
