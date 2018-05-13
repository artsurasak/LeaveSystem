using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page
{
    public partial class HistoryLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getLeaveType();
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
                li = new ListItem("All", "*");
                ddlLeaveType.Items.Add(li);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    li = new ListItem(ds.Tables[0].Rows[i]["TYPE"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                    ddlLeaveType.Items.Add(li);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFDate.Text == "" && txtTDate.Text == "")
            {
                Response.Write("<script> alert('Please Input Date!!!') </script>");
            }
            else
            {
                string url = "HistoryLeave1.aspx?Type=" + rblType.SelectedValue + "&FDate=" + txtFDate.Text + "&TDate=" + txtTDate.Text + "&LeaveType=" + ddlLeaveType.SelectedValue;
                Response.Redirect(url);
            }
        }
    }
}