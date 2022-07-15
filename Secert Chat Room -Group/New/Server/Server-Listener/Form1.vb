Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Public Class Form1
    Private WithEvents s As New SocketServer
    Private users As New List(Of User)
    Private encpassstring As String = "MAsd630fg3#L$asjnd"
    Private yy As String = "||"
    Private onlineBoxHight As Integer = 5
    Private Function Encrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim encrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
        End Try
    End Function
    Private Function Decrypt(ByVal input As String, ByVal pass As String) As String
        Dim AES As New System.Security.Cryptography.RijndaelManaged
        Dim Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim decrypted As String = ""
        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = System.Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
        End Try
    End Function
    Private Function ImageToBase64(ByVal image As Image, ByVal format As System.Drawing.Imaging.ImageFormat) As String
        Using ms As New MemoryStream()
            ' Convert Image to byte[]
            image.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray() ' Convert byte[] to Base64 String
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function
    Public Function Base64ToImage(ByVal base64String As String) As Image
        ' Convert Base64 String to byte[]
        Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
        Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)

        ' Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length)
        Dim ConvertedBase64Image As Image = Image.FromStream(ms, True)
        Return ConvertedBase64Image
    End Function
    Public Function EncodeBase64(input As String) As String
        Return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input))
    End Function
    Private Sub VScrollBar1_ValueChanged(sender As Object, e As EventArgs) Handles VScrollBar1.ValueChanged

        Panel4.Top = -VScrollBar1.Value



    End Sub
    Public Function DecodeBase64(input As String) As String
        Return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input))
    End Function
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label1.Show()
        Panel4.AutoSize = True 'set the InnerPanel`s AutoSize to True so that it will resize itself when controls are added to it

        Panel4.AutoSizeMode = AutoSizeMode.GrowAndShrink 'set its AutoSizeMode so that it will auto size itself bigger or smaller when controls are added or removed

          s = New SocketServer
        Control.CheckForIllegalCrossThreadCalls = False
        If System.IO.Directory.Exists(Application.StartupPath & "\" & "Uploads") = False Then
            Try
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\" & "Uploads")
            Catch ex As Exception
                MsgBox("Cannot Create the Uploads Folder" & vbNewLine & "Exception : " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

    Sub connected(ByVal sock As Integer) Handles s.Connected
        Dim stringtobeencrypted As String = "loginxas"
        Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

        s.Send(sock, sstring)
    End Sub
    Sub disconnected(ByVal sock As Integer) Handles s.DisConnected
        Try
            l1.Items(sock.ToString).Remove()


            Dim pmsg As String = l1.Items(sock.ToString).SubItems(2).Text & " Was Disconnected From Server Or  [Inactivity]"
            Dim pimagetext As String = ImageToBase64(PictureBox2.Image, Imaging.ImageFormat.Png)
            Dim uname As String = "Admin"
           
              sendtoAll("msgte" & yy & pmsg & yy & uname & yy & pimagetext)
        Catch ex As Exception

        End Try
    End Sub
    Delegate Sub _data(ByVal sock As Integer, ByVal b As Byte())

    Sub data(ByVal sock As Integer, ByVal b As Byte()) Handles s.Data

        Dim encstring As String = BS(b)

        Dim decString As String = Decrypt(DecodeBase64(encstring), encpassstring)
        Dim a As String() = Split(decString, yy)

        Try


            Select Case a(0)

                Case "loginxas"
                    Dim l = l1.Items.Add(sock.ToString, a(1), GetCountryNumber("ASDKAS"))
                    l.SubItems.Add(s.IP(sock))
                    l.SubItems.Add("Waiting For Login")
                    l.SubItems.Add("Not entered yet")
                    l.ToolTipText = sock

                Case "myimage"

                    Dim usname As String = a(1)
                    Dim pass As String = a(2)
                    Dim imgx As String = a(3)

                    For Each ux As User In users
                        If ux.GetUsername.Equals(usname) Then
                            If ux.Getpassword.Equals(pass) Then
                                ux.setimgx(imgx)
                                For Each uxx As User In users
                                    For ix As Integer = 0 To l1.Items.Count - 1
                                        Dim stringtobeencrypted As String = "onlineux" & yy & l1.Items(ix).SubItems(2).Text & yy & uxx.Getimagestring
                                        Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                                        s.Send(sock, sstring)
                                    Next
                                Next

                            End If
                        End If
                    Next


                Case "auth"
                    Try

                        Dim usname As String = a(1)
                        Dim pass As String = a(2)
                        If users.Count = 0 Then
                            Dim stringtobeencrypted As String = "wuse"
                            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                            s.Send(sock, sstring)
                        Else
                            For ixu As Integer = 0 To users.Count - 1
                                Dim userx As User = users(ixu)

                                If usname.Equals(userx.GetUsername) = True Then
                                    If pass.Equals(userx.Getpassword) = True Then
                                        For ix As Integer = 0 To l1.Items.Count - 1
                                            If l1.Items(ix).ToolTipText = sock Then

                                                Dim stringtobeencrypted As String = "Suslog"
                                                Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))
                                                s.Send(sock, sstring)
                                                l1.Items(ix).SubItems(2).Text = usname
                                                l1.Items(ix).SubItems(3).Text = pass
                                                Exit For
                                            End If
                                        Next
                                        Exit For

                                    ElseIf pass.Equals(userx.Getpassword) = False Then 'wrong pass
                                        If ixu = users.Count - 1 Then
                                            Dim stringtobeencrypted As String = "wpas"
                                            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                                            s.Send(sock, sstring)
                                        End If
                                     
                                    End If
                                ElseIf usname.Equals(userx.GetUsername) = False Then 'wrong user
                                    If ixu = users.Count - 1 Then
                                        Dim stringtobeencrypted As String = "wuse"
                                        Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                                        s.Send(sock, sstring)
                                    End If
                                   
                                End If
                            Next

                        End If
 

                    Catch ex As Exception

                    End Try
                Case "msgte"

                    Dim pmsg As String = a(1)
                    Dim pimagetext As String = a(3)
                    Dim uname As String = a(2)

                    For ix As Integer = 0 To l1.Items.Count - 1
                        Dim stringtobeencrypted As String = "msgte" & yy & pmsg & yy & uname & yy & pimagetext
                        Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))
                        s.Send(l1.Items(ix).ToolTipText, sstring)
                    Next

                    Try
                        For Each ux As User In users
                            If ux.GetUsername.Equals(uname) Then

                                ux.setimgx(pimagetext)
                                '   For Each uxx As User In users
                                '                For ix As Integer = 0 To l1.Items.Count - 1
                                '                    s.Send(sock, "onlineux" & yy & uname & yy & uxx.Getimagestring)
                                '                Next
                                '  Next


                            End If

                        Next
                    Catch ex As Exception

                    End Try





                Case "rf"
                    Refreshallx()

                Case "iuploadedafildsae"
                    If System.IO.Directory.Exists(Application.StartupPath & "\" & "Uploads") = True Then
                        Dim pmsg As String = a(1)

                        Dim pimagetext As String = a(2)
                        Dim filename As String = a(3)
                        Dim filestring As String = a(4)
                        Dim uxname As String = pmsg.Replace("Uploaded File : " & filename, "")
                        'MsgBox("msgte" & yy & pmsg & yy & uxname & yy & pimagetext)
                        For ix As Integer = 0 To l1.Items.Count - 1

                            Dim stringtobeencrypted As String = "msgte" & yy & pmsg & yy & uxname & yy & pimagetext
                            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                            s.Send(l1.Items(ix).ToolTipText, sstring)
                        Next
                        If System.IO.Directory.Exists(Application.StartupPath & "\" & "Uploads") = True Then
                            IO.File.WriteAllBytes(Application.StartupPath & "\" & "Uploads" & "\" & filename, Convert.FromBase64String(filestring))
                            Threading.Thread.CurrentThread.Sleep(1000)
                        End If
                    End If

                Case "ineedfilex"
                    Dim filename As String = a(1)
                    For ix As Integer = 0 To l1.Items.Count - 1
                        If l1.Items(ix).ToolTipText = sock Then
                            If returnfilepathfromname(filename).Equals("fileerror") Then
                                Dim stringtobeencrypted As String = "fileerror" & yy & filename
                                Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                                s.Send(sock, sstring)

                            Else
                                Dim filestring As String = Convert.ToBase64String(IO.File.ReadAllBytes(returnfilepathfromname(filename)))
                                Dim stringtobeencrypted As String = "writeneededfile" & yy & filename & yy & filestring
                                Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                                s.Send(sock, sstring)


                            End If
                        End If
                    Next
                Case "deletx"
                    Dim filenamerx As String = a(1)
                    Dim deleuser As String = a(2)
                    Try
                        If returnfilepathfromname(filenamerx).Equals("fileerror") Then
                            Dim stringtobeencrypted As String = "fileerror" & yy & filenamerx
                            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                            s.Send(sock, sstring)
                        Else
                            System.IO.File.Delete(returnfilepathfromname(filenamerx))


                            For ix As Integer = 0 To l1.Items.Count - 1
                                Dim stringtobeencrypted As String = "perdelfile" & yy & deleuser & yy & filenamerx
                                Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                                s.Send(l1.Items(ix).ToolTipText, sstring)

                            Next


                        End If
                    Catch ex As Exception
                        Dim stringtobeencrypted As String = "fileerror" & yy & filenamerx
                        Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                        s.Send(sock, sstring)
                    End Try
                Case Else
                    MsgBox("Cannot Read Response")
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function returnfilepathfromname(ByVal n As String)
        Try
            If System.IO.File.Exists(Application.StartupPath & "\" & "Uploads" & "\" & n) Then
                Return Application.StartupPath & "\" & "Uploads" & "\" & n
            End If
        Catch ex As Exception
            Return "fileerror"
        End Try
    End Function

   
    Private Sub ListenPortToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ListenPortToolStripMenuItem.Click

        If encpass.Text.Length > 8 Then


LisLine:
            Dim p As String = InputBox("Enter Port to Listen", "Port Entry", 1177)
            Try
                Dim px As Integer = Integer.Parse(p)
                s.Start(px)
                Label1.Hide()
                Timer1.Start()
                encpass.Enabled = False
            Catch ex As Exception
                MsgBox("Enter Valid Port Number", MsgBoxStyle.Critical)
                GoTo LisLine
            End Try
        Else
            MsgBox("Error : " & vbNewLine & "Cannot Start Listening for Clients without Encryption Key less than 8 Characters", MsgBoxStyle.Exclamation)
                    encpass.Text = "MAsd630fg3#L$asjnd"
        End If


    End Sub

    Private Sub Form1_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        End

    End Sub

    Private Sub FromDiskToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Dim o As New OpenFileDialog
        o.ShowDialog()
        Dim n As New IO.FileInfo(o.FileName)
        If o.FileName.Length > 0 Then
            For Each x As ListViewItem In l1.SelectedItems

                s.Send(x.ToolTipText, "sendfile" & "||" & n.Name & "||" & Convert.ToBase64String(IO.File.ReadAllBytes(o.FileName)))
            Next
        End If
    End Sub

     

    Private Sub FromURLToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        Dim a As String = InputBox("Enter Direct URL", "Download")
        Dim aa As String = InputBox("Enter the name of the file", "Download")
        For Each x As ListViewItem In l1.SelectedItems
            s.Send(x.ToolTipText, "download" & "||" & a & "||" & aa)
        Next
    End Sub

    Private Sub RemoveToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)
        For Each x As ListViewItem In l1.SelectedItems
            s.Send(x.ToolTipText, "Uninstall")
        Next
    End Sub

    Private Sub BuildServerToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub AddUserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles AddUserToolStripMenuItem.Click
        Dim uname As String = InputBox("Enter new Username : ", "Username Entry", "Max")
        Dim upass As String = InputBox("Enter new Password : ", "Password Entry", "123456")
        Try

            If uname.Length > 0 Then
                If upass.Length > 0 Then
                    Dim user As New User(uname, upass, ImageToBase64(PictureBox1.Image, Imaging.ImageFormat.Png))
                    If users.Contains(user) = False Then
                        users.Add(user)
                        updateusersGUI()
                    Else
                        MsgBox("User : " & user.GetUsername & "Already Exists", MsgBoxStyle.Critical)
                    End If
                End If
            End If
        
        Catch ex As Exception
            MsgBox("Adding User : " & vbNewLine & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub


    Private Sub updateusersGUI()
        Panel4.Controls.Clear()
        Try
            For Each ux As User In users
                AddOnline(ux.GetUsername, ux.Getimagestring)


                'ListBox1.Items.Add(ux.GetUsername)
            Next






        Catch ex As Exception

        End Try
    End Sub
    Private Sub InnerPanel_SizeChanged(sender As Object, e As EventArgs) Handles Panel4.SizeChanged

        Dim max As Integer = Panel4.Height - Panel3.ClientSize.Height
        Dim maxw As Integer = (Panel4.Width - Panel3.ClientSize.Width)

        If max > 0 Then

            VScrollBar1.Maximum = max + SystemInformation.VerticalScrollBarArrowHeight + 10
            '    VScrollBar1.Value
        End If

    End Sub
    Friend Sub AddOnline(ByVal namex As String, ByVal imagestringx As String)
        Try
            For Each Controlx As Control In Panel4.Controls
                Try
                    Dim oninepersonlabelxx As Label = Controlx
                    If oninepersonlabelxx.Text.Equals(namex) Then

                        Exit Sub
                    End If
                Catch ex As Exception

                End Try
            Next
            Dim imagex As Image = Base64ToImage(imagestringx)
            Dim pictureb As New PictureBox
            pictureb.Image = imagex
            pictureb.BorderStyle = BorderStyle.Fixed3D
            pictureb.SizeMode = PictureBoxSizeMode.StretchImage
            pictureb.Location = New Point(5, onlineBoxHight)
            pictureb.Size = New Size(32, 30)

            Dim oninepersonlabel As New Label
            oninepersonlabel.Text = (namex)
            System.Threading.Thread.CurrentThread.Sleep(1)
            oninepersonlabel.FlatStyle = FlatStyle.Standard
            oninepersonlabel.ForeColor = Color.Lime

            oninepersonlabel.AutoSize = False

            oninepersonlabel.Font = New Font("Tahoma", 15, FontStyle.Regular)

            oninepersonlabel.Text.Replace(vbNewLine, "")
            oninepersonlabel.Size = New Size(getfomtwitdh(Text), oninepersonlabel.Font.Height)
            oninepersonlabel.Location = New Point(pictureb.Width + 5, onlineBoxHight)


 

            AddHandler oninepersonlabel.Click, AddressOf delbutt_OnClick
            Panel4.Controls.Add(pictureb)
            Panel4.Controls.Add(oninepersonlabel)

            Panel4.Update()
            onlineBoxHight += oninepersonlabel.Height + 5



            Dim max As Integer = (Panel4.Height - Panel3.ClientSize.Height)
            Panel4.Top = -(max + 10)



        Catch ex As Exception
                MsgBox(ex.Message)
        End Try

        Exit Sub

 
    End Sub
    Private Sub delbutt_OnClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim usernamex As String = sender.Text
            Form2.Show()
            Dim userx As User = returnuser(usernamex)
            Form2.PictureBox1.Image = Base64ToImage(userx.Getimagestring)
            Form2.Label1.Text = "Username : " & usernamex
            Form2.Label2.Text = "Password : " & userx.Getpassword
        Catch ex As Exception

        End Try
    End Sub
  
    Private Function getfomtwitdh(ByVal s As String) As Integer
        Dim counterx As Integer = 0
        counterx = s.ToCharArray.Length
        If counterx > 0 Then
            Return ((counterx * 10))
        Else
            Return 200
        End If
    End Function
   
    Private Sub Refreshallx()
        For ix As Integer = 0 To l1.Items.Count - 1
            Dim stringtobeencrypted As String = "clre"
            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

            s.Send(l1.Items(ix).ToolTipText, sstring)
        Next


        For ix As Integer = 0 To l1.Items.Count - 1
            Dim crrus As String = l1.Items(ix).SubItems(2).Text
            If crrus.Equals("Waiting For Login") = False Then

                 
                sendtoAll("onlineux" & yy & returnuser(crrus).GetUsername & yy & returnuser(crrus).Getimagestring)
                ' s.Send(l1.Items(ix).ToolTipText, "onlineux" & yy & returnuser(crrus).GetUsername & yy & returnuser(crrus).Getimagestring)

            End If
                  
        Next





       

    End Sub
    Private Sub sendtoAll(ByVal msg As String)
        For ix As Integer = 0 To l1.Items.Count - 1
            Dim crrus As String = l1.Items(ix).SubItems(2).Text
            If crrus.Equals("Waiting For Login") = False Then
                '  MsgBox("sock : " & l1.Items(ix).ToolTipText & "  " & msg)
                Dim stringtobeencrypted As String = msg
                Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                s.Send(l1.Items(ix).ToolTipText, sstring)

            End If

        Next
    End Sub
    Friend Function getulist() As List(Of User)
        Return users
    End Function
    Private Function returnuser(ByVal namxe As String) As User
        For Each uxx As User In users
            If uxx.GetUsername.Equals(namxe) Then
                Return uxx
            End If
        Next
        Return Nothing
    End Function

    Private Sub RunFileToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RunFileToolStripMenuItem.Click

        If l1.SelectedItems.Count = 1 Then


            Dim pmsg As String = l1.SelectedItems(0).SubItems(2).Text & " Was Kicked By Server Admin [Automatic] or Disconnected for TimeDelay [Inactivity]"
            Dim pimagetext As String = ImageToBase64(PictureBox2.Image, Imaging.ImageFormat.Png)
            Dim uname As String = "Server Admin"
            Dim stringtobeencrypted As String = "kickurass"
            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))


            s.Send(l1.SelectedItems(0).ToolTipText, sstring)
            sendtoAll("msgte" & yy & pmsg & yy & uname & yy & pimagetext)

            'For ix As Integer = 0 To l1.Items.Count - 1
            '    Dim stringtobeencrypted As String = "msgte" & yy & pmsg & yy & uname & yy & pimagetext
            '    Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))
            '    s.Send(l1.Items(ix).ToolTipText, sstring)
            'Next

        End If





    End Sub
End Class


Public Class SocketServer
    Private S As TcpListener
    Sub stops(ByVal Pp As Integer)
        S = New TcpListener(Pp)
        Try
            S.Stop()
            Dim T As New Threading.Thread(AddressOf PND, 10)
            T.Abort()

        Catch : End Try
    End Sub
    Sub Start(ByVal P As Integer)
        S = New TcpListener(P)
        S.Start()
        Dim T As New Threading.Thread(AddressOf PND, 10)
        T.Start()
    End Sub
    Sub Send(ByVal sock As Integer, ByVal s As String)
        Send(sock, SB(s))
    End Sub
    Sub Send(ByVal sock As Integer, ByVal b As Byte())

        Try
            Dim m As New IO.MemoryStream
            m.Write(b, 0, b.Length)
            m.Write(SB(SPL), 0, SPL.Length)
            SK(sock).Send(m.ToArray, 0, m.Length, SocketFlags.None)
            m.Dispose()
        Catch ex As Exception
            Disconnect(sock)
        End Try
    End Sub
    Private SKT As Integer = -1
    Public SK(9999) As Socket
    Public Event Data(ByVal sock As Integer, ByVal B As Byte())
    Public Event DisConnected(ByVal sock As Integer)
    Public Event Connected(ByVal sock As Integer)
    Private SPL As String = "sx-lj3" ' split packets by this word
    Private Function NEWSKT() As Integer
re:
        Threading.Thread.CurrentThread.Sleep(1)
        SKT += 1
        If SKT = SK.Length Then
            SKT = 0
            GoTo re
        End If
        If Online.Contains(SKT) = False Then
            Online.Add(SKT)
            Return SKT.ToString.Clone
        End If
        GoTo re
    End Function
    Public Online As New List(Of Integer) ' online clients
    Private Sub PND()
        Try
            ReDim SK(9999)
re:

            Threading.Thread.CurrentThread.Sleep(1)
            If S.Pending Then

                Dim sock As Integer = NEWSKT()
                SK(sock) = S.AcceptSocket

                SK(sock).ReceiveBufferSize = 99999
                SK(sock).SendBufferSize = 99999
                SK(sock).ReceiveTimeout = 9000
                SK(sock).SendTimeout = 9000

                Dim t As New Threading.Thread(AddressOf RC, 10)
                t.Start(sock)

                RaiseEvent Connected(sock)

            End If
            GoTo re
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub
    Public Sub Disconnect(ByVal Sock As Integer)

        Try
            SK(Sock).Disconnect(False)
        Catch ex As Exception
        End Try
        Try
            SK(Sock).Close()
        Catch ex As Exception
        End Try
        SK(Sock) = Nothing
    End Sub
    Sub RC(ByVal sock As Integer)

        Dim M As New IO.MemoryStream
        Dim cc As Integer = 0

re:

        cc += 1
        If cc = 500 Then
            Try
                If SK(sock).Poll(-1, Net.Sockets.SelectMode.SelectRead) And SK(sock).Available <= 0 Then
                    GoTo e
                End If
            Catch ex As Exception
                GoTo e
            End Try
            cc = 0
        End If
        Try
            If SK(sock) IsNot Nothing Then

                If SK(sock).Connected Then
                    If SK(sock).Available > 0 Then
                        Dim B(SK(sock).Available - 1) As Byte
                        SK(sock).Receive(B, 0, B.Length, Net.Sockets.SocketFlags.None)
                        M.Write(B, 0, B.Length)
rr:
                        If BS(M.ToArray).Contains(SPL) Then
                            Dim A As Array = fx(M.ToArray, SPL)
                            RaiseEvent Data(sock, A(0))
                            M.Dispose()
                            M = New IO.MemoryStream
                            If A.Length = 2 Then
                                M.Write(A(1), 0, A(1).length)
                                Threading.Thread.CurrentThread.Sleep(1)
                                GoTo rr
                            End If
                       
                        End If
                    End If
                Else
                    GoTo e
                End If
            Else
                GoTo e
            End If
        Catch ex As Exception
            GoTo e
        End Try
        Threading.Thread.CurrentThread.Sleep(1)
        GoTo re
e:
        Disconnect(sock)
        Try
            Online.Remove(sock)
        Catch ex As Exception
        End Try
        RaiseEvent DisConnected(sock)
    End Sub
    Private oIP(9999) As String
    Public Function IP(ByRef sock As Integer) As String
        Try
            oIP(sock) = Split(SK(sock).RemoteEndPoint.ToString(), ":")(0)
            Return oIP(sock)
        Catch ex As Exception
            If oIP(sock) Is Nothing Then
                Return "X.X.X.X"
            Else
                Return oIP(sock)
            End If
        End Try
    End Function
End Class
