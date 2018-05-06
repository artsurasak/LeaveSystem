using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page.popUp
{   

    public partial class ManageLeave : System.Web.UI.Page
    {
        string Role;
        string Leave;
        protected void Page_Load(object sender, EventArgs e)
        {
            initialData();
            if (!Page.IsPostBack)
            {
                getLeaveType();
                GetUserRole();
                if (Role != "undefined" && Leave != "undefined")
                {
                    getData();
                }
            }
        }

        private void initialData()
        {
            Role = Request.QueryString["Role"];
            Leave = Request.QueryString["Leave"];
        }

        private void getData()
        {
            string sql;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "select NO_LEAVE ";
            sql += "from [ROLE_NO_LEAVE] ";
            sql += "where ROLE_ID = '" + Role + "'";
            sql += "and LEAVE_TYPE = '" + Leave + "'";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtNoLeave.Text = ds.Tables[0].Rows[0]["NO_LEAVE"].ToString();
            }
        }

        private void GetUserRole()
        {
            DataSet ds = new DataSet();
            class_is.ClassLeaveSystem objLeave = new class_is.ClassLeaveSystem();
            int i;
            int count;
            string RoleID, RoleName;
            ListItem li;
            ds = objLeave.getUserRole();
            count = ds.Tables[0].Rows.Count;
            for (i = 0; i <= count - 1; i++)
            {
                RoleID = ds.Tables[0].Rows[i]["ROLE_ID"].ToString();
                RoleName = ds.Tables[0].Rows[i]["ROLE_NAME"].ToString();
                li = new ListItem(RoleName, RoleID);
                ddlRole.Items.Add(li);
                ddlRole.SelectedValue = Role;
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
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    li = new ListItem(ds.Tables[0].Rows[i]["TYPE"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                    ddlLeaveType.Items.Add(li);
                    ddlLeaveType.SelectedValue = Leave;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hasData())
            {
                if (UpdateData())
                {
                    Response.Write("<script> window.parent.close(); </script>");
                }
            }
            else
            {
                if (InsertData()){
                    Response.Write("<script> window.parent.close(); </script>");
                }
            }
        }

        private Boolean hasData()
        {
            string sql;
            Boolean _hasData;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "select NO_LEAVE ";
            sql += "from [ROLE_NO_LEAVE] ";
            sql += "where ROLE_ID = '" + ddlRole.SelectedValue +"'";
            sql += "and LEAVE_TYPE = '" +  ddlLeaveType.SelectedValue + "'";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0) _hasData = true;
            else _hasData = false;
            return _hasData;
        }

        private Boolean UpdateData()
        {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "update [ROLE_NO_LEAVE] set ";
            sql += "NO_LEAVE = '" + txtNoLeave.Text + "'";
            sql += "where ROLE_ID = '" + ddlRole.SelectedValue + "'";
            sql += "and LEAVE_TYPE = '" + ddlLeaveType.SelectedValue + "'";
            if (db.ExecuteSQL(sql))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean InsertData()
        {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "insert into [ROLE_NO_LEAVE] (ROLE_ID,LEAVE_TYPE,NO_LEAVE) ";
            sql += "VALUES ( ";
            sql += "'" + ddlRole.SelectedValue + "',";
            sql += "'" + ddlLeaveType.SelectedValue + "',";
            sql += "'" + txtNoLeave.Text + "'";
            sql += ")";
            if (db.ExecuteSQL(sql))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script> window.close(); </script>");
        }
    }
}