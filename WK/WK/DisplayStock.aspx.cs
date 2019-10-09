using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WK.DataAccess;
using WK.Data;
using System.Data;

namespace WK
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["ReqId"] != null)
                {
                    if (Request["ReqId"] != "")
                    {
                        string req_id = Request["ReqId"].ToString();

                        ItemDal dal = new ItemDal();

                        DataSet ds = dal.GetRequestStockById(req_id);

                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            lbReqId.Text = req_id;

                            ResultGridView.DataSource = ds.Tables[0];
                            ResultGridView.DataBind();
                        }
                    }
                }
            }
            else
            {

            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            Response.Redirect("RequestBuy.aspx");
        }
    }
}