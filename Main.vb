Module Main
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

    Sub Main()
        Try
            'Skip a line
            Console.WriteLine("")

            'Get command line arguments
            Dim s() As String = Environment.GetCommandLineArgs()

            'Default vars
            Dim process_path As String = Nothing
            Dim process_args As String = Nothing
            Dim process_windowstyle As ProcessWindowStyle = ProcessWindowStyle.Normal
            Dim process_workingdirectory As String = Nothing

            'If no arguments are specified, print help text
            If s.Length = 1 Then PrintHelpTextAndEnd()

            'Loop through the command line arguments
            For i As Integer = 1 To s.Length - 1
                Select Case LCase(s(i))
                    Case "-path"
                        'Store the path
                        process_path = s(i + 1)
                    Case "-args"
                        'Store the args (if provided)
                        process_args = s(i + 1)
                    Case "-windowstyle"
                        'Store the window style (if provided)
                        Select Case LCase(s(i + 1))
                            Case "hidden"
                                process_windowstyle = ProcessWindowStyle.Hidden
                            Case "normal"
                                process_windowstyle = ProcessWindowStyle.Normal
                            Case "minimized"
                                process_windowstyle = ProcessWindowStyle.Minimized
                            Case "maximized"
                                process_windowstyle = ProcessWindowStyle.Maximized
                            Case Else
                                'If something other than the above choices is specified, print help text
                                PrintHelpTextAndEnd()
                        End Select
                    Case "-workingdirectory"
                        'Store the working directory (if provided)
                        process_workingdirectory = s(i + 1)
                End Select
            Next

            'If path is not specified, print help text
            If process_path = "" Then
                PrintHelpTextAndEnd()
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

            'Wait half a second
            Dim j As New Stopwatch
            j.Start()
            If j.ElapsedMilliseconds < 500 Then
                'Wait
            End If
            j.Stop()

            'If supposed to be minimized, force it to be minimized (console apps don't minimize process, .NET bug)
            If p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized Then
                'Waits until mainwindowhandle is created
                Dim sw As New Stopwatch
                sw.Start()
                Do Until p.MainWindowHandle.ToInt32 > 0 Or sw.Elapsed.Seconds > 2
                    'Nothing
                Loop
                sw.Stop()

                'Reset stopwatch, then repeat force window minimized for 1 second to ensure minimized
                sw.Reset()
                sw.Start()
                Do Until sw.Elapsed.Seconds > 1
                    ShowWindow(p.MainWindowHandle, ShowWindowCommand.SW_FORCEMINIMIZE)
                Loop
                sw.Stop()
            End If

            'Report process ID and end
            Console.WriteLine("Spawned process: " + p.Id.ToString)
            Console.WriteLine()
            End
        Catch ex As Exception
            'On error, print the error message
            Console.WriteLine("Error: " + ex.Message)
            Console.WriteLine()
        End Try
    End Sub

    ' Print help text and then end
    Sub PrintHelpTextAndEnd()
        Console.WriteLine(My.Resources.HelpText)
        Console.WriteLine()
        End
    End Sub
End Module
