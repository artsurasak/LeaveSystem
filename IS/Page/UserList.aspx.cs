using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getUserList();
        }

        private void getUserList()
        {
            string depName = Request.QueryString["depName"];
            string userName = Request.QueryString["userName"];
            string group = Request.QueryString["GroupName"];
            string Name = Request.QueryString["Name"];
            string empCode = Request.QueryString["empCode"];
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds;
            string sql;
            string whereClause = "";
            if (userName != "") whereClause += "AND USER_NAME like '%" + userName + "%'";
            if (Name != "") whereClause += "AND FIRST_NAME like N'%" + Name + "%' ";
            if (group != "*") whereClause += "AND USER_GROUP = '" + group + "' ";
            if (empCode != "") whereClause += "AND EMP_CODE like '%" + empCode + "%' ";
            if (depName != "*") whereClause += "AND DeptID = '" + depName + "' ";
            //if (whereClause != "") whereClause = whereClause.Substring(4, whereClause.Length - 4);
            sql = "with listUser as ( ";
            sql += "SELECT EMP_CODE,FIRST_NAME + ' ' + LAST_NAME AS NAME , USER_NAME,usrGroup.GroupName,LINE_ID,DeptID,dep.DEPARTMENT_NAME,EMAIL  ";
            sql += "FROM [USER] usr , [DEPARTMENT] dep , [USER_GROUP] usrGroup ";
            sql += "where usr.DeptID = dep.DEPARTMENT_ID ";
            sql += "and usr.USER_GROUP = usrGroup.GroupID ";
            if (whereClause != "") sql += whereClause;
            sql += ")";
            sql += "select * ";
            sql += ",(select usr.FIRST_NAME + ' ' + usr.LAST_NAME from [USER] usr where DeptID = listUser.DeptID and usr.USER_GROUP = '3') as APPROVE_NAME ";
            sql += ",(select usr.FIRST_NAME + ' ' + usr.LAST_NAME from [USER] usr where DeptID = listUser.DeptID and usr.USER_GROUP = '4') as COMFIRM_NAME ";
            sql += "from listUser ";
            //if(whereClause != "") sql += "WHERE " + whereClause;
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtg.DataSource = ds;
                dtg.DataBind();
            }
            else {
                BuildNoRecords(dtg, ds);
            }
        }

        private void BuildNoRecords(DataGrid gridView, DataSet ds)
        {
            ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            gridView.DataSource = ds;
            gridView.DataBind();
            int columnCount = gridView.Items[0].Cells.Count;
            gridView.Items[0].Cells.Clear();
            gridView.Items[0].Cells.Add(new TableCell());
            //gridView.Items[0].Cells.Add(emptyTableCell(columnCount));
            gridView.Items[0].Cells[0].ColumnSpan = columnCount;
            gridView.Items[0].Cells[0].Text = "Data Not Found";
            gridView.Items[0].Cells[0].ForeColor = System.Drawing.Color.Red;
            gridView.Items[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
        }
    }
}