using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page
{
    public partial class StateLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }

        private void BindData() {
            string sql;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "select t1.NO_LEAVE , t1.TYPE , iif(t2.NoLeave is null,0,t2.NoLeave) as NoLeave , (t1.NO_LEAVE- iif(t2.NoLeave is null,0,t2.NoLeave) ) as remain ";
            sql += "from ( select  noLeave.NO_LEAVE , leave.TYPE , leave.ID ";
            sql += "from [USER] usr , [ROLE_NO_LEAVE] noLeave ";
            sql += "right join [LEAVE_TYPE] leave ";
            sql += "on noLeave.LEAVE_TYPE = leave.ID ";
            sql += "where usr.ROLE_ID = noLeave.ROLE_ID ";
            sql += "and usr.EMP_CODE = '" + Session["empCode"] + "' ";
            sql += ")as t1 ";
            sql += "left join ";
            sql += "(select LEAVETYPE_ID , SUM(NO_LEAVE) as NoLeave ";
            sql += "from REQUEST_LEAVE "; 
            sql += "where CREATE_BY = '" + Session["empCode"] + "' ";
            sql += "and REQ_CONFIRM = 'true' ";
            sql += "and STATUS = 'A' ";
            sql += "group by LEAVETYPE_ID ";
            sql += ")as t2 ";
            sql += "on t1.ID = t2.LEAVETYPE_ID";
            ds = db.getData(sql);
            gvState.DataSource = ds;
            gvState.DataBind();
        }
    }
}