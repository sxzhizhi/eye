
Partial Class Frm_XXCX
    Inherits System.Web.UI.Page
    Dim sQuerys As String

    Private Sub Frm_XXGL_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim rs As ADODB.Recordset
        If Session("userid") = "" Then Response.Redirect("Login.aspx")
        Session("menu") = "信息查询"
        Session("onelevel") = "1"
        Session("twolevel") = "1.3"
        Dim sND As String
        Dim sDDJG As String
        Dim sQuery As String

        'Me.SqlDataSource1.SelectCommand = "SELECT * FROM [HZXXBView]" & Session("sWhere") & " order by BH asc "



        If IsPostBack = False Then



            rs = cPConn.Execute("Select * from DicND order by zNDName desc")

            If rs.RecordCount <> 0 Then


                For i = 0 To rs.RecordCount - 1

                    Me.cbo_ND.Items.Add(rs.Fields("zNDName").Value)
                    rs.MoveNext()

                Next

            End If

            rs = cPConn.Execute("Select * from DDJGBView " & Session("sWhere"))

            If rs.RecordCount <> 0 Then

                'Me.cbo_DDJG.Items.Add("请选择")
                For i = 0 To rs.RecordCount - 1

                    Me.cbo_DDJG.Items.Add(rs.Fields("DDJGMC").Value)
                    rs.MoveNext()

                Next

            End If

            cbo_NR.Items.Add("请选择")
            cbo_NR.Items.Add("编号")
            cbo_NR.Items.Add("姓名")
            cbo_NR.Items.Add("性别")
            cbo_NR.Items.Add("手术眼")
            cbo_NR.Items.Add("身份证号")

        End If

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

        sND = cbo_ND.Text
        sDDJG = cbo_DDJG.Text

        sQuerys = " zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "' " & IIf(sQuery = "", "", sQuery)


        Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where " & sQuerys, " and " & sQuerys) & " order by BH asc "


        '检测表内是否有信息
        'rs = cPConn.Execute("select * from [HZXXBView]" & Session("sWhere"))
        rs = cPConn.Execute("Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where " & sQuerys, " and " & sQuerys) & " order by BH asc ")


        If rs.RecordCount <> 0 Then
            Session("DataList") = "1"
        Else
            Session("DataList") = "0"
        End If



    End Sub


    Private Sub bind()
        On Error Resume Next


        'Me.SqlDataSource1.SelectCommand = "SELECT * FROM [HZXXBView]" & Session("sWhere") & " order by BH asc "

        Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where " & sQuerys, " and " & sQuerys) & " order by BH asc "



        GridView1.DataBind()
        Me.ddlCurrentPage.Items.Clear()
        For i = 1 To Me.GridView1.PageCount
            Me.ddlCurrentPage.Items.Add(i.ToString())
        Next
        ddlCurrentPage.SelectedIndex = Me.GridView1.PageIndex
        'Me.All_CheckBox.Checked = False




    End Sub
    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        lblCurrentPage.Text = String.Format("当前第{0}页/总共{1}页", GridView1.PageIndex + 1, GridView1.PageCount)


        '指定给某一列的每一行增加一个js事件，这样前端就不能调动js了，主要考虑到需要带入参数
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim cCode As String = e.Row.Cells(0).Text
            Dim cEdit As String


            cEdit = "javascript:WinOpen('" & cCode & "','1');"

            e.Row.Cells(11).Attributes.Add("onclick", cEdit)


        End If
    End Sub
    Protected Sub but_Query_Click(sender As Object, e As EventArgs) Handles but_Query.Click
        Dim rs As New ADODB.Recordset
        Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where " & sQuerys, " and " & sQuerys) & " order by BH asc "
        GridView1.DataBind()

        rs = cPConn.Execute("Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where " & sQuerys, " and " & sQuerys) & " order by BH asc ")
        If rs.RecordCount <> 0 Then
            Session("DataList") = "1"
            Session("DataNumber") = rs.RecordCount
        Else
            Session("DataList") = "0"
        End If
        bind()
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

End Class
