<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Key_SH.aspx.vb" Inherits="Key_SH" %>

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
    <div class='span12 box bordered-box red-border' style='margin-bottom:0;'>
        <div class='box-header red-background'>
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
                            <asp:Button ID="UNSH_obj" runat="server" Text="取消审核" CssClass="btn btn-danger"  Width ="80px" />
                                
                        </div>
                         <div style="float: right; margin-right: 20px; margin-top :20px;">
                              
                            <asp:Button ID="SH_obj" runat="server" Text="审核" CssClass="btn btn-success"  Width ="80px" />
                             
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




        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass ="table table-bordered table-hover table-striped" AllowPaging="True">
        <Columns>
                              <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"
                                    ItemStyle-Width="50px">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></ItemStyle>
                                </asp:TemplateField>
            <asp:BoundField DataField="Code" HeaderText="系统编码" SortExpression="Code" />

                                    <asp:BoundField DataField="XM" HeaderText="姓名" SortExpression="XM" />
            

                                                <asp:BoundField DataField="XB" HeaderText="性别" SortExpression="XB" />
                                                <asp:BoundField DataField="SFZH" HeaderText="身份证号" SortExpression="SFZH" />
                                                <asp:BoundField DataField="LXDH" HeaderText="联系电话" SortExpression="LXDH" />
                                                <asp:BoundField DataField="JTZZ" HeaderText="家庭住址" SortExpression="JTZZ" />
                           <asp:BoundField DataField="SSY" HeaderText="手术眼" SortExpression="SSY" />
                           <asp:BoundField DataField="SSFS" HeaderText="手术方式" SortExpression="SSFS" />
                           <asp:BoundField DataField="SSRQ" HeaderText="手术日期" SortExpression="SSRQ" DataFormatString=" {0:D}" />
                           <asp:BoundField DataField="DDJGMC" HeaderText="定点机构名称" SortExpression="DDJGMC" />

            <asp:CheckBoxField  DataField="SH" HeaderText="审核状态" SortExpression="SH" />

        </Columns>



        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BNZXMConnectionString %>" SelectCommand="SELECT * FROM [HZXXBView]"></asp:SqlDataSource>



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

