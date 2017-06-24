using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using PowerDGA.BLL;
using PowerDGA.Models;
using System.Collections.Generic;
using PowerDGA.DAL;
using System.Data.SqlClient;

public partial class PlanTable : System.Web.UI.Page
{
    public int dateCount = 0;
    public static DateTime newDate = DateTime.Now;//获得当前时间
    public string[] ClassIdArr;
    public string[] ClassNameArr;
    public string addHtml = "";
    public string BeginHtml = "";
    public string Year = "";
    public string Month = "";
    public string XMonth = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // 获取一个月多少天
        dateCount = newDate.AddDays(1 - newDate.Day).AddMonths(1).AddDays(-1).Day;
        GetClass();//获取班级的个数
        Year = newDate.Year.ToString();
        Month = newDate.Month.ToString();
        
        if (!IsPostBack) 
        {
            if (this.Session["user"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            { 
                //下拉列表选中为当前月
               this.DropDownList1.Items.FindByValue(Month).Selected = true;
               XMonth = this.DropDownList1.SelectedValue;
            }
        }
    } 
    //点击获取表单选中的数据 
    protected void btn_Click(object sender, EventArgs e)
    {
        getPlanValue();
    }
    //存储表单选中的数据
    private void getPlanValue() { 
       string name="";
        int count=0;
        string Day = "";
        int ClassId = 0; 
        int q=0;
        string TimeSlot = "";
        string[] middleDataArr = new string[93 * ClassIdArr.Length]; 
        for (int k = 1; k <= dateCount; k++)
        {
          for(int t=1;t<=3;t++){
             name=k+"-"+t;
             middleDataArr[count] = Request.Form[name];
             count++;
          }
        }
        //存储之前先把当前月份的记录清除
        for (int a= 0; a < middleDataArr.Length; a++) {
           if (middleDataArr[a]!=null)
             {q++;}
        } 
        if(q>0){
           string sql = "delete from dbo.PlanTable where Month = '" + newDate.Month.ToString() + "'";
           sql = string.Format(sql, newDate.Month.ToString());
           DBHelpSQL.ExcuteSQL(sql);
            }

        for(int m=0;m <middleDataArr.Length;m++){
          if (middleDataArr[m] != null) {
           ClassId = int.Parse(middleDataArr[m].Split('-').GetValue(0).ToString());
           Day = middleDataArr[m].Split('-').GetValue(1).ToString();
           TimeSlot = middleDataArr[m].Split('-').GetValue(2).ToString();
           string sql2 = "insert into dbo.PlanTable(ClassId,TimeSlot,Year,Month,Day) values(" + ClassId + "," + TimeSlot + "," + newDate.Year.ToString() + "," + newDate.Month.ToString() + "," + Day + ")";
           sql2 = string.Format(sql2, ClassId, TimeSlot, newDate.Year.ToString(), newDate.Month.ToString(), Day);
           DBHelpSQL.ExcuteSQL(sql2);
            }
        }
    }
    //查询数据库获取班级的个数
    private void GetClass() 
    {
        string sql = "select ClassId,ClassName from dbo.Class";
        DataSet ForTable = DBHelpSQL.Query(sql);
        ClassIdArr = new string[ForTable.Tables[0].Rows.Count];
        ClassNameArr = new string[ForTable.Tables[0].Rows.Count];

        for (int i = 0; i < ForTable.Tables[0].Rows.Count; i++)
        {
            ClassIdArr[i] = ForTable.Tables[0].Rows[i]["ClassId"].ToString();
            ClassNameArr[i] = ForTable.Tables[0].Rows[i]["ClassName"].ToString();
        }
    }
    //判断是否选中
    public Boolean OrNotCho(int Day,string ClassId,string SlotNo)
    {
        string TMonth =this.DropDownList1.SelectedValue;
       string slotTime = "";
       string sql = "select TimeSlot from dbo.PlanTable where Year=" + Year + " and Month=" + TMonth + " and Day=" + Day + " and ClassId=" + ClassId;
       DataSet ForPlanTable = DBHelpSQL.Query(sql);
       if (ForPlanTable.Tables[0].Rows.Count!=0) {
           if (ForPlanTable.Tables[0].Rows.Count > 1)
           {
               for (int n = 0; n < ForPlanTable.Tables[0].Rows.Count; n++)
               {
                   slotTime += ForPlanTable.Tables[0].Rows[n]["TimeSlot"].ToString();
                   if (n < ForPlanTable.Tables[0].Rows.Count - 1) { slotTime += ","; }
               }
           }
           else { slotTime = ForPlanTable.Tables[0].Rows[0]["TimeSlot"].ToString();}
       }
       if(slotTime ==""){
            return false;
        }
        else{
           int r=slotTime.IndexOf(SlotNo);
            if (slotTime.IndexOf(SlotNo) >= 0)
                return true;
            else return false;
        }
      
    }
    //下拉选择其他月份时（只能编辑当前月）
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.DropDownList1.SelectedValue != newDate.Month.ToString())
        {
            this.ReplacePoint.Style.Value = "display:none";
            //只是查看，用*表示已选中
            addHtml += "<table width='100%'>";
            addHtml += "<tr>";
            addHtml += "   <th>序号</th>";
            for (int i = 0; i < ClassIdArr.Length; i++)
            {
                addHtml += "<th>" + ClassNameArr[i] + "</th>";
            }
            addHtml += "</tr>";
            for (int j = 0; j < dateCount; j++)
            {
                addHtml += "<tr>";
                addHtml += "<td>" + (j + 1) + "</td>";
                for (int k = 0; k < ClassIdArr.Length; k++)
                {
                    addHtml += "<td id=" + ClassIdArr[k] + ">";
                    addHtml += "<table>";
                    addHtml += "<tr>";
                    addHtml += "<td>00:00~08:00</td>";
                    if (OrNotCho(j+1, ClassIdArr[k], "1"))
                    {
                        addHtml += "<td>*</td>";
                    }
                    else
                    {
                        addHtml += "<td></td>";
                    }
                    addHtml += "</tr>";
                    addHtml += "<tr>";
                    addHtml += "<td>08:00~16:00</td>";
                    if (OrNotCho(j + 1, ClassIdArr[k], "2"))
                    {
                        addHtml += "<td>*</td>";
                    }
                    else
                    {
                        addHtml += "<td></td>";
                    }
                    addHtml += "</tr>";
                    addHtml += "<tr>";
                    addHtml += "<td>16:00~24:00</td>";
                    if (OrNotCho(j + 1, ClassIdArr[k], "3"))
                    {
                        addHtml += "<td>*</td>";
                    }
                    else
                    {
                        addHtml += "<td></td>";
                    }
                    addHtml += "</tr>";
                    addHtml += "</table>";
                    addHtml += "</td>";
                }
                addHtml += "</tr>";
            }
            addHtml += "</table>";
        }
        else {
            this.ReplacePoint.Style.Value = "display:block";  
        }
    }
}
