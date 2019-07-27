
Partial Class Key_Print
    Inherits System.Web.UI.Page

    Private Sub Key_Print_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim rs As New ADODB.Recordset
        If Session("userid") = "" Then Response.Redirect("Login.aspx")
        Session("menu") = "打印汇总表"
        Session("onelevel") = "1"
        Session("twolevel") = "1.4"





        If IsPostBack = False Then
            rs = cPConn.Execute("Select * from DicND order by zNDName desc")

            If rs.RecordCount <> 0 Then


                For i = 0 To rs.RecordCount - 1

                    Me.cbo_ND.Items.Add(rs.Fields("zNDName").Value)
                    rs.MoveNext()

                Next

            End If

            rs = cPConn.Execute("Select * from DDJGBView" & Session("sWhere"))

            If rs.RecordCount <> 0 Then

                'Me.cbo_DDJG.Items.Add("请选择")
                For i = 0 To rs.RecordCount - 1

                    Me.cbo_DDJG.Items.Add(rs.Fields("DDJGMC").Value)
                    rs.MoveNext()

                Next

            End If



            rs.Close()
            rs = Nothing
        End If

        '检测表内是否有信息

        rs = cPConn.Execute("Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "'", " and zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "'") & " order by BH asc ")
        If rs.RecordCount <> 0 Then
            Session("DataList") = "1"

        Else
            Session("DataList") = "0"
        End If


        If IsPostBack = False Then
            'Me.SqlDataSource1.SelectCommand = "select * from [HZXXBView]" & Session("sWhere") & " order by zNDName,BH asc "
            Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "'", " and zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "'") & " order by BH asc "

            GridView1.DataBind()
        End If






    End Sub




    Protected Sub but_Query_Click(sender As Object, e As EventArgs) Handles but_Query.Click
        Dim rs As New ADODB.Recordset
        Me.SqlDataSource1.SelectCommand = "Select * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "'", " and zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "'") & " order by BH asc "
        GridView1.DataBind()

        rs = cPConn.Execute("SELECT * FROM [HZXXBView]" & Session("sWhere") & IIf(Session("sWhere") = "", " where zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "'", " and zNDName='" & cbo_ND.Text & "' and DDJGMC='" & cbo_DDJG.Text & "'") & " order by BH asc ")
        If rs.RecordCount <> 0 Then
            Session("DataList") = "1"
            Session("PrintND") = "年度：" & cbo_ND.Text
            Session("PrintDDJG") = "单位（盖章）：" & cbo_DDJG.Text
        Else

            Session("DataList") = "0"
        End If







    End Sub
    Protected Sub but_Excel_Click(sender As Object, e As EventArgs) Handles but_Excel.Click
        On Error Resume Next
        Dim myExcel As New Microsoft.Office.Interop.Excel.Application


        myExcel.Application.Workbooks.Add(True)
        myExcel.Visible = False


        '去除datagridview1的编号列  
        Dim m As Integer
        For m = 0 To GridView1.Columns.Count - 1

            myExcel.Cells(1, m + 1) = Me.GridView1.Columns(m).HeaderText

        Next m

        '往excel表里添加数据  
        Dim i As Integer
        For i = 0 To GridView1.Rows.Count - 1
            Dim j As Integer
            For j = 0 To GridView1.Columns.Count - 1

                If Me.GridView1 Is System.DBNull.Value Then
                    myExcel.Cells(i + 2, j + 1) = ""
                Else
                    'myExcel.Cells(i + 2, j + 1) = GridView1
                    If Len(GridView1.Rows(i).Cells(j).Text) >= 18 And IsNumeric(GridView1.Rows(i).Cells(j).Text) Then
                        myExcel.Cells(i + 2, j + 1) = "'" & GridView1.Rows(i).Cells(j).Text
                    Else
                        myExcel.Cells(i + 2, j + 1) = GridView1.Rows(i).Cells(j).Text
                    End If


                End If

            Next j
        Next i


        myExcel.Quit()
    End Sub
End Class
