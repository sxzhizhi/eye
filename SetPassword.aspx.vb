
Partial Class SetPassword
    Inherits System.Web.UI.Page
    Dim cMCode As String



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '设计思路：
        '1、首先从主窗体传入参数，定义一个Session("bMLoadData")来判断是否第一次加载（因为button确定一次就要加载一次）
        '2、执行前台getArguments();函数，传入参数到隐藏域HiddenField1中，两种情况用传入的参数"obj.value"是否为空来判断
        '3、前台getArguments();函数执行后台点击事件，如果为空则生成新编码，如果不为空说明要调入已有数据

        '判断是否正确登录，每个页面都要有
        If Session("userid") = "" Then Response.Redirect("index.aspx")

        '只加载一次，判断是否第一次加载数据0为第一次，加载后更新为1，主窗体点击时恢复成0
        If Session("bMLoadData") = 0 Then
            Call LoadData()
            Session("bMLoadData") = 1
        End If


    End Sub

    Protected Sub LoadData()
        '执行前台js函数给隐藏域HiddenField1后，执行Load单击事件来加载数据
        'Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>getArguments();</script>")

        Me.Password1.Text = ""
        Me.Password2.Text = ""
        Me.Password3.Text = ""


    End Sub


    Protected Sub But_Save_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles But_Save.Click
        Dim rs As New ADODB.Recordset
        Dim sMM As String
        '判断关键字段不能为空，做校验


        Password1.Text = Replace(Password1.Text, " ", "")
        Password1.Text = Replace(Password1.Text, "'", "")

        Password2.Text = Replace(Password2.Text, " ", "")
        Password2.Text = Replace(Password2.Text, "'", "")

        Password3.Text = Replace(Password3.Text, " ", "")
        Password3.Text = Replace(Password3.Text, "'", "")


        If Me.Password1.Text = "" Or Me.Password2.Text = "" Or Me.Password3.Text = "" Then

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('不能为空，请正确录入！')</script>")

            Exit Sub

        End If

        rs = cPConn.Execute("select * from dbUser where cName='" & Session("userid") & "'")



        If rs.Fields("cPass").Value <> DigestStrToHexStr(Me.Password1.Text) Then

            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('录入的旧密码不正确！')</script>")
            Me.Password1.Focus()
            Exit Sub

        End If

        If Me.Password2.Text <> Me.Password3.Text Then
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('录入的两次新密码不一致！')</script>")
            Me.Password2.Focus()
            Exit Sub
        End If

        '保存数据


        sMM = DigestStrToHexStr(Me.Password3.Text)


        rs = cPConn.Execute("update dbUser set cPass='" & sMM & "' where cName='" & Session("userid") & "'")


        '提示保存完毕
        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>SaveMsg();</script>")



    End Sub



End Class
