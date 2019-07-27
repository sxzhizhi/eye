
Partial Class Frm_XXGL
    Inherits System.Web.UI.Page

    Private Sub Frm_XXGL_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Session("userid") = "" Then Response.Redirect("Login.aspx")
        Session("menu") = "信息录入"
        Session("onelevel") = "1"
        Session("twolevel") = "1.1"
    End Sub
End Class
