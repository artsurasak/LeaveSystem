using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page
{
    public partial class CreateManageLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getLeaveType();
            GetUserRole();
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
            }
        }
        
        private void getLeaveType()
        {
            ListItem li;
            class_is.dbconfig db = new class_is.dbconfig();
            string sql;
            DataSet ds;
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[LEAVE_TYPE] ";
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

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}