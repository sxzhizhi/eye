<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">




<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>

        <!--[if lt IE 9]>
    <script src='assets/javascripts/html5shiv.js' type='text/javascript'></script>
    <![endif]-->
    <link href='assets/stylesheets/bootstrap/bootstrap.css' media='all' rel='stylesheet' type='text/css' />
    <link href='assets/stylesheets/bootstrap/bootstrap-responsive.css' media='all' rel='stylesheet' type='text/css' />


    <!-- / flatty theme -->
    <link href='assets/stylesheets/light-theme.css'  media='all' rel='stylesheet' type='text/css' />





    <title>系统登录-白内障复明手术救助项目管理系统</title>
</head>
<body class='contrast-blue sign-in contrast-background'>
    
        <div id='wrapper'>
    <div class='application'>
        <div class='application-content'>
            <a href="#">
                <span>白内障复明手术救助项目管理系统</span>
            </a>
        </div>
    </div>
    <div class='controls'>
        <div class='caret'></div>
        <div class='form-wrapper'>
            <h1 class='text-center'></h1>
            <form id="form1" runat="server"><div style="margin:0;padding:0;display:inline"><input name="utf8" type="hidden" value="&#x2713;" /></div>
                <div class='row-fluid'>
                    <div class='span12 icon-over-input'>
                        <asp:TextBox ID="email" runat="server" CssClass ="span12" placeholder="账号"></asp:TextBox>
                        
                        <i class='icon-user muted'></i>
                    </div>
                </div>
                <div class='row-fluid'>
                    <div class='span12 icon-over-input'>
                        <asp:TextBox ID="password" runat="server" CssClass ="span12" placeholder="密码" type="password" ></asp:TextBox>
                        
                        <i class='icon-lock muted'></i>
                    </div>
                </div>
                <label class="checkbox" for="remember_me">
                    
                    <asp:Label ID="Label1" runat="server" Text="请输入账号" Visible="False" ForeColor="#FF6666"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="请输入密码" Visible="False" ForeColor="#FF6666"></asp:Label>
                    <asp:Label ID="Label3" runat="server" Text="帐号或密码错误，请重新输入" Visible="False" ForeColor="#FF6666"></asp:Label>
                    
                </label>
                
                <asp:Button  CssClass ="btn btn-block" ID="Button1" runat="server" Text="登 录" />
            </form>
            <div class='text-center'>
                <hr class='hr-normal' />
                <a href="forgot_password.html"></a>
            </div>
        </div>
    </div>
    <div class='login-action text-center'>
        <a href="#">
            Copyright © 2017
            <strong></strong>
        </a>
    </div>
</div>
       


    
</body>

</html>
