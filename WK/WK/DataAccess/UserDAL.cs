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

        public Info GetInfo(string username)
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            SqlConnection connect = null;

            Info data = null;

            try
            {
                #region 1.การเชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2.การยิง query
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT [USER_NAME],[NAME],[POSITION],[DEPT]FROM [INFO] WHERE USER_NAME = '"+username+"' ";
                #endregion

                #region 3.การรีเทินร์ผลลัพท์
                reader = command.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data = new Info();

                        data.UserName = reader["USER_NAME"].ToString();
                        data.Name = reader["NAME"].ToString();
                        data.Position = reader["POSITION"].ToString();
                        data.Dept = reader["DEPT"].ToString();
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader = null;
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
            return data;
        }

        public List<Info> GetManager()
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            SqlConnection connect = null;

            List<Info> listmanager = null;

            try
            {
                #region 1.การเชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2.การยิง query
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT INFO.USER_NAME, INFO.NAME FROM INFO INNER JOIN[USER] ON([USER].USER_NAME = INFO.USER_NAME) WHERE[USER].USER_ROLE = 'MANAGER'";
                #endregion

                #region 3.การรีเทินร์ผลลัพท์
                reader = command.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    listmanager = new List<Info>();

                    while (reader.Read())
                    {
                        Info data = new Info();

                        data.UserName = reader["USER_NAME"].ToString();
                        data.Name = reader["NAME"].ToString();

                        listmanager.Add(data);
                    }
                }
                #endregion
            }//try
            catch (Exception ex)
            {
                string x = ex.Message;
            }//catch
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader = null;
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
            }//finally

            return listmanager;

        }//GetManager()

    }
}