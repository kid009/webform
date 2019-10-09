using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WK.Data;

namespace WK.DataAccess
{
    public class ItemDal:Base
    {

        public DataSet GetListItem()
        {
            DataSet result = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlConnection connect = new SqlConnection();
            SqlCommand command = new SqlCommand();

            try
            {
                #region 1. เชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2. การยิง query sql
                command.CommandType = CommandType.Text;
                command.CommandText = " SELECT [CODE] ,[NAME] ,[PRICE] FROM [DB_WORKFLOW].[dbo].[ITEM] ";
                #endregion

                #region 3. รีเทินผลลัพท์
                adapter.SelectCommand = command;
                adapter.Fill(result);
                #endregion
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            finally
            {
                if (command != null)
                {
                    command = null;
                }

                if ((connect != null) && (connect.State == ConnectionState.Open))
                {
                    connect.Close();
                    connect = null;
                }
            }

            return result;
        }//GetListItem()


        public bool RequestStockInsert(Item item, string reqId)
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
                command.CommandText = " INSERT INTO [REQUEST_STOCK] ([REQUEST_ID],[CODE],[NAME]) VALUES('"+reqId+"', '"+item.Code+"', '"+item.Name+"') ";
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

        public bool StockInsert(Item item)
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
                command.CommandText = " INSERT INTO [ITEM] ([CODE],[NAME],[PRICE]) VALUES('" + (item.Code + DateTime.Now.Ticks.ToString().Substring(DateTime.Now.Ticks.ToString().Length - 5, 5)) + "', '" + item.Name + "', '"+item.Price+"') ";
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

        public DataSet GetRequestStockById(string req_id)
        {
            DataSet result = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlConnection connect = new SqlConnection();
            SqlCommand command = new SqlCommand();

            try
            {
                #region 1. เชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2. การยิง query sql
                command.CommandType = CommandType.Text;
                command.CommandText = " SELECT [REQUEST_ID],[CODE] ,[NAME] FROM [DB_WORKFLOW].[dbo].[REQUEST_STOCK] where [REQUEST_ID] = '"+req_id+"' ";
                #endregion

                #region 3. รีเทินผลลัพท์
                adapter.SelectCommand = command;
                adapter.Fill(result);
                #endregion
            }
            catch (Exception ex)
            {
                string x = ex.Message;
            }
            finally
            {
                if (command != null)
                {
                    command = null;
                }

                if ((connect != null) && (connect.State == ConnectionState.Open))
                {
                    connect.Close();
                    connect = null;
                }
            }

            return result;
        }//GetListItem()
    }
}