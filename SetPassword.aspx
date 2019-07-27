<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SetPassword.aspx.vb" Inherits="SetPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>修改密码</title>
     <base target="_self" /> 
<meta name="viewport" content="width=device-width, initial-scale=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="keywords" content="" />
<!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.css" rel="stylesheet" />

    <script src="js/jquery.js" type="text/javascript"></script>
<script src="layer/layer.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {
                layer.config({
                    extend: ['layui/css/layui.css'], //加载新皮肤
                    skin: 'layer-ext-espresso' //一旦设定，所有弹层风格都采用此主题。
                });
            });



            //弹出框
            function SaveMsg() {
                layer.msg('密码修改成功！');
            }
    </script>
    


        <script language="javascript" type="text/javascript">
            function getArguments() {
                var obj = window.dialogArguments
                
                //            alert("您传递的参数为：" + obj.value)
                if (obj.value != "") {

                    document.getElementById("Code").value = obj.value;
                    document.getElementById("HiddenField1").value = obj.value;

                }
                document.getElementById("but_LoadData").click();
                
            }
            function windowclose() {

                window.returnValue = "Default.Close";
                self.close();
            }
    </script>



<style  type="text/css">
.body{ margin:10px auto; overflow: hidden; position: relative; z-index: 2;background-color :#ffffff; width :98%;}

.butNav1 { right: 120px;text-align: right;top: 0px; width: 100px; position:absolute;}
.butNav2 { right: 0px;text-align: right;top: 0px; width: 100px; position:absolute;}
.butNav3 { right: 0px;text-align: right;top: 0px; width: 100px; position:absolute;}
</style>



</head>
<body>
    <form id="form1" runat="server">





	<div class ="body">




    <div class="form-horizontal">
  <br />
        <asp:HiddenField ID="HiddenField1" runat="server" />
  <div class="form-group">
  
    <label class="col-sm-2 control-label hor-form">旧密码</label>
    <div class="col-sm-9">
        <asp:TextBox ID="Password1" runat="server" CssClass ="form-control" placeholder="" 
            TextMode="Password" MaxLength="20" ></asp:TextBox>
    </div>
  </div>

    <div class="form-group">
  
    <label class="col-sm-2 control-label hor-form">新密码</label>
    <div class="col-sm-9">
        <asp:TextBox ID="Password2" runat="server" CssClass ="form-control" placeholder="" 
            TextMode="Password" MaxLength="20"></asp:TextBox>
    </div>
  </div>

      <div class="form-group">
  
    <label class="col-sm-2 control-label hor-form">再次确认</label><div class="col-sm-9">
        <asp:TextBox ID="Password3" runat="server" CssClass ="form-control" placeholder="" 
                  TextMode="Password" MaxLength="20"></asp:TextBox>
        </div>
          
  </div>

    



    

   


    <div class="col-sm-offset-2 col-sm-10">
        <asp:Button ID="But_Save" runat="server" Text="保存" CssClass ="btn btn-success"/>
        <asp:Button ID="But_Close" runat="server" Text="关闭" CssClass ="btn-default btn" 
            OnClientClick="windowclose();" Visible="False"  />
    </div>
    <br/>

</div>

    </div>
		
    </form>

</body>
</html>
