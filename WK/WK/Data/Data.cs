using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WK.Data //สร้าง class จากฐานข้อมูล
{
    public class MenuData
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Info
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Dept { get; set; }
    }

    public class RequestData
    {
        private string code = "REQ" + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 5, 5);

        public string RequestID
        {
            get { return code; }
            set { code = value; }
        }

        public string ApproveID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string Path_File { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }
    }

}