
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Private Sub ContentPlaceHolder1_Load(sender As Object, e As EventArgs) Handles ContentPlaceHolder1.Load
        Dim rs As New ADODB.Recordset

        rs = cPConn.Execute("Select count(*) as a from HZXXBView " & Session("sWhere"))
        Session("count") = rs.Fields("a").Value
    End Sub
End Class

