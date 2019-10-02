using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WK.DataAccess;
using WK.Data;

namespace WK
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string role = null;

                if (Session["role"] != null)
                {
                    role = Session["role"].ToString().Trim();
                }

                MenuDAL menu = new MenuDAL();

                List<MenuData> listmenu = menu.LoadMenu(role);

                if (listmenu != null && listmenu.Count > 0)
                {
                    ListMenuId.DataSource = listmenu;
                    ListMenuId.DataBind();
                }

            }
            else
            {

            }
        }//Page_Load


    }
}