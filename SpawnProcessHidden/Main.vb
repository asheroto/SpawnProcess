Public Class Main
    Public Declare Function ShowWindow Lib "user32.dll" (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Boolean

    Public Enum ShowWindowCommand As Integer
        SW_HIDE = 0             ' Hides the window and activates another window.
        SW_SHOWNORMAL = 1       ' Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
        SW_SHOWMINIMIZED = 2    ' Activates the window and displays it as a minimized window.
        SW_MAXIMIZE = 3         ' Maximizes the specified window.
        SW_SHOWMAXIMIZED = 3    ' Activates the window and displays it as a maximized window.
        SW_SHOWNOACTIVATE = 4   ' Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.
        SW_SHOW = 5             ' Activates the window and displays it in its current size and position. 
        SW_MINIMIZE = 6         ' Minimizes the specified window and activates the next top-level window in the Z order.
        SW_SHOWMINNOACTIVE = 7  ' Displays the window as a minimized window. This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
        SW_SHOWNA = 8           ' Displays the window in its current size and position. This value is similar to SW_SHOW, except that the window is not activated.
        SW_RESTORE = 9          ' Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
        SW_SHOWDEFAULT = 10     ' Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application. 
        SW_FORCEMINIMIZE = 11   ' Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.
    End Enum
    Private Sub Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            'Get command line arguments
            Dim s() As String = Environment.GetCommandLineArgs()

            'Default vars
            Dim process_path As String = Nothing
            Dim process_args As String = Nothing
            Dim process_windowstyle As ProcessWindowStyle = ProcessWindowStyle.Hidden
            Dim process_workingdirectory As String = Nothing

            'If no arguments are specified, print help text
            If s.Length = 1 Then Exit Sub

            'Loop through the command line arguments
            For i As Integer = 1 To s.Length - 1
                Select Case LCase(s(i))
                    Case "-path"
                        'Store the path
                        process_path = s(i + 1)
                    Case "-args"
                        'Store the args (if provided)
                        process_args = s(i + 1)
                    Case "-workingdirectory"
                        'Store the working directory (if provided)
                        process_workingdirectory = s(i + 1)
                End Select
            Next

            'If path is not specified, print help text
            If process_path = "" Then
                Exit Sub
            End If

            'Create the process
            Dim p As New Process
            p.StartInfo.UseShellExecute = True
            p.StartInfo.FileName = process_path
            p.StartInfo.Arguments = process_args
            p.StartInfo.WindowStyle = process_windowstyle
            If process_workingdirectory <> "" Then
                p.StartInfo.WorkingDirectory = process_workingdirectory
            End If
            p.Start()

            End
        Catch ex As Exception
            'On error, print the error message
            TextBox.Text = "Error: " + Environment.NewLine + Environment.NewLine + ex.Message
        End Try
    End Sub

    Private Sub Main_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        TextBox.Select(0, 0)
    End Sub
End Class
