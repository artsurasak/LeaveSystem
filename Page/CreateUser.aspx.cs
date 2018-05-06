using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


namespace IS.Page
{
    public partial class CreateUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetUserGroup();
        }

        private void GetUserGroup()
        {
            DataSet ds = new DataSet();
            string sql;
            int i;
            int count;
            string GroupID,GroupName;
            SqlCommand cmd;
            ListItem li;
            class_is.dbconfig db = new class_is.dbconfig();
            SqlConnection conn = new SqlConnection(db.config());
            SqlDataAdapter data;
            conn.Open();
            sql = "select * ";
            sql += "from [IS].[dbo].[UserGroup] ";
            cmd = new SqlCommand(sql);
            data = new SqlDataAdapter(sql, conn);
            data.Fill(ds);
            count = ds.Tables[0].Rows.Count;
            for (i = 0; i <= count - 1; i++)
            {
                GroupID = ds.Tables[0].Rows[i]["GroupID"].ToString();
                GroupName = ds.Tables[0].Rows[i]["GroupName"].ToString();
                li = new ListItem(GroupName,GroupID);
                ddlGroup.Items.Add(li);
            }
            conn.Close();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData() == false) {
                Response.Write("<script> alert('Save Data Fail!!!') </script>");
            }
        }

        private Boolean SaveData()
        {
            SqlConnection conn;
            SqlCommand command;
            string sql;
            DataSet ds;
            class_is.dbconfig db = new class_is.dbconfig();
            //config = db.config();
            conn = new SqlConnection(db.config());
            sql = "INSERT INTO [IS].[dbo].[USER] ";
            sql += "(USER_NAME,PASSWORD,USER_GROUP,LINE_ID,FIRST_NAME,LAST_NAME,USER_CODE,DEPARTMENT,EMAIL) ";
            sql += "VALUES ( ";
            sql += "'" + txtUserName.Text + "'";
            sql += "'Password'";
            sql += "'" + ddlGroup.SelectedValue + "'";
            sql += "'" + txtLine.Text + "'";
            sql += "'" + txtFName.Text + "'";
            sql += "'" + txtLName.Text + "'";
            sql += "'" + txtEmpID.Text + "'";
            sql += "'" + ddlDep.SelectedValue + "'";
            sql += "'" + txtEmail.Text + "'";
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
    }
}