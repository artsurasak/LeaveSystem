using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace IS
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string sql = "";
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            class_is.ClassLeaveSystem objLeave = new class_is.ClassLeaveSystem();
            //string username = txtUsername.Text;
            //string password = txtPassword.Text;
            string userName;
            string password;
            sql = "SELECT USER_NAME , PASSWORD , EMP_CODE , DeptID , ROLE_ID , USER_GROUP ";
            sql += "FROM [USER] ";
            sql += "WHERE USER_NAME = '" +  txtUsername.Text + "'";
            ds = db.getData(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                userName = ds.Tables[0].Rows[0]["USER_NAME"].ToString();
                password = ds.Tables[0].Rows[0]["PASSWORD"].ToString();
                Session["userName"] = userName;
                Session["empCode"] = ds.Tables[0].Rows[0]["EMP_CODE"].ToString();
                Session["dep"] = ds.Tables[0].Rows[0]["DeptID"].ToString();
                Session["ROLE"] = ds.Tables[0].Rows[0]["ROLE_ID"].ToString();
                Session["Group"] = ds.Tables[0].Rows[0]["USER_GROUP"].ToString();
                //bool flag = Helper.VerifyHash(txtPassword.Text, "SHA512", password);
                bool flag = objLeave.VerifyHash(txtPassword.Text, "SHA512", password);
                if (userName == txtUsername.Text && flag == true)
                {
                    string url = getURL(userName);
                    Response.Redirect(url);
                    //Response.Write("<script> alert('Login Success') </script>");
                }
                else
                {
                    Response.Write("<script> alert('Username or Password Wrong') </script>");
                }
            }
            else
            {
                Response.Write("<script> alert('User Name Not Found!!!') </script>");
            }
        }

        private string getURL(string _userName)
        {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            int cnt;
            string url = "";
            sql = "select menu.MENU_NAME , menu.MENU_LINK ";
            sql += "from [USER] usr , [GROUP_MENU_PERMISSION] per , [MENU] menu ";
            sql += "where USER_NAME = '" + Session["userName"] + "'";
            sql += "and usr.USER_GROUP = per.GROUP_ID ";
            sql += "and per.MENU_ID_PERMISSION = menu.MENU_ID ";
            sql += "and (MENU_SUB_LINK is null or MENU_LINK = '') ";
            sql += "order by menu.SEQ ";
            ds = db.getData(sql);
            cnt = ds.Tables[0].Rows.Count;
            if (cnt > 0)
            {
                url = ds.Tables[0].Rows[0]["MENU_LINK"].ToString();
            }
            return url;
        }
    }

    
}