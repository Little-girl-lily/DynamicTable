<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanTable.aspx.cs" Inherits="PlanTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安排计划表</title>
    <link href="LgCss/style.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        div.wrap{width:100%; overflow:hidden; _zoom:1; margin:0 auto;}
        .wrap ul,li{margin:0; padding:0; list-style:none;}
        .wrap ul{float:left; display:inline; width:210px;text-align:center;}
        .wrap ul li{padding:5px; background:#eee; margin:8px; box-shadow:0px 0px 0px #333;
        border:1px solid #808080;border-radius:10px;height:100px}
        /*.wrap div{width:100%; font-size:17px;color:#27A9F4;}*/
        .wrap p{text-align:center;font-size:30px;height:70xp; line-height:70px; vertical-align:middle;font-weight:bold}
        table,table tr th, table tr td { border:1px solid #BABDBF; }
        table tr td { width:30px;text-align:center; }
    </style>
    <script language="javascript" type="text/javascript">
    </script>
    
</head>
<body>
 <div class="place">
    <span>位置：</span>
    <ul class="placeul">
        <li><a href="#">配置页面</a></li>
    </ul>
 </div>
 <form id="form2" runat="server" class="">
     <div style="float:right;margin:10px 20px 10px 0px;"><%=Year%>年
         <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem Text="一月" Value="1"></asp:ListItem>
            <asp:ListItem Text="二月" Value="2"></asp:ListItem>
            <asp:ListItem Text="三月" Value="3"></asp:ListItem>
            <asp:ListItem Text="四月" Value="4"></asp:ListItem>
            <asp:ListItem Text="五月" Value="5"></asp:ListItem>
            <asp:ListItem Text="六月" Value="6"></asp:ListItem>
            <asp:ListItem Text="七月" Value="7"></asp:ListItem>
            <asp:ListItem Text="八月" Value="8"></asp:ListItem>
            <asp:ListItem Text="九月" Value="9"></asp:ListItem>
            <asp:ListItem Text="十月" Value="10"></asp:ListItem>
            <asp:ListItem Text="十一月" Value="11"></asp:ListItem>
            <asp:ListItem Text="十二月" Value="12"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div><%=addHtml%></div>
    <div id="ReplacePoint" runat="server">
       <table width="100%"> 
        <tr> 
           <th>日期</th> 
             <% for (int i = 0; i < ClassIdArr.Length; i++){%>
            <th><%=ClassNameArr[i]%> </th> 
            <% } %>
        </tr> 
        <% for (int j = 0; j < dateCount; j++){%>
        <tr> 
            <td><%=XMonth%>月<%=(j + 1)%>日</td> 
            <% for (int k = 0; k < ClassIdArr.Length; k++){%>
            
                <td id="<%=ClassIdArr[k]%>"> 
                <table> 
                <tr> 
                <td>00:00~08:00</td> 
               <%string Temp1 = (j + 1).ToString() + "-" + 1;%>
               <%string Utemp1 = ClassIdArr[k] + "-" + (j + 1).ToString() + "-" + 1;%>
               <%if(OrNotCho(j + 1, ClassIdArr[k], "1")){%>
                    <td><input type='radio' name="<%=Temp1%>" value="<%=Utemp1%>" checked='checked'/></td> 
               <%}%>
                <% else{%>
                
                    <td><input type='radio' name="<%=Temp1%>" value="<%=Utemp1%>" /></td> 
                <%}%>
                </tr> 
                 <tr> 
                <td>00:08~08:16</td> 
                <% string Temp2 = (j + 1).ToString() + "-" + 2;%>
                <% string Utemp2 = ClassIdArr[k] + "-" + (j + 1).ToString() + "-" + 2;%>
               <% if (OrNotCho(j + 1, ClassIdArr[k], "2")){%>
                    <td><input type='radio' name="<%=Temp2%>" value="<%=Utemp2%>" checked='checked'/></td> 
               <%}%>
                <% else{%>
                    <td><input type='radio' name="<%=Temp2%>" value="<%=Utemp2%>" /></td> 
                <%}%>
                </tr> 
                  <tr> 
                <td>16:00~24:00</td> 
                <% string Temp3 = (j + 1).ToString() + "-" + 3;%>
                <% string Utemp3 = ClassIdArr[k] + "-" + (j + 1).ToString() + "-" + 3;%>
               <%  if (OrNotCho(j + 1, ClassIdArr[k], "3")){%>
                
                    <td><input type='radio' name="<%=Temp3%>" value="<%=Utemp3%>" checked='checked'/></td> 
               <%}%>
                <% else{%>
                
                    <td><input type='radio' name="<%=Temp3%>" value="<%=Utemp3%>" /></td> 
                <%}%>
                </tr> 
                </table> 
                </td> 
            <%}%>
            </tr> 
        <%}%>
        </table> 
        <asp:Button style="margin: 10px auto;width: 10%;float:right;" ID="Button1" runat="server" CssClass="ibtn" Text="确认提交" OnClick="btn_Click"
                                                UseSubmitBehavior="false" />
   </div>
   </form>
</body>
</html>
