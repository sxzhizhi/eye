
Partial Class frm_HZXX
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '设计思路：
        '1、首先从主窗体传入参数，定义一个Session("bMLoadData")来判断是否第一次加载（因为button确定一次就要加载一次）
        '2、执行前台getcode();函数，传入参数到隐藏域HiddenField1中，两种情况用传入的参数"obj.value"是否为空来判断
        '3、前台getcode();函数执行后台点击事件，如果为空则生成新编码，如果不为空说明要调入已有数据
        '4、如果是修改数据，Session("cMCode")将不为空，以此来判断插入新数据或更新，空为插入的新数据，不为空则是要更新的
        '5、Session("bMInsert") 为判断是不是新加载的，窗体加载时赋值为false，正确加载数据后赋值为ture，正确保存后再赋值为false，这样的话在没有正确保存的时候，不能加载
        '6、Session("cMKey")是这个表中不能重覆的关键字，用这个来判断是否有重覆
        '判断是否正确登录, 每个页面都要有
        If Session("userid") = "" Then Response.Redirect("index.aspx")

        '只加载一次, 判断是否第一次加载数据0为第一次, 加载后更新为1, 主窗体点击时恢复成0
        If IsPostBack = False Then
            Call LoadData()

            Session("bMInsert") = False
            Session("bMSave") = True
            '重覆关键字
            Session("cMKey") = ""
        End If

        'If Session("bMLoadData") = 0 Then
        '    Call LoadData()
        '    Session("bMLoadData") = 1

        '    Session("bMInsert") = False
        '    Session("bMSave") = True

        'End If


    End Sub

    Protected Sub LoadData()
        'Dim rs As New ADODB.Recordset
        '执行前台js函数给隐藏域HiddenField1后，执行Load单击事件来加载数据
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>getcode();</script>")

    End Sub

    Protected Sub but_LoadData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles but_LoadData.Click
        Dim rs As New ADODB.Recordset
        Session("cMCode") = Me.HiddenField1.Value
        Session("bMEdit") = Me.HiddenField2.Value

        '判断是不是加载了
        If Session("bMInsert") = True Then Exit Sub

        If Session("cMCode") <> "" Then
            rs = cPConn.Execute("select * from DicBY where zBYCode='" & Session("cMCode") & "'")
            If rs.RecordCount <> 0 Then

                T_code.Text = rs.Fields("zBYcode").Value

                T_tslb.Text = rs.Fields("zBYName").Value

                Session("cMKey") = T_tslb.Text
            Else
                T_code.Text = GetCode("BYZD", "R", Year(Now.Date), Month(Now.Date), Day(Now.Date))

                T_tslb.Text = ""
                Session("cMKey") = ""
            End If
        Else

            T_code.Text = GetCode("BYZD", "R", Year(Now.Date), Month(Now.Date), Day(Now.Date))

            T_tslb.Text = ""
            Session("cMKey") = ""

        End If
    End Sub

    Protected Sub But_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles But_Save.Click
        On Error GoTo errhand
        Dim rs, re As New ADODB.Recordset
        Dim sBillCode As String
        Dim sBillCode1 As String

        '判断关键字段不能为空，做校验

        T_tslb.Text = Trim(Replace(T_tslb.Text, "'", ""))

        If T_tslb.Text = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请正确录入！')</script>")
            T_tslb.Focus()
            Exit Sub

        End If

        '保存数据

        '保存数据。如果不是新增则直接更新数据，如果是新增则直接插入数据（这个时候，可以判断除了code为关键字段，以外的不能重复的字段）。
        rs = cPConn.Execute("select * from DicBY where zBYcode='" & T_code.Text & "'")
        If rs.RecordCount <> 0 Then

            '判断重覆关键字
            If Session("cMKey") <> T_tslb.Text Then
                re = cPConn.Execute("Select * from DicBY where zBYName='" & T_tslb.Text & "'")
                If re.RecordCount <> 0 Then
                    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>RepeatMsg();</script>")
                    T_tslb.Focus()
                    Exit Sub
                End If
            End If

            cPConn.Execute("update DicBY set zBYcode='" & T_code.Text & "',zBYName='" & T_tslb.Text & "' where zBYcode='" & T_code.Text & "'")

            cPConn.Execute("insert into WaterLog(cUser,cAction,cRemark)values('" & Session("userid") & "','修改病因字典数据!','" & T_code.Text & "')")




        Else

            re = cPConn.Execute("Select * from DicBY where zBYName='" & T_tslb.Text & "'")
            If re.RecordCount <> 0 Then
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>RepeatMsg();</script>")
                T_tslb.Focus()
                Exit Sub
            End If

            sBillCode = GetCode("BYZD", "W", Year(Now.Date), Month(Now.Date), Day(Now.Date))

            Me.T_code.Text = sBillCode


            cPConn.Execute("insert into DicBY(zBYCode,zBYName) values('" & sBillCode & "','" & T_tslb.Text & "')")

            cPConn.Execute("insert into WaterLog(cUser,cAction,cRemark)values('" & Session("userid") & "','录入病因字典数据!','" & sBillCode & "')")
        End If

        '提示保存完毕

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>SaveMsg();</script>")

        Session("bMInsert") = False
        Session("bMSave") = True
        '把不能重复的关键字段付给Session("cMKey")，然后后面做判断
        Session("cMKey") = T_tslb.Text

errhand:
        If Err.Number <> 0 Then
            Select Case Err.Number
                Case Else
                    MsgBox(Err.Number & vbLf &
                           Err.Description, vbInformation, "提示")
                    Exit Sub
            End Select
        End If
    End Sub
End Class
