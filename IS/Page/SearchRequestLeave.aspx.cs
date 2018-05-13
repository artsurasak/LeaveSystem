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
    public partial class SearchRequestLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            defaultDate();
            if (!Page.IsPostBack)
            {
                getLeaveType();
            }
        }

        private void defaultDate()
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFDate.Text == "" || txtTDate.Text == "")
            {
                Response.Write("<script> alert('Please Input Date!!!') </script>");
            }
            else
            {
                CultureInfo cultureinfo = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = cultureinfo;
                DateTime _FDate = DateTime.ParseExact(txtFDate.Text, "dd/MM/yyyy", cultureinfo);
                DateTime _TDate = DateTime.ParseExact(txtTDate.Text, "dd/MM/yyyy", cultureinfo);
                string url = "ListRequestAppr.aspx?Type=" + rblTypeDate.SelectedValue + "&FDate=" + _FDate.ToString("yyyy-MM-dd") + "&TDate=" + _TDate.ToString("yyyy-MM-dd") + "&LeaveType=" + ddlLeaveType.SelectedValue;
                url += "&Name=" + txtName.Text + "&EmpCode=" + txtEmpCode.Text + "&Status=" + ddlStatus.SelectedValue;
                Response.Redirect(url);
            }
        }

        private void getLeaveType() {
            ListItem li;
            class_is.dbconfig db = new class_is.dbconfig();
            string sql;
            DataSet ds;
            sql = "select * ";
            sql += "from [LEAVE_TYPE] ";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                li = new ListItem("ทั้งหมด", "*");
                ddlLeaveType.Items.Add(li);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    li = new ListItem(ds.Tables[0].Rows[i]["TYPE"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                    ddlLeaveType.Items.Add(li);
                }
            }
        }
    }
}