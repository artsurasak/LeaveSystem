using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Threading;

namespace IS.Page
{
    public partial class CreateRequestLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultureinfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureinfo;
            if (!Page.IsPostBack)
            {
                getLeaveType();
                if (Request.QueryString["Req"] != "")
                {
                    GetData();
                }
            }
        }

        private void GetData() {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds;
            sql = "select * ";
            sql += "from [REQUEST_LEAVE] ";
            sql += "where LEAVE_ID = '" + Request.QueryString["Req"] + "'";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0) {
                DateTime _FDateLeave = Convert.ToDateTime(ds.Tables[0].Rows[0]["FROM_LEAVE_DATE"]);
                TimeSpan _FTimeLeave = TimeSpan.Parse(ds.Tables[0].Rows[0]["FROM_LEAVE_TIME"].ToString());
                DateTime _TDateLeave = Convert.ToDateTime(ds.Tables[0].Rows[0]["TO_LEAVE_DATE"]);
                TimeSpan _TTimeLeave = TimeSpan.Parse(ds.Tables[0].Rows[0]["TO_LEAVE_TIME"].ToString());
                ddlLeaveType.SelectedValue = ds.Tables[0].Rows[0]["LEAVETYPE_ID"].ToString();
                txtFDateLeave.Text = _FDateLeave.ToString("dd/MM/yyyy");
                txtFTimeLeave.Text = _FTimeLeave.ToString(@"hh\:mm");
                txtTDateLeave.Text = _TDateLeave.ToString("dd/MM/yyyy");
                txtTTimeLeave.Text = _TTimeLeave.ToString(@"hh\:mm");
                txtDay.Text = ds.Tables[0].Rows[0]["NO_LEAVE"].ToString();
                txtHour.Text = ds.Tables[0].Rows[0]["NO_LEAVE_HOUR"].ToString();
                txtCauseleave.Text = ds.Tables[0].Rows[0]["NOTE"].ToString();
                txtContact.Text = ds.Tables[0].Rows[0]["CONTACT"].ToString();
                txtTelContact.Text = ds.Tables[0].Rows[0]["CONTACT_TEL"].ToString();
                hdftime.Value = txtFTimeLeave.Text;
                hdttime.Value = txtTTimeLeave.Text;
            }
        }

        private void getLeaveType()
        {
            ListItem li;
            class_is.dbconfig db = new class_is.dbconfig();
            string sql;
            DataSet ds;
            sql = "select * ";
            sql += "from [LEAVE_TYPE] ";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                    li = new ListItem(ds.Tables[0].Rows[i]["TYPE"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                    ddlLeaveType.Items.Add(li);
                }       
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (chkValidNoLeave())
            {
                if (Request.QueryString["Req"] != null)
                {
                    if (UpdateData()){ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Update Request Complete'); window.location='CreateRequestLeave.aspx';", true);}
                    else{ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Update Request Error');", true);}
                }
                else
                {
                    if (SaveData()){ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Create Request Complete'); window.location='CreateRequestLeave.aspx';", true);}
                    else { ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Create Request Error');", true); }
                }
            }
            else {
                Response.Write("<script> alert('จำนวนวันลาหมดแล้ว') </script>");
            }
           
        }

        private Boolean chkValidNoLeave() {
            int ValidDate;
            int noLeave;
            int reqNoLeave;
            int resultLeave;
            ValidDate = getValidDate();
            noLeave = getNoLeave();
            reqNoLeave = Convert.ToInt16(txtDay.Text);
            resultLeave = noLeave + reqNoLeave;
            int result = ValidDate.CompareTo(resultLeave);
            if (result == -1){return false;}
            else{return true;}
        }

        private int getNoLeave() {
            int noLeave;
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            sql = "select sum(NO_LEAVE) as noLeaveDate ";
            sql += "from [dbo].[REQUEST_LEAVE] ";
            sql += "where CREATE_BY = '" + Session["empCode"] + "'"; 
            sql += "and LEAVETYPE_ID = '" + ddlLeaveType.Text + "'";
            sql += "group by LEAVETYPE_ID ";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0) {noLeave = Convert.ToInt16(ds.Tables[0].Rows[0]["noLeaveDate"].ToString());}
            else { noLeave = 0; }
            return noLeave;
        }

        private int getValidDate()
        {
            int ValidDate;
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            sql = "select NO_LEAVE  ";
            sql += "from [dbo].[ROLE_NO_LEAVE] ";
            sql += "where ROLE_ID = '" + Session["ROLE"] + "'";
            sql += "and LEAVE_TYPE = '" + ddlLeaveType.SelectedValue + "'";
            ds = db.getData(sql);
            ValidDate = Convert.ToInt16(ds.Tables[0].Rows[0]["NO_LEAVE"].ToString());
            return ValidDate;
        }

        private Boolean SaveData() {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            if (FileUpload.FileName != "") {
                FileUpload.SaveAs(Server.MapPath("Files") + "//" + FileUpload.FileName);
            }
            sql = "INSERT INTO [REQUEST_LEAVE] ( ";
            sql += "[LEAVETYPE_ID],[FROM_LEAVE_DATE],[FROM_LEAVE_TIME] ";
            sql += ",[TO_LEAVE_DATE],[TO_LEAVE_TIME],[NO_LEAVE],[NO_LEAVE_HOUR] ";
            sql += ",[NOTE],[CONTACT],[CONTACT_TEL],[STATUS],[APPROVE_BY],[REQ_CONFIRM],CONFIRM_BY ";
            sql += ",[CREATE_DATE],[CREATE_BY],[UPDATE_DATE],[UPDATE_BY] ";
            sql += ") VALUES (";
            sql += "'" + ddlLeaveType.SelectedValue + "',";
            sql += "'" + setFormatDate(txtFDateLeave.Text) + "',";
            sql += "'" + txtFTimeLeave.Text + "',";
            sql += "'" + setFormatDate(txtTDateLeave.Text) + "',";
            sql += "'" + txtTTimeLeave.Text + "',";
            sql += "'" + txtDay.Text + "',";
            sql += "'" + txtHour.Text +"'," ;
            sql += "'" + txtCauseleave.Text + "',";
            sql += "'" + txtContact.Text + "',";
            sql += "'" + txtTelContact.Text + "',";
            sql += "'I',";
            sql += "'" + getuserApprComfirm("3") + "',";
            if (ddlLeaveType.SelectedValue == "3") // ถ้าประเภทการลาเป็นลาพักร้อนจะ set req_confirm เป็น FALSE เพื่อมีการยืนยันการ confirm ต่อไป 
            {
                sql += "'FALSE',";
            }
            else {
                sql += "'TRUE',";
            }
            sql += "'" + getuserApprComfirm("4") + "',";
            sql += "sysdatetime(),";
            sql += "'" + Session["empCode"] + "',";
            sql += "sysdatetime(),";
            sql += "'" + Session["empCode"] + "'";
            sql += ")";
            return db.ExecuteSQL(sql);
        }

        private string getuserApprComfirm(string TypeGroupAppr) {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            string empCode;
            sql = "select EMP_CODE ";
            sql += "from [USER] usr ";
            sql += "where usr.DeptID = '" + Session["dep"] + "'";
            sql += "and usr.USER_GROUP = '" + TypeGroupAppr + "'";
            ds = db.getData(sql);
            empCode = ds.Tables[0].Rows[0]["EMP_CODE"].ToString();
            return empCode;
        }

        private Boolean UpdateData() {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            if (FileUpload.FileName != "")
            {
                FileUpload.SaveAs(Server.MapPath("Files") + "//" + FileUpload.FileName);
            }
            sql = "UPDATE [REQUEST_LEAVE] SET ";
            sql += "LEAVETYPE_ID = '" + ddlLeaveType.SelectedValue + "',";
            sql += "FROM_LEAVE_DATE = '" + setFormatDate(txtFDateLeave.Text) + "',";
            sql += "FROM_LEAVE_TIME = '" + txtFTimeLeave.Text + "',";
            sql += "TO_LEAVE_DATE = '" + setFormatDate(txtTDateLeave.Text) + "',";
            sql += "TO_LEAVE_TIME = '" + txtTTimeLeave.Text + "',";
            sql += "NO_LEAVE = '" + txtDay.Text + "',";
            sql += "NO_LEAVE_HOUR = '" + txtHour.Text + "',";
            sql += "NOTE = '" + txtCauseleave.Text + "',";
            sql += "CONTACT = '" + txtContact.Text + "',";
            sql += "CONTACT_TEL = '" + txtTelContact.Text + "',";
            sql += "STATUS = 'I',";
            sql += "UPDATE_DATE = sysdatetime(),";
            sql += "UPDATE_BY = '" + Session["empCode"] + "'";
            sql += "WHERE LEAVE_ID = '" + Request.QueryString["Req"] + "'";
            return db.ExecuteSQL(sql);
        }

        private string setFormatDate(string _Date)
        {
            string result;
            DateTime dt = DateTime.ParseExact(_Date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            result = dt.ToString("yyyy-MM-dd");
            return result;
        }

        private void calculatetime() {
            int noWeekend = 0;
            int Hour = 0;
            int day = 0;
            hdttime.Value = txtTTimeLeave.Text;
            hdftime.Value = txtFTimeLeave.Text;
            //hdftime.Value = txtFTimeLeave.Text;
            //hdttime.Value = txtTTimeLeave.Text;
            if(txtFDateLeave.Text != "" && txtTDateLeave.Text != "" && txtFTimeLeave.Text != "" && txtTTimeLeave.Text != ""){
                DateTime fDate = DateTime.ParseExact(txtFDateLeave.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture);
                DateTime tDate = DateTime.ParseExact(txtTDateLeave.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                TimeSpan fTime = TimeSpan.Parse(txtFTimeLeave.Text.Replace(" ", ""));
                TimeSpan tTime = TimeSpan.Parse(txtTTimeLeave.Text.Replace(" ", ""));
                TimeSpan result;
                fDate = fDate + fTime;
                tDate = tDate + tTime;
                result = (tDate - fDate);
                int days = result.Days;
                for (var i = 0; i <= days; i++)
                {
                    DateTime resultDate = fDate.AddDays(i);
                    if (DateWeekend(resultDate))
                    {
                        noWeekend += 1;
                    }
                }
                day = result.Days - noWeekend;
                Hour = result.Hours;
                if (result.Hours == 9) { Hour = 0; day += 1; }
                txtDay.Text = day.ToString();
                txtHour.Text =Hour.ToString();
                //double day;
                //day = (tDate - fDate).TotalDays;
            }
        }

        private Boolean DateWeekend(DateTime _Date) {

            if (_Date.DayOfWeek == DayOfWeek.Saturday || _Date.DayOfWeek == DayOfWeek.Sunday || DateHolliday(_Date))
            {
                return true;
            }
            else return false;
        }

        private Boolean DateHolliday(DateTime _Date)
        {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            sql = "select * ";
            sql += "from [HOLIDAY] ";
            sql += "where convert(varchar(10),DATE_HOLIDAY,120) = '" + _Date.ToString("yyyy-MM-dd") + "'";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0) return true;
            else return false;
        }

        protected void btnCal_Click(object sender, EventArgs e)
        {
            calculatetime();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData() {
            Response.Redirect("CreateRequestLeave.aspx");
        }
    }
}