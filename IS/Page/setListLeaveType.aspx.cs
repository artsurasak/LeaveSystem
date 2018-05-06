using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page
{
    public partial class setListLeaveType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData() {
            string sql;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "SELECT NoL.ROLE_ID as RoleID , NoL.LEAVE_TYPE as Leave , Urole.ROLE_NAME as Role , Ltype.TYPE as LeaveType , NoL.NO_LEAVE as NoLeave ";
            sql += "FROM [ROLE_NO_LEAVE] NoL , [LEAVE_TYPE] Ltype , [USER_ROLE] Urole ";
            sql += "where Ltype.ID = NoL.LEAVE_TYPE ";
            sql += "and Urole.ROLE_ID = NoL.ROLE_ID ";
            sql += "order by NoL.ROLE_ID , NoL.LEAVE_TYPE ";
            ds = db.getData(sql);
            dtg.DataSource = ds;
            dtg.DataBind();
        }
    }
}