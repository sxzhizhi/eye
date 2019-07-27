
Partial Class _Default
    Inherits System.Web.UI.Page

    Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") = "" Then Response.Redirect("Login.aspx")
        Session("menu") = "主页"
        Session("onelevel") = "0"
        Session("twolevel") = "0"


    End Sub
End Class
