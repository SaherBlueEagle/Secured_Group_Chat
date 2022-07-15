Public Class emojform
    Dim coun As Integer = 0
    Private Sub emojform_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        coun += 1
        Try
            p1.Image = ImageList1.Images(coun)
        Catch ex As Exception
            coun = ImageList1.Images.Count - 1
        End Try

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)
        coun -= 1
        Try
            p1.Image = ImageList1.Images(coun)
        Catch ex As Exception
            coun = 0
        End Try

    End Sub
End Class