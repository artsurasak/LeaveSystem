using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page
{
    public partial class ListRequestAppr : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
            btnCancel.Attributes.Add("onclick", "history.back();return false");
        }

        private void BindData()
        {
            string sql;
            DataSet ds;
            class_is.dbconfig db = new class_is.dbconfig();
            string Type = Request.QueryString["Type"];
            string _FDate = Request.QueryString["FDate"];
            string _TDate = Request.QueryString["TDate"];
            TimeSpan FTime = TimeSpan.Parse("00:00:00");
            TimeSpan TTime = TimeSpan.Parse("23:59:59");
            string Name = Request.QueryString["Name"];
            string EmpCode = Request.QueryString["EmpCode"];
            string LType = Request.QueryString["LeaveType"];
            string Status = Request.QueryString["Status"];
            _FDate = _FDate + " " +FTime;
            _TDate = _TDate + " " +TTime;
            sql = "select tbl1.*,isnull(RemainDate,(select NO_LEAVE from [ROLE_NO_LEAVE] where LEAVE_TYPE = tbl1.TypeID and ROLE_ID = tbl1.ROLE_ID)) as RemainDate ";
            sql += "from ( ";
            sql += "SELECT reqLeave.LEAVE_ID as LeaveID , usr.EMP_CODE AS EmpCode  , usr.FIRST_NAME + ' ' + usr.LAST_NAME  AS NAME , convert(varchar(10),CREATE_DATE,103) AS CreateRequest , ";
            sql += "convert(varchar(10),FROM_LEAVE_DATE,103) + ' ' + convert(varchar(5), reqLeave.FROM_LEAVE_TIME,114) + ' - ' + ";
            sql += "convert(varchar(10),TO_LEAVE_DATE,103) + ' ' + convert(varchar(5), reqLeave.TO_LEAVE_TIME,114) as LeaveDate, ";
            sql += "reqLeave.NO_LEAVE AS NoLeave ,  STATUS , LType.ID as TypeID, ";
            sql += "LType.TYPE AS Type ,NOTE AS Note ,usr.EMP_CODE ,usr.ROLE_ID ";
            sql += "from [REQUEST_LEAVE] reqLeave , [USER] usr , LEAVE_TYPE LType ";
            sql += "where reqLeave.CREATE_BY = usr.EMP_CODE ";
            sql += "and reqLeave.LEAVETYPE_ID = LType.ID ";
            sql += "and STATUS = 'I'";
            if (Type == "L") sql += "AND reqLeave.FROM_LEAVE_DATE >= '" + _FDate + "' AND reqLeave.TO_LEAVE_DATE <= '" + _TDate + "'";
            else if (Type == "C") sql += "AND CREATE_DATE between '" + _FDate + "' AND '" + _TDate + "'";
            if (Name != "") sql += "AND usr.FIRST_NAME like '%" + Name + "%' ";
            if (EmpCode != "") sql += "AND reqLeave.CREATE_BY = '" + EmpCode + "' ";
            if (LType != "*") sql += "AND reqLeave.LEAVETYPE_ID = '" + LType + "' ";
            if (Status != "*") sql += "AND STATUS = '" + Status + "'";
            sql += "and reqLeave.APPROVE_BY = '" + Session["empCode"] + "'";
            sql += ") as tbl1 ";
            sql += "left join ";
            sql += "( ";
            sql += "select (t2.NO_LEAVE - t1.sumLeaveDay) as RemainDate , EMP_CODE , LEAVE_TYPE ";
            sql += "from ( "; 
			sql += "select CREATE_BY, LEAVETYPE_ID, sum(NO_LEAVE) as sumLeaveDay , sum(NO_LEAVE_HOUR) as sumLeaveHour ";
			sql += "from [REQUEST_LEAVE] ";
			sql += "where STATUS = 'A' ";
			sql += "and REQ_CONFIRM = 'true' ";
			sql += "group by LEAVETYPE_ID , CREATE_BY ";
			sql += ") as t1 "; 
			sql += ", "; 
			sql += "( ";
			sql += "select noLeave.LEAVE_TYPE , noLeave.NO_LEAVE , usr.* ";
			sql += "from [USER] usr ,  [ROLE_NO_LEAVE] noLeave ";
			sql += "where usr.ROLE_ID = noLeave.ROLE_ID ";
		    sql += ") as t2 ";
            sql += "where t1.CREATE_BY = t2.EMP_CODE ";
            sql += "and t1.LEAVETYPE_ID = t2.LEAVE_TYPE ";
            sql += ") as tbl2 ";
            sql += "on tbl1.EMP_CODE = tbl2.EMP_CODE ";
            sql += "and tbl1.TypeID = tbl2.LEAVE_TYPE ";
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

        private void hidevisibleRadio() {
            for (int i = 0; i <= dtgList.Items.Count - 1; i++)
            {
                RadioButton rbValid = (RadioButton)dtgList.Items[i].Cells[0].FindControl("rd1");
                RadioButton rbNValid = (RadioButton)dtgList.Items[i].Cells[1].FindControl("rd2");
                string Status;
                Status = dtgList.Items[i].Cells[3].Text.ToString();
                if (Status == "I")
                {
                    rbNValid.Visible = true;
                    rbValid.Visible = true;
                }
                else {
                    rbNValid.Visible = false;
                    rbValid.Visible = false;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            class_is.dbconfig db = new class_is.dbconfig();
            string sql = "";
            string LeaveID;

            //foreach (DataGridItem item in dtgList.Items)
            for (int i = 0; i <= dtgList.Items.Count - 1; i++)
            {
                RadioButton rbValid = (RadioButton)dtgList.Items[i].Cells[0].FindControl("rd1");
                RadioButton rbNValid = (RadioButton)dtgList.Items[i].Cells[1].FindControl("rd2");
                if (rbValid != null && rbValid.Checked)
                {
                    //update Status Request = A
                    LeaveID = dtgList.Items[i].Cells[2].Text.ToString();
                    sql += UpdateStatusRequest(LeaveID, "A");
                }
                else if (rbNValid != null && rbNValid.Checked)
                {
                    // Update Status Request = R
                    LeaveID = dtgList.Items[i].Cells[2].Text.ToString();
                    sql += UpdateStatusRequest(LeaveID, "R");
                }
            }
            if (db.ExecuteSQL(sql)) {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Redit", "alert('Save Successfully!!!'); window.location='SearchRequestLeave.aspx';", true);
                //BindData();
            }
        }

        int indexOfColumn = 1; //Note : Index will start with 0 so set this value accordingly
        protected void mygrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count > indexOfColumn)
            {
                e.Row.Cells[indexOfColumn].Visible = false;
            }
        }

        private string UpdateStatusRequest(string _LeaveID, string _Status)
        {
            string sql;
            sql = "Update [REQUEST_LEAVE] set ";
            sql += "STATUS = '" + _Status + "',";
            sql += "APPROVE_DATE = sysdatetime(),";
            sql += "APPROVE_BY = '" + Session["empCode"] + "'";
            sql += "where LEAVE_ID = '" + _LeaveID + "'";
            return sql;
        }
    }
}