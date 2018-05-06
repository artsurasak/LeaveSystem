using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace IS.Page
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getDDLYears();
                setDefaultMonth();
            }
        }

        private void setDefaultMonth()
        {
            string Years = ddlYears.SelectedValue.Substring(2,2);
            M1.Text = "เมษายน";
            M2.Text = "พฤษภาคม";
            M3.Text = "มิถุนายน";
            M4.Text = "กรกฏาคม";
            M5.Text = "สิงหาคม";
            M6.Text = "กันยายน";
            VacationPeriod.Text = "เม.ย " + Years + " - ก.ย " + Years;
            HeaderPeriodMonth.Text = "ตั้งแต่เดือน 1 เมษายน " + ddlYears.SelectedValue + " - 30 กันยายน " + ddlYears.SelectedValue;
        }

        private void getDDLYears() {
            int Years;
            ListItem li;
            Years = DateTime.Now.Year;
            for (int i = 0; i < 5; i++) {
                li = new ListItem((Years + i).ToString(), (Years + i).ToString());
                ddlYears.Items.Add(li);
            }
        }

        private void getDataList() {
            string sql;
            int intval = 0;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "with reqList as ( ";
            sql += "select convert(varchar(5),sum(NO_LEAVE)) + '/' + convert(varchar(5),COUNT(LEAVETYPE_ID)) as NoLeave ,YEAR(FROM_LEAVE_DATE) as yLeaveDate,MONTH(FROM_LEAVE_DATE) as mLeaveDate , CREATE_BY ";
            sql += "from [LEAVE].[dbo].[REQUEST_LEAVE] "; 
            sql += "where LEAVETYPE_ID  = '1' ";
            sql += "and REQ_CONFIRM = 'true' ";
            if (rblPeriod.SelectedValue == "2") sql += "and MONTH(FROM_LEAVE_DATE) in (1,2,3,10,11,12)";
            else if (rblPeriod.SelectedValue == "1") sql += "and MONTH(FROM_LEAVE_DATE) in (4,5,6,7,8,9)";
            sql += "and YEAR(FROM_LEAVE_DATE) = '" + ddlYears.SelectedValue +"'";
            sql += "group by CREATE_BY , LEAVETYPE_ID , YEAR(FROM_LEAVE_DATE) , MONTH(FROM_LEAVE_DATE) ";
            sql += ") ";
            sql += "SELECT distinct usr.FIRST_NAME + ' ' + usr.LAST_NAME as Name  , usrRole.ROLE_NAME as Position , req.CREATE_BY , ";
            if (rblPeriod.SelectedValue == "1") {
                sql += "(select NoLeave from reqList where mLeaveDate = '4' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true' ) as Time1, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '5' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time2, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '6' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time3, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '7' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time4, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '8' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time5, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '9' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time6, ";
            }
            else if (rblPeriod.SelectedValue == "2") {
                sql += "(select NoLeave from reqList where mLeaveDate = '10' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time1, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '11' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time2, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '12' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time3, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '1' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time4, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '2' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time5, ";
                sql += "(select NoLeave from reqList where mLeaveDate = '3' and CREATE_BY = req.CREATE_BY and REQ_CONFIRM = 'true') as Time6, ";
            }
            sql += "(select convert(varchar(5),sum(NO_LEAVE)) + '/' + convert(varchar(5),COUNT(LEAVETYPE_ID)) from [LEAVE].[dbo].[REQUEST_LEAVE] ";
            sql += "where LEAVETYPE_ID  = '1' ";
            sql += "and CREATE_BY = req.CREATE_BY ";
            sql += "and REQ_CONFIRM = 'true' ";
            if (rblPeriod.SelectedValue == "2") sql += "and MONTH(FROM_LEAVE_DATE) in (1,2,3,10,11,12)";
            else if (rblPeriod.SelectedValue == "1") sql += "and MONTH(FROM_LEAVE_DATE) in (4,5,6,7,8,9)";
            sql += "group by CREATE_BY , LEAVETYPE_ID)  as TotalLeave , "; 
            sql += "(select convert(varchar(5),COUNT(LEAVETYPE_ID)) + '/' + convert(varchar(5),sum(NO_LEAVE)) as NoLeave ";
            sql += "from [LEAVE].[dbo].[REQUEST_LEAVE] ";
            sql += "where LEAVETYPE_ID  = '2' ";
            sql += "and CREATE_BY = req.CREATE_BY ";
            sql += "and REQ_CONFIRM = 'true' ";
            if (rblPeriod.SelectedValue == "2") sql += "and MONTH(FROM_LEAVE_DATE) in (1,2,3,10,11,12)";
            else if (rblPeriod.SelectedValue == "1") sql += "and MONTH(FROM_LEAVE_DATE) in (4,5,6,7,8,9)";
            sql += "and YEAR(FROM_LEAVE_DATE) = '" + ddlYears.SelectedValue + "'";
            sql += "group by CREATE_BY , LEAVETYPE_ID) as Errand, ";
            sql += "(select convert(varchar(5),sum(NO_LEAVE)) + '/' + convert(varchar(5),COUNT(LEAVETYPE_ID)) as NoLeave ";
            sql += " from [LEAVE].[dbo].[REQUEST_LEAVE] ";
            sql += "where LEAVETYPE_ID  = '3' ";
            sql += "and CREATE_BY = req.CREATE_BY ";
            sql += "and REQ_CONFIRM = 'true' ";
            if (rblPeriod.SelectedValue == "2") sql += "and MONTH(FROM_LEAVE_DATE) in (1,2,3,10,11,12)";
            else if (rblPeriod.SelectedValue == "1") sql += "and MONTH(FROM_LEAVE_DATE) in (4,5,6,7,8,9)";
            sql += "and YEAR(FROM_LEAVE_DATE) = '" + ddlYears.SelectedValue + "'";
            sql += "group by CREATE_BY , LEAVETYPE_ID ";
            sql += ") as Vacation ";
            sql += "FROM [LEAVE].[dbo].[REQUEST_LEAVE] req , ";
            sql += "[LEAVE].[dbo].[USER] usr , ";
            sql += "[LEAVE].[dbo].[USER_ROLE] usrRole ";
            sql += "where req.CREATE_BY = usr.EMP_CODE ";
            sql += "and usr.ROLE_ID = usrRole.ROLE_ID ";
            sql += "and REQ_CONFIRM = 'true'";
            if (rblPeriod.SelectedValue == "2") sql += "and MONTH(FROM_LEAVE_DATE) in (1,2,3,10,11,12)";
            else if (rblPeriod.SelectedValue == "1") sql += "and MONTH(FROM_LEAVE_DATE) in (4,5,6,7,8,9)";
            sql += "and YEAR(FROM_LEAVE_DATE) = '" + ddlYears.SelectedValue + "'";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0) {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                    intval += 1;
                    tblist.Rows.Add(genTableRow(intval, 
                                                ds.Tables[0].Rows[i]["Name"].ToString(),
                                                ds.Tables[0].Rows[i]["Position"].ToString(),
                                                ds.Tables[0].Rows[i]["Time1"].ToString(),
                                                ds.Tables[0].Rows[i]["Time2"].ToString(),
                                                ds.Tables[0].Rows[i]["Time3"].ToString(),
                                                ds.Tables[0].Rows[i]["Time4"].ToString(),
                                                ds.Tables[0].Rows[i]["Time5"].ToString(),
                                                ds.Tables[0].Rows[i]["Time6"].ToString(),
                                                ds.Tables[0].Rows[i]["TotalLeave"].ToString(),
                                                ds.Tables[0].Rows[i]["Errand"].ToString(),
                                                ds.Tables[0].Rows[i]["Vacation"].ToString()
                                                ));
                }
            }
            else
            {
                tblist.Rows.Add(genTableRowEmpty()); 
            }

            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            divContainData.RenderControl(htmlWrite);
            Session["dataList"] = stringWrite.ToString();

        }

        private TableRow genTableRowEmpty() {
            TableRow tr = new TableRow();
            TableCell td = new TableCell();

            tr = new TableRow();
            td = new TableCell();
            td.Text = "Data Not Found";
            td.ColumnSpan = 12;
            td.ForeColor = Color.Red;
            td.HorizontalAlign = HorizontalAlign.Center;
            tr.Cells.Add(td);
            return tr;
        }

        private TableRow genTableRow(int seq, string Name, string Position, string Time1, string Time2, string Time3, string Time4, string Time5, string Time6,
                                    string Total, string Errand, string Vacation)
        {
            TableRow tr = new TableRow();
            TableCell td = new TableCell();
            
            //tr = new TableRow();
            td = new TableCell();
            td.Text = seq.ToString();
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Name;
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Position;
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Time1;
            td.Style.Add("mso-number-format", @"\@");
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Time2;
            td.Style.Add("mso-number-format", @"\@");
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Time3;
            td.Style.Add("mso-number-format", @"\@");
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Time4;
            td.Style.Add("mso-number-format", @"\@");
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Time5;
            td.Style.Add("mso-number-format", @"\@");
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Time6;
            td.Style.Add("mso-number-format", @"\@");
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Total;
            td.Style.Add("mso-number-format", @"\@");
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Errand;
            td.Style.Add("mso-number-format", @"\@");
            tr.Cells.Add(td);

            td = new TableCell();
            td.Text = Vacation;
            td.Style.Add("mso-number-format", @"\@");
            tr.Cells.Add(td);

            return tr;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            setNameMonth();
            getDataList();
        }

        private void setNameMonth() {
            string Years = ddlYears.SelectedValue.Substring(2,2);
            if (rblPeriod.SelectedValue == "1") {
                M1.Text = "เมษายน";
                M2.Text = "พฤษภาคม";
                M3.Text = "มิถุนายน";
                M4.Text = "กรกฏาคม";
                M5.Text = "สิงหาคม";
                M6.Text = "กันยายน";
                VacationPeriod.Text = "เม.ย " + Years + " - ก.ย " + Years;
                HeaderPeriodMonth.Text = "ตั้งแต่เดือน 1 เมษายน " + ddlYears.SelectedValue + " - 30 กันยายน " + ddlYears.SelectedValue;
            }
            else if (rblPeriod.SelectedValue == "2") {
                M1.Text = "ตุลาคม";
                M2.Text = "พฤศจิกายน";
                M3.Text = "ธันวาคม";
                M4.Text = "มกราคม";
                M5.Text = "กุมภาพันธ์";
                M6.Text = "มีนาคม";
                VacationPeriod.Text = "ต.ค " + Years + " - มี.ค " + Years;
                HeaderPeriodMonth.Text = "ตั้งแต่เดือน 1 ตุลาคม " + ddlYears.SelectedValue + " - 31 มีนาคม " + ddlYears.SelectedValue;
            }
            
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(Session["dataList"]);
            Response.End();
        }
    }
}