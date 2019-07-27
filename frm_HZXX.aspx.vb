
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
        Dim rs As New ADODB.Recordset
        '执行前台js函数给隐藏域HiddenField1后，执行Load单击事件来加载数据
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>getcode();</script>")


        rs = cPConn.Execute("Select * from DicND")

        If rs.RecordCount <> 0 Then


            For i = 0 To rs.RecordCount - 1

                Me.cbo_ND.Items.Add(rs.Fields("zNDName").Value)
                rs.MoveNext()

            Next

        End If

        rs = cPConn.Execute("Select * from DicHKLX")

        If rs.RecordCount <> 0 Then


            For i = 0 To rs.RecordCount - 1

                Me.cbo_HKLX.Items.Add(rs.Fields("zHKLXName").Value)
                rs.MoveNext()

            Next

        End If

        rs = cPConn.Execute("Select * from DicBY")

        If rs.RecordCount <> 0 Then


            For i = 0 To rs.RecordCount - 1

                Me.cbo_BY.Items.Add(rs.Fields("zBYName").Value)
                rs.MoveNext()

            Next

        End If




        rs.Close()
        rs = Nothing


    End Sub

    Protected Sub but_LoadData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles but_LoadData.Click
        Dim rs As New ADODB.Recordset
        Session("cMCode") = Me.HiddenField1.Value
        Session("bMEdit") = Me.HiddenField2.Value

        '判断是不是加载了
        If Session("bMInsert") = True Then Exit Sub

        If Session("cMCode") <> "" Then
            rs = cPConn.Execute("select * from HZXXBView where Code='" & Session("cMCode") & "'")
            If rs.RecordCount <> 0 Then

                T_code.Text = rs.Fields("code").Value
                cbo_ND.Text = rs.Fields("zNDName").Value
                T_BH.Text = rs.Fields("BH").Value

                T_XM.Text = rs.Fields("XM").Value

                cbo_XB.Text = rs.Fields("XB").Value
                T_SFZH.Text = rs.Fields("SFZH").Value
                cbo_HKLX.Text = rs.Fields("zHKLXName").Value
                T_LXDH.Text = rs.Fields("LXDH").Value
                T_JTZZ.Text = rs.Fields("JTZZ").Value
                cbo_BY.Text = rs.Fields("zBYName").Value

                cbo_FYQK.Text = rs.Fields("FYQK").Value
                cbo_SSY.Text = rs.Fields("SSY").Value
                cbo_SSFS.Text = rs.Fields("SSFS").Value
                T_SQSL.Text = rs.Fields("SQSL").Value
                T_SHSL.Text = rs.Fields("SHSL").Value
                T_SSRQ.Value = Format(rs.Fields("SSRQ").Value, "yyyy-MM-dd")
                T_BZ.Text = rs.Fields("BZ").Value


                T_XM.Enabled = False
                Session("cMKey") = T_BH.Text


            Else

                T_code.Text = GetCode("HZXXB", "R", Year(Now.Date), Month(Now.Date), Day(Now.Date))

                T_BH.Text = ""
                T_XM.Text = ""

                T_SFZH.Text = ""
                T_LXDH.Text = ""
                T_JTZZ.Text = ""
                T_SQSL.Text = ""
                T_SHSL.Text = ""

                T_SSRQ.Value = Format(Now, "yyyy-MM-dd")
                T_BZ.Text = ""

                T_XM.Enabled = True
                Session("cMKey") = ""
            End If
        Else

            T_code.Text = GetCode("HZXXB", "R", Year(Now.Date), Month(Now.Date), Day(Now.Date))

            T_BH.Text = ""

            T_XM.Text = ""

            T_SFZH.Text = ""
            T_LXDH.Text = ""
            T_JTZZ.Text = ""
            T_SQSL.Text = ""
            T_SHSL.Text = ""

            T_SSRQ.Value = Format(Now, "yyyy-MM-dd")
            T_BZ.Text = ""

            T_XM.Enabled = True
            Session("cMKey") = ""

        End If

        If Session("bMEdit") = "1" Then
            But_Save.Visible = False
            but_LoadData.Visible = False
        End If

    End Sub

    Protected Sub But_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles But_Save.Click
        On Error GoTo errhand
        Dim rs, re As New ADODB.Recordset
        Dim sBillCode As String
        Dim sBillCode1 As String

        Dim sND, sBY, sHKLX As String


        '判断关键字段不能为空，做校验

        T_BH.Text = Trim(Replace(T_BH.Text, "'", ""))
        T_XM.Text = Trim(Replace(T_XM.Text, "'", ""))

        If cbo_ND.Text = "请选择" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请选择年度！')</script>")
            cbo_ND.Focus()
            Exit Sub
        End If

        If T_BH.Text = "" Or T_XM.Text = "" Or T_SFZH.Text = "" Or T_LXDH.Text = "" Or T_SQSL.Text = "" Or T_SHSL.Text = "" Or T_SSRQ.Value = "" Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('请正确录入！')</script>")

            Exit Sub

        End If

        '保存字典编码

        rs = cPConn.Execute("Select * from DicND where zNDName='" & cbo_ND.Text & "'")
        sND = rs.Fields("zNDCode").Value

        rs = cPConn.Execute("Select * from DicBY where zBYName='" & cbo_BY.Text & "'")
        sBY = rs.Fields("zBYCode").Value

        rs = cPConn.Execute("Select * from DicHKLX where zHKLXName='" & cbo_HKLX.Text & "'")
        sHKLX = rs.Fields("zHKLXCode").Value

        '保存数据。如果不是新增则直接更新数据，如果是新增则直接插入数据（这个时候，可以判断除了code为关键字段，以外的不能重复的字段）。
        '为了避免多人同时录入时，只用系统编码来作关键字段时，会出现录入错误的情况，所以用两个字段来做关键字段，另一个字段可以根据系统需求来改变，
        '因此第一次保存成功后，这个字段设置成不可修改，要不会又生成一条数据来，如果要修改这个字段的话就删除后再重新录入

        rs = cPConn.Execute("select * from HZXXBView where code='" & T_code.Text & "' and XM='" & T_XM.Text & "'")
        If rs.RecordCount <> 0 Then

            '判断重覆关键字，也可以加入其他判断的条件，根据软件需要来设计

            'If Session("cMKey") <> T_BH.Text Then
            '    re = cPConn.Execute("Select * from DicBY where zBYName='" & T_BH.Text & "'")
            '    If re.RecordCount <> 0 Then
            '        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>RepeatMsg();</script>")
            '        T_BH.Focus()
            '        Exit Sub
            '    End If
            'End If

            cPConn.Execute("update HZXXB set BH='" & T_BH.Text & "',zNDCode='" & sND & "',xm='" & T_XM.Text & "',xb='" & cbo_XB.Text & "'," &
                           "zHKLXCode='" & sHKLX & "',SFZH='" & T_SFZH.Text & "',LXDH='" & T_LXDH.Text & "'," & "JTZZ='" & T_JTZZ.Text & "'," &
                           "zBYcode='" & sBY & "',SSY='" & cbo_SSY.Text & "',SSFS='" & cbo_SSFS.Text & "'," &
                           "SQSL='" & T_SQSL.Text & "',SHSL='" & T_SHSL.Text & "',SSRQ='" & T_SSRQ.Value & "',FYQK='" & cbo_FYQK.Text & "'," &
                           "BZ='" & T_BZ.Text & "',JGBH='" & Session("usercode") & "' where code='" & T_code.Text & "' and xm='" & T_XM.Text & "'")

            cPConn.Execute("insert into WaterLog(cUser,cAction,cRemark)values('" & Session("userid") & "','修改患者数据!','" & T_code.Text & "')")




        Else

            're = cPConn.Execute("Select * from DicBY where zBYName='" & T_BH.Text & "'")
            'If re.RecordCount <> 0 Then
            '    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>RepeatMsg();</script>")
            '    T_BH.Focus()
            '    Exit Sub
            'End If

            're = cPConn.Execute("select * from HZXXBView where code='" & T_code.Text & "'")
            'If re.RecordCount <> 0 Then

            'End If



            sBillCode = GetCode("HZXXB", "W", Year(Now.Date), Month(Now.Date), Day(Now.Date))

            Me.T_code.Text = sBillCode


            cPConn.Execute("insert into HZXXB(Code,BH,zNDCode,XM,XB,zHKLXCode,SFZH,LXDH,JTZZ,zBYCode,SSY,SSFS,SQSL,SHSL,SSRQ,FYQK,BZ,JGBH) values('" &
                           sBillCode & "','" & T_BH.Text & "','" & sND & "','" & T_XM.Text & "','" & cbo_XB.Text & "','" & sHKLX & "','" & T_SFZH.Text & "','" &
                           T_LXDH.Text & "','" & T_JTZZ.Text & "','" & sBY & "','" & cbo_SSY.Text & "','" & cbo_SSFS.Text & "','" & T_SQSL.Text & "','" &
                           T_SHSL.Text & "','" & T_SSRQ.Value & "','" & cbo_FYQK.Text & "','" & T_BZ.Text & "','" & Session("usercode") & "')")

            cPConn.Execute("insert into WaterLog(cUser,cAction,cRemark)values('" & Session("userid") & "','录入患者数据!','" & sBillCode & "')")



        End If

        '提示保存完毕

        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>SaveMsg();</script>")

        Session("bMInsert") = False
        Session("bMSave") = True
        '把不能重复的关键字段付给Session("cMKey")，然后后面做判断
        Me.T_XM.Enabled = False
        Session("cMKey") = T_BH.Text

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
