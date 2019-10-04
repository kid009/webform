using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WK.DataAccess;
using WK.Data;
using System.Configuration;
using System.IO;

namespace WK
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private string path_file = ConfigurationManager.AppSettings["path_file"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null)
                {
                    string username = Session["username"].ToString().Trim();

                    hdUserName.Value = username;

                    UserDAL dal = new UserDAL();

                    Info data = dal.GetInfo(username);

                    if (data != null)
                    {
                        lbUserName.Text = data.UserName;
                        lbName.Text = data.Name;
                        lbPosition.Text = data.Position;
                        lbDept.Text = data.Dept;

                        List<Info> listmanager = dal.GetManager();

                        if (listmanager != null && listmanager.Count > 0)
                        {
                            ddlApprove.DataValueField = "UserName";
                            ddlApprove.DataTextField = "Name";

                            ddlApprove.DataSource = listmanager;
                            ddlApprove.DataBind();
                        }

                    }
                }
            }
            else
            {

            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile == true)
            {
                RequestData data = new RequestData();

                data.ApproveID = ddlApprove.SelectedItem.Value;
                data.Subject = txtSubject.Text;
                data.Description = txtDescription.Text;
                data.CreateBy = hdUserName.Value;
                data.Path_File = path_file + fileUpload.PostedFile.FileName;

                RequestDAL dal = new RequestDAL();

                bool check = dal.RequestInsert(data);

                if (check == true)
                {
                    string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                    fileUpload.PostedFile.SaveAs(Server.MapPath(path_file) + fileName);

                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "Message_Show('Save Success')", true);
                }
            }
            else
            {
                //เป็นฟังก์ชัน javascript เรียกที่ main.master
                //RegisterClientScriptBlock(typeof(Page), "myscript", "ชื่อฟังก์ชัน", true);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "Message_Show('กรุณาอัพโหลดไฟล์')", true);
            }
        }//btnSend_Click

    }
}