




' THIS OPEN-SOURCE APPLICATION IS POWERED BY DEVCASE CLASS LIBRARY, CREATED BY ELEKTRO STUDIOS.

' WHAT YOU SEE HERE IS FREE CUTTED CONTENT OF THIS FRAMEWORK.

' IF YOU LIKED THIS FREE APPLICATION, THEN PLEASE CONSIDER TO BUY DEVCASE CLASS LIBRARY FOR .NET AT:
' https://codecanyon.net/item/elektrokit-class-library-for-net/19260282

' YOU CAN FIND THESE HELPER METHODS AND A MASSIVE AMOUNT MORE!, 
' +850 EXTENSION METHODS FOR ALL KIND OF TYPES, CUSTOM USER-CONTROLS, 
' EVERYTHING FOR THE NEWBIE And THE ADVANCED USER, FOR VB.NET AND C#. 

' ElektroKit is a utility framework containing new APIs and extensions to the core .NET Framework 
' to help complete your developer toolbox.
' It Is a set of general purpose classes provided as easy to consume packages.
' These utility classes and components provide productivity in day to day software development 
' mainly focused To WindowsForms. 

' UPDATES OF ELEKTROKIT ARE MAINTAINED AND RELEASED EVERY MONTH.






#Region " Usage Examples "

'Using msg As New CenteredMessageBox(owner:=Me, timeOut:=2500)
'
'    msg.Show("Text", "Title", MessageBoxButtons.OK, MessageBoxIcon.Information)
'
'End Using

#End Region

#Region " Option Statements "

Option Strict On
Option Explicit On
Option Infer Off

#End Region

#Region " Imports "

Imports System.Text
Imports System.Threading

Imports ElektroKit.Interop.Win32
Imports ElektroKit.Interop.Win32.Enums
Imports ElektroKit.Interop.Win32.Types

#End Region

#Region " Centered MessageBox "

Namespace ElektroKit.Core.Application.UserInterface.Types

    ''' ----------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Represents a <see cref="MessageBox"/> that will be displayed centered to the 
    ''' specified <see cref="Form"/> or <see cref="Control"/>.
    ''' </summary>
    ''' ----------------------------------------------------------------------------------------------------
    ''' <example> This is a code example.
    ''' <code>
    ''' Using msg As New CenteredMessageBox(owner:=Me, timeOut:=2500)
    ''' 
    '''     msg.Show("Text", "Title", MessageBoxButtons.OK, MessageBoxIcon.Information)
    ''' 
    ''' End Using
    ''' </code>
    ''' </example>
    ''' ----------------------------------------------------------------------------------------------------
    Public Class CenteredMessageBox : Implements IDisposable

#Region " Private Fields "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' A <see cref="Windows.Forms.Timer"/> that keeps track of <see cref="CenteredMessageBox.TimeOut"/> value to close this <see cref="CenteredMessageBox"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private WithEvents TimeoutTimer As Windows.Forms.Timer

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Keeps track of the current amount of tries to find this <see cref="CenteredMessageBox"/> dialog.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private tries As Integer

#End Region

#Region " Properties "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the messagebox main window handle (hwnd).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The messagebox main window handle (hwnd).
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Protected ReadOnly Property DialogWindowHandle As IntPtr
            Get
                Return Me.dialogWindowHandleB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The messagebox main window handle (hwnd).
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private dialogWindowHandleB As IntPtr

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets the owner <see cref="Control"/> to center the <see cref="CenteredMessageBox"/>.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <value>
        ''' The owner <see cref="Control"/> to center the <see cref="CenteredMessageBox"/>.
        ''' </value>
        ''' ----------------------------------------------------------------------------------------------------
        Public ReadOnly Property Owner As Control
            Get
                Return Me.ownerB
            End Get
        End Property
        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' The owner <see cref="Form"/> to center the <see cref="CenteredMessageBox"/>
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private ownerB As Control

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Gets or sets the time interval to auto-close this <see cref="CenteredMessageBox"/>, in milliseconds.
        ''' Default value is '0', which means Infinite.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Public Property TimeOut As Integer

#End Region

#Region " Constructors "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="CenteredMessageBox"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="owner">T
        ''' The <see cref="Form"/> that owns this <see cref="CenteredMessageBox"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal owner As Form)
            Me.New(owner, Global.System.Threading.Timeout.Infinite)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="CenteredMessageBox"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="owner">T
        ''' The <see cref="Form"/> that owns this <see cref="CenteredMessageBox"/>.
        ''' </param>
        ''' 
        ''' <param name="timeOut">
        ''' The time interval to auto-close this <see cref="CenteredMessageBox"/>, in milliseconds.
        ''' <para></para>
        ''' Default value is 0, which means Infinite.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal owner As Form, ByVal timeOut As Integer)
            Me.New(DirectCast(owner, Control), timeOut)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="CenteredMessageBox"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="owner">T
        ''' The <see cref="Control"/> that owns this <see cref="CenteredMessageBox"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal owner As Control)
            Me.New(owner, Global.System.Threading.Timeout.Infinite)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Initializes a new instance of the <see cref="CenteredMessageBox"/> class.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="owner">T
        ''' The <see cref="Control"/> that owns this <see cref="CenteredMessageBox"/>.
        ''' </param>
        ''' 
        ''' <param name="timeOut">
        ''' The time interval to auto-close this <see cref="CenteredMessageBox"/>, in milliseconds.
        ''' <para></para>
        ''' Default value is 0, which means Infinite.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        Public Sub New(ByVal owner As Control, ByVal timeOut As Integer)

            Me.ownerB = owner
            Me.TimeOut = timeOut
            Me.ownerB.BeginInvoke(New MethodInvoker(AddressOf Me.FindDialog))

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Prevents a default instance of the <see cref="CenteredMessageBox"/> class from being created.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub New()
        End Sub

#End Region

#Region " Public Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with specified text.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(ByVal text As String) As DialogResult
            Return MessageBox.Show(Me.Owner, text)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with specified text and caption.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String) As DialogResult
            Return MessageBox.Show(Me.Owner, text, caption)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with specified text, caption, and buttons.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' 
        ''' <param name="buttons">
        ''' One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String, buttons As MessageBoxButtons) As DialogResult
            Return MessageBox.Show(Me.Owner, text, caption, buttons)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with specified text, caption, buttons, and icon.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' 
        ''' <param name="buttons">
        ''' One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="icon">
        ''' One of the <see cref="MessageBoxIcon"/> values that specifies which icon  to display in the message box.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon) As DialogResult
            Return MessageBox.Show(Me.Owner, text, caption, buttons, icon)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with the specified text, caption, buttons, icon, and default button.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' 
        ''' <param name="buttons">
        ''' One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="icon">
        ''' One of the <see cref="MessageBoxIcon"/> values that specifies which icon  to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="defaultButton">
        ''' One of the <see cref="MessageBoxDefaultButton"/> values that specifies the default button for the message box.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton) As DialogResult
            Return MessageBox.Show(Me.Owner, text, caption, buttons, icon, defaultButton)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with the specified text, caption, buttons, icon, default button, and options.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' 
        ''' <param name="buttons">
        ''' One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="icon">
        ''' One of the <see cref="MessageBoxIcon"/> values that specifies which icon  to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="defaultButton">
        ''' One of the <see cref="MessageBoxDefaultButton"/> values that specifies the default button for the message box.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' One of the <see cref="MessageBoxOptions"/> values that specifies which display and association options will be used for 
        ''' the message box. 
        ''' <para></para>
        ''' You may pass in 0 if you wish to use the defaults.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions) As DialogResult
            Return MessageBox.Show(Me.Owner, text, caption, buttons, icon, defaultButton, options)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, 
        ''' using the specified Help file.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' 
        ''' <param name="buttons">
        ''' One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="icon">
        ''' One of the <see cref="MessageBoxIcon"/> values that specifies which icon  to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="defaultButton">
        ''' One of the <see cref="MessageBoxDefaultButton"/> values that specifies the default button for the message box.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' One of the <see cref="MessageBoxOptions"/> values that specifies which display and association options will be used for 
        ''' the message box. 
        ''' <para></para>
        ''' You may pass in 0 if you wish to use the defaults.
        ''' </param>
        ''' 
        ''' <param name="helpFilePath">
        ''' The path and name of the Help file to display when the user clicks the Help button.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String) As DialogResult
            Return MessageBox.Show(Me.Owner, text, caption, buttons, icon, defaultButton, options, helpFilePath)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' 
        ''' <param name="buttons">
        ''' One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="icon">
        ''' One of the <see cref="MessageBoxIcon"/> values that specifies which icon  to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="defaultButton">
        ''' One of the <see cref="MessageBoxDefaultButton"/> values that specifies the default button for the message box.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' One of the <see cref="MessageBoxOptions"/> values that specifies which display and association options will be used for 
        ''' the message box. 
        ''' <para></para>
        ''' You may pass in 0 if you wish to use the defaults.
        ''' </param>
        ''' 
        ''' <param name="displayHelpButton">
        ''' <see langword="True"/> to show the Help button; otherwise, false. The default is <see langword="False"/>.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, displayHelpButton As Boolean) As DialogResult
            Return MessageBox.Show(text, caption, buttons, icon, defaultButton, options, displayHelpButton)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, 
        ''' using the specified Help file and HelpNavigator.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' 
        ''' <param name="buttons">
        ''' One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="icon">
        ''' One of the <see cref="MessageBoxIcon"/> values that specifies which icon  to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="defaultButton">
        ''' One of the <see cref="MessageBoxDefaultButton"/> values that specifies the default button for the message box.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' One of the <see cref="MessageBoxOptions"/> values that specifies which display and association options will be used for 
        ''' the message box. 
        ''' <para></para>
        ''' You may pass in 0 if you wish to use the defaults.
        ''' </param>
        ''' 
        ''' <param name="helpFilePath">
        ''' The path and name of the Help file to display when the user clicks the Help button.
        ''' </param>
        ''' 
        ''' <param name="navigator">
        ''' One of the <see cref="HelpNavigator"/> values.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String, navigator As HelpNavigator) As DialogResult
            Return MessageBox.Show(Me.Owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, 
        ''' using the specified Help file and Help keyword.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' 
        ''' <param name="buttons">
        ''' One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="icon">
        ''' One of the <see cref="MessageBoxIcon"/> values that specifies which icon  to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="defaultButton">
        ''' One of the <see cref="MessageBoxDefaultButton"/> values that specifies the default button for the message box.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' One of the <see cref="MessageBoxOptions"/> values that specifies which display and association options will be used for 
        ''' the message box. 
        ''' <para></para>
        ''' You may pass in 0 if you wish to use the defaults.
        ''' </param>
        ''' 
        ''' <param name="helpFilePath">
        ''' The path and name of the Help file to display when the user clicks the Help button.
        ''' </param>
        ''' 
        ''' <param name="keyword">
        ''' The Help keyword to display when the user clicks the Help button.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String, keyword As String) As DialogResult
            Return MessageBox.Show(Me.Owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, keyword)
        End Function

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Displays a message box with the specified text, caption, buttons, icon, default button, options, and Help button, 
        ''' using the specified Help file, HelpNavigator, and Help topic.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="text">
        ''' The text to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="caption">
        ''' The text to display in the title bar of the message box.
        ''' </param>
        ''' 
        ''' <param name="buttons">
        ''' One of the <see cref="MessageBoxButtons"/> values that specifies which buttons to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="icon">
        ''' One of the <see cref="MessageBoxIcon"/> values that specifies which icon  to display in the message box.
        ''' </param>
        ''' 
        ''' <param name="defaultButton">
        ''' One of the <see cref="MessageBoxDefaultButton"/> values that specifies the default button for the message box.
        ''' </param>
        ''' 
        ''' <param name="options">
        ''' One of the <see cref="MessageBoxOptions"/> values that specifies which display and association options will be used for 
        ''' the message box. 
        ''' <para></para>
        ''' You may pass in 0 if you wish to use the defaults.
        ''' </param>
        ''' 
        ''' <param name="helpFilePath">
        ''' The path and name of the Help file to display when the user clicks the Help button.
        ''' </param>
        ''' 
        ''' <param name="navigator">
        ''' One of the <see cref="HelpNavigator"/> values.
        ''' </param>
        ''' 
        ''' <param name="param">
        ''' The numeric ID of the Help topic to display when the user clicks the Help button.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' One of the <see cref="DialogResult"/> values.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Public Overridable Function Show(text As String, caption As String, buttons As MessageBoxButtons, icon As MessageBoxIcon, defaultButton As MessageBoxDefaultButton, options As MessageBoxOptions, helpFilePath As String, navigator As HelpNavigator, param As Object) As DialogResult
            Return MessageBox.Show(Me.Owner, text, caption, buttons, icon, defaultButton, options, helpFilePath, navigator, param)
        End Function

#End Region

#Region " Private Methods "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Finds the <see cref="CenteredMessageBox"/> dialog window.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private Sub FindDialog()

            ' Enumerate windows to find the messagebox dialog window.
            If (Me.tries < 0) Then
                Exit Sub
            End If

            Dim callback As New Delegates.EnumThreadWindowsProc(AddressOf Me.CheckWindow)

            If NativeMethods.EnumThreadWindows(NativeMethods.GetCurrentThreadId(), callback, IntPtr.Zero) Then

                If Interlocked.Increment(Me.tries) < 10 Then
                    Me.ownerB.BeginInvoke(New MethodInvoker(AddressOf Me.FindDialog))
                End If

            End If

            If (Me.TimeOut > 0) Then

                If (Me.TimeoutTimer IsNot Nothing) Then
                    Me.TimeoutTimer.Stop()
                    Me.TimeoutTimer.Enabled = False
                    Me.TimeoutTimer.Dispose()
                    Me.TimeoutTimer = Nothing
                End If

                Me.TimeoutTimer = New Windows.Forms.Timer With
                                  {
                                      .Interval = Me.TimeOut,
                                      .Enabled = True
                                  }

                Me.TimeoutTimer.Start()

            End If

        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Checks whether the specified window is our <see cref="CenteredMessageBox"/> dialog window.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="hwnd">
        ''' A handle to the window to check.
        ''' </param>
        ''' 
        ''' <param name="lParam">
        ''' The application-defined value given in the <see cref="NativeMethods.EnumThreadWindows"/> function.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <returns>
        ''' <see langword="True"/> the specified window is our <see cref="CenteredMessageBox"/> dialog window, 
        ''' <see langword="False"/> otherwise.
        ''' </returns>
        ''' ----------------------------------------------------------------------------------------------------
        Private Function CheckWindow(ByVal hwnd As IntPtr, ByVal lParam As IntPtr) As Boolean

            ' Checks if <hWnd> is a dialog
            Dim sb As New StringBuilder(260)
            NativeMethods.GetClassName(hwnd, sb, sb.Capacity)
            If (sb.ToString() <> "#32770") Then
                Return True
            End If

            ' Get the control that displays the text.
            Dim hText As IntPtr = NativeMethods.GetDlgItem(hwnd, &HFFFFI)
            Me.dialogWindowHandleB = hwnd

            ' Get the dialog Rect.
            Dim frmRect As New Rectangle(Me.ownerB.Location, Me.ownerB.Size)
            Dim dlgRect As NativeRectangle
            NativeMethods.GetWindowRect(hwnd, dlgRect)

            ' Center the dialog window in the owner Form.
            NativeMethods.SetWindowPos(hwnd:=hwnd, hwndInsertAfter:=IntPtr.Zero,
                                       x:=frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) \ 2I,
                                       y:=frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) \ 2I,
                                       cx:=0, cy:=0,
                                       uFlags:=SetWindowPosFlags.IgnoreResize)

            ' Stop the EnumThreadWndProc callback by returning False.
            Return False

        End Function

#End Region

#Region " Event Handlers "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Handles the <see cref="Windows.Forms.Timer.Tick"/> event of the <see cref="CenteredMessageBox.TimeoutTimer"/> control.
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
        Private Sub TimeoutTimer_Tick(ByVal sender As Object, ByVal e As Global.System.EventArgs) Handles TimeoutTimer.Tick

            NativeMethods.DestroyWindow(Me.dialogWindowHandleB)
            Me.Dispose()

        End Sub

#End Region

#Region " IDisposable Implementation "

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Flag to detect redundant calls when disposing.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        Private isDisposed As Boolean = False

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Releases all the resources used by this instance.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(isDisposing:=True)
            GC.SuppressFinalize(obj:=Me)
        End Sub

        ''' ----------------------------------------------------------------------------------------------------
        ''' <summary>
        ''' Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ''' Releases unmanaged and, optionally, managed resources.
        ''' </summary>
        ''' ----------------------------------------------------------------------------------------------------
        ''' <param name="isDisposing">
        ''' <see langword="True"/>  to release both managed and unmanaged resources; 
        ''' <see langword="False"/> to release only unmanaged resources.
        ''' </param>
        ''' ----------------------------------------------------------------------------------------------------
        <DebuggerStepThrough>
        Protected Overridable Sub Dispose(ByVal isDisposing As Boolean)

            If (Not Me.isDisposed) AndAlso (isDisposing) Then
                Me.tries = -1
                Me.ownerB = Nothing

                If (Me.TimeoutTimer IsNot Nothing) Then
                    Me.TimeoutTimer.Stop()
                    Me.TimeoutTimer.Enabled = False
                    Me.TimeoutTimer.Dispose()
                End If
            End If

            Me.isDisposed = True

        End Sub

#End Region

    End Class

End Namespace

#End Region
