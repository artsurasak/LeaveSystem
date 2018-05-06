using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;

namespace IS.Page
{
    public partial class MainFrame : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayMenu();
            if (Session["userName"] != null)
            {
                string sql;
                DataSet ds = new DataSet();
                class_is.dbconfig db = new class_is.dbconfig();
                sql = "SELECT FIRST_NAME , LAST_NAME , ROLE_ID , USER_GROUP , GroupName ";
                sql += "FROM [USER] usr , [USER_GROUP] gr ";
                sql += "WHERE USER_NAME = '" + Session["userName"] + "'";
                sql += "AND usr.USER_GROUP = gr.GroupID ";
                ds = db.getData(sql);
                lblUsername.Text = ds.Tables[0].Rows[0]["FIRST_NAME"].ToString() + ' ' + ds.Tables[0].Rows[0]["LAST_NAME"].ToString();
                lblGroup.Text = ds.Tables[0].Rows[0]["GroupName"].ToString();
            }
        }

        private void DisplayMenu() {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            int cnt;
            sql = "select menu.MENU_NAME , menu.MENU_LINK , menu.MENU_SUB_LINK ";
            sql += "from [USER] usr , [GROUP_MENU_PERMISSION] per , [MENU] menu ";
            sql += "where USER_NAME = '" + Session["userName"] + "'";
            sql += "and usr.USER_GROUP = per.GROUP_ID ";
            sql += "and per.MENU_ID_PERMISSION = menu.MENU_ID ";
            sql += "order by menu.SEQ ";
            ds = db.getData(sql);
            cnt = ds.Tables[0].Rows.Count;
            if (cnt > 0)
            {
                for (int i = 0; i < cnt; i++)
                {
                    HtmlAnchor a = new HtmlAnchor();
                    HtmlGenericControl li = new HtmlGenericControl();
                    HtmlGenericControl div = new HtmlGenericControl();
                    if (ds.Tables[0].Rows[i]["MENU_SUB_LINK"].ToString() == "")
                    {
                        li.Attributes["class"] = "nav-item";
                        a.Attributes["class"] = "nav-link";
                        a.HRef = ds.Tables[0].Rows[i]["MENU_LINK"].ToString();
                        a.InnerText = ds.Tables[0].Rows[i]["MENU_NAME"].ToString();
                        li.Controls.Add(a);
                        ulMenu.Controls.Add(li);
                    }
                    else{
                        li.Attributes["class"] = "nav-item dropdown";
                        a.Attributes["class"] = "nav-link dropdown-toggle";
                        a.HRef = ds.Tables[0].Rows[i]["MENU_LINK"].ToString();
                        a.InnerText = ds.Tables[0].Rows[i]["MENU_NAME"].ToString();
                        div.Attributes["class"] = "dropdown-menu";
                        li.Controls.Add(a);
                        ulMenu.Controls.Add(li);
                    }
                    
                }
            }
        }
    }
}