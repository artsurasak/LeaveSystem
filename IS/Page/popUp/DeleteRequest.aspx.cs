using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Windows.Forms;

namespace IS.Page.popUp
{
    public partial class DeleteRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("ยืนยันยกเลิกการลางาน", "", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Boolean DeleteSuccess = false;
                string LeaveID = Request.QueryString["Req"];
                string sql;
                class_is.dbconfig db = new class_is.dbconfig();
                sql = "UPDATE [REQUEST_LEAVE] SET ";
                sql += "STATUS = 'C',";
                sql += "CANCEL_DATE = sysdatetime()";
                sql += "WHERE LEAVE_ID = '" + LeaveID + "'";
                //sql = "DELETE FROM [REQUEST_LEAVE] ";
                //sql += "WHERE LEAVE_ID = '" + reqID + "' ";
                DeleteSuccess = db.ExecuteSQL(sql);
                if (DeleteSuccess)
                {
                    Response.Write("<script> alert('Delete Complete') </script>");
                    Response.Write("<script> window.close(); </script>");
                }
                else
                {
                    Response.Write("<script> alert('Delete Error') </script>");
                    Response.Write("<script> window.close(); </script>");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                Response.Write("<script> window.close(); </script>");
            }
        }
    }
}