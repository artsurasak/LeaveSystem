using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;

namespace IS.Page
{
    public partial class CreateGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getDataGroup();
                //gentableListPermission();
            }
            //getDataGroup();
            gentableListPermission();
        }

        private void getDataGroup() {
            string sql;
            DataSet ds;
            int count;
            ListItem li;
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[USER_GROUP] ";
            ds = db.getData(sql);
            count = ds.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)     
            {
                string groupID = ds.Tables[0].Rows[i]["GroupID"].ToString();
                string groupName = ds.Tables[0].Rows[i]["GroupName"].ToString();
                li = new ListItem(groupName, groupID);
                ddlGroup.Items.Add(li);
            }
        }

        private void gentableListPermission() {
            string sql;
            DataSet ds;
            int count;
            int intval = 0;
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[MENU] menu left join  [LEAVE].[dbo].GROUP_MENU_PERMISSION per ";
            sql += "on  menu.MENU_ID = per.MENU_ID_PERMISSION ";
            sql += "and per.GROUP_ID = '" + ddlGroup.SelectedValue + "' ";
            sql += "order by menu.SEQ ";
            ds = db.getData(sql);
            count = ds.Tables[0].Rows.Count;
            hdCountLine.Value = count.ToString();
            tbList.Rows.Clear();
            tbList.Rows.Add(gentableHeader());
            if (count > 0) { 
                for(int i = 0;i< count;i++){
                    intval += 1;
                    string menu = ds.Tables[0].Rows[i]["MENU_NAME"].ToString();
                    Boolean menuPermission = ds.Tables[0].Rows[i]["MENU_ID_PERMISSION"].ToString() == "" ? false : true;
                    tbList.Rows.Add(gentableRow(intval, menu, menuPermission));
                }
            }
        }

        private TableHeaderRow gentableHeader()
        {
            TableHeaderRow tr;
            TableHeaderCell td;

            tr = new TableHeaderRow();
            td = new TableHeaderCell();
            td.Text = "Menu";
            tr.Cells.Add(td);

            td = new TableHeaderCell();
            td.Text = "Permission";
            tr.Cells.Add(td);

            tr.CssClass = "table-active";
            return tr;
        }

        private void genTableMenu() {
            string sql;
            DataSet ds;
            int count;
            int intval = 0;
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "select * ";
            sql += "from [LEAVE].[dbo].[MENU] menu ";
            sql += "order by SEQ ";
            ds = db.getData(sql);
            count = ds.Tables[0].Rows.Count;
            hdCountLine.Value = count.ToString();
            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    intval += 1;
                    string menu = ds.Tables[0].Rows[i]["MENU_NAME"].ToString();
                    Boolean menuPermission = false;
                    tbList.Rows.Add(gentableRow(intval, menu, menuPermission));
                }
            }
        }

        private TableRow gentableRow(int seq,string _menuName,Boolean _menuPermission) {
            TableRow tr;
            TableCell td;
            CheckBox chk;
            //CheckBox[] chk;

            tr = new TableRow();
            td = new TableCell();
            td.Text = _menuName;
            tr.Cells.Add(td);


            td = new TableCell();
            //td = new TableCell();
            chk = new CheckBox();
            chk.ID = "Chk_" + seq;
            //chk.Name = "Chk_" + seq;
            chk.Checked = _menuPermission;
            td.Controls.Add(chk);
            tr.Cells.Add(td);

            //chk = null;
            return tr;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtAddGroup.Visible == true) {
                if (txtAddGroup.Text != "") {
                    if (CreateGroupPermission())
                    {
                        Response.Write("<script> alert('Complete') </script>");
                    }
                    else
                    {
                        Response.Write("<script> alert('Save Error') </script>");
                    }
                }
                else
                {
                    Response.Write("<script> alert('Please Input Group Name') </script>");
                }
            }
            else
            {
                if (SaveData())
                {
                    Response.Write("<script> alert('Complete') </script>");
                }
                else
                {
                    Response.Write("<script> alert('Save Error') </script>");
                }
            }
        }

        private Boolean CreateGroupPermission() {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            DataSet ds = new DataSet();
            int idGroup;
            sql = "INSERT [LEAVE].[dbo].[USER_GROUP] (GroupName) VALUES ('" + txtAddGroup.Text + "') ";
            //sql += "select SCOPE_IDENTITY() ";
            db.ExecuteSQL(sql);

            sql = "SELECT IDENT_CURRENT('[LEAVE].[dbo].[USER_GROUP]') ";
            ds = db.getData(sql);
            idGroup = int.Parse(ds.Tables[0].Rows[0][0].ToString());
            for (int i = 1; i <= Int32.Parse(hdCountLine.Value); i++)
            {
                CheckBox chk = (CheckBox)tbList.FindControl("Chk_" + i);
                if (chk != null)
                {
                    if (chk.Checked == true)
                    {
                        sql = "insert [LEAVE].[dbo].[GROUP_MENU_PERMISSION] ([GROUP_ID],[MENU_ID_PERMISSION]) VALUES ";
                        sql += "(" + idGroup + "," + i + ")";
                        if (db.ExecuteSQL(sql) == false)
                        {
                            return false;
                        }
                    }
                };
            }
            return true;
        }

        private Boolean SaveData() {
            string sql;
            int Group = Int16.Parse(ddlGroup.SelectedValue);
            class_is.dbconfig db = new class_is.dbconfig();
            if (deleteAllPermission(Group))
            {
                for (int i = 1; i <= Int32.Parse(hdCountLine.Value); i++)
                {
                    //HtmlInputCheckBox chk = (HtmlInputCheckBox)this.Master.FindControl("ContentPlaceHolder1").FindControl("Chk_" + i);
                    CheckBox chkTest = (CheckBox)tbList.FindControl("Chk_" + i);
                    if (chkTest != null)
                    {
                        if (chkTest.Checked == true)
                        {
                            sql = "insert [LEAVE].[dbo].[GROUP_MENU_PERMISSION] ([GROUP_ID],[MENU_ID_PERMISSION]) VALUES ";
                            sql += "(" + Group + "," + i + ")";
                            if (db.ExecuteSQL(sql) == false){
                                return false;
                            }
                        }
                    };
                }
            }
            return true;
        }

        private Boolean deleteAllPermission(int _Group) {
            string sql;
            class_is.dbconfig db = new class_is.dbconfig();
            sql = "delete from [LEAVE].[dbo].[GROUP_MENU_PERMISSION] ";
            sql += "where [GROUP_ID] = " + _Group + "";
            return db.ExecuteSQL(sql);
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            gentableListPermission();
        }

        protected void btnAddGroup_Click(object sender, EventArgs e)
        {
            ddlGroup.Visible = false;
            //btnAddGroup.Visible = false;
            txtAddGroup.Visible = true;
            btnCancelAddGroup.Visible = true;
            genTableMenu();
        }

        protected void btnCancelAddGroup_Click(object sender, EventArgs e)
        {
            ddlGroup.Visible = true;
            txtAddGroup.Visible = false;
            btnCancelAddGroup.Visible = false;
            gentableListPermission();   
        }
    }
}