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
            DataSet ds1 = new DataSet();
            int cnt;
            sql = "select menu.MENU_ID ,menu.MENU_NAME , menu.MENU_LINK , menu.MENU_SUB_LINK ";
            sql += "from [USER] usr , [GROUP_MENU_PERMISSION] per , [MENU] menu ";
            sql += "where USER_NAME = '" + Session["userName"] + "'";
            sql += "and usr.USER_GROUP = per.GROUP_ID ";
            sql += "and per.MENU_ID_PERMISSION = menu.MENU_ID ";
            sql += "and (MENU_SUB_LINK is null or MENU_LINK = '')";
            sql += "order by menu.SEQ ";
            ds = db.getData(sql);
            cnt = ds.Tables[0].Rows.Count;
            if (cnt > 0)
            {
                for (int i = 0; i < cnt; i++)
                {
                    HtmlAnchor a = new HtmlAnchor();
                    HtmlGenericControl li = new HtmlGenericControl();
                    HtmlGenericControl divDropdown = new HtmlGenericControl();
                    HtmlAnchor aDropdown = new HtmlAnchor();
                    if (ds.Tables[0].Rows[i]["MENU_SUB_LINK"].ToString() == "")
                    {
                        if (Request.QueryString["Menu"] == ds.Tables[0].Rows[i]["MENU_NAME"].ToString())
                        {
                            li.Attributes["class"] = "nav-item";
                            li.Attributes.Add("Style", "display: table-cell;");
                            a.Attributes["class"] = "nav-link active";
                            a.Attributes.Add("Style", "color:#ffffff; background-color:#e5b0f2; display: block;");
                            a.HRef = ds.Tables[0].Rows[i]["MENU_LINK"].ToString();
                            a.InnerText = ds.Tables[0].Rows[i]["MENU_NAME"].ToString();
                            li.Controls.Add(a);
                            ulMenu.Controls.Add(li);
                        }
                        else
                        {
                            li.Attributes["class"] = "nav-item";
                            li.Attributes.Add("Style", "display: table-cell;");
                            a.Attributes["class"] = "nav-link";
                            a.Attributes.Add("Style", "display: block;");
                            a.HRef = ds.Tables[0].Rows[i]["MENU_LINK"].ToString();
                            a.InnerText = ds.Tables[0].Rows[i]["MENU_NAME"].ToString();
                            li.Controls.Add(a);
                            ulMenu.Controls.Add(li);
                        }
                        
                    }
                    else{
                        if (Request.QueryString["Menu"] == "รายงานวันลา" || Request.QueryString["Menu"] == "รายงานสถานะวันลา" || Request.QueryString["Menu"] == "รายงานวันลาคงเหลือ")
                        {
                            ds1 = getMenuSubLink(ds.Tables[0].Rows[i]["MENU_ID"].ToString());
                            li.Attributes["class"] = "nav-item dropdown";
                            li.Attributes.Add("Style", "display: table-cell;");
                            a.Attributes["class"] = "nav-link dropdown-toggle active";
                            //a.Attributes["class"] = "nav-link active";
                            a.Attributes["data-toggle"] = "dropdown";
                            a.Attributes.Add("Style", "color:#ffffff; background-color:#e5b0f2; display: block;");
                            a.HRef = ds.Tables[0].Rows[i]["MENU_LINK"].ToString();
                            a.InnerText = ds.Tables[0].Rows[i]["MENU_NAME"].ToString();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                //divDropdown.Attributes["class"] = "dropdown-menu";
                                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                                {
                                    //string dropdownName = "aDropdown" + j;
                                    divDropdown.Attributes["class"] = "dropdown-menu";
                                    aDropdown = new HtmlAnchor();
                                    aDropdown.Attributes["class"] = "dropdown-item";
                                    aDropdown.HRef = ds1.Tables[0].Rows[j]["MENU_LINK"].ToString();
                                    aDropdown.InnerText = ds1.Tables[0].Rows[j]["MENU_NAME"].ToString();
                                    divDropdown.Controls.Add(aDropdown);
                                }
                                //divDropdown.Controls.Add(aDropdown);
                            }
                            li.Controls.Add(a);
                            li.Controls.Add(divDropdown);
                            ulMenu.Controls.Add(li);
                        }
                        else
                        {
                            ds1 = getMenuSubLink(ds.Tables[0].Rows[i]["MENU_ID"].ToString());
                            li.Attributes["class"] = "nav-item dropdown";
                            li.Attributes.Add("Style", "display: table-cell;");
                            a.Attributes["class"] = "nav-link dropdown-toggle";
                            //a.Attributes["class"] = "nav-link active";
                            a.Attributes["data-toggle"] = "dropdown";
                            a.Attributes.Add("Style", "color:#007bff; display: block;");
                            a.HRef = ds.Tables[0].Rows[i]["MENU_LINK"].ToString();
                            a.InnerText = ds.Tables[0].Rows[i]["MENU_NAME"].ToString();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                //divDropdown.Attributes["class"] = "dropdown-menu";
                                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                                {
                                    //string dropdownName = "aDropdown" + j;
                                    divDropdown.Attributes["class"] = "dropdown-menu";
                                    aDropdown = new HtmlAnchor();
                                    aDropdown.Attributes["class"] = "dropdown-item";
                                    aDropdown.HRef = ds1.Tables[0].Rows[j]["MENU_LINK"].ToString();
                                    aDropdown.InnerText = ds1.Tables[0].Rows[j]["MENU_NAME"].ToString();
                                    divDropdown.Controls.Add(aDropdown);
                                }
                                //divDropdown.Controls.Add(aDropdown);
                            }
                            li.Controls.Add(a);
                            li.Controls.Add(divDropdown);
                            ulMenu.Controls.Add(li);
                        }
                    }
                    
                }
            }
        }

        private DataSet getMenuSubLink(string MenuID)
        {
            DataSet ds = new DataSet();
            class_is.dbconfig db = new class_is.dbconfig();
            string sql;
            sql = "select * ";
            sql += "from [MENU] ";
            sql += "where MENU_SUB_LINK = '" + MenuID + "'";
            sql += "and MENU_ID <> '" + MenuID + "'";
            sql += "order by seq ";
            ds = db.getData(sql);
            return ds;
        }
    }
}