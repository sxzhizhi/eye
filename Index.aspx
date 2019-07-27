<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Index.aspx.vb" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
           <style type="text/css" >

    .but1{ height: 130px;margin:5px auto; overflow: hidden; position: relative; z-index: 2;}
    .buttsxx { left: 5px;text-align: left;top: 0px; width: 100px;height: 95px; position:absolute;}
    .buttsxxname { left: 5px;text-align: center;top: 96px; width: 100px; position:absolute;}
    .butjsxx { left: 135px;text-align: left;top: 3px; width: 100px;height: 95px; position:absolute;}
    .butjsxxname { left: 135px;text-align: center;top: 96px; width: 100px; position:absolute;}
    .buthsxx { left: 265px;text-align: center;top: 3px; width: 100px; position:absolute;}
    .buthsxxname { left: 265px;text-align: center;top: 96px; width: 100px; position:absolute;}
    .buttslb { left: 395px;text-align: center;top: 3px; width: 100px; position:absolute;}
    .buttslbname { left: 395px;text-align: center;top: 96px; width: 100px; position:absolute;}
    .butryxx { left: 525px;text-align: center;top: 3px; width: 100px; position:absolute;}
    .butryxxname { left: 525px;text-align: center;top: 96px; width: 100px; position:absolute;}
    .butyhgl { left: 655px;text-align: center;top: 3px; width: 100px; position:absolute;}
    .butyhglname { left: 655px;text-align: center;top: 96px; width: 100px; position:absolute;}
            img:hover {
                box-shadow:0px 0px 0px #666
            }
            </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class='alert alert-info'>
    <a class='close' data-dismiss='alert' href='#'>&times;</a>
    欢迎使用
    <strong>白内障复明手术项目管理系统</strong>
    - 单击
    <i class='icon-rss'></i>
    查看需要管理的信息.
    </div>

<div class='row-fluid'>
    <div class='span12 box box-transparent'>
        <div class='row-fluid'>
            <div class='span2 box-quick-link blue-background'>
                <a href='Frm_XXXG.aspx'>
                    <div class='header'>
                        <div class='icon-comments'></div>
                    </div>
                    <div class='content'>信息录入</div>
                </a>
            </div>
<%--            <div class='span2 box-quick-link green-background'>
                <a href='#'>
                    <div class='header'>
                        <div class='icon-star'></div>
                    </div>
                    <div class='content'>信息修改</div>
                </a>
            </div>--%>
            <div class='span2 box-quick-link orange-background'>
                <a href='Frm_XXCX.aspx'>
                    <div class='header'>
                        <div class='icon-magic'></div>
                    </div>
                    <div class='content'>信息查询</div>
                </a>
            </div>


            <%If Session("bSH") = True Then %>
            <div class='span2 box-quick-link purple-background'>
                <a href='Key_SH.aspx'>
                    <div class='header'>
                        <div class='icon-eye-open'></div>
                    </div>
                    <div class='content'>审核</div>
                </a>
            </div>
            <% End if %>
            <div class='span2 box-quick-link red-background'>
                <a href='Key_TJ.aspx'>
                    <div class='header'>
                        <div class='icon-inbox'></div>
                    </div>
                    <div class='content'>统计分析</div>
                </a>
            </div>
<%--            <div class='span2 box-quick-link muted-background'>
                <a href='#'>
                    <div class='header'>
                        <div class='icon-refresh'></div>
                    </div>
                    <div class='content'>机构信息</div>
                </a>
            </div>--%>

        </div>
    </div>
</div>


    



    




</asp:Content>

