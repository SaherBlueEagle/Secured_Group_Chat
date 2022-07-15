Imports System.Globalization
Imports System.IO

Public Class Form1
    Dim WithEvents Tryer As New System.Windows.Forms.Timer
    Private yy As String = "||"
    Private encpassstring As String = "MAsd630fg3#L$asjnd"
    Friend BoxHight As Integer = 15
    Private culture As String = CultureInfo.CurrentCulture.EnglishName
    Private country As String = culture.Substring(culture.IndexOf("("c) + 1, culture.LastIndexOf(")"c) - culture.IndexOf("("c) - 1)
    Private WithEvents c As New Client
    Dim host As String
    Dim port As Integer
    Dim connected As Boolean
    Private showedmain As Boolean = False
    Private MainAfterConnection As ChatForm
    Private smilecja As String = ""
    Private loggedin As Boolean = False
    Private username, password, imagestring As String
    Private smileindex As Integer = 0
    Delegate Sub Mainappd(ByVal data1 As String, ByVal data2 As String, ByVal data3 As String)
    Private Sub Mainappds(ByVal data1 As String, ByVal data2 As String, ByVal data3 As String)
        If loggedin = False Then
            MsgBox("Error : " & vbNewLine & "You Aren`t Logged in ", MsgBoxStyle.Critical)
            If showedmain = True Then
                showedmain = False
                Try
                    MainAfterConnection.Close()
                Catch ex As Exception
                    MainAfterConnection.Close()
                End Try
            Else
                showedmain = False
                Try
                    MainAfterConnection.Close()
                Catch ex As Exception
                    MainAfterConnection.Close()
                End Try
            End If
            Exit Sub
        Else
            If showedmain = True Then
                If MainAfterConnection.Focused = False Then
                    MainAfterConnection.TopMost = True
                    MainAfterConnection.BringToFront()
                    MainAfterConnection.Focus()

                End If
                If data1.Equals("onlineus") Then

                    Dim uxname As String = data2
                    Dim personaccountimage As String = data3
                    For Each control As Control In MainAfterConnection.ChatPanel.Controls
                        Try 'To Avoid Object Casting Exceptions for Other Controls
                            Dim pic As PictureBox = control
                            If pic.Name.Equals(uxname) Then
                                pic.Image = Base64ToImage(personaccountimage)
                            End If
                        Catch ex As Exception : End Try
                        Try 'To Avoid Object Casting Exceptions for Other Controls
                            Dim Tatx As TextBox = control
                            If Tatx.Name.Equals(TextBox1.Text) Then
                                Tatx.ForeColor = Color.Black
                                Tatx.BackColor = Color.DarkCyan
                            Else
                                Tatx.ForeColor = Color.Black
                                Tatx.BackColor = Color.Snow
                            End If
                        Catch ex As Exception

                        End Try

                    Next

                    Panel2.Enabled = False

                    Label5.Text = "Online Now"

                    MainAfterConnection.AddOnline(data2, data3)
                End If
                If data1.Equals("clre") Then

                    MainAfterConnection.Panel5.Controls.Clear()
                    MainAfterConnection.onlineBoxHight = 10
                End If
                '
            ElseIf showedmain = False Then

                If data1.Equals("onlineus") Then

                    Panel2.Enabled = False
                    Label5.Text = "Online Now"
                    MainAfterConnection.AddOnline(data2, data3)
                End If
                If data1.Equals("clre") Then
                    MainAfterConnection.Panel5.Controls.Clear()
                    MainAfterConnection.onlineBoxHight = 10
                End If

                MainAfterConnection = New ChatForm
                MainAfterConnection.setuname(username)
                MainAfterConnection.Setpass(password)
                MainAfterConnection.Show()

                showedmain = True

            End If

        End If
     
    End Sub
    Friend Function getuname() As String
        Return username
    End Function
    Private Function getfomtwitdh(ByVal s As String) As Integer
        Dim counterx As Integer = 0
        counterx = s.ToCharArray.Length
        If counterx > 0 Then
            Return ((counterx * 12))
        Else
            Return 200
        End If
    End Function

    Private Sub AddchatGUI(ByVal text As String, ByVal personaccountimage As String, ByVal uxname As String)
        Try
            If showedmain = True Then
                'Controls Adder
                'Add Account Image
                If MainAfterConnection.TopMost = True Then
                    MainAfterConnection.Focus()
                    MainAfterConnection.TopMost = False
                End If
                If text.Contains("Has Deleted File : ") Then

                    Dim TextBo As New TextBox
                    TextBo.Text = text
                    System.Threading.Thread.CurrentThread.Sleep(1)
                    TextBo.ReadOnly = True
                    TextBo.ForeColor = Color.Black
                    TextBo.BackColor = Color.Gray
                    TextBo.AutoSize = False
                    TextBo.Multiline = True '; 10pt
                    TextBo.Font = New Font("Tahoma", 15, FontStyle.Regular)
                     
                    TextBo.Size = New Size(getfomtwitdh(text), TextBo.Font.Height + 5)

                    TextBo.Location = New Point(5, BoxHight)
                    BoxHight += TextBo.Height + 15
                    MainAfterConnection.ChatPanel.Controls.Add(TextBo)
                    MainAfterConnection.ChatPanel.Update()
                    Try

                        Dim max As Integer = (MainAfterConnection.ChatPanel.Height - MainAfterConnection.outer.ClientSize.Height)
                        MainAfterConnection.ChatPanel.Top = -(max + 17)



                    Catch ex As Exception

                    End Try
                    Exit Sub
                End If

                If (text).Contains("Uploaded File : ") Then

                 

                    Dim p As New PictureBox
                    p.Image = Base64ToImage(personaccountimage)
                    p.SizeMode = PictureBoxSizeMode.StretchImage
                    p.Location = New Point(5, BoxHight)
                    p.Size = New Size(32, 30)
                    p.BorderStyle = BorderStyle.Fixed3D
                    p.Name = uxname
                    Dim LinkLabelfile As New LinkLabel
                    LinkLabelfile.Text = (text)
                    System.Threading.Thread.CurrentThread.Sleep(1)

                    LinkLabelfile.ForeColor = Color.Navy

                    LinkLabelfile.AutoSize = False

                    LinkLabelfile.Font = New Font("Tahoma", 15, FontStyle.Regular)

                    LinkLabelfile.Text.Replace(vbNewLine, "")
                    LinkLabelfile.Size = New Size(getfomtwitdh(text), LinkLabelfile.Font.Height + 5)

                    AddHandler LinkLabelfile.Click, AddressOf Link_OnClick
                    '
                    LinkLabelfile.Location = New Point(p.Width + 15, BoxHight)





                    Dim delbutt As New Button

                    delbutt.FlatStyle = FlatStyle.System
                    delbutt.BackColor = Color.Snow
                    delbutt.ForeColor = Color.Black
                    delbutt.Location = New Point(LinkLabelfile.Width + LinkLabelfile.Location.X + 10, BoxHight)
                    delbutt.Size = New Size(90, 30)
                    delbutt.Text = "Delete"
                    delbutt.Name = LinkLabelfile.Text.Replace("Uploaded File : ", "")

                    AddHandler delbutt.Click, AddressOf delbutt_OnClick

                    BoxHight += LinkLabelfile.Height + 15

                    MainAfterConnection.ChatPanel.Controls.Add(p)
                    MainAfterConnection.ChatPanel.Controls.Add(LinkLabelfile)
                    MainAfterConnection.ChatPanel.Controls.Add(delbutt)
                    MainAfterConnection.ChatPanel.Update()

                    




                    Try

                        Dim max As Integer = (MainAfterConnection.ChatPanel.Height - MainAfterConnection.outer.ClientSize.Height)
                        MainAfterConnection.ChatPanel.Top = -(max + 17)

                        Dim maxw As Integer = (MainAfterConnection.ChatPanel.Width - MainAfterConnection.outer.ClientSize.Width)
                        '  MainAfterConnection.ChatPanel.Left = -maxw



                    Catch ex As Exception

                    End Try



                    Exit Sub
                End If

                If text.Contains("Was Kicked By Server Admin [Autom") Then

                    Dim p As New PictureBox
                    p.Image = Base64ToImage(personaccountimage)
                    p.SizeMode = PictureBoxSizeMode.StretchImage
                    p.Location = New Point(5, BoxHight)
                    p.Size = New Size(32, 30)
                    p.BorderStyle = BorderStyle.Fixed3D
                    p.Name = uxname
                    'Add Text Message 
                    Dim TextBo As New TextBox
                    TextBo.Text = text
                    System.Threading.Thread.CurrentThread.Sleep(1)
                    TextBo.ReadOnly = True
                    TextBo.ForeColor = Color.Red
                    TextBo.BackColor = Color.Black
                    TextBo.AutoSize = False
                    TextBo.Multiline = True '; 10pt
                    TextBo.Font = New Font("Tahoma", 15, FontStyle.Regular)

                    TextBo.Text.Replace(vbNewLine, "")
                    TextBo.Size = New Size(getfomtwitdh(text), TextBo.Font.Height + 5)

                    TextBo.Location = New Point(p.Width + 15, BoxHight)
                    BoxHight += TextBo.Height + 15

                    MainAfterConnection.ChatPanel.Controls.Add(p)
                    MainAfterConnection.ChatPanel.Controls.Add(TextBo)
                    MainAfterConnection.ChatPanel.Update()
                    Try

                        Dim max As Integer = (MainAfterConnection.ChatPanel.Height - MainAfterConnection.outer.ClientSize.Height)
                        MainAfterConnection.ChatPanel.Top = -(max + 17)



                    Catch ex As Exception

                    End Try

                    Exit Sub
                End If

                If text.Contains("IAMTEXTYADSF") Then

                    Dim p As New PictureBox
                    p.Image = Base64ToImage(personaccountimage)
                    p.SizeMode = PictureBoxSizeMode.StretchImage
                    p.Location = New Point(5, BoxHight)
                    p.Size = New Size(32, 30)
                    p.BorderStyle = BorderStyle.Fixed3D
                    p.Name = uxname
                    'Add Text Message 
                    Dim TextBo As New TextBox
                    TextBo.ReadOnly = True
                    TextBo.Text = TextBo.Text.Replace(vbNewLine, "")
                    TextBo.ForeColor = Color.Black
                    TextBo.BackColor = Color.DodgerBlue
                    TextBo.Multiline = True '; 10pt
                    TextBo.Font = New Font("Tahoma", 15, FontStyle.Regular)
                    TextBo.Name = uxname

                    TextBo.Text = " " & (DecodeBase64(text.Replace("IAMTEXTYADSF", "")))

                    'TextBo.ScrollBars = RichTextBoxScrollBars.None
                    Dim smilescounter As Integer = 0
                    For ix As Integer = 0 To TextBo.Text.Length - 1
                        If TextBo.Text(ix).ToString.Equals(":") Then
                            If TextBo.Text(ix + 1).ToString.Equals("D") Then
                                smilescounter += 1
                            End If
                            If TextBo.Text(ix + 1).ToString.Equals(":") Then
                                smilescounter += 1
                            End If
                            If TextBo.Text(ix + 1).ToString.Equals(")") Then
                                smilescounter += 1
                            End If
                            If TextBo.Text(ix + 1).ToString.Equals("(") Then
                                smilescounter += 1
                            End If
                            If TextBo.Text(ix + 1).ToString.Equals("3") Then
                                smilescounter += 1
                            End If
                            If TextBo.Text(ix + 1).ToString.Equals("o") Then
                                smilescounter += 1
                            End If
                            If TextBo.Text(ix + 1).ToString.Equals("7") Then
                                smilescounter += 1
                            End If
                            If TextBo.Text(ix + 1).ToString.Equals("8") Then
                                smilescounter += 1
                            End If
                            If TextBo.Text(ix + 1).ToString.Equals("#") Then
                                smilescounter += 1
                            End If
                           

                        End If
                     
                    Next


RecheckLine:
                    If TextBo.Text.Contains(":D") Or TextBo.Text.Contains(":3") Or TextBo.Text.Contains(":)") Or TextBo.Text.Contains(":(") Or TextBo.Text.Contains(":8") Or TextBo.Text.Contains(":o") Or TextBo.Text.Contains(":7") Or TextBo.Text.Contains(":#") Then
                        Try


                            Dim te As String = TextBo.Text

                            For ec As Integer = 0 To TextBo.Text.Length - 1
                                If TextBo.Text(ec).ToString.Equals(":") Then
                                    smilecja = TextBo.Text(ec).ToString & TextBo.Text(ec + 1).ToString
                                    smileindex = ec
                                    Exit For
                                End If
                            Next
                            If smilecja.Equals("") = False Then

                                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                '@@@@@@@@@@@@@@@@@@@ getting X posion @@@@@@@@@@@@@@@@@@@@@@@@
                                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                Dim xpos As Integer = ReturnXpositionofSmile(te)
                                Dim EmojImage As Image = ReturnSmileImages(smilecja)
                                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                '@@@@@@@@@@@@@@@@@@@ Graphics Area @@@@@@@@@@@@@@@@@@@@@@@@@@@
                                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                

                                TextBo.TextAlign = HorizontalAlignment.Left
                                Dim pxsmile As New PictureBox

                                pxsmile.Image = ReturnSmileImages(smilecja)
                                pxsmile.SizeMode = PictureBoxSizeMode.Normal
                                pxsmile.Location = New Point(ReturnXpositionofSmile(TextBo.Text), 5) ' TextBo.GetPositionFromCharIndex(smileindex)
                                pxsmile.Size = ReturnSmileImages(smilecja).Size
                                pxsmile.BorderStyle = BorderStyle.None
                                pxsmile.Top = 5
                                pxsmile.Left -= 5
                                ' pxsmile.Left -= pxsmile.Width
                                TextBo.Controls.Add(pxsmile)
                                pxsmile.BringToFront()
                                Dim encx As Integer = TextBo.Text.Length - 1
                                Try

reline:                             Dim pas1 As String = TextBo.Text.Substring(0, smileindex - 1) & "     "
                                    Dim pas2 As String = TextBo.Text.Substring(smileindex + 2, encx)
                                    TextBo.Text = pas1 & pas2
                                Catch ex As Exception
                                    encx -= 1
                                    GoTo reline
                                End Try






                                '  TextBo.Text = TextBo.Text.Remove(smileindex, 2)


                                '& TextBo.Text.Substring(smileindex + 2, TextBo.Text.Length - 1)





                                If smilescounter > 0 Then
                                    GoTo RecheckLine

                                    smilescounter -= 1
                                End If



                                'Dim g As Graphics = TextBo.CreateGraphics
                                ' g.DrawImage(EmojImage, 0, 0, 30, 30)
                                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                                '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                            Else
                                Exit Try
                            End If


                        Catch ex As Exception
                            MsgBox("Ex : " & ex.Message)


                        End Try
                    End If
 

                    TextBo.Size = New Size(getfomtwitdh(text), TextBo.Font.Height + 5)
                    TextBo.Location = New Point(p.Width + 15, BoxHight)
                   
                    BoxHight += TextBo.Height + 15
                    MainAfterConnection.ChatPanel.Controls.Add(p)
                    MainAfterConnection.ChatPanel.Controls.Add(TextBo)

                   

                    MainAfterConnection.ChatPanel.Update()
                    Try

                        Dim max As Integer = (MainAfterConnection.ChatPanel.Height - MainAfterConnection.outer.ClientSize.Height)
                        MainAfterConnection.ChatPanel.Top = -(max + 17)



                    Catch ex As Exception

                    End Try

                    Exit Sub
                ElseIf text.Contains("IAMIMAGENOTTEXT") Then
                    Dim p As New PictureBox
                    p.Image = Base64ToImage(personaccountimage)
                    p.SizeMode = PictureBoxSizeMode.StretchImage
                    p.Location = New Point(5, BoxHight)
                    p.Size = New Size(32, 30)
                    p.Name = uxname
                    'Add Text Message 
                    Dim PictureBox1x As New PictureBox
                    PictureBox1x.Image = Base64ToImage(DecodeBase64(text.Replace("IAMIMAGENOTTEXT", "")))
                    PictureBox1x.BorderStyle = BorderStyle.Fixed3D
                    PictureBox1x.Cursor = Cursors.Hand
                    System.Threading.Thread.CurrentThread.Sleep(1)
                    '  TextBo.ReadOnly = True
                    PictureBox1x.ForeColor = Color.Black
                    PictureBox1x.BackColor = Color.Snow
                    PictureBox1x.SizeMode = PictureBoxSizeMode.StretchImage
                    '  TextBo.Multiline = True '; 10pt
                    ' TextBo.Font = New Font("Tahoma", 15, FontStyle.Regular)
                    AddHandler PictureBox1x.Click, AddressOf Pic_OnClick
                    'Link_OnClick
                    PictureBox1x.Text.Replace(vbNewLine, "")
                    PictureBox1x.Size = New Size(200, 150)

                    PictureBox1x.Location = New Point(p.Width + 15, BoxHight)
                    BoxHight += PictureBox1x.Height + 15

                    MainAfterConnection.ChatPanel.Controls.Add(p)
                    MainAfterConnection.ChatPanel.Controls.Add(PictureBox1x)
                    MainAfterConnection.ChatPanel.Update()
                    Try

                        Dim max As Integer = (MainAfterConnection.ChatPanel.Height - MainAfterConnection.outer.ClientSize.Height)
                        MainAfterConnection.ChatPanel.Top = -(max + 17)



                    Catch ex As Exception

                    End Try
                    Exit Sub
                End If


            End If
            If MainAfterConnection.WindowState = FormWindowState.Minimized Then
                MainAfterConnection.WindowState = FormWindowState.Normal
                MainAfterConnection.TopMost = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
   
    Public Function getImage(ByVal st As String) As Image
        Select Case st
            Case ":@"
                Return ImageList1.Images(0)
            Case ":?"
                Return ImageList1.Images(1)
            Case ":!"
                Return ImageList1.Images(2)
            Case ":D"
                Return ImageList1.Images(3)
            Case ":)"
                Return ImageList1.Images(4)
            Case ":("
                Return ImageList1.Images(5)
            Case ":p"
                Return ImageList1.Images(6)
            Case Else
                Return Nothing
        End Select
    End Function
    Private Function ReturnSmileImages(ByVal S As String) As Image

        If S.Equals(":D") Then
            Return p6.Image
        ElseIf S.Equals(":)") Then
            Return p12.Image
        ElseIf S.Equals(":(") Then
            Return p9.Image
        ElseIf S.Equals(":P") Then
            Return p2.Image
        ElseIf S.Equals(":3") Then
            Return p1.Image
        ElseIf S.Equals(":o") Then
            Return p13.Image
        ElseIf S.Equals(":7") Then
            Return p4.Image
        ElseIf S.Equals(":8") Then
            Return p1.Image
        ElseIf S.Equals(":#") Then
            Return p14.Image
        ElseIf S.Equals("XD") Then
            Return p5.Image
        Else
            Return Nothing
        End If
    End Function
    Private Function ReturnXpositionofSmile(ByVal s As String) As Integer
        Dim cwidth As Integer = -1

        On Error Resume Next
        For ic As Integer = 0 To s.Length - 1
            If s(ic).ToString.Equals(":") Then
                Dim Sx As String = (s(ic) & s(ic + 1))
                If ReturnSmileImages(Sx) IsNot Nothing Then
                    smilecja = Sx
                    Return cwidth
                Else
                    Return cwidth - 2
                End If
            End If
            If s(ic).ToString.Equals("X") Then
                If s(ic + 1).ToString.Equals("D") Then
                    Dim Sx As String = (s(ic) & s(ic + 1))
                    If ReturnSmileImages(Sx) IsNot Nothing Then
                        smilecja = Sx
                        Return cwidth
                    Else
                        Return cwidth - 2
                    End If
                End If
               
            End If
            cwidth += 10
        Next



        Return cwidth + 1
    End Function
    Private Sub Pic_OnClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Form2.PictureBox1.Image = sender.Image
            Form2.Show()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub delbutt_OnClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim filename As String = sender.Name


            '"ineedfilex"
            Dim stringtobeencrypted As String = "deletx" & yy & filename & yy & TextBox1.Text
            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

            c.Send(sstring)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Link_OnClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim filename As String = sender.Text
            filename = filename.Replace("Uploaded File : ", "")

            '"ineedfilex"
            Dim stringtobeencrypted As String = "ineedfilex" & yy & filename
            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

            c.Send(sstring)
        Catch ex As Exception

        End Try
    End Sub '
    Private Sub Addotherbox(ByVal s As String)
        Dim mybox As New TextBox
        mybox.Text = s
        mybox.Update()
        mybox.Font = MainAfterConnection.TextBox1.Font
        mybox.ForeColor = Color.Black
        mybox.BackColor = Color.Snow
        ' mybox.AutoSize = True
        mybox.Size = New Size(getfomtwitdh(s), mybox.Font.Height + 2)
        ' mybox.Name = "him"
        BoxHight += mybox.Height + 5
        mybox.Location = New Point(2, BoxHight)
        mybox.ReadOnly = True
        mybox.BorderStyle = BorderStyle.None
        mybox.Update()
        MainAfterConnection.ChatPanel.Controls.Add(mybox)
        MainAfterConnection.ChatPanel.Update()
        Try

            Dim max As Integer = MainAfterConnection.ChatPanel.Height - MainAfterConnection.outer.ClientSize.Height
            MainAfterConnection.ChatPanel.Top = -max
        Catch ex As Exception

        End Try
    End Sub

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
            AES.Mode = Security.Cryptography.CipherMode.ECB
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
            AES.Mode = Security.Cryptography.CipherMode.ECB
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
    Private Function Base64ToImage(ByVal base64String As String) As Image
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

    Public Function DecodeBase64(input As String) As String
        Return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input))
    End Function
    Private Sub Tryer_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Tryer.Tick
        On Error Resume Next
        '  copytostartup()

    End Sub
    Private Sub connect()

        If connected = False Then
            c.Connect(host, port)
        End If

    End Sub

    Private Sub dsiconnected() Handles c.Disconnected
        Try
            connected = False
            Try
                Panel2.Visible = False
                Panel1.Visible = True
                Panel2.Enabled = False
                Panel1.Enabled = True
                Panel1.BringToFront()
                If showedmain = True Then

                    MainAfterConnection.counterpeople = 0

                    showedmain = False
                    loggedin = False
                End If
            Catch ex As Exception

            End Try
            Label6.ForeColor = Color.Red
            Label6.Text = "Not Connected"
            Try
                If connected = False Then
                    c.Connect(host, port)
                End If
            Catch ex As Exception
                dsiconnected()
            End Try
        Catch ex As Exception

        End Try
   
    End Sub
    Friend Sub SendVaar(ByVal s As String, ByVal images As String, ByVal uname As String)
        Try
            Dim stringtobeencrypted As String = "msgte" & yy & s & yy & uname & yy & images
            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

            c.Send(sstring)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Friend Sub Sendfilex(ByVal s As String, ByVal images As String, ByVal filename As String, ByVal filestring As String)
        Try
            Dim stringtobeencrypted As String = "iuploadedafildsae" & yy & s & yy & images & yy & filename & yy & filestring
            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

            c.Send(sstring)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Friend Sub sencst(ByVal s As String)
        Try
            Dim stringtobeencrypted As String = s
            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

            c.Send(sstring)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub connectedx() Handles c.Connected
        connected = True
        Try
            Label6.ForeColor = Color.Lime
            Label6.Text = "Connected -Online"
            System.Threading.Thread.CurrentThread.Sleep(5)
            Panel2.Visible = True
            Panel1.Visible = False
            Panel1.Enabled = False
            Panel2.Enabled = True
            Panel2.BringToFront()
            Panel2.Location = Panel1.Location

        Catch ex As Exception

        End Try
    End Sub
    Private Sub data(ByVal b As Byte()) Handles c.Data

        Dim encstring As String = BS(b)

        Dim decString As String = Decrypt(DecodeBase64(encstring), encpassstring)

        Dim a As String() = Split(decString, yy)

        Try
            Select Case a(0)
                Case "loginxas"
                    System.Threading.Thread.CurrentThread.Sleep(5)
                    encpass.Enabled = False
                    Dim stringtobeencrypted As String = "loginxas" & yy & My.Computer.Name
                    Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                    c.Send(sstring)
                Case "wpas"
                    MsgBox("Error : " & vbNewLine & "Wrong Password Was Entered ", MsgBoxStyle.Critical)
                    MsgBox("Exiting !!!", MsgBoxStyle.Exclamation)
                    End
                Case "wuse"
                    MsgBox("Error : " & vbNewLine & "Your user have been Deleted or not Signed to the Server ", MsgBoxStyle.Critical)
                    MsgBox("Exiting !!!", MsgBoxStyle.Exclamation)
                    End
                Case "Suslog"
                    loggedin = True
                    Dim stringtobeencrypted As String = "myimage" & yy & username & yy & password & yy & ImageToBase64(ChatForm.PictureBox1.Image, Imaging.ImageFormat.Png)
                    Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))

                    c.Send(sstring)
                    Invoke(New Mainappd(AddressOf Mainappds), "onlinep", "", "")

                Case "onlineux"

                    Dim nax As String = a(1)
                    Dim imgx As String = a(2)

                    Invoke(New Mainappd(AddressOf Mainappds), "onlineus", nax, imgx)


                Case "clre"
                    Invoke(New Mainappd(AddressOf Mainappds), "clre", "", "")
                Case "msgte"
                 
                    Dim pmsg As String = a(1)
                    Dim uxname As String = a(2)
                    Dim pimagetext As String = a(3)

                    If a(1).Contains("Uploaded File : ") Then
                        'A(1) is string , A(2) is account name , a(3) is account image

                        Invoke(New chatappd(AddressOf chatappds), "msgup", pmsg.Replace(uxname, ""), pimagetext, uxname)

                    Else

                        Invoke(New chatappd(AddressOf chatappds), "msg", pmsg, pimagetext, uxname)

                    End If

                Case "perdelfile"
                    Dim deluser As String = a(1)

                    Invoke(New chatappd(AddressOf chatappds), "perdelfile", "User : " & deluser & " Has Deleted File : " & a(2) & " From the Server ", "", deluser)

                Case "writeneededfile"
                    IO.File.WriteAllBytes(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & a(1), Convert.FromBase64String(a(2)))
                    Threading.Thread.CurrentThread.Sleep(1000)
                    MsgBox("File : " & a(1) & " Was Downloaded to " & vbNewLine & Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\" & a(1), MsgBoxStyle.Information)

                Case "fileerror"
                    MsgBox("File : " & a(1) & " Was Removed By Any Remover or Deleted By Server Admin", MsgBoxStyle.Critical)

                Case "kickurass"
                    MsgBox("You Have Been Kicked By Server Admin Because of Violating Rules or Disconnected for Time Delay [Inactivity]", MsgBoxStyle.Exclamation)
                    Try
                        c.DisConnect()
                        End
                    Catch ex As Exception
                        End
                    End Try


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
            
        End Try
    End Sub

    Delegate Sub chatappd(ByVal data1 As String, ByVal data2 As String, ByVal data3 As String, ByVal data4 As String)
    Private Sub chatappds(ByVal data1 As String, ByVal data2 As String, ByVal data3 As String, ByVal data4 As String)
        If showedmain = True Then
            If data1.Equals("msg") Then
             
                AddchatGUI(data2, data3, data4)
            ElseIf data1.Equals("msgup") Then
                AddchatGUI(data2, data3, data4)
            ElseIf data1.Equals("perdelfile") Then

                AddchatGUI(data2, "", data4)
            End If


        Else

            MainAfterConnection = New ChatForm
            MainAfterConnection.setuname(username)
            MainAfterConnection.Setpass(password)
            MainAfterConnection.Show()

            showedmain = True
        End If
    End Sub

    Private Function SB(ByVal s As String) As Byte()
        Return System.Text.Encoding.Default.GetBytes(s)
    End Function
    Private Function BS(ByVal b As Byte()) As String
        Return System.Text.Encoding.Default.GetString(b)
    End Function
    Private Function ENB(ByRef s As String) As String
        Dim byt As Byte() = System.Text.Encoding.UTF8.GetBytes(s)
        ENB = Convert.ToBase64String(byt)
    End Function
    Private Function DEB(ByRef s As String) As String
        Dim b As Byte() = Convert.FromBase64String(s)
        DEB = System.Text.Encoding.UTF8.GetString(b)
    End Function

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        If encpass.Text.Length > 8 Then
            encpassstring = encpass.Text
            host = TextBox5.Text
            Try
                port = Integer.Parse(TextBox4.Text)

            Catch ex As Exception
                TextBox4.Text = "1177"
                MsgBox("Enter Valid And Usable Port Number ", MsgBoxStyle.Critical)

            End Try
            If connected = False Then
                Try
                    c.Connect(host, port)
                Catch ex As Exception
                    Label6.ForeColor = Color.Red
                    Label6.Text = "Not Connected"
                End Try

            End If


        ElseIf encpass.Text.Length < 8 Then
            MsgBox("Error : " & vbNewLine & "Cannot connect without Encryption Key less than 8 Characters", MsgBoxStyle.Exclamation)
            encpass.Text = "MAsd630fg3#L$asjnd"
        End If
      
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.UseSystemPasswordChar = False
            TextBox2.PasswordChar = ""
        Else
            TextBox2.UseSystemPasswordChar = True
            TextBox2.PasswordChar = TextBox3.Text
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        'send login 
        Dim username As String = TextBox1.Text
        Dim password As String = TextBox2.Text


        Try
            Dim stringtobeencrypted As String = "auth" & yy & username & yy & password
            Dim sstring As String = EncodeBase64(Encrypt(stringtobeencrypted, encpassstring))
            c.Send(sstring)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub encpass_TextChanged(sender As System.Object, e As System.EventArgs) Handles encpass.TextChanged

    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Try
            End
        Catch ex As Exception
            Me.Close()
            End
        End Try
    End Sub
End Class