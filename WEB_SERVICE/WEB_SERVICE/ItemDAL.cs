using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WEB_SERVICE
{
    public class ItemDAL:Base
    {

        public ItemDATA GetListItem(string code)
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            SqlConnection connect = null;

            ItemDATA data = null;

            try
            {
                #region 1. เชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2. การยิง query sql
                command.CommandType = CommandType.Text;
                command.CommandText = " SELECT* FROM [ITEM] where CODE = '"+code+"' ";
                #endregion

                #region 3. รีเทินผลลัพท์
                reader = command.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data = new ItemDATA();

                        data.CODE = reader["CODE"].ToString();
                        data.NAME = reader["NAME"].ToString();
                        data.PRICE = reader["PRICE"].ToString();
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
                if ((reader != null) && (!reader.IsClosed))
                {
                    reader = null;
                }

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
            return data;
        }

        public List<ItemDATA> GetListItem()
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            SqlConnection connect = null;

            List<ItemDATA> listresult = null;

            try
            {
                #region 1. เชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2. การยิง query sql               
                command.CommandType = CommandType.Text;
                command.CommandText = " select * from item ";
                #endregion

                #region 3. รีเทินผลลัพท์
                reader = command.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    listresult = new List<ItemDATA>();

                    while (reader.Read())
                    {
                        ItemDATA data = new ItemDATA();

                        data.CODE = reader["CODE"].ToString();
                        data.NAME = reader["NAME"].ToString();
                        data.PRICE = reader["PRICE"].ToString();

                        listresult.Add(data);
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
                if ((reader != null) && (!reader.IsClosed))
                {
                    reader = null;
                }

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
            return listresult;
        }
    }
}