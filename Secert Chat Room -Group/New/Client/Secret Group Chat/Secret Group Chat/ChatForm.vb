Imports System.IO

Public Class ChatForm
    Private yy As String = "||"
    Private username, password, imagestring As String
    Friend onlineBoxHight As Integer = 10
    Dim imgl As New ImageList
    Friend Sub Refreshme()
        If Me.Focused = False Then
            Me.BringToFront()
            Me.Focus()
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub
    Private Sub InnerPanel_SizeChanged(sender As Object, e As EventArgs) Handles ChatPanel.SizeChanged

        Dim max As Integer = ChatPanel.Height - outer.ClientSize.Height
        Dim maxw As Integer = (ChatPanel.Width - outer.ClientSize.Width)
      
        If max > 0 Then

            VScrollBar1.Maximum = max + SystemInformation.VerticalScrollBarArrowHeight + 17
            '    VScrollBar1.Value
        End If
        If maxw > 0 Then
            HScrollBar1.Maximum = maxw + SystemInformation.HorizontalScrollBarArrowWidth
        End If
    End Sub
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '   Button6.PerformClick()
        ChatPanel.AutoSize = True 'set the InnerPanel`s AutoSize to True so that it will resize itself when controls are added to it

        ChatPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink 'set its AutoSizeMode so that it will auto size itself bigger or smaller when controls are added or removed
        Timer1.Start()
    End Sub
    Private Sub VScrollBar1_ValueChanged(sender As Object, e As EventArgs) Handles VScrollBar1.ValueChanged

        ChatPanel.Top = -VScrollBar1.Value



    End Sub
    Private Sub HScrollBar1_ValueChanged(sender As Object, e As EventArgs) Handles HScrollBar1.ValueChanged

        ChatPanel.Left = -HScrollBar1.Value



    End Sub
    'Dim max As Integer = (MainAfterConnection.ChatPanel.Height - MainAfterConnection.outer.ClientSize.Height)
    '                    MainAfterConnection.ChatPanel.Top = -max
   
    Friend Sub setuname(ByVal u As String)

        Me.username = u
    End Sub
    Friend Sub Setpass(ByVal p As String)
        Me.password = p
    End Sub
    Private Function Base64ToImage(ByVal base64String As String) As Image
        ' Convert Base64 String to byte[]
        Dim imageBytes As Byte() = Convert.FromBase64String(base64String)
        Dim ms As New MemoryStream(imageBytes, 0, imageBytes.Length)

        ' Convert byte[] to Image
        ms.Write(imageBytes, 0, imageBytes.Length)
        Dim ConvertedBase64Image As Image = Image.FromStream(ms, True)
        Return ConvertedBase64Image
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
    Public Function EncodeBase64(input As String) As String
        Return System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input))
    End Function

    Public Function DecodeBase64(input As String) As String
        Return System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(input))
    End Function
    Friend counterpeople As Integer = 0
    Friend Function AddOnline(ByVal namex As String, ByVal imagestringx As String)
        Try
            For Each Controlx As Control In Panel5.Controls
                Try
                    If Controlx.Text.Equals(namex) Then
                        Exit Function
                    End If
                Catch ex As Exception

                End Try
            Next
            Dim imagex As Image = Base64ToImage(imagestringx)
            Dim pictureb As New PictureBox
            pictureb.Image = imagex
            pictureb.SizeMode = PictureBoxSizeMode.StretchImage
            pictureb.Location = New Point(5, onlineBoxHight)
            pictureb.Size = New Size(32, 30)

            Dim oninepersonlabel As New Label
            oninepersonlabel.Text = (namex)
            System.Threading.Thread.CurrentThread.Sleep(1)

            oninepersonlabel.ForeColor = Color.Navy

            oninepersonlabel.AutoSize = False

            oninepersonlabel.Font = New Font("Tahoma", 15, FontStyle.Regular)

            oninepersonlabel.Text.Replace(vbNewLine, "")
            oninepersonlabel.Size = New Size(getfomtwitdh(Text), oninepersonlabel.Font.Height)
            oninepersonlabel.Location = New Point(pictureb.Width + 5, onlineBoxHight)

            Panel5.Controls.Add(pictureb)
            Panel5.Controls.Add(oninepersonlabel)

            onlineBoxHight += oninepersonlabel.Height + 5
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
    Private Function getfomtwitdh(ByVal s As String) As Integer
        Dim counterx As Integer = 0
        counterx = s.ToCharArray.Length
        If counterx > 0 Then
            Return ((counterx * 10))
        Else
            Return 200
        End If
    End Function
    Private Sub RichTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter Then
            If TextBox1.Text.Length > 0 Then
                Dim fx As String = ""
                Dim newfiltered As String = ""
                For ix As Integer = 0 To TextBox1.Lines.Length - 1
                    If TextBox1.Lines(ix).ToString.Equals("") = False Then
                        fx &= TextBox1.Lines(ix)
                    End If
                    If ix = TextBox1.Lines.Length - 1 Then
                        Exit For
                    End If
                Next
                For ixx As Integer = 0 To fx.Length - 1
                    If fx(ixx).ToString.Equals("") = False Then
                        newfiltered &= fx(ixx)
                    End If
                Next

                Form1.SendVaar("IAMTEXTYADSF" & EncodeBase64(newfiltered), ImageToBase64(PictureBox1.Image, Imaging.ImageFormat.Png), (Form1.TextBox1.Text))
                Form1.sencst("rf")
                ' RichTextBox1.Text &= vbNewLine & Form1.gets1 & " : " & RichTextBox2.Text & vbNewLine
                ' Addmybox(RichTextBox2.Text)
                ' RichTextBox2.Text.Replace(vbNewLine, "")
                TextBox1.Clear()
            End If



        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        If OpenFileDialog1.FileName.EndsWith(".png") Then
            Try
                PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)

                System.Threading.Thread.CurrentThread.Sleep(5)



                Form1.sencst("myimage" & yy & username & yy & password & yy & ImageToBase64(PictureBox1.Image, Imaging.ImageFormat.Png))
                Form1.sencst("rf")

            Catch ex As Exception
                MsgBox("Error : Cannot Change Icon", MsgBoxStyle.Critical)

            End Try
        Else
            MsgBox("Error : Only .png Files", MsgBoxStyle.Critical)

        End If
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        '"rf"
        Form1.sencst("rf")
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        'upload image 
        OpenFileDialog2.ShowDialog()

    End Sub

    Private Sub OpenFileDialog2_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog2.FileOk
        If OpenFileDialog2.FileName.EndsWith(".png") Then
            Try
                Dim imagestrxxxing As String = ImageToBase64(Image.FromFile(OpenFileDialog2.FileName), Imaging.ImageFormat.Png)
                Form1.SendVaar("IAMIMAGENOTTEXT" & EncodeBase64(imagestrxxxing), ImageToBase64(PictureBox1.Image, Imaging.ImageFormat.Png), (Form1.TextBox1.Text))
                Form1.sencst("rf")

            Catch ex As Exception
                MsgBox("Error : Cannot upload image", MsgBoxStyle.Critical)

            End Try
        Else
            MsgBox("Error : Only .png Files", MsgBoxStyle.Critical)

        End If
    End Sub

   
    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        Dim o As New OpenFileDialog
        o.ShowDialog()
        Dim n As New IO.FileInfo(o.FileName)
        If o.FileName.Length > 0 Then
            If n.Length > 5000000 Then
                MsgBox("Maximum Size allowed is 5 MB size", MsgBoxStyle.Critical)
                Exit Sub
            Else
                Dim filesstring As String = Convert.ToBase64String(IO.File.ReadAllBytes(o.FileName))
                '  Form1.sencst("iuploadedafildsae" & yy & filesstring)
                Form1.Sendfilex(Form1.TextBox1.Text & "Uploaded File : " & n.Name, ImageToBase64(PictureBox1.Image, Imaging.ImageFormat.Png), n.Name, filesstring)
                Form1.sencst("rf")
            End If

                 End If
    End Sub

  
    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        Try
            Me.BringToFront()
            Me.Focus()
            Me.TopMost = True
            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Normal
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ChatForm_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
      
        If e.CloseReason = CloseReason.MdiFormClosing Then
            End
        End If
        If e.CloseReason = CloseReason.UserClosing Then
            End
        End If
    End Sub
End Class