<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Key_Print.aspx.vb" Inherits="Key_Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>打印汇总表</title>

    <%--<link href='assets/stylesheets/bootstrap/bootstrap.css' media='all' rel='stylesheet' type='text/css' />--%>

</head>
<body>
    <form id="form1" runat="server">



        <div class='row-fluid'>







    <div class='span12 box bordered-box red-border' style='margin-bottom:0;'>
        <div class='box-header red-background'>
<%--            <div class='title'><%=Session("menu")%>

            </div>--%>


            <hr />
            <div class='actions'>
                <a href="#" class="btn box-collapse btn-mini btn-link"><i></i>
                </a>
                <asp:Label ID="Label5" runat="server" Text="年度"></asp:Label><asp:DropDownList ID="cbo_ND" runat="server" Width ="80px"></asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Text="定点机构"></asp:Label><asp:DropDownList ID="cbo_DDJG" runat="server" Width ="200px"></asp:DropDownList>

                            <asp:Button ID="but_Query" runat="server" Text="查询" CssClass="btn btn-warning" Width ="60px" Height ="25px" />

                            <input id="btnPrint" type="button" value="打印" style="width: 60px;height :25px;"></input>

                <asp:Button ID="but_Excel" runat="server" Text="导出" CssClass="btn btn-warning" Width ="60px" Height ="25px" Visible="False" />
                <input type="button" onclick="getXlsFromTbl('GridView1',null)"  style="width: 60px;height :25px;" value="导出"></input> 
            </div>

            <hr />

        </div>
                               
        <div class='box-content box-no-padding'>
                <div class=''>
                


                         



<div id="printArea">

    <style >
table {
    max-width: 100%;
    background-color: transparent;
    border-collapse: collapse;
    border-spacing: 0;
}

.table {
    width: 100%;
    margin-bottom: 20px;
}

.table th,
.table td {
    padding: 8px;
    line-height: 20px;
    text-align: left;
    vertical-align: top;
    border-top: 1px solid #dddddd;
}

.table th {
    font-weight: bold;
}

.table thead th {
    vertical-align: bottom;
}

.table caption + thead tr:first-child th,
.table caption + thead tr:first-child td,
.table colgroup + thead tr:first-child th,
.table colgroup + thead tr:first-child td,
.table thead:first-child tr:first-child th,
.table thead:first-child tr:first-child td {
    border-top: 0;
}

.table tbody + tbody {
    border-top: 2px solid #dddddd;
}

.table .table {
    background-color: #ffffff;
}

.table-condensed th,
.table-condensed td {
    padding: 4px 5px;
}

.table-bordered {
    border: 1px solid #dddddd;
    border-collapse: separate;
    *border-collapse: collapse;
    border-left: 0;
    -webkit-border-radius: 4px;
    -moz-border-radius: 4px;
    border-radius: 4px;
}

.table-bordered th,
.table-bordered td {
    border-left: 1px solid #dddddd;
}

.table-bordered caption + thead tr:first-child th,
.table-bordered caption + tbody tr:first-child th,
.table-bordered caption + tbody tr:first-child td,
.table-bordered colgroup + thead tr:first-child th,
.table-bordered colgroup + tbody tr:first-child th,
.table-bordered colgroup + tbody tr:first-child td,
.table-bordered thead:first-child tr:first-child th,
.table-bordered tbody:first-child tr:first-child th,
.table-bordered tbody:first-child tr:first-child td {
    border-top: 0;
}

.table-bordered thead:first-child tr:first-child > th:first-child,
.table-bordered tbody:first-child tr:first-child > td:first-child,
.table-bordered tbody:first-child tr:first-child > th:first-child {
    -webkit-border-top-left-radius: 4px;
    border-top-left-radius: 4px;
    -moz-border-radius-topleft: 4px;
}

.table-bordered thead:first-child tr:first-child > th:last-child,
.table-bordered tbody:first-child tr:first-child > td:last-child,
.table-bordered tbody:first-child tr:first-child > th:last-child {
    -webkit-border-top-right-radius: 4px;
    border-top-right-radius: 4px;
    -moz-border-radius-topright: 4px;
}

.table-bordered thead:last-child tr:last-child > th:first-child,
.table-bordered tbody:last-child tr:last-child > td:first-child,
.table-bordered tbody:last-child tr:last-child > th:first-child,
.table-bordered tfoot:last-child tr:last-child > td:first-child,
.table-bordered tfoot:last-child tr:last-child > th:first-child {
    -webkit-border-bottom-left-radius: 4px;
    border-bottom-left-radius: 4px;
    -moz-border-radius-bottomleft: 4px;
}

.table-bordered thead:last-child tr:last-child > th:last-child,
.table-bordered tbody:last-child tr:last-child > td:last-child,
.table-bordered tbody:last-child tr:last-child > th:last-child,
.table-bordered tfoot:last-child tr:last-child > td:last-child,
.table-bordered tfoot:last-child tr:last-child > th:last-child {
    -webkit-border-bottom-right-radius: 4px;
    border-bottom-right-radius: 4px;
    -moz-border-radius-bottomright: 4px;
}

.table-bordered tfoot + tbody:last-child tr:last-child td:first-child {
    -webkit-border-bottom-left-radius: 0;
    border-bottom-left-radius: 0;
    -moz-border-radius-bottomleft: 0;
}

.table-bordered tfoot + tbody:last-child tr:last-child td:last-child {
    -webkit-border-bottom-right-radius: 0;
    border-bottom-right-radius: 0;
    -moz-border-radius-bottomright: 0;
}

.table-bordered caption + thead tr:first-child th:first-child,
.table-bordered caption + tbody tr:first-child td:first-child,
.table-bordered colgroup + thead tr:first-child th:first-child,
.table-bordered colgroup + tbody tr:first-child td:first-child {
    -webkit-border-top-left-radius: 4px;
    border-top-left-radius: 4px;
    -moz-border-radius-topleft: 4px;
}

.table-bordered caption + thead tr:first-child th:last-child,
.table-bordered caption + tbody tr:first-child td:last-child,
.table-bordered colgroup + thead tr:first-child th:last-child,
.table-bordered colgroup + tbody tr:first-child td:last-child {
    -webkit-border-top-right-radius: 4px;
    border-top-right-radius: 4px;
    -moz-border-radius-topright: 4px;
}

.table-striped tbody > tr:nth-child(odd) > td,
.table-striped tbody > tr:nth-child(odd) > th {
    background-color: #f9f9f9;
}

.table-hover tbody tr:hover > td,
.table-hover tbody tr:hover > th {
    background-color: #f5f5f5;
}
 

        .auto-style1 {
            width: 100%;
        }
 

    </style>

       <% If Session("DataList") = "1" Then %>                 
                    
                        <div style="margin: 15px; font-size: x-large; text-align: center;" class="auto-style1">
                            <asp:Label ID="Label4" runat="server" Text="“复明6号”手术车贫困白内障复明救助项目手术患者汇总表"></asp:Label>
                        </div>

            <div style="float: left; margin-top :10px;margin-left :15px;margin-bottom :15px;">
                            <%=Session("PrintND") %></div>
    <div style="float: left; margin-top :10px;margin-left :35px;margin-bottom :15px;">
                            <%=Session("PrintDDJG") %>
                            
                        </div>
                             





        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" CssClass ="table table-bordered table-hover table-striped">
        <Columns>

                                    <asp:BoundField DataField="BH" HeaderText="编号" SortExpression="BH" />
            

                                                <asp:BoundField DataField="XM" HeaderText="姓名" SortExpression="XM" />
                                                <asp:BoundField DataField="XB" HeaderText="性别" SortExpression="XB" />
                           <asp:BoundField DataField="SSY" HeaderText="手术眼" SortExpression="SSY" />

                              <asp:BoundField DataField="SQSL" HeaderText="术前视力" SortExpression="SQSL" />
                              <asp:BoundField DataField="SHSL" HeaderText="术后视力" SortExpression="SHSL" />
                              <asp:BoundField DataField="DDJGMC" HeaderText="手术机构" SortExpression="DDJGMC" />
                              <asp:BoundField DataField="SFZH" HeaderText="身份证号" SortExpression="SFZH" />
                              <asp:BoundField DataField="JTZZ" HeaderText="家庭住址" SortExpression="JTZZ" />
                              <asp:BoundField DataField="LXDH" HeaderText="联系电话" SortExpression="LXDH" />

        </Columns>



        </asp:GridView>
    </div>



        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:BNZXMConnectionString %>" SelectCommand="SELECT * FROM [HZXXBView]"></asp:SqlDataSource>





                    <% Else %>
                        <div style="float: left;margin-top :25px; margin-left :15px;margin-bottom:20px;">
                            <asp:Label ID="Label1" runat="server" Text="当前没有数据！"></asp:Label>
                        </div>



                    <% End If %>



                
            </div>
        </div>
    </div>
</div>

                         
       

    </form>

    <script src="js/jquery-1.8.3.min.js"></script>
    <script src="js/jquery.PrintArea.min.js"></script>
        <script> 
  $("#btnPrint").click(function(){  
    $("#printArea").printArea(); 
  }); 
 
  $("#btnPrintFull").click(function(){  
    $("body").printArea(); 
  }); 
   
</script>
    <script>
        function getXlsFromTbl(inTblId, inWindow) {
            try {
                var allStr = "";
                var curStr = "";
                //alert("getXlsFromTbl");
                if (inTblId != null && inTblId != "" && inTblId != "null") {
                    curStr = getTblData(inTblId, inWindow);
                }
                if (curStr != null) {
                    allStr += curStr;
                }
                else {
                    alert("你要导出的表不存在！");
                    return;
                }
                var fileName = getExcelFileName();
                doFileExport(fileName, allStr);
            }
            catch (e) {
                alert("导出发生异常:" + e.name + "->" + e.description + "!");
            }
        }
        function getTblData(inTbl, inWindow) {
            var rows = 0;
            //alert("getTblData is " + inWindow);
            var tblDocument = document;
            if (!!inWindow && inWindow != "") {
                if (!document.all(inWindow)) {
                    return null;
                }
                else {
                    tblDocument = eval(inWindow).document;
                }
            }
            var curTbl = tblDocument.getElementById(inTbl);
            var outStr = "";
            if (curTbl != null) {
                for (var j = 0; j < curTbl.rows.length; j++) {
                    for (var i = 0; i < curTbl.rows[j].cells.length; i++) {
                        if (i == 0 && rows > 0) {
                            outStr += " \t";
                            rows -= 1;
                        }
                        outStr += curTbl.rows[j].cells[i].innerText + "\t";
                        if (curTbl.rows[j].cells[i].colSpan > 1) {
                            for (var k = 0; k < curTbl.rows[j].cells[i].colSpan - 1; k++) {
                                outStr += " \t";
                            }
                        }
                        if (i == 0) {
                            if (rows == 0 && curTbl.rows[j].cells[i].rowSpan > 1) {
                                rows = curTbl.rows[j].cells[i].rowSpan - 1;
                            }
                        }
                    }
                    outStr += "\r\n";
                }
            }
            else {
                outStr = null;
                alert(inTbl + "不存在!");
            }
            return outStr;
        }
        function getExcelFileName() {
            var d = new Date();
            var curYear = d.getYear();
            var curMonth = "" + (d.getMonth() + 1);
            var curDate = "" + d.getDate();
            var curHour = "" + d.getHours();
            var curMinute = "" + d.getMinutes();
            var curSecond = "" + d.getSeconds();
            if (curMonth.length == 1) {
                curMonth = "0" + curMonth;
            }
            if (curDate.length == 1) {
                curDate = "0" + curDate;
            }
            if (curHour.length == 1) {
                curHour = "0" + curHour;
            }
            if (curMinute.length == 1) {
                curMinute = "0" + curMinute;
            }
            if (curSecond.length == 1) {
                curSecond = "0" + curSecond;
            }
            //var fileName = "数据导出" + "_" + curYear + curMonth + curDate + "_"
            //        + curHour + curMinute + curSecond + ".xls";
            var fileName = "数据导出" +  ".xls";
            return fileName;
        }
        function doFileExport(inName, inStr) {
            var xlsWin = null;
            if (!!document.all("glbHideFrm")) {
                xlsWin = glbHideFrm;
            }
            else {
                var width = 10;
                var height = 10;
                var openPara = "left=" + (window.screen.width / 2 - width / 2)
                        + ",top=" + (window.screen.height / 2 - height / 2)
                        + ",scrollbars=no,width=" + width + ",height=" + height;
                xlsWin = window.open("", "_blank", openPara);

            }
            xlsWin.document.write(inStr);
            xlsWin.document.close();
            xlsWin.document.execCommand('Saveas', true, inName);
            xlsWin.close();
        }

        </script>

</body>
</html>
