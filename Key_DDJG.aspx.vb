
Partial Class Key_DDJG
    Inherits System.Web.UI.Page

    Private Sub NDZD_Load(sender As Object, e As EventArgs) Handles Me.Load
        '判断是否正确登录，每个页面都要有
        Dim rs As ADODB.Recordset
        If Session("userid") = "" Then Response.Redirect("Login.aspx")

        Session("menu") = "定点机构信息"
        Session("onelevel") = "5"
        Session("twolevel") = "5.2"

        Me.SqlDataSource1.SelectCommand = "SELECT * FROM [DDJGBView]"
        If IsPostBack = False Then
            Call bind()
        End If

        '检测表内是否有信息
        rs = cPConn.Execute("select * from [DDJGBView]")
        If rs.RecordCount <> 0 Then
            Session("DataList") = "1"
        Else
            Session("DataList") = "0"
        End If

    End Sub

    Private Sub bind()
        On Error Resume Next


        Me.SqlDataSource1.SelectCommand = "SELECT * FROM [DDJGBView]"
        GridView1.DataBind()
        Me.ddlCurrentPage.Items.Clear()
        For i = 1 To Me.GridView1.PageCount
            Me.ddlCurrentPage.Items.Add(i.ToString())
        Next
        ddlCurrentPage.SelectedIndex = Me.GridView1.PageIndex
        Me.All_CheckBox.Checked = False




    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function del(ByVal k1 As String) As String
        Dim arr = Split(k1, ",")


        If arr.Length = 1 Then
            Return "请选择要删除的数据!"
        Else
            Dim rs As New ADODB.Recordset
            For i = 0 To arr.Length - 2

                rs = cPConn.Execute("Select * from dbUser where cCode='" & arr(i) & "'")

                If rs.RecordCount = 0 Then
                    cPConn.Execute("delete from DDJGB where Code='" & arr(i) & "'")

                    '
                    cPConn.Execute("insert into WaterLog(cUser,cAction,cRemark)values('" & HttpContext.Current.Session("userid") & "','删除定点机构数据!','" & arr(i) & "')")

                Else
                    Return "定点机构账号已使用不能删除，请在用户管理中处理后再试！"
                    Exit Function
                End If


            Next
            Return "成功删除" + (arr.Length - 1).ToString + "条数据!"
        End If
    End Function
    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        lblCurrentPage.Text = String.Format("当前第{0}页/总共{1}页", GridView1.PageIndex + 1, GridView1.PageCount)


        '指定给某一列的每一行增加一个js事件，这样前端就不能调动js了，主要考虑到需要带入参数
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cCode As String = e.Row.Cells(1).Text
            Dim cEdit As String


            cEdit = "javascript:WinOpen('" & cCode & "');"

            e.Row.Cells(5).Attributes.Add("onclick", cEdit)


        End If
    End Sub

    Protected Sub lnkbtnFrist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFrist.Click
        GridView1.PageIndex = 0
        bind()
    End Sub

    Protected Sub lnkbtnPre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnPre.Click
        If Me.GridView1.PageIndex > 0 Then
            Me.GridView1.PageIndex = Me.GridView1.PageIndex - 1
            bind()
        End If
    End Sub

    Protected Sub lnkbtnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnNext.Click
        If Me.GridView1.PageIndex < Me.GridView1.PageCount Then
            Me.GridView1.PageIndex = Me.GridView1.PageIndex + 1
            bind()
        End If
    End Sub

    Protected Sub lnkbtnLast_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLast.Click
        GridView1.PageIndex = GridView1.PageCount
        bind()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCurrentPage.SelectedIndexChanged

        Dim i As Integer

        i = Me.ddlCurrentPage.SelectedIndex
        Me.GridView1.PageIndex = Me.ddlCurrentPage.SelectedIndex
        bind()

    End Sub

    Protected Sub but_update_Click(ByVal sender As Object, ByVal e As EventArgs) Handles but_update.Click
        bind()
    End Sub
    Protected Sub delete_update_Click(sender As Object, e As EventArgs) Handles delete_update.Click
        GridView1.DataBind()
    End Sub


    Protected Sub All_CheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles All_CheckBox.CheckedChanged
        Dim i As Integer
        For i = 0 To GridView1.Rows.Count - 1

            Dim cbox As CheckBox = Me.GridView1.Rows(i).FindControl("CheckBox1")
            If Me.All_CheckBox.Checked = True Then
                cbox.Checked = True
            Else
                cbox.Checked = False
            End If
        Next
    End Sub


    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        '表格中的Button执行完js后，判断是那个Button然后执行下面的事件
        If e.CommandName = "btn_modify" Then
            Me.GridView1.DataBind()
            'bind()
        Else

        End If
    End Sub


End Class
