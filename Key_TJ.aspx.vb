
Partial Class Key_TJ
    Inherits System.Web.UI.Page

    Private Sub cbo_ND_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_ND.SelectedIndexChanged
        Session("TJND") = cbo_ND.Text

    End Sub

    Private Sub Key_TJ_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim rs As New ADODB.Recordset
        If Session("userid") = "" Then Response.Redirect("Login.aspx")

        Session("menu") = "统计分析"
        Session("onelevel") = "3"
        Session("twolevel") = "3.1"


        '页面第一次加载时为false'
        If IsPostBack = False Then
            rs = cPConn.Execute("Select * from DicND")

            If rs.RecordCount <> 0 Then


                For i = 0 To rs.RecordCount - 1

                    Me.cbo_ND.Items.Add(rs.Fields("zNDName").Value)
                    rs.MoveNext()

                Next

            End If

            rs = cPConn.Execute("Select * from DDJGB" & Session("sWhere"))

            If rs.RecordCount <> 0 Then

                Me.cbo_DDJG.Items.Add("请选择")
                For i = 0 To rs.RecordCount - 1

                    Me.cbo_DDJG.Items.Add(rs.Fields("DDJGMC").Value)
                    rs.MoveNext()

                Next

            End If



            rs.Close()
            rs = Nothing

            Session("TJND") = cbo_ND.Text
            Session("TJDDJG") = cbo_DDJG.Text
            Session("TJData") = "1"

        End If
    End Sub
    <System.Web.Services.WebMethod()>
    Public Shared Function EyeData(str As String) As String
        If str = "" Then
            Return "请选择日期!"
            Exit Function
        End If
        Dim rs As New ADODB.Recordset
        Dim str1 As String = ""

        Dim DDJG As String = IIf(HttpContext.Current.Session("TJDDJG") = "请选择", "", " and DDJGMC='" & HttpContext.Current.Session("TJDDJG") & "'")

        If HttpContext.Current.Session("TJData") = "1" Then

            rs = cPConn.Execute("select DDJGMC,count(DDJGMC) as mc from HZXXBView where zNDName='" & HttpContext.Current.Session("TJND") & "' " & DDJG & " group by DDJGMC  ")
            If rs.RecordCount <> 0 Then
                'str1 = rs.Fields("GLJG").Value
                For i = 0 To rs.RecordCount - 1
                    'str1 += """" + rs.Fields("GLJGMC").Value.ToString + ""","
                    str1 += """" + rs.Fields("DDJGMC").Value.ToString + """," & """" + rs.Fields("MC").Value.ToString + ""","
                    rs.MoveNext()
                Next
            Else
                Return "没有数据!"
                Exit Function
            End If

        ElseIf HttpContext.Current.Session("TJData") = "2" Then
            rs = cPConn.Execute("select SSFS,count(SSFS) as FS from HZXXBView where zNDName='" & HttpContext.Current.Session("TJND") & "' " & DDJG & " group by SSFS  ")
            If rs.RecordCount <> 0 Then
                'str1 = rs.Fields("GLJG").Value
                For i = 0 To rs.RecordCount - 1
                    'str1 += """" + rs.Fields("GLJGMC").Value.ToString + ""","
                    str1 += """" + rs.Fields("SSFS").Value.ToString + """," & """" + rs.Fields("FS").Value.ToString + ""","
                    rs.MoveNext()
                Next
            Else
                Return "没有数据!"
                Exit Function
            End If
        ElseIf HttpContext.Current.Session("TJData") = "3" Then

            rs = cPConn.Execute("select FYQK,count(FYQK) as QK from HZXXBView where zNDName='" & HttpContext.Current.Session("TJND") & "' " & DDJG & " group by FYQK  ")
            If rs.RecordCount <> 0 Then
                'str1 = rs.Fields("GLJG").Value
                For i = 0 To rs.RecordCount - 1
                    'str1 += """" + rs.Fields("GLJGMC").Value.ToString + ""","
                    str1 += """" + rs.Fields("FYQK").Value.ToString + """," & """" + rs.Fields("QK").Value.ToString + ""","
                    rs.MoveNext()
                Next
            Else
                Return "没有数据!"
                Exit Function
            End If




        ElseIf HttpContext.Current.Session("TJData") = "4" Then

            rs = cPConn.Execute("select XB,count(XB) as B from HZXXBView where zNDName='" & HttpContext.Current.Session("TJND") & "' " & DDJG & " group by XB  ")
            If rs.RecordCount <> 0 Then
                'str1 = rs.Fields("GLJG").Value
                For i = 0 To rs.RecordCount - 1
                    'str1 += """" + rs.Fields("GLJGMC").Value.ToString + ""","
                    str1 += """" + rs.Fields("XB").Value.ToString + """," & """" + rs.Fields("B").Value.ToString + ""","
                    rs.MoveNext()
                Next
            Else
                Return "没有数据!"
                Exit Function
            End If



        End If



        str1 = Strings.Left(str1, Strings.Len(str1) - 1)

        Return "[" + str1 + "]"
    End Function
    Protected Sub cbo_DDJG_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbo_DDJG.SelectedIndexChanged
        Session("TJDDJG") = cbo_DDJG.Text
    End Sub
    Protected Sub R_1_CheckedChanged(sender As Object, e As EventArgs) Handles R_1.CheckedChanged
        R_2.Checked = False
        R_3.Checked = False
        R_4.Checked = False
        Session("TJData") = "1"
    End Sub
    Protected Sub R_2_CheckedChanged(sender As Object, e As EventArgs) Handles R_2.CheckedChanged
        R_1.Checked = False
        R_3.Checked = False
        R_4.Checked = False
        Session("TJData") = "2"
    End Sub

    Private Sub R_3_CheckedChanged(sender As Object, e As EventArgs) Handles R_3.CheckedChanged
        R_2.Checked = False
        R_1.Checked = False
        R_4.Checked = False
        Session("TJData") = "3"
    End Sub

    Private Sub R_4_CheckedChanged(sender As Object, e As EventArgs) Handles R_4.CheckedChanged
        R_2.Checked = False
        R_3.Checked = False
        R_1.Checked = False
        Session("TJData") = "4"
    End Sub
End Class
