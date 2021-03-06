﻿Module Main

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

            'Report process ID and end
            Console.WriteLine("Spawned process: " + p.Id.ToString)
            End
        Catch ex As Exception
            'On error, print the error message
            Console.WriteLine("Error: " + ex.Message)
        End Try
    End Sub

    ' Print help text and then end
    Sub PrintHelpTextAndEnd()
        Console.WriteLine(My.Resources.HelpText)
        End
    End Sub
End Module
