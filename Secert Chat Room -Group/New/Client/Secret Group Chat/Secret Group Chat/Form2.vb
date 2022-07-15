Public Class Form2

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Try
            SaveFileDialog1.ShowDialog()
            SaveFileDialog1.Title = "Save Image As "

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SaveFileDialog1_FileOk(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Try
            PictureBox1.Image.Save(SaveFileDialog1.FileName)
        Catch ex As Exception

        End Try
    End Sub
End Class