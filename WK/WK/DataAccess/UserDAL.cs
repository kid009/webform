using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using WK.Data;

namespace WK.DataAccess
{
    public class UserDAL:Base
    {
        public int CheckLogin(string username, string password, string role)
        {
            int count = 0;

            SqlCommand command = new SqlCommand();
            SqlDataReader datareader = null;
            SqlConnection connect = null;

            try
            {
                #region 1.การเชื่อมต่อฐานข้อมูล

                connect = this.GetConnection();
                command.Connection = connect;

                #endregion

                #region 2.การยิง query

                command.CommandType = CommandType.Text;
                command.CommandText = "select count(*) as [count] from[USER] where USER_NAME = '"+username+"' and USER_PASSWORD = '"+password+"' and USER_ROLE = '"+role+"' ";

                #endregion

                #region 3.การรีเทินร์ผลลัพท์

                datareader = command.ExecuteReader();

                if (datareader != null && datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        count = Convert.ToInt32(datareader["count"]);
                    }
                }

                #endregion
            }
            catch(Exception ex)
            {
                string x = ex.Message;
            }
            finally
            {
                if (datareader != null && !datareader.IsClosed)
                {
                    datareader = null;
                }

                if (command != null)
                {
                    command = null;
                }

                if (connect != null && connect.State == ConnectionState.Open)
                {
                    connect.Close();
                    connect = null;
                }

            }

            return count;
        }//checklogin

    }
}