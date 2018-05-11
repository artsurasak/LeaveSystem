using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using System.Threading;
using System.Drawing;

namespace IS.Page
{
    public partial class HistoryLeave1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            getDataList();
        }

        private void getDataList()
        {
            CultureInfo cultureinfo = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = cultureinfo;
            string Type = Request.QueryString["Type"];
            string FDate = Request.QueryString["FDate"];
            string TDate = Request.QueryString["TDate"];
            string LType = Request.QueryString["LeaveType"];
            TimeSpan FTime = TimeSpan.Parse("00:00:00");
            TimeSpan TTime = TimeSpan.Parse("23:59:59");
            DateTime _FDate = DateTime.ParseExact(FDate, "dd/MM/yyyy" ,cultureinfo);
            DateTime _TDate = DateTime.ParseExact(TDate, "dd/MM/yyyy", cultureinfo);
            _FDate = _FDate + FTime;
            _TDate = _TDate + TTime;
            //DateTime dt = DateTime.Parse("11/04/2013", cultureinfo);
            string sql;
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "with reqLeaveData as ( ";
            sql += "select LEAVE_ID as LeaveID ";
            sql += ",convert(varchar(10),CREATE_DATE,103)  as CreateDate ";
            sql += ",convert(varchar(10),FROM_LEAVE_DATE,103) + ' ' + convert(varchar(5), reqLeave.FROM_LEAVE_TIME,114) + ' - ' + ";
            sql += "convert(varchar(10),TO_LEAVE_DATE,103) + ' ' + convert(varchar(5), reqLeave.TO_LEAVE_TIME,114) as LeaveDate";
            sql += ",NO_LEAVE AS NoLeave ";
            sql += ",LTYPE.TYPE AS LeaveType ";
            sql += ",NOTE ";
            sql += ",CASE ";
            sql += "WHEN STATUS = 'A' THEN 'อนุญาต' "; 
            sql += "WHEN STATUS = 'R' THEN 'ไม่อนุญาต' ";
            sql += "WHEN STATUS = 'I' THEN 'รอพิจารณา' ";
            sql +="END as  STATUS ";
            sql += ",reqLeave.APPROVE_BY AS APPROVE_BY ";
            sql += ",REQ_CONFIRM AS ConfirmStatus ";
            sql += ",reqLeave.CONFIRM_BY AS CONFIRM_BY ";
            sql += "from [REQUEST_LEAVE] reqLeave  , [LEAVE_TYPE] LTYPE ";
            sql += "where reqLeave.LEAVETYPE_ID = LTYPE.ID ";
            sql += "and STATUS <> 'C'";
            sql += "and CREATE_BY = '" + Session["empCode"] + "' ";
            if (Type == "L") sql += "AND FROM_LEAVE_DATE >= '" + _FDate + "' AND TO_LEAVE_DATE <= '" + _TDate + "'";
            else sql += "AND CREATE_DATE between '" + _FDate + "' AND '" + _TDate + "'";
            if (LType != "*") sql += "AND LEAVETYPE_ID =" + LType;
            sql += ")";
            sql += "select * , ";
            sql += "(select usr.FIRST_NAME + ' ' + usr.LAST_NAME from [USER] usr where usr.EMP_CODE = APPROVE_BY and STATUS <> 'รอพิจารณา') as APPROVE_NAME , ";
            sql += "(select usr.FIRST_NAME + ' ' + usr.LAST_NAME from [USER] usr where usr.EMP_CODE = CONFIRM_BY and ConfirmStatus <> 'false') AS COMFIRM_NAME ";
            sql += "from reqLeaveData ";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dtgList.DataSource = ds;
                dtgList.DataBind();
                HideColumn();
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
            //gridView.Items[0].Cells[0].ColumnSpan = columnCount;
            //gridView.Items[0].Cells[0].Text = "Data Not Found";
            //gridView.Items[0].Cells[0].ForeColor = Color.Red;
            gridView.Items[0].Cells.Add(emptyTableCell(columnCount));
        }

        private TableCell emptyTableCell(int columncount) {
            TableCell td = new TableCell();
            td.Text = "Data Not Found";
            td.ColumnSpan = columncount;
            td.ForeColor = Color.Red;
            return td;
        }

        private void HideColumn() {
            for (int i = 0; i <= dtgList.Items.Count - 1; i++)
            {
                string Status;
                string ConfirmStatus;
                Status = dtgList.Items[i].Cells[6].Text.ToString();
                ConfirmStatus = dtgList.Items[i].Cells[8].Text.ToString();
                if (Status == "อนุญาต")
                {
                    //dtgList.Items[i].Cells[6].Text = "\u221A";
                    dtgList.Items[i].Cells[10].Text = "&nbsp;";
                    dtgList.Items[i].Cells[11].Text = "&nbsp;";
                }
                else if (Status == "ไม่อนุญาต")
                {
                    //dtgList.Items[i].Cells[6].Text = "\u2A09"; 
                    dtgList.Items[i].Cells[10].Visible = true;
                    dtgList.Items[i].Cells[11].Visible = true;
                }
                else
                {
                    dtgList.Items[i].Cells[10].Visible = true;
                    dtgList.Items[i].Cells[11].Visible = true;
                }

                if (ConfirmStatus == "True"){
                    dtgList.Items[i].Cells[8].Text = "\u221A";
                }else{
                    dtgList.Items[i].Cells[8].Text = "\u2A09";
                }
            }
        }
    }
}