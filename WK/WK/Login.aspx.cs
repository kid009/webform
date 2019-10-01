using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WK.DataAccess;

namespace WK
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            else
            {

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            string role_text = ddlRole.SelectedItem.Text.Trim();
            string role_value = ddlRole.SelectedItem.Value;

            if (role_value != "0" && username != "" && password != "")
            {
                UserDAL dal = new UserDAL();

                int check = dal.CheckLogin(username, password, role_text);

                if (check > 0)
                {
                    Session["role"] = role_text;
                    Session["username"] = username;
                    Response.Redirect("Request.aspx");
                }

            }
        }
    }
}