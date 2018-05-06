using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page
{
    public partial class ConfirmRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) {
                bindData();
            }
        }

        private void bindData() {
            string sql;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();

            sql = "select reqLeave.LEAVE_ID as reqID , usr.EMP_CODE as EmpCode , usr.FIRST_NAME + ' ' + usr.LAST_NAME as Name , lType.TYPE , ";
            sql += "convert(varchar(10),FROM_LEAVE_DATE,103) + ' ' + convert(varchar(5), reqLeave.FROM_LEAVE_TIME,114) + ' - ' + ";
            sql += "convert(varchar(10),TO_LEAVE_DATE,103) + ' ' + convert(varchar(5), reqLeave.TO_LEAVE_TIME,114) as LeaveDate, ";
            sql += "(select FIRST_NAME + ' ' + LAST_NAME from [LEAVE].[dbo].[USER] where EMP_CODE = '" + Session["empCode"] + "' ) as confirmName , ";
            sql += "reqLeave.NOTE as Note ";
            sql += "from [LEAVE].[dbo].[USER] usr , [LEAVE].[dbo].[REQUEST_LEAVE] reqLeave , ";
            sql += "[LEAVE].[dbo].[LEAVE_TYPE] lType ";
            sql += "where usr.EMP_CODE = reqLeave.CREATE_BY ";
            sql += "and reqLeave.LEAVETYPE_ID = lType.ID ";
            sql += "and reqLeave.REQ_CONFIRM = 'false' ";
            sql += "and reqLeave.[STATUS] = 'A'";
            sql += "and reqLeave.CONFIRM_BY = '" + Session["empCode"] + "'";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtgList.DataSource = ds;
                dtgList.DataBind();
            }
            else {
                BuildNoRecords(dtgList, ds);
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
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData()) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Save Successfully!!!'); window.location='ConfirmRequest.aspx';", true);
                //Response.Write("<script> alert('Complete') </script>");
            }else{
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Save Error!!!');", true);
                //Response.Write("<script> alert('Error') </script>");
            }
        }

        private Boolean SaveData() {
            class_is.dbconfig db = new class_is.dbconfig();
            string sql = "";
            string reqID;
            for (int i = 0; i <= dtgList.Items.Count - 1; i++)
            {
                RadioButton rbValid = (RadioButton)dtgList.Items[i].Cells[0].FindControl("rd1");
                //RadioButton rbNValid = (RadioButton)dtgList.Items[i].Cells[1].FindControl("rd2");
                if (rbValid != null && rbValid.Checked)
                {
                    //update Status Request = A
                    reqID = dtgList.Items[i].Cells[1].Text.ToString();
                    sql += UpdateStatusConfirm(reqID, "A");
                }
                //else if (rbNValid != null && rbNValid.Checked)
                //{
                //    // Update Status Request = R
                //    reqID = dtgList.Items[i].Cells[2].Text.ToString();
                //    sql += UpdateStatusConfirm(reqID, "R");
                //}
            }
            if (db.ExecuteSQL(sql)) {
                return true;
            }else{
                return false;
            }
        }

        private string UpdateStatusConfirm(string _leaveID, string _Status)
        {
            string sql;
            sql = "Update [LEAVE].[dbo].[REQUEST_LEAVE] set ";
            sql += "REQ_CONFIRM = 'true',";
            sql += "CONFIRM_DATE = sysdatetime(),";
            sql += "CONFIRM_BY = '" + Session["empCode"] + "'";
            sql += "where LEAVE_ID = '" + _leaveID + "'";
            return sql;
        }
        
    }
}