#Region "Import-Settings"
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Globalization
Imports Microsoft.Win32
#End Region
Module Server
    
  
End Module
Friend Class Client
    Private Function SB(ByVal s As String) As Byte()
        Return System.Text.Encoding.Default.GetBytes(s)
    End Function
    Private Function BS(ByVal b As Byte()) As String
        Return System.Text.Encoding.Default.GetString(b)
    End Function
    Private C As TcpClient
    Friend Event Connected()
    Private restarted As Boolean = False
    Friend Event Disconnected()
    Friend Event Data(ByVal b As Byte())
    Private IsBuzy As Boolean = False
    Private SPL As String = "sx-lj3"
    Friend Function Statconnected() As Boolean
        Try
            If C.Client.Connected = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
        End Try
    End Function
    Friend Sub DisConnect()
        Try
            C.Close()
        Catch ex As Exception
        End Try
        C = Nothing
        RaiseEvent Disconnected()
        If restarted = False Then
            Application.Restart()
            restarted = True
        End If

    End Sub
    Private Sub notify()
        Try
            My.Computer.Audio.Play(My.Resources.chord, AudioPlayMode.Background)

        Catch ex As Exception

        End Try
    End Sub
    Friend Sub Send(ByVal s As String)
    
        Send(SB(s))
        restarted = False
    End Sub
     
    Friend Sub Send(ByVal b As Byte())
        Try
            Dim m As New IO.MemoryStream

            m.Write(b, 0, b.Length)
            m.Write(SB(SPL), 0, SPL.Length)
            C.Client.Send(m.ToArray, 0, m.Length, SocketFlags.None)
        Catch ex As Exception
            DisConnect()
        End Try
    End Sub
    Friend Sub Connect(ByVal h As String, ByVal p As Integer)
        Try
            Try
                If C IsNot Nothing Then
                    C.Close()
                    C = Nothing
                End If
            Catch ex As Exception
            End Try
            Do Until IsBuzy = False
                Threading.Thread.CurrentThread.Sleep(1)
            Loop
            Try
                C = New TcpClient
                C.Connect(h, p)
                Dim t As New Threading.Thread(AddressOf RC, 10)
                t.Start()
                RaiseEvent Connected()
            Catch ex As Exception
            End Try
        Catch ex As Exception
            RaiseEvent Disconnected()
        End Try
    End Sub
    Private Sub RC()
        IsBuzy = True
        Dim M As New IO.MemoryStream
        Dim cc As Integer = 0
re:
        Threading.Thread.CurrentThread.Sleep(1)
        Try
            If C Is Nothing Then
                GoTo co
            Else
                If C.Client.Connected = False Then
                    GoTo co
                Else
                    cc += 1
                    If cc > 100 Then
                        cc = 0
                        Try
                            If C.Client.Poll(-1, Net.Sockets.SelectMode.SelectRead) And C.Client.Available <= 0 Then
                                GoTo co
                            End If
                        Catch ex As Exception
                            GoTo co
                        End Try
                    End If
                End If
            End If
            If C.Available > 0 Then
                Dim B(C.Available - 1) As Byte
                C.Client.Receive(B, 0, B.Length, Net.Sockets.SocketFlags.None)
                M.Write(B, 0, B.Length)
rr:
                If BS(M.ToArray).Contains(SPL) Then
                    Dim A As Array = fx(M.ToArray, SPL)
                    RaiseEvent Data(A(0))
                    M.Dispose()
                    M = New IO.MemoryStream
                    If A.Length = 2 Then
                        M.Write(A(1), 0, A(1).length)
                        Threading.Thread.CurrentThread.Sleep(1)
                        GoTo rr
                    End If
                End If
            End If
        Catch ex As Exception
            GoTo co
        End Try
        GoTo re
co:
        IsBuzy = False
        DisConnect()
    End Sub
    Friend Function fx(ByVal b As Byte(), ByVal WRD As String) As Array
        Dim a As New List(Of Byte())
        Dim M As New IO.MemoryStream
        Dim MM As New IO.MemoryStream
        Dim T As String() = Split(BS(b), WRD)
        M.Write(b, 0, T(0).Length)
        MM.Write(b, T(0).Length + WRD.Length, b.Length - (T(0).Length + WRD.Length))
        a.Add(M.ToArray)
        a.Add(MM.ToArray)
        M.Dispose()
        MM.Dispose()
        Return a.ToArray
    End Function
End Class