
Partial Class TG_fm_JSXX
	Inherits System.Web.UI.Page


	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        '设计思路：
        '1、首先从主窗体传入参数，定义一个Session("bMLoadData")来判断是否第一次加载（因为button确定一次就要加载一次）
        '2、执行前台getcode();函数，传入参数到隐藏域HiddenField1中，两种情况用传入的参数"obj.value"是否为空来判断
        '3、前台getcode();函数执行后台点击事件，如果为空则生成新编码，如果不为空说明要调入已有数据
        '4、如果是修改数据，Session("cMCode")将不为空，以此来判断插入新数据或更新，空为插入的新数据，不为空则是要更新的
        '5、Session("bMInsert") 为判断是不是新加载的，窗体加载时赋值为false，正确加载数据后赋值为ture，正确保存后再赋值为false，这样的话在没有正确保存的时候，不能加载

        '判断是否正确登录, 每个页面都要有
        If Session("userid") = "" Then Response.Redirect("index.aspx")

        '只加载一次, 判断是否第一次加载数据0为第一次, 加载后更新为1, 主窗体点击时恢复成0
        If IsPostBack = False Then
            Call LoadData()

            Session("bMInsert") = False
            Session("bMSave") = True

        End If

        'If Session("bMLoadData") = 0 Then
        '    Call LoadData()
        '    Session("bMLoadData") = 1

        '    Session("bMInsert") = False
        '    Session("bMSave") = True

        'End If


    End Sub

	Protected Sub LoadData()
        Dim rs As New ADODB.Recordset
        '执行前台js函数给隐藏域HiddenField1后，执行Load单击事件来加载数据
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>getcode();</script>")

        '加载必要的数据

        rs = cPConn.Execute("Select * from dbRYXX")

        If rs.RecordCount <> 0 Then


            For i = 0 To rs.RecordCount - 1

                Me.cbo_xm.Items.Add(rs.Fields("cXM").Value)
                rs.MoveNext()

            Next

        End If

        rs.Close()
        rs = Nothing
    End Sub

	Protected Sub but_LoadData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles but_LoadData.Click
        Dim rs, re As New ADODB.Recordset
        Session("cMCode") = Me.HiddenField1.Value
        Session("bMEdit") = Me.HiddenField2.Value

        '判断是不是加载了
        If Session("bMInsert") = True Then Exit Sub



        If Session("cMCode") <> "" Then

            re = cPConn.Execute("select * from TSXXView where code='" & Session("cMCode") & "'")
            If re.RecordCount <> 0 Then
                T_Book.Text = re.Fields("SM").Value
            End If

            T_code.Text = GetCode("JSXX", "R", Year(Now.Date), Month(Now.Date), Day(Now.Date))
            cbo_xm.SelectedIndex = 0
            Time_jsrq.Value = Format(Now, "yyyy-MM-dd")
            'shijian.Value = Format(Now, "yyyy-MM-dd")
            Time_yhrq.Value = Format(DateAdd("d", 14, Now), "yyyy-MM-dd")
        End If

        're = cPConn.Execute("select * from TSXXView where code='" & Session("cMCode") & "'")
        '    rs = cPConn.Execute("select * from JSXXView")

        '    If re.RecordCount <> 0 Then

        '        T_code.Text = rs.Fields("code").Value

        '        cbo_xm.Text = rs.Fields("xm").Value
        '        T_Book.Text = rs.Fields("lxfs").Value
        '        Time_jsrq.Value = Format(rs.Fields("jsrq").Value, "yyyy-MM-dd")
        '        Time_yhrq.Value = Format(rs.Fields("yhrq").Value, "yyyy-MM-dd")
        '        T_bz.Text = rs.Fields("bz").Value

        '    End If
        'Else

        '    T_code.Text = GetCode("JSXX", "R", Year(Now.Date), Month(Now.Date), Day(Now.Date))

        '    cbo_xm.SelectedIndex = 0
        '    T_Book.Text = ""
        '    Time_jsrq.Value = ""
        '    Time_yhrq.Value = ""
        '    T_bz.Text = ""

        'End If



    End Sub

    Protected Sub But_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles But_Save.Click
        On Error GoTo errhand
        Dim rs, re As New ADODB.Recordset
        Dim sBillCode As String
        Dim sBillCode1 As String

        '判断关键字段不能为空，做校验

        T_Book.Text = Trim(Replace(T_Book.Text, "'", ""))

        T_bz.Text = Trim(Replace(T_bz.Text, "'", ""))

        If cbo_xm.Text = "" Or cbo_xm.Text = "请选择" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择借书人！')</script>")
            cbo_xm.Focus()
            Exit Sub
        ElseIf Time_jsrq.Value = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择借书日期！')</script>")
            Time_jsrq.Focus()
            Exit Sub
        ElseIf Time_yhrq.Value = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择还书日期！')</script>")
            Time_yhrq.Focus()
            Exit Sub


        End If

        '保存数据

        '保存数据。如果不是新增则直接更新数据，如果是新增则直接插入数据（这个时候，可以判断除了code为关键字段，以外的不能重复的字段）。
        rs = cPConn.Execute("select * from TSJY_JSXX where code='" & T_code.Text & "'")
        If rs.RecordCount <> 0 Then



            cPConn.Execute("update TSJY_JSXX set code='" & T_code.Text & "',xm='" & cbo_xm.Text & "',lxfs='" & T_Book.Text & "',jsrq='" & Time_jsrq.Value & "',yhrq='" & Time_yhrq.Value & "',bz='" & T_bz.Text & "' where code='" & T_code.Text & "'")
            cPConn.Execute("update TSJY_TSXX set sfjy='1', jybh='" & Me.T_code.Text & "',jsrq='" & Time_jsrq.Value & "',hsrq='" & Time_yhrq.Value & "' where code='" & Session("cMCode") & "'")

            cPConn.Execute("insert into WaterLog(cUser,cAction,cRemark)values('" & Session("userid") & "','修改借书信息','" & T_code.Text & "')")




        Else

            sBillCode = GetCode("JSXX", "W", Year(Now.Date), Month(Now.Date), Day(Now.Date))

            Me.T_code.Text = sBillCode


            cPConn.Execute("insert into TSJY_JSXX(code,xm,lxfs,jsrq,yhrq,bz) values('" & sBillCode & "','" & cbo_xm.Text & "','" & T_Book.Text & "','" & Time_jsrq.Value & "','" & Time_yhrq.Value & "','" & T_bz.Text & "')")

            cPConn.Execute("update TSJY_TSXX set sfjy='1', jybh='" & Me.T_code.Text & "',jsrq='" & Time_jsrq.Value & "',hsrq='" & Time_yhrq.Value & "' where code='" & Session("cMCode") & "'")

            cPConn.Execute("insert into WaterLog(cUser,cAction,cRemark)values('" & Session("userid") & "','录入借书信息','" & sBillCode & "')")
        End If

		'提示保存完毕

		Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>SaveMsg();</script>")

		Session("bMInsert") = False
		Session("bMSave") = True

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
