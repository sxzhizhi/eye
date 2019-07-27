<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frm_HZXX.aspx.vb" Inherits="frm_HZXX" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>年度信息</title>
    <base target="_self" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="" />
    <!-- Bootstrap Styles-->
    <link href="css/bootstrap.css" rel="stylesheet" />
    

    <style type="text/css">
        .clearfix:after, .container:after, .tab-nav:after {
            content: ".";
            display: block;
            height: 0;
            clear: both;
            visibility: hidden;
        }

        /* ==========
	   Setup Page */
        *, *:before, *:after {
            box-sizing: border-box;
        }

        body_tab {
            font-family: 'Quicksand', sans-serif;
        }

        /* =================
	   Container Styling */
        .container {
            top:-30px;
            position: relative;
            background: white;
            padding: 1em;
            left: 0px;
        }

        /* ===========
	   Tab Styling */
        .tab-group {
            position: relative;
            border: 1px solid #eee;
            margin-top: 2.5em;
            border-radius: 0 0 25px 25px;
        }

            .tab-group section {
                opacity: 0;
                height: 0;
                padding: 0 1em;
                overflow: hidden;
                transition: opacity 0.4s ease, height 0.4s ease;
            }

                .tab-group section.active {
                    opacity: 1;
                    height: auto;
                    overflow: visible;
                }

        .tab-nav {
            list-style: none;
            margin: -2.5em -1px 0 0;
            padding: 0;
            height: 2.5em;
            overflow: hidden;
        }

            .tab-nav li {
                display: inline;
            }

                .tab-nav li a {
                    top: 1px;
                    position: relative;
                    display: block;
                    float: left;
                    border-radius: 10px 10px 0 0;
                    background: #eee;
                    line-height: 2em;
                    padding: 0 1em;
                    text-decoration: none;
                    color: grey;
                    margin-top: .5em;
                    margin-right: 1px;
                    transition: background .2s ease, line-height .2s ease, margin .2s ease;
                }

                .tab-nav li.active a {
                    background: #1B93E1;
                    color: white;
                    line-height: 2.5em;
                    margin-top: 0;
                }
    </style>
    <style type="text/css">
        .body {
            margin: 10px auto;
            overflow: hidden;
            position: relative;
            z-index: 2;
            background-color: #ffffff;
            width: 98%;
        }

        .butNav1 {
            right: 120px;
            text-align: right;
            top: 0px;
            width: 100px;
            position: absolute;
        }

        .butNav2 {
            right: 0px;
            text-align: right;
            top: 0px;
            width: 100px;
            position: absolute;
        }

        .butNav3 {
            right: 0px;
            text-align: right;
            top: 0px;
            width: 100px;
            position: absolute;
        }
    </style>
    <style type="text/css">
        ul, ol, li {
            list-style: none;
            padding: 0;
            margin: 0;
        }
    </style>
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
           layer.msg('数据保存成功！');
       }
       function RepeatMsg() {
           layer.msg('数据已存在！');
       }
    </script>



    <script type="text/javascript">
        function getcode() {
            function GetRequest() {
                var url = location.search; //获取url中"?"符后的字串    
                var theRequest = new Object();
                if (url.indexOf("?") != -1) {
                    var str = url.substr(1);
                    strs = str.split("&");
                    for (var i = 0; i < strs.length; i++) {
                        theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);
                    }
                }
                return theRequest;
            }


            var Request = new Object();
            Request = GetRequest();
            var a = Request["UnitCode"];
            var b = Request["UnitEdit"];


            if (a != "") {

                document.getElementById("T_code").value = a;
                document.getElementById("HiddenField1").value = a;
                document.getElementById("HiddenField2").value = b;
            }
            document.getElementById("but_LoadData").click();

        }


        function windowclose() {

            layer.close(layer.index);
           
        }
    </script>
     <!-- 时间控件 -->
    <link type ="text/css"  href="layui/css/layui.css" rel="stylesheet" media ="all"  />
    <script type ="text/javascript"  src="layui/layui.js" charset="utf-8"></script>
    <script type ="text/javascript" >
        layui.use('laydate', function(){
            var laydate = layui.laydate;
        });
        //$(function () {
        //    $("#T_zzsj").val("");
        //    $("#T_shsj").val("");
        //});
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="body">
            <div class="container">
                <div class="tab-group">
                    <div class="form-horizontal">
                        <br />
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                        <div class="form-group">
                            <label class="col-sm-2 control-label hor-form">
                                系统编码</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="T_code" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        
                          <div class="form-group">
                            <label class="col-sm-2 control-label hor-form">
                                年度*</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="cbo_ND" runat="server" CssClass="form-control"  TabIndex="1">
                                <asp:ListItem>请选择</asp:ListItem></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label hor-form">
                                患者编号*</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="T_BH" runat="server" CssClass="form-control" TabIndex="1" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                           
                        <div class="form-group">
                            <label class="col-sm-2 control-label hor-form">
                                姓名*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:TextBox ID="T_XM" runat="server" CssClass="form-control" TabIndex="1" MaxLength="10"></asp:TextBox>
                            </div>
                       
                            <label class="col-sm-2 control-label hor-form">
                                性别*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:DropDownList ID="cbo_XB" runat="server" CssClass="form-control" TabIndex="1"><asp:ListItem>男</asp:ListItem><asp:ListItem>女</asp:ListItem></asp:DropDownList>
                            </div>
                        </div>

                         <div class="form-group"  style="clear: left;">
                            <label class="col-sm-2 control-label hor-form">
                                身份证号*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:TextBox ID="T_SFZH" runat="server" CssClass="form-control" TabIndex="1" MaxLength="18"></asp:TextBox>
                            </div>
                            <label class="col-sm-2 control-label hor-form">
                                户口类型*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:DropDownList ID="cbo_HKLX" runat="server" CssClass="form-control" TabIndex="1">
                                    

                                </asp:DropDownList>
                            </div>
                       

                        </div>




                        <div class="form-group"  style="clear: left;">
                            <label class="col-sm-2 control-label hor-form">
                                联系电话*</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="T_LXDH" runat="server" CssClass="form-control" TabIndex="1" MaxLength="15"></asp:TextBox>
                            </div>
                        </div>
                                                <div class="form-group">
                            <label class="col-sm-2 control-label hor-form">
                                住址</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="T_JTZZ" runat="server" CssClass="form-control" TabIndex="1" MaxLength="30"></asp:TextBox>
                            </div>
                        </div>


                        <div class="form-group">
                            <label class="col-sm-2 control-label hor-form">
                                病因*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:DropDownList ID="cbo_BY" runat="server" CssClass="form-control" TabIndex="1">

                                </asp:DropDownList>
                            </div>
                            <label class="col-sm-2 control-label hor-form">
                                费用情况*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:DropDownList ID="cbo_FYQK" runat="server" CssClass="form-control" TabIndex="1">
                                    <asp:ListItem>全免费</asp:ListItem>
                                    <asp:ListItem>减免</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                       

                        </div>

                        <div class="form-group"   style="clear: left;">
                            <label class="col-sm-2 control-label hor-form">
                                手术眼*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:DropDownList ID="cbo_SSY" runat="server" CssClass="form-control" TabIndex="1">
                                    <asp:ListItem>左眼</asp:ListItem>
                                    <asp:ListItem>右眼</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label class="col-sm-2 control-label hor-form">
                                手术方式*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:DropDownList ID="cbo_SSFS" runat="server" CssClass="form-control" TabIndex="1">
                                    <asp:ListItem>小切口</asp:ListItem>
                                    <asp:ListItem>超乳</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                       

                        </div>






                           <div class="form-group"   style="clear: left;">
                            <label class="col-sm-2 control-label hor-form">
                                术前视力*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:TextBox ID="T_SQSL" runat="server" CssClass="form-control" TabIndex="1" MaxLength="10"></asp:TextBox>
                            </div>
                       
                            <label class="col-sm-2 control-label hor-form">
                                术后视力*</label>
                            <div class="col-sm-9" style="float: left;width :220px;">
                                <asp:TextBox ID="T_SHSL" runat="server" CssClass="form-control" TabIndex="1" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group" style="clear: left;">
                            <label class="col-sm-2 control-label hor-form">
                                手术日期*</label>
                            

                            <div class="col-sm-9">
                                
               <input id="T_SSRQ" class="layui-input" onclick="layui.laydate({ elem: this, istime: true, format: 'YYYY-MM-DD' })" runat="server" tabindex="1"/></div>
                            
                        
                            </div>
                        



                       <div class="form-group">
                            <label class="col-sm-2 control-label hor-form">
                                备注</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="T_BZ" runat="server" CssClass="form-control" TabIndex="1" MaxLength="30"></asp:TextBox>
                            </div>
                        
                        </div>
                    </div>
                </div>

            <br />
            <div class="col-sm-offset-2 col-sm-10">
                <asp:Button ID="But_Save" runat="server" Text="保存" CssClass="btn btn-success" TabIndex="2" />
                <%--<asp:Button ID="But_Close" runat="server" Text="关闭" CssClass="btn-default btn" OnClientClick="windowclose();" />--%>
                <asp:Button ID="but_LoadData" runat="server" Text="新增" CssClass="btn-inverse btn" TabIndex="3" />
            </div>


            </div>
            </div>

       
    </form>

</body>
</html>
