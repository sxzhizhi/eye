<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Key_TJ.aspx.vb" Inherits="Key_TJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/jquery_1.8.3.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            var dEye;
            $("#chaxun").click(function () {
                var data = "{str:'" + $("#cbo_ND").val() + "'}";
                $.ajax({
                    type: "Post", //要用post方式   
                    url: "Key_TJ.aspx/EyeData", //方法所在页面和方法名
                    contentType: "application/json; charset=utf-8",
                    data: data, //方法传参的写法一定要对，str为形参的名字,str2为第二个形参的名字, data: "{'str1':'参数值1','str2':'参数值2'}",
                    dataType: "json",
                    success: function (data) {
                        if (data.d === "没有数据!") {
                            alert(data.d)
                        } else if (data.d === "请选择日期!") {
                            alert(data.d)
                        } else {

                            dEye = JSON.parse(data.d);

                            (function () {
                                var data, dataset, gd, options, previousLabel, previousPoint, showTooltip, ticks;

                                $(document).ready(function () {
                                    var blue, data, datareal, getRandomData, green, i, newOrders, options, orange, orders, placeholder, plot, purple, randNumber, randSmallerNumber, red, series, totalPoints, update, updateInterval;

                                    red = "#f34541";
                                    orange = "#f8a326";
                                    blue = "#00acec";
                                    purple = "#9564e2";
                                    green = "#49bf67";
                                    randNumber = function () {
                                        return ((Math.floor(Math.random() * (1 + 50 - 20))) + 20) * 800;
                                    };
                                    randSmallerNumber = function () {
                                        return ((Math.floor(Math.random() * (1 + 40 - 20))) + 10) * 200;
                                    };

                                    if ($("#stats-chart8").length !== 0) {
                                        data = [];
                                        series = dEye.length/2;
                                        i = 0;
                                        j = 0;
                                        while (i < series) {
                                            data[i] = {
                                                label: dEye[j] + '—' + dEye[j + 1] + '例',
                                                data: parseInt(dEye[j + 1])
                                            };
                                            j = j + 2;
                                            i++;
                                        }
                                        placeholder = $("#stats-chart8");
                                        return $.plot(placeholder, data, {
                                            series: {
                                                pie: {
                                                    show: true
                                                }
                                            }
                                        });
                                    }
                                });



                            }).call(this);
                            




                        }//if的尾括号
                    },
                    error: function (err) {
                        alert(err);
                    }
                });
                return false; //禁用按钮的提交

            });

        });








      


        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    

    <div class='row-fluid'>
                    <div class='span12 box'>
                        <div class='box-header'>
                            <div class='title'>
                                定点机构工作量统计分析
                            </div>
                            <div class='actions'>
                                <a href="#" class="btn box-remove btn-mini btn-link"><i class='icon-remove'></i>
                                </a>
                                <a href="#" class="btn box-collapse btn-mini btn-link"><i></i>
                                </a>
                            </div>
                        </div>
                        <div class='box-content'>
                            <div class='row-fluid'>
                               <div class='span4'>
                                   <div class="form-group">
                            <label class="col-sm-2 control-label hor-form">
                                年度</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="cbo_ND" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                                
                            </div>
                        </div>
                                   <div class="form-group">
                            <label class="col-sm-2 control-label hor-form">
                                定点机构</label>
                            <div class="col-sm-9">
                                <asp:DropDownList ID="cbo_DDJG" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                                
                            </div>
                        </div>
                             
                            <div class="form-group">

                             <div class="col-sm-9">
                                <asp:RadioButton ID="R_1" runat="server" CssClass="form-control" Text="完成数" AutoPostBack="True" Visible="True" Checked="True" />
                            </div>

                            <div class="col-sm-9">
                                <asp:RadioButton ID="R_2" runat="server" CssClass="form-control" Text="手术方式" AutoPostBack="True" />
                            </div>
                                
                           
                                <div class="col-sm-9">
                                <asp:RadioButton ID="R_3" runat="server" CssClass="form-control" Text="费用情况" AutoPostBack="True" />
                            </div>

                                <div class="col-sm-9">
                                <asp:RadioButton ID="R_4" runat="server" CssClass="form-control" Text="男女比例" AutoPostBack="True" />
                            </div>
                        </div>
                                   <input type ="button" id="chaxun" value ="统计" class="btn btn-info" />
                                    <div class='text-center'>
                                        <div id=''></div>
                                    </div>
                                </div>
                                <div class='span4'>
                                    <div class='text-center'>
                                        <div id='stats-chart8'></div>
                                    </div>
                                </div>
                                <div class='span4'>
                                    <div class='text-center'>
                                        <div id=''></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

    



</asp:Content>


