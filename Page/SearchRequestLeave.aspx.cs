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
            getLeaveType();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            CultureInfo cultureinfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureinfo;
            DateTime _FDate = DateTime.ParseExact(txtFDate.Text, "dd/MM/yyyy", cultureinfo);
            DateTime _TDate = DateTime.ParseExact(txtTDate.Text, "dd/MM/yyyy", cultureinfo);
            //string sql;
            //class_is.dbconfig db = new class_is.dbconfig();
            //DataSet ds = new DataSet();
            //sql = "select * ";
            //sql += "from [LEAVE].[dbo].[REQUEST_LEAVE] reqLeave , [LEAVE].[dbo].[USER] usr ";
            //sql += "where reqLeave.CREATE_BY = usr.EMP_CODE ";
            //if (rblTypeDate.SelectedValue == "L") sql += "AND FROM_LEAVE_DATE >= '" + _FDate + "' AND TO_LEAVE_DATE < '" + _TDate + "'";
            //else if (rblTypeDate.SelectedValue == "C") sql += "AND CREATE_DATE between '" + _FDate + "' AND '" + _TDate + "'";
            //if (txtName.Text != "") sql += "AND usr.FIRST_NAME like '%" + txtName.Text + "%' ";
            //if (txtEmpCode.Text != "") sql += "AND reqLeave.CREATE_BY = '" + txtEmpCode.Text + "' ";
            //sql += "AND LEAVE_ID = '" + ddlLeaveType.SelectedValue + "' ";
            //if (ddlStatus.SelectedValue != "*") sql += "AND STATUS = '" + ddlStatus.SelectedValue  +"'";
            //ds = db.getData(sql);
            //if (txtFDate.Text == "" && txtTDate.Text == "")
            //{
            //    Response.Write("<script> alert('Please Input Date!!!') </script>");
            //}
            //else
            //{
            string url = "ListRequestAppr.aspx?Type=" + rblTypeDate.SelectedValue + "&FDate=" + _FDate.ToString("yyyy-MM-dd") + "&TDate=" + _TDate.ToString("yyyy-MM-dd") + "&LeaveType=" + ddlLeaveType.SelectedValue;
                url += "&Name=" + txtName.Text + "&EmpCode=" + txtEmpCode.Text + "&Status=" + ddlStatus.SelectedValue;
                Response.Redirect(url);
            //}
        }

        private void getLeaveType() {
            ListItem li;
            class_is.dbconfig db = new class_is.dbconfig();
            string sql;
            DataSet ds;
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[LEAVE_TYPE] ";
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