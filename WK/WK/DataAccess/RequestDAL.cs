using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WK.Data;

namespace WK.DataAccess
{
    public class RequestDAL:Base
    {

        public bool RequestInsert(RequestData data)
        {
            int count = 0;

            bool result = false;

            SqlCommand command = new SqlCommand();          
            SqlConnection connect = null;
            SqlTransaction transection;

            try
            {
                #region 1.การเชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2.การยิง query
                transection = connect.BeginTransaction(IsolationLevel.ReadCommitted);

                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO REQUEST (REQUEST_ID, APPROVE_ID, [SUBJECT], [DESCRIPTION], CREATE_BY, CREATE_DATE, PATH_FILE) VALUES('"+data.RequestID+"', '"+data.ApproveID+"', '"+data.Subject+"', '"+data.Description+"', '"+data.CreateBy+"', CONVERT(datetime, '"+DateTime.Now.ToString("dd/MM/yyyy")+"', 103), '"+data.Pate_File+"'); ";
                command.Transaction = transection;
                #endregion

                #region 3.การรีเทินร์ผลลัพท์
                count = command.ExecuteNonQuery();

                if (count > 0)
                {
                    result = true;
                    transection.Commit();
                }
                else
                {
                    result = false;
                    transection.Rollback();
                }

                #endregion
            }//try
            catch (Exception ex)
            {
                string x = ex.Message;
            }//catch
            finally
            {
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

            return result;
        }

    }
}