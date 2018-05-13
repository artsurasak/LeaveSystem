using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace IS.Page
{
    public partial class ReportNoLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                bindData();
                getLeaveType();
            }
        }

        private void bindData()
        {
            string sEmpCode;
            string sName;
            DataSet ds = new DataSet();
            string sql;
            string whereCause = "";
            sEmpCode = txtEmpCode.Text;
            //else sEmpCode = Session["empCode"].ToString();
            sName = txtName.Text;
            if (sEmpCode != "") whereCause += "and t1.EMP_CODE = '" + sEmpCode + "' ";
            if (sName != "") whereCause += "and t1.Name like N'%" + sName + "%'";
            if (ddlTypeLeave.SelectedValue != "*" && ddlTypeLeave.SelectedValue != "") whereCause += "and t2.LEAVETYPE_ID = '" + ddlTypeLeave.SelectedValue + "'";
            class_is.dbconfig db = new class_is.dbconfig();

            sql = "select t1.EMP_CODE as EmpCode , t1.Name ,t1.NO_LEAVE , t1.TYPE , iif(t2.NoLeave is null,0,t2.NoLeave) as NoLeave , ";
            sql += "(t1.NO_LEAVE- iif(t2.NoLeave is null,0,t2.NoLeave) ) as remain ";
            sql += "from (	select usr.EMP_CODE , usr.FIRST_NAME + ' ' + usr.LAST_NAME as Name, noLeave.NO_LEAVE , leave.TYPE , leave.ID ";
	        sql += "from [USER] usr , [ROLE_NO_LEAVE] noLeave right join [LEAVE_TYPE] leave on noLeave.LEAVE_TYPE = leave.ID ";
            sql += "where usr.ROLE_ID = noLeave.ROLE_ID ";
		    //sql += "and usr.EMP_CODE = '580009' ";
	        sql += ")as t1 "; 
            sql += "left join "; 
	        sql += "(select LEAVETYPE_ID , SUM(NO_LEAVE) as NoLeave , CREATE_BY ";
            sql += "from REQUEST_LEAVE "; 
            sql += "where  REQ_CONFIRM = 'true' ";
            sql += "and STATUS = 'A' ";
            sql += "group by LEAVETYPE_ID , CREATE_BY ";
            sql += ")as t2 ";
            sql += "on t1.ID = t2.LEAVETYPE_ID ";
            sql += "and t1.EMP_CODE = t2.CREATE_BY ";
            if (whereCause.Length > 0)
            {
                sql += "where ";
                sql += whereCause.Substring(4, whereCause.Length - 4);                    
            }
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtgList.DataSource = ds;
                dtgList.DataBind();
            }
            else
            {
                BuildNoRecords(dtgList, ds);
            }
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
            dtgList.RenderControl(htmlWrite);
            Session["dataList"] = stringWrite.ToString();
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
            gridView.Items[0].Cells[0].ForeColor = Color.Red;
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
                ddlTypeLeave.Items.Add(li);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    li = new ListItem(ds.Tables[0].Rows[i]["TYPE"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString());
                    ddlTypeLeave.Items.Add(li);
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindData();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            Response.Write(Session["dataList"]);
            Response.End();
        }
    }
}