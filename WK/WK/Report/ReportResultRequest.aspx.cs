using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WK.DataAccess;
using WK.Data;
using System.Data;

namespace WK.Report
{
    public partial class ReportResultRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["username"] != null)
                {
                    pnl_eidt.Visible = false;

                    string username = Session["username"].ToString();

                    HiddenField1.Value = username;

                    DisplayInfo(username);

                    DisplayRequest(username);
                }
            }
            else
            {

            }
        }//Page_Load

        protected string GetHost(Object file)
        {
            string fullpath = "";

            if (file != null)
            {
                if (file.ToString() != "")
                {
                    fullpath = "../DisplayPDF.aspx?url=" + file.ToString();
                }
            }
            return fullpath;
        }//GetHost

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            pnl_eidt.Visible = true;

            Button btnEdit = sender as Button;

            GridViewRow gRow = (GridViewRow)btnEdit.NamingContainer;

            string request_id = ResultGridView.DataKeys[gRow.RowIndex].Value.ToString();

            if (request_id != "")
            {
                RequestDAL dal = new RequestDAL();

                RequestData data = dal.GetRequestId(request_id);

                if (data != null)
                {
                    txtRequestId.Text = data.RequestID;
                    txtSubject.Text = data.Subject;
                    txtDescription.Text = data.Description;
                    txtNote.Text = data.Note;
                }
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //เมื่อกดปุ่ม btnDelete ให้ฟอร์มแก้ไขหาย
            pnl_eidt.Visible = false;
            //เมื่อกดปุ่มลบ จะแปลงค่าให้อยู่ในรูปแบบของ ImageButton
            Button btnImg = sender as Button;
            //เเปลง ImageButton ให้อยู่ในรูปของ GridViewRow
            GridViewRow gRow = (GridViewRow)btnImg.NamingContainer;
            //get ค่า DataKeyNames="RequestID" 
            string request_id = ResultGridView.DataKeys[gRow.RowIndex].Value.ToString();

            if (request_id != "")
            {
                RequestDAL dal = new RequestDAL();

                bool cehck_delete = dal.RequestDelete(request_id);

                if (cehck_delete == true)
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "Message_Show('Delete_Success')", true);

                    string username = HiddenField1.Value;

                    DisplayRequest(username);
                }
            }
        }

        private void DisplayInfo(string username)
        {
            UserDAL dal = new UserDAL();

            Info data = dal.GetInfo(username);

            if (data != null)
            {
                lbUserName.Text = data.UserName;
                lbName.Text = data.Name;
                lbPosition.Text = data.Position;
                lbDept.Text = data.Dept;
            }
        }

        private void DisplayRequest(string username)
        {
            RequestDAL dal = new RequestDAL();

            List<RequestData> listresult = dal.GetRequest(username);

            if (listresult != null && listresult.Count > 0)
            {
                ResultGridView.DataSource = listresult;
                ResultGridView.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            RequestData data = new RequestData();

            data.RequestID = txtRequestId.Text;
            data.Subject = txtSubject.Text;
            data.Description = txtDescription.Text;
            data.Note = txtNote.Text;
            data.Status = "Reject";

            RequestDAL dal = new RequestDAL();

            #region update request table

            bool check_request = dal.RequestUpdate(data);

            #endregion

            #region update request_reject table

            bool check_request_reject = dal.RequestRejectInsert(data);

            #endregion

            if (check_request == true && check_request_reject == true)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "Message_Show('Save_success')", true);

                string username = HiddenField1.Value;

                DisplayRequest(username);
            }
            else
            {
                this.Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "myscript", "Message_Show('Error')", true);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string[] data_arr = new string[3];

            data_arr[0] = txtSearch.Text;
            data_arr[1] = txtSearcjNameSubject.Text;
            data_arr[2] = txtDt.Text;

            RequestDAL dal = new RequestDAL();

            List<RequestData> listResult = dal.GetRequest(data_arr);

            if (listResult != null && listResult.Count > 0)
            {
                ResultGridView.DataSource = listResult;
                ResultGridView.DataBind();
            }
        }

        protected void btnLinQ_Click(object sender, EventArgs e)
        {
            RequestDAL dal = new RequestDAL();

            string dt = txtDt.Text;

            DataSet ds = dal.GetRequest(dt, null);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dataTB = ds.Tables[0];

                var dataRow = dataTB.AsEnumerable().Where(x => x.Field<string>("REQUEST_ID") == txtSearch.Text);

                DataTable result = dataRow.CopyToDataTable<DataRow>();

                if (result != null && result.Rows.Count > 0)
                {
                    ResultGridView.DataSource = result;
                    ResultGridView.DataBind();
                }
                else
                {
                    ResultGridView.DataSource = null;
                    ResultGridView.DataBind();
                }
            }

        }
    }
}