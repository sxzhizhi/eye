<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Key_DDJG.aspx.vb" Inherits="Key_DDJG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">




    <script src="js/jquery_1.8.3.js" type="text/javascript"></script>
   
    <script src="layer/layer.js" type="text/javascript"></script>

     <script type="text/javascript">
        $(function () {
            layer.config({
                extend: ['layui/css/layui.css'], //加载新皮肤
                skin: 'layer-ext-espresso' //一旦设定，所有弹层风格都采用此主题。
            });
        });

        //弹出框
        function WinOpen(code, edit) {
            layer.open({

                title: ['定点机构信息'],
                type: 2,
                content: "frm_DDJG.aspx?UnitCode=" + code + "&UnitEdit=" + edit,
                area: ['800px', '60%'],
                shade: 0.3,
                cancel: function (index, layero) {
                    if (confirm('确定要关闭么?')) { //只有当点击confirm框的确定时，该层才会关闭
                        //document.getElementById("ContentPlaceHolder1_Button2").click();
                        $("#ContentPlaceHolder1_but_update").click();
                        layer.close(index);

                    }
                    return false;
                }
            });  
        }
    </script>
    <script type="text/javascript">
        function del() {
            var str = "";
            if (window.confirm('您确定要删除吗？')) {
                var gv = document.getElementById("ContentPlaceHolder1_GridView1");

                for (i = 1; i < gv.rows.length; i++) {
                    var cbx = gv.rows(i).cells(0).children(0)
                    if (cbx.type == 'checkbox' && cbx.checked == true) {
                        str += gv.rows(i).cells(1).innerText + ","
                    };
                }
                $.ajax({
                    type: "Post",
                    contentType: "application/json; charset=utf-8",
                    url: "Key_DDJG.aspx/del",
                    data: "{k1:'" + str + "'}",
                    datatype: "json",
                    success: function (data) {
                        document.getElementById("<%=delete_update.ClientID%>").click();
                        alert(data.d);   
                        
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
                return false;
            };
        }
    </script>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <div class='row-fluid'>
    <div class='span12 box bordered-box purple-border' style='margin-bottom:0;'>
        <div class='box-header purple-background'>
            <div class='title'><%=Session("menu")%></div>
            <div class='actions'>
                
                <a href="#" class="btn box-collapse btn-mini btn-link"><i></i>
                </a>
            </div>
        </div>
        
        <div class='box-content box-no-padding'>
                <div class=''>
                <div class='scrollable-area'>


                         


                        <div style="float:right; margin-top :20px;margin-bottom:20px; margin-right:20px;">
                            
                                <input id="new_obj" type="button" value="新增" class="btn btn-success" style="background :limegreen;color:white;width:73px; " onclick ="WinOpen('','');"/>
                        </div>
                         <div style="float: right; margin-right: 20px; margin-top :20px;">
                            <asp:Button ID="del_obj" runat="server" Text="删除" CssClass="btn btn-danger"  OnClientClick="del();"  Width ="73px" />
                         </div>
                        <div style="float: right; margin-right: 20px; margin-top :20px;">
                            <asp:Button ID="delete_obj" runat="server" Text="删除执行" CssClass="btn" style="display:none"/>
                            <asp:Button ID="but_update" runat="server" Text="刷新" Width="144px" style=" display:none"/>
                            <asp:Button ID="delete_update" runat="server" Text="刷新删除" Width="144px" style=" display:none"/>

                        </div>

                    
                    <% If Session("DataList") = "1" Then %>
                        <div style="float: left; margin-top :15px;margin-left :15px;">
                            <asp:CheckBox ID="All_CheckBox" runat="server" Text="全选" 
                                AutoPostBack="True" />
                        </div>




        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Code" DataSourceID="SqlDataSource1" CssClass ="table table-bordered table-hover table-striped" AllowPaging="True">
        <Columns>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                    ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                </asp:TemplateField>


            <asp:BoundField DataField="Code" HeaderText="系统编码" ReadOnly="True" SortExpression="Code" />
                                    <asp:BoundField DataField="GLJGMC" HeaderText="所属管理机构" SortExpression="GLJGMC" />
            <asp:BoundField DataField="DDJGMC" HeaderText="定点机构名称" SortExpression="DDJGMC" />

                                    <asp:BoundField DataField="DDJG" HeaderText="定点机构账号" SortExpression="DDJG" />
            

                                    <asp:TemplateField>
                                    <ItemTemplate>
                                        <input id="btn_modify" type="button" value="修改" style="background :deepskyblue;color:white;width:73px;" class="btn btn-primary" />
                                    </ItemTemplate>
                                    <ItemStyle Width="60px"></ItemStyle>
                                </asp:TemplateField>

        </Columns>



        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BNZXMConnectionString %>" SelectCommand="SELECT * FROM [DDJGBView]"></asp:SqlDataSource>



        <div class="but" style="float:right;">
            <div class="butNav3">
                <asp:Label ID="lblCurrentPage" runat="server"></asp:Label>
                <asp:LinkButton ID="lnkbtnFrist" runat="server">首页</asp:LinkButton>
                <asp:LinkButton ID="lnkbtnPre" runat="server">上一页</asp:LinkButton>
                <asp:LinkButton ID="lnkbtnNext" runat="server">下一页</asp:LinkButton>
                <asp:LinkButton ID="lnkbtnLast" runat="server">尾页</asp:LinkButton>
                跳转到第<asp:DropDownList ID="ddlCurrentPage" runat="server" AutoPostBack="True" CssClass="form-control2" Width="50px">
                </asp:DropDownList>
                页</div>
        </div>

                    <% Else %>
                        <div style="float: left;margin-top :25px; margin-left :15px;margin-bottom:20px;">
                            <asp:Label ID="Label1" runat="server" Text="当前没有数据！"></asp:Label>
                        </div>



                    <% End If %>



                </div>
            </div>
        </div>
    </div>
</div>



  


</asp:Content>

