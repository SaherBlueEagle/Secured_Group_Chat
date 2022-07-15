Public Class User
    Private Username, Password, imagestring As String
   
    Friend Sub New(ByVal username As String, ByVal password As String, ByVal px As String)
        Me.Username = username
        Me.Password = password
        Me.imagestring = px
    End Sub
    Friend Function Getpassword() As String
        Dim tempvalue As String = Password
        Return tempvalue
    End Function
 
    Friend Function GetUsername() As String
        Return Me.Username
    End Function
    Friend Function Getimagestring() As String
        Return imagestring
    End Function
    Friend Sub setimgx(ByVal s As String)
        Me.imagestring = s

    End Sub
    Friend Sub setpass(ByVal s As String, ByVal methodpassword As String)
        If methodpassword.Equals("GFPT#(*$JM<FDWQF()#MG(P$#MG$#!(|GKM$#GF$#") Then
            Password = s
        End If
    End Sub
End Class
