
Partial Class Login
    Inherits System.Web.UI.Page

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub


    Public Function main01() As Boolean
        On Error GoTo errhand
        Dim mSql As String



        cPConn.CursorLocation = ADODB.CursorLocationEnum.adUseClient
        cPConn.ConnectionTimeout = 10
        cPConn.CommandTimeout = 0



        mSql = "Provider=SQLOLEDB;server=" & mServer & ";database=" & mDataBase & ";uid=" & mUser & ";pwd=" & mPassWord
        'mSql = "Provider=SQLOLEDB;server=" & mServer & ";database=" & mDataBase & ";uid=conn_wl;pwd=" & mPassWord
        'mSql = "Data Source=" & mServer & ";Initial Catalog=" & mDataBase & ";User ID=" & mUser & ";Password=" & mPassWord


        'mSql = "Provider=SQLOLEDB.1;Password=" & mPassWord & ";Persist Security Info=True;User ID=" & mUser & ";Initial Catalog=" & mDataBase & ";Data Source=" & mServer
        'mSql = "Data Source=47.94.23.102;Initial Catalog=WSN_TY;User ID=sa;Password=p@ssw0rd"
        'mSql = "PROVIDER=MSDataShape;Data PROVIDER=MSDASQL;uid=as;pwd=p@ssw0rd;DRIVER=SQL Server;DATABASE=WSN_TY;WSID=GQSOFT;SERVER=47.94.23.102"



        cPConn.Open(mSql)

        main01 = True

        On Error GoTo 0

        Exit Function
errhand:
        Select Case Err.Number
            Case -2147467259
                'MsgBox(vbCrLf & "数据库管理服务器中数据库管理程序未启动，请先打开该程序。" & vbCrLf & _
                '        vbCrLf & _
                '       "或指定的服务器不存在。", vbOKOnly + vbInformation, "提示")
                main01 = False
                Exit Function
            Case 3705
                'MsgBox("错 误 号：" & Err.Number & vbCrLf & "错误信息：" & Err.Description & _
                '    vbCrLf & "请通知系统管理员！", vbOKOnly + vbSystemModal, "提示")
                main01 = False
                Exit Function
            Case Else
                'MsgBox("错 误 号：" & Err.Number & vbCrLf & "错误信息：" & Err.Description & _
                '    vbCrLf & "请通知系统管理员！", vbOKOnly + vbSystemModal, "提示")
                main01 = False
                Exit Function
        End Select

        Err.Clear()
Err_Hand:
        'MsgBox("系统文件错误，请通知系统管理员...", vbOKOnly + vbInformation, "提示")
        main01 = False



    End Function
    Public Function ReadSystemSet() As Boolean
        'On Error Resume Next
        'Dim oFSO As New Scripting.FileSystemObject
        'Dim tTextStream As Scripting.TextStream
        'cPHostName = GetHostName()
        'If cPHostName = "Cancel" Then
        '    MsgBox("主机名称不能正确取得！请通知系统管理员...", MsgBoxStyle.Exclamation + MsgBoxStyle.OkOnly, "提示")
        '    Exit Sub
        'End If

        Dim Fso = Server.CreateObject("Scripting.FileSystemObject")
        Dim Filen = Server.MapPath("App_Code/AiWei.ini")
        Dim StrBuf As String

        Dim sServer As String = ""
        Dim sDataBase As String = ""
        Dim sUser As String = ""
        Dim sPassWord As String = ""
        Dim sDomain As String = ""

        Dim Site_Config = Fso.OpenTextFile(Filen, 1)

        Do While Not Site_Config.AtEndOfStream

            StrBuf = Site_Config.ReadLine
            If StrBuf = "[Data]" Then
                StrBuf = Site_Config.ReadLine
                If LCase(Left(StrBuf, InStr(1, StrBuf, "=") - 1)) = "server" Then
                    sServer = Trim(Mid(StrBuf, InStr(1, StrBuf, "=") + 1))
                End If

                StrBuf = Site_Config.ReadLine
                If LCase(Left(StrBuf, InStr(1, StrBuf, "=") - 1)) = "database" Then
                    sDataBase = Trim(Mid(StrBuf, InStr(1, StrBuf, "=") + 1))
                End If

                StrBuf = Site_Config.ReadLine

                If LCase(Left(StrBuf, InStr(1, StrBuf, "=") - 1)) = "user" Then
                    sUser = Trim(Mid(StrBuf, InStr(1, StrBuf, "=") + 1))
                End If

                StrBuf = Site_Config.ReadLine
                If LCase(Left(StrBuf, InStr(1, StrBuf, "=") - 1)) = "password" Then
                    sPassWord = Trim(Mid(StrBuf, InStr(1, StrBuf, "=") + 1))
                End If

                StrBuf = Site_Config.ReadLine
                If LCase(Left(StrBuf, InStr(1, StrBuf, "=") - 1)) = "domain" Then
                    sDomain = Trim(Mid(StrBuf, InStr(1, StrBuf, "=") + 1))
                End If
            End If
        Loop



        If sServer = "" Or sDataBase = "" Or sUser = "" Or sPassWord = "" Or sDomain = "" Then GoTo Err_Hand

        mServer = sServer
        mDataBase = sDataBase
        mUser = sUser
        mPassWord = sPassWord
        mDomain = sDomain

        ReadSystemSet = True
        Exit Function


Err_Hand:
        'MsgBox("系统文件错误，请正确部署系统或咨询售后技术支持...", vbOKOnly + vbInformation, "提示")
        ReadSystemSet = False
    End Function



    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        On Error GoTo errhand
        Dim rs, re As New ADODB.Recordset
        Dim cWebUser As String
        Dim cWebPassword As String

        If email.Text = "" Then
            Me.Label1.Visible = True
            Me.Label2.Visible = False
            Me.Label3.Visible = False
            'Me.Label4.Visible = False
            email.Focus()
            Exit Sub
        End If

        If password.Text = "" Then
            Me.Label1.Visible = False
            Me.Label2.Visible = True
            Me.Label3.Visible = False
            'Me.Label4.Visible = False
            password.Focus()
            Exit Sub
        End If

        'If YZCode.Text = "" Then
        '    Me.Label1.Visible = False
        '    Me.Label2.Visible = False
        '    Me.Label3.Visible = False
        '    Me.Label4.Visible = True
        '    YZCode.Focus()
        '    Exit Sub
        'End If

        'If YZCode.Text <> LabelCode.Text Then
        '    Me.Label1.Visible = False
        '    Me.Label2.Visible = False
        '    Me.Label3.Visible = False
        '    Me.Label4.Visible = True
        '    YZCode.Focus()
        '    Exit Sub
        'End If

        cWebUser = Replace(email.Text, " ", "")
        cWebUser = Replace(email.Text, "'", "")

        cWebPassword = Replace(password.Text, " ", "")
        cWebPassword = Replace(password.Text, "'", "")

        '连接数据库文档，如不能成功则不执行
        If ReadSystemSet() = False Then Exit Sub


        If bMLink = False Then

            bMLink = main01()

        End If

        rs = cPConn.Execute("Select * from dbUserview where cName='" & cWebUser & "'")

        If rs.RecordCount = 0 Then


            Me.Label1.Visible = False
            Me.Label2.Visible = False
            Me.Label3.Visible = True
            'Me.Label4.Visible = False
            Me.email.Focus()
            'LinkButton1_Click(sender, e)
            Exit Sub

        Else

            If rs.Fields("cPass").Value <> DigestStrToHexStr(cWebPassword) Then



                Me.Label1.Visible = False
                Me.Label2.Visible = False
                Me.Label3.Visible = True
                'Me.Label4.Visible = False
                Me.password.Focus()
                'LinkButton1_Click(sender, e)
                Exit Sub
            Else

                'cPCurUser = cWebUser

                '当前用户
                Session("userid") = cWebUser
                '当前用户编码
                Session("usercode") = rs.Fields("cCode").Value
                'SQL条件
                Session("sWhere") = ""

                '是否有审核限权
                Session("bSH") = False

                '是否有录入权限
                Session（"bLR"） = False

                '当前用户


                If Left(Session("usercode"), 2) = "DB" Then
                    Session("username") = rs.Fields("DDJGMC").Value
                    Session("sWhere") = " where DDJGMC='" & Session("username") & "' "
                    Session("bLR") = True

                ElseIf Left(Session("usercode"), 2) = "GB" Then
                    Session("username") = rs.Fields("GLJGMC").Value
                    Session("sWhere") = " where GLJGMC='" & Session("username") & "' "
                    Session("bSH") = True

                Else
                    Session("username") = "超级管理员"
                    Session("bSH") = True
                    Session("bLR") = True
                End If



                '项目编码
                Session("Code") = ""
                    '初始
                    Session("bMLoadCode") = 0
                End If

                cPLogin = True
            rs.Close()
            rs = Nothing

            Response.Redirect("Index.aspx")

errhand:
            If Err.Number <> 0 Then
                Select Case Err.Number
                    Case Else
                        'MsgBox("错 误 号：" & Err.Number & vbCrLf & _
                        '"错误信息：" & Err.Description & vbCr & _
                        '       vbTab & "    录入的查询条件不正确，请重试！" & _
                        '  "", vbInformation, "提示")
                        'bMLink = False
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>alert('" & Err.Description & "！')</script>")
                        Me.Label1.Visible = False
                        Me.Label2.Visible = False
                        Me.Label3.Visible = False
                        'Me.Label4.Visible = False
                        Exit Sub
                End Select
            End If
        End If
    End Sub
End Class
