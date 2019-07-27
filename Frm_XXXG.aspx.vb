
Partial Class Frm_XXXG
    Inherits System.Web.UI.Page
    Dim sQuerys As String
    Private Sub Frm_XXGL_Load(sender As Object, e As EventArgs) Handles Me.Load
        '判断是否正确登录，每个页面都要有
        Dim rs As ADODB.Recordset
        If Session("userid") = "" Then Response.Redirect("Login.aspx")

        Session("menu") = "信息录入"
        Session("onelevel") = "1"
        Session("twolevel") = "1.2"

        Dim sQuery As String = ""
        'Dim sND As String
        'Dim sDDJG As String
        'Dim sQuery As String


        If IsPostBack = False Then




            rs = cPConn.Execute("Select * from DicND order by zNDName desc")

            If rs.RecordCount <> 0 Then

                Me.cbo_ND.Items.Add("请选择")

                For i = 0 To rs.RecordCount - 1

                    Me.cbo_ND.Items.Add(rs.Fields("zNDName").Value)
                    rs.MoveNext()

                Next
                Me.cbo_ND.SelectedIndex = 1

                Session("sQuerys") = " zNDName='" & cbo_ND.Text & "'"

            End If

            rs = cPConn.Execute("Select * from DDJGBView " & Session("sWhere"))

            If rs.RecordCount <> 0 Then
                'If Session("sWhere") = "" Then
                '    Me.cbo_DDJG.Items.Add("请选择")
                'End If

                For i = 0 To rs.RecordCount - 1

                    Me.cbo_DDJG.Items.Add(rs.Fields("DDJGMC").Value)
                    rs.MoveNext()

                Next

                Session("sQuerys") = IIf(Session("sQuerys") = "", " DDJGMC='" & cbo_DDJG.Text & "'", " zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "'")
            End If

            cbo_NR.Items.Add("请选择")
            cbo_NR.Items.Add("编号")
            cbo_NR.Items.Add("姓名")
            cbo_NR.Items.Add("性别")
            cbo_NR.Items.Add("手术眼")
            cbo_NR.Items.Add("身份证号")





            Call bind()

            'sND = cbo_ND.Text
            'sDDJG = cbo_DDJG.Text

            'sQuerys = " zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "' " & IIf(sQuery = "", "", sQuery)
        End If



        '因为第一次打开的时，要展示当前条件的数据，所以才把Session("sQuerys") = ""放在了查询功能 下

        If Session("sWhere") = "" Then
            Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " where " & Session("sQuerys")) & " order by zNDName,BH desc "
        Else
            Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " and " & Session("sQuerys")) & " order by zNDName,BH desc "
        End If

        'Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where " & sQuerys, " and " & sQuerys) & " order by BH asc "


        'GridView1.DataBind()



        '检测表内是否有信息
        'rs = cPConn.Execute("Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " where " & Session("sQuerys") & " "))
        If Session("sWhere") = "" Then
            rs = cPConn.Execute("Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " where " & Session("sQuerys"))）
        Else
            rs = cPConn.Execute("Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " and " & Session("sQuerys"))）
        End If

        If rs.RecordCount <> 0 Then
            Session("DataList") = "1"
            Session("DataNumber") = rs.RecordCount
        Else
            Session("DataList") = "0"
        End If



    End Sub
    Private Sub bind()
        On Error Resume Next

        'Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where " & Session("sQuerys"), " and " & Session("sQuerys"))

        If Session("sWhere") = "" Then
            Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " where " & Session("sQuerys")) & " order by zNDName,BH desc "
        Else
            Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " and " & Session("sQuerys")) & " order by zNDName,BH desc "
        End If


        'Me.SqlDataSource1.SelectCommand = "SELECT * FROM [HZXXBView]" & Session("sWhere") & " order by zNDName,BH desc "
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

                rs = cPConn.Execute("Select * from HZXXB where code='" & arr(i) & "'")
                If rs.Fields("SH").Value Then
                    Return "患者信息已审核不能删除，请取消审核后再试！"
                    Exit Function
                End If

                cPConn.Execute("delete from HZXXB where code='" & arr(i) & "'")

                '
                cPConn.Execute("insert into WaterLog(cUser,cAction,cRemark)values('" & HttpContext.Current.Session("userid") & "','删除患者数据!','" & arr(i) & "')")

            Next
            Return "成功删除" + (arr.Length - 1).ToString + "条数据!"
        End If
    End Function

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        '表格中的Button执行完js后，判断是那个Button然后执行下面的事件
        If e.CommandName = "btn_modify" Then
            Me.GridView1.DataBind()
            'bind()
        Else

        End If
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        lblCurrentPage.Text = String.Format("当前第{0}页/总共{1}页", GridView1.PageIndex + 1, GridView1.PageCount)


        '指定给某一列的每一行增加一个js事件，这样前端就不能调动js了，主要考虑到需要带入参数
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cCode As String = e.Row.Cells(1).Text
            Dim cEdit As String


            cEdit = "javascript:WinOpen('" & cCode & "');"

            e.Row.Cells(9).Attributes.Add("onclick", cEdit)


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





    Protected Sub but_Query_Click(sender As Object, e As EventArgs) Handles but_Query.Click
        Dim rs As New ADODB.Recordset
        Dim sQuery As String
        Dim a1, a2, a3 As String

        sQuery = ""
        If cbo_NR.Text = "请选择" Or t_NR.Text = "" Then
            sQuery = ""
        Else
            t_NR.Text = Trim(Replace(t_NR.Text, "'", ""))
            t_NR.Text = Trim(Replace(t_NR.Text, " ", ""))

            Select Case cbo_NR.Text
                Case "编号"
                    sQuery = " and bh='" & t_NR.Text & "' "
                Case "姓名"
                    sQuery = " and xm='" & t_NR.Text & "' "
                Case "性别"
                    sQuery = " and xb='" & t_NR.Text & "' "
                Case "手术眼"
                    sQuery = " and ssy='" & t_NR.Text & "' "
                Case "身份证号"
                    sQuery = " and sfzh='" & t_NR.Text & "' "

            End Select
        End If

        a3 = ""

        If cbo_DDJG.Text = "请选择" Then
            a1 = ""
        Else
            a1 = " DDJGMC='" & cbo_DDJG.Text & "'"
        End If

        If cbo_ND.Text = "请选择" Then
            a2 = ""
        Else
            a2 = " zNDName='" & cbo_ND.Text & "'"
        End If

        If a2 = "" Then
            a3 = IIf(a1 = "", "", a1)
        Else
            a3 = IIf(a1 = "", "" & a2, a1 & " and " & a2)
        End If


        Session("sQuerys") = a3 & IIf(sQuery = "", "", sQuery)

        If Session("sWhere") = "" Then
            Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " where " & Session("sQuerys")) & " order by zNDName,BH desc "
        Else
            Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " and " & Session("sQuerys")) & " order by zNDName,BH desc "
        End If

        GridView1.DataBind()



        If Session("sWhere") = "" Then
            rs = cPConn.Execute("Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " where " & Session("sQuerys"))）
        Else
            rs = cPConn.Execute("Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sQuerys") = "", "", " and " & Session("sQuerys"))）
        End If

        If rs.RecordCount <> 0 Then
            Session("DataList") = "1"
            Session("DataNumber") = rs.RecordCount
        Else
            Session("DataList") = "0"
        End If

        bind()




    End Sub
End Class
