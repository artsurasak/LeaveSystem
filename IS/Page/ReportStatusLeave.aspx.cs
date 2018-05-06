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
    public partial class ReportStatusLeave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bindData();
        }

        private void bindData()
        {
            string sEmpCode;
            string sName;
            DataSet ds = new DataSet();
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            sEmpCode = txtEmpCode.Text;
            //else sEmpCode = Session["empCode"].ToString();
            sName = txtName.Text;
            sql = "with reqData as ( ";
            sql += "SELECT usr.EMP_CODE as EmpCode , usr.[FIRST_NAME] + ' ' + usr.[LAST_NAME] as Name, convert(varchar(10), reqLeave.CREATE_DATE ,103) as CreateDate , ";
            sql += "convert(varchar(10),FROM_LEAVE_DATE,103) + ' ' + convert(varchar(5), reqLeave.FROM_LEAVE_TIME,114) + ' - ' + ";
            sql += "convert(varchar(10),TO_LEAVE_DATE,103) + ' ' + convert(varchar(5), reqLeave.TO_LEAVE_TIME,114) as LeaveDate , ";
            sql += "lType.TYPE  as LeaveType , ";
            sql += "CASE ";
            sql += "WHEN STATUS = 'A' THEN 'อนุญาต' ";
            sql += "WHEN STATUS = 'R' THEN 'ไม่อนุญาต' ";
            sql += "WHEN STATUS = 'I' THEN 'รอพิจารณา' ";
            sql += "END as  STATUS , ";
            sql += "reqLeave.APRROVE_BY as APPROVE_BY , ";
            sql += "convert(varchar(10),reqLeave.APPROVE_DATE,103) + ' ' + convert(varchar(5), reqLeave.APPROVE_DATE,114) as ApprDate , ";
            sql += "reqLeave.REQ_CONFIRM AS confirmStatus , ";
            sql += "reqLeave.CONFIRM_BY as empConfirm, ";
            sql += "convert(varchar(15),reqLeave.CONFIRM_DATE,103) + ' ' + convert(varchar(5), reqLeave.CONFIRM_DATE,114) as confirmDate ";
            sql += "FROM [REQUEST_LEAVE] reqLeave , [USER] usr , [LEAVE_TYPE] lType ";
            sql += "where reqLeave.CREATE_BY = usr.EMP_CODE ";
            sql += "and STATUS <> 'C'";
            sql += "and reqLeave.LEAVETYPE_ID = lType.ID ";
            if (sEmpCode != "") sql += "and reqLeave.CREATE_BY = '" + sEmpCode + "' ";
            if (sName != "") sql += "and usr.FIRST_NAME like N'%" + sName + "%'";
            sql += "and year(CREATE_DATE) = YEAR(GETDATE()) ";
            sql += ") ";
            sql += "select * , ";
            sql += "(select FIRST_NAME + ' ' + LAST_NAME from [USER] where EMP_CODE = APPROVE_BY) as ApprName , ";
            sql += "(select FIRST_NAME + ' ' + LAST_NAME from [USER] where EMP_CODE = reqData.empConfirm) as confirmName ";
            sql += "from reqData ";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtgList.DataSource = ds;
                dtgList.DataBind();
                insertSymbol();
            }
            else
            {
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
            gridView.Items[0].Cells[0].ForeColor = Color.Red;
        }

        private void insertSymbol()
        {
            for (int i = 0; i <= dtgList.Items.Count - 1; i++)
            {
                string Status;
                Status = dtgList.Items[i].Cells[8].Text.ToString();
                if (Status == "True")
                {
                    dtgList.Items[i].Cells[8].Text = "\u221A";
                }
                else if (Status == "False")
                {
                    dtgList.Items[i].Cells[8].Text = "\u2A09";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            bindData();
        }
    }
}