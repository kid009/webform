using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WK_API.DataAccess;
using WK_API.Data;

namespace WK_API
{
    public class ReqController : ApiController
    {
        // GET api/<controller>

        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        public IEnumerable<RequestData> GetRequestAll()
        {
            RequestDAL dal = new RequestDAL();
            List<RequestData> listData = dal.GetRequestAll();

            return listData;
        }

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public IEnumerable<RequestData> GetRequestByUser(string name)
        {
            RequestDAL dal = new RequestDAL();
            List<RequestData> listData = dal.GetRequest(name);

            return listData;
        }


        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}