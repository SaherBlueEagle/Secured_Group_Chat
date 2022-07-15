Imports System.IO

Public Class Form2
    Private username, password, imagestring As String
    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Form2_Resize(sender As System.Object, e As System.EventArgs) Handles MyBase.Resize
        Me.Size = New Size(268, 200)
    End Sub

    Private Sub Form2_SizeChanged(sender As System.Object, e As System.EventArgs) Handles MyBase.SizeChanged
        Me.Size = New Size(268, 200)
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim usernamex As String = Label1.Text.Replace("Username : ", "")
        Dim userx As User = returnuser(usernamex)

        Dim passx As String = InputBox("New Password : ", "Password entry", userx.Getpassword)
        Try
            If passx.Length > 0 Then
                userx.setpass(passx, "GFPT#(*$JM<FDWQF()#MG(P$#MG$#!(|GKM$#GF$#")
                Label2.Text = "Password : " & userx.Getpassword
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Function returnuser(ByVal namxe As String) As User
        For Each uxx As User In Form1.getulist
            If uxx.GetUsername.Equals(namxe) Then
                Return uxx
            End If
        Next
        Return Nothing
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
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim o As New OpenFileDialog
        o.ShowDialog()
        Dim n As New IO.FileInfo(o.FileName)
        Dim usernamex As String = Label1.Text.Replace("Username : ", "")
        Dim userx As User = returnuser(usernamex)

        If o.FileName.Length > 0 Then
            If o.FileName.EndsWith(".png") Then
                PictureBox1.Image = Image.FromFile(o.FileName)
                Dim imgstring As String = ImageToBase64(PictureBox1.Image, Imaging.ImageFormat.Png)
                userx.setimgx(imagestring)
            Else
                MsgBox("only .png Files are allowed", MsgBoxStyle.Critical)
            End If


        End If

    End Sub
End Class