using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace WEB_SERVICE
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public int CalculateNum(int x, int y)
        {
            int result = 0;
            result = x + y;
            return result;
        }


        [WebMethod]
        public string GetItemByCode(string code)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ItemDATA), new XmlRootAttribute("Item"));
            string xml = "";

            ItemDAL dal = new ItemDAL();
            ItemDATA data = dal.GetListItem(code);

            if (data != null)
            {
                using (StringWriter stringWriter = new StringWriter())
                {
                    serializer.Serialize(stringWriter, data);
                    xml = stringWriter.ToString();
                }
            }

            return xml;

        }//GetItemByCode(string code)


        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetItemAll()
        {
            ItemDAL dal = new ItemDAL();
            List<ItemDATA> listData = dal.GetListItem();

            return JsonConvert.SerializeObject(listData);
        }//GetItemAll()


    }
}
