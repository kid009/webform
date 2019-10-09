using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WK_API
{
    public class Base
    {
        string connect = ConfigurationManager.ConnectionStrings["SqlConnection"].ToString();

        public SqlConnection GetConnection()
        {
            SqlConnection sqlconnection = null;

            if (!connect.Equals(""))
            {
                sqlconnection = new SqlConnection(connect); //สร้างการ connection

                if (sqlconnection != null && sqlconnection.State == ConnectionState.Closed)
                {
                    sqlconnection.Open();
                }
            }

            return sqlconnection;
        }//GetConnection()
    }
}