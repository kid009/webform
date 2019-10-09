using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WK.DataAccess;
using WK.Data;
using System.IO;
using System.Configuration;
using System.Data;
using WK.WK_SERVICE;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Net;
using System.Web.Script.Serialization;

namespace WK
{   
    public partial class WebForm3 : System.Web.UI.Page
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

                        //แสดงข้อมูลสินค้าใน checklist bod
                        GetListItem();
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
                string req_Id = hdReqIdStock.Value;
                bool check = dal.RequestInsert(data, req_Id);

                if (check == true)
                {
                    string fileName = Path.GetFileName(fileUpload.PostedFile.FileName);
                    fileUpload.PostedFile.SaveAs(Server.MapPath(path_file) + fileName);

                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "Message_Show('Save Success')", true);

                    Response.Redirect("DisplayStock.aspx?ReqId=" + req_Id);
                }
            }
            else
            {
                //เป็นฟังก์ชัน javascript เรียกที่ main.master
                //RegisterClientScriptBlock(typeof(Page), "myscript", "ชื่อฟังก์ชัน", true);
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "Message_Show('กรุณาอัพโหลดไฟล์')", true);
            }
        }//btnSend_Click

        private void GetListItem()
        {
            ItemDal dal = new ItemDal();

            DataSet ds = dal.GetListItem();

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                chbProdect.DataValueField = "Code";
                chbProdect.DataTextField = "Name";

                chbProdect.DataSource = ds.Tables[0];
                chbProdect.DataBind();
            }
        }

        protected void btnSaveProduct_Click(object sender, EventArgs e)
        {
            RequestData data = new RequestData();

            hdReqIdStock.Value = data.RequestID_Stock;

            Item item = null;

            foreach (ListItem chb in chbProdect.Items)
            {
                item = new Item();

                string reqId = hdReqIdStock.Value;

                if (chb.Selected == true)
                {
                    item.Code = chb.Value;
                    item.Name = chb.Text;

                    //insert to table request_stock
                    ItemDal dal = new ItemDal();

                    dal.RequestStockInsert(item, reqId);

                    lblStatus.Text = "บันทึกสินค้า เรียบร้อย";
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                }
            }

        }//btnSaveProduct_Click

        protected void btnLoadDataService_Click(object sender, EventArgs e)
        {
            WebService1 obj = new WebService1();

            string dataJson = obj.GetItemAll(); //return json

            string xml = obj.GetItemByCode("ITEM_003");//retuen xml

            if (xml != "")
            {
                StringReader txtReader = new StringReader(xml);

                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Item));
                Item data = (Item)xmlSerializer.Deserialize(txtReader);
            }

            if (dataJson != "")
            {
                List<Item> item_data = JsonConvert.DeserializeObject<List<Item>>(dataJson);

                if (item_data != null && item_data.Count > 0)
                {
                    foreach (Item item in item_data)
                    {
                        ItemDal dal = new ItemDal();
                        dal.StockInsert(item);

                        this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "Message_Show('Upload Success')", true);

                        GetListItem();

                        btnLoadDataService.Enabled = false;
                    }
                }
            }

        }

        protected void ddlApi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApi.SelectedItem.Value == "1")
            {
                string url = "http://localhost:54327/api/req";

                WebClient client = new WebClient();

                string data = client.DownloadString(url);

                JavaScriptSerializer serializer = new JavaScriptSerializer(); //create object JavaScriptSerializer

                var deSerializerResult = serializer.Deserialize<List<RequestData>>(data); // change string to RequestData

                List<RequestData> listData = deSerializerResult as List<RequestData>;
            }

            if (ddlApi.SelectedItem.Value == "2")
            {
                string url = "http://localhost:54327/api/req?name=man001";

                WebClient client = new WebClient();

                string data = client.DownloadString(url);

                JavaScriptSerializer serializer = new JavaScriptSerializer(); //create object JavaScriptSerializer

                var deSerializerResult = serializer.Deserialize<List<RequestData>>(data); // change string to RequestData

                List<RequestData> listData = deSerializerResult as List<RequestData>;
            }

        }
    }
}