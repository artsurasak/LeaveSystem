using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace IS.Page
{
    public partial class CreateUserList : System.Web.UI.Page
    {
        string Mode;
        string userName;
        protected void Page_Load(object sender, EventArgs e)
        {
            userName = Request.QueryString["userName"];
            Mode = Request.QueryString["Mode"];
            if (Mode == "Edit") { btnDelete.Visible = true; HeaderText.Text = "แก้ไขผู้ใช้งาน"; trPassword.Visible = false; }
            if (!Page.IsPostBack) {
                GetDepartment();
                GetUserGroup();
                GetUserRole();
                if (Mode == "Edit"){
                    GetDataUser();
                }
            }
        }

        private void GetDataUser() {
            string userName = Request.QueryString["userName"];
            DataSet ds = new DataSet();
            class_is.ClassLeaveSystem objLeave = new class_is.ClassLeaveSystem();
            ds = objLeave.getUserData(userName);
            if (ds.Tables[0].Rows.Count > 0) {
                txtUserName.Enabled = false;
                //txtPassword.Enabled = false;
                txtUserName.Text = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
                //txtPassword.Text = "Password";
                txtLine.Text = ds.Tables[0].Rows[0]["LINE_ID"].ToString();
                txtFName.Text = ds.Tables[0].Rows[0]["FIRST_NAME"].ToString();
                txtLName.Text = ds.Tables[0].Rows[0]["LAST_NAME"].ToString();
                txtEmpID.Text = ds.Tables[0].Rows[0]["EMP_CODE"].ToString();
                txtEmail.Text = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                ddlRole.SelectedValue = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                ddlGroup.SelectedValue = ds.Tables[0].Rows[0]["USER_GROUP"].ToString();
                ddlDep.SelectedValue = ds.Tables[0].Rows[0]["DeptID"].ToString();
            }
        }

        private void GetDepartment() {
            string depID;
            string depName;
            ListItem li;
            DataSet ds = new DataSet();
            class_is.ClassLeaveSystem objLeaveSystem = new class_is.ClassLeaveSystem();
            li = new ListItem("Please Select...", "*");
            ddlDep.Items.Add(li);
            ds = objLeaveSystem.getDepartment();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                depID = ds.Tables[0].Rows[i]["DEPARTMENT_ID"].ToString();
                depName = ds.Tables[0].Rows[i]["DEPARTMENT_NAME"].ToString();
                li = new ListItem(depName, depID);
                ddlDep.Items.Add(li);
            }
        }

        private void GetUserGroup()
        {
            DataSet ds = new DataSet();
            class_is.ClassLeaveSystem objLeave = new class_is.ClassLeaveSystem();
            int i;
            int count;
            string GroupID, GroupName;
            ListItem li;
            li = new ListItem("Please Select...", "*");
            ddlGroup.Items.Add(li);
            ds = objLeave.getUserGroup();
            count = ds.Tables[0].Rows.Count;
            for (i = 0; i <= count - 1; i++)
            {
                GroupID = ds.Tables[0].Rows[i]["GroupID"].ToString();
                GroupName = ds.Tables[0].Rows[i]["GroupName"].ToString();
                li = new ListItem(GroupName, GroupID);
                ddlGroup.Items.Add(li);
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
            li = new ListItem("Please Select...", "*");
            ddlRole.Items.Add(li);
            ds = objLeave.getUserRole();
            count = ds.Tables[0].Rows.Count;
            for (i = 0; i <= count - 1; i++)
            {
                RoleID = ds.Tables[0].Rows[i]["ROLE_ID"].ToString();
                RoleName = ds.Tables[0].Rows[i]["ROLE_NAME"].ToString();
                li = new ListItem(RoleName, RoleID);
                ddlRole.Items.Add(li);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                if (Mode == "Edit") {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Update User Conmplete!!!'); window.location='SearchUser.aspx';", true);
                    //Response.Write("<script> alert('Update User Conmplete!!!') </script>");
                }
                else {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Create User Conmplete!!!'); window.location='SearchUser.aspx';", true);
                }
            }
            else {
                if (Mode == "Edit") { Response.Write("<script> alert('Update Data Fail!!!') </script>"); }
                else { Response.Write("<script> alert('Save Data Fail!!!') </script>"); }
            } 
        }

        private Boolean FillData() {
            return true;
        }

        private Boolean SaveData()
        {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            class_is.ClassLeaveSystem objLeave = new class_is.ClassLeaveSystem();
            if (Mode == "Edit")
            {
                sql = "UPDATE [LEAVE].[dbo].[USER] SET ";
                sql += "USER_GROUP = '" + ddlGroup.SelectedValue + "',";
                sql += "ROLE_ID = '" + ddlRole.SelectedValue + "',";
                sql += "LINE_ID = '" + txtLine.Text + "',";
                sql += "FIRST_NAME = '" + txtFName.Text + "',";
                sql += "LAST_NAME = '" + txtLName.Text + "',";
                sql += "EMP_CODE = '" + txtEmpID.Text + "',";
                sql += "DeptID = '" + ddlDep.SelectedValue + "',";
                sql += "EMAIL = '" + txtEmail.Text + "'";
                sql += "where USER_NAME = '" + userName + "'";
                if (db.ExecuteSQL(sql)) return true;
                else return false;
            }
            else
            {
                if (UserNameExist())
                {
                    Response.Write("<script> alert('User Name is Exist') </script>");
                    return false;
                }
                else if (empCodeExist())
                {
                    Response.Write("<script> alert('Emp Code is Exist') </script>");
                    return false;
                }
                else
                {
                    sql = "INSERT INTO [LEAVE].[dbo].[USER] ";
                    sql += "(USER_NAME,PASSWORD,USER_GROUP,ROLE_ID,LINE_ID,FIRST_NAME,LAST_NAME,EMP_CODE,DeptID,EMAIL) ";
                    sql += "VALUES ( ";
                    sql += "'" + txtUserName.Text + "',";
                    sql += "'" + objLeave.ComputeHash(txtPassword.Text, "SHA512", null) + "',";
                    sql += "'" + ddlGroup.SelectedValue + "',";
                    sql += "'" + ddlRole.SelectedValue + "',";
                    sql += "'" + txtLine.Text + "',";
                    sql += "'" + txtFName.Text + "',";
                    sql += "'" + txtLName.Text + "',";
                    sql += "'" + txtEmpID.Text + "',";
                    sql += "'" + ddlDep.SelectedValue + "',";
                    sql += "'" + txtEmail.Text + "'";
                    sql += ")";
                    if (db.ExecuteSQL(sql)) return true;
                    else return false;
                }
            }
        }

        private Boolean UserNameExist() {
            string sql;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[USER] ";
            sql += "where USER_NAME = '" + txtUserName.Text + "' ";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0) return true;
            else return false;
        }

        private Boolean empCodeExist()
        {
            string sql;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[USER] ";
            sql += "where EMP_CODE = '" + txtEmpID.Text + "' ";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0) return true;
            else return false;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (deleteData()) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Delete Conmplete!!!'); window.location='SearchUser.aspx';", true);
            }
            else Response.Write("<script> alert('Delete Error!!!') </script>");
        }

        private Boolean deleteData() {
            string sql;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "delete from [LEAVE].[dbo].[USER] where USER_NAME = '" + txtUserName.Text + "'";
            if (db.ExecuteSQL(sql)) return true;
            else return false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url;
            url = "SearchUser.aspx";
            Response.Redirect(url);
        }
    }
}