using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page
{
    public partial class SearchUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { 
                GetUserGroup();
                getDepartment();
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
            li = new ListItem("All", "*");
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

        private void getDepartment() {
            string depID;
            string depName;
            ListItem li;
            DataSet ds = new DataSet();
            class_is.ClassLeaveSystem objLeaveSystem = new class_is.ClassLeaveSystem();
            ds = objLeaveSystem.getDepartment();
            li = new ListItem("All", "*");
            ddlDep.Items.Add(li);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                depID = ds.Tables[0].Rows[i]["DEPARTMENT_ID"].ToString();
                depName = ds.Tables[0].Rows[i]["DEPARTMENT_NAME"].ToString();
                li = new ListItem(depName, depID);
                ddlDep.Items.Add(li);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string url = "UserList.aspx?userName=" + txtUserName.Text + "&Name=" + txtName.Text + "&GroupName=" + ddlGroup.SelectedValue + "&empCode=" + txtEmpCode.Text + "&depName=" + ddlDep.SelectedValue;
            Response.Redirect(url);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            url = "CreateUserList.aspx";
            Response.Redirect(url);
        }
    }
}