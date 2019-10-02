using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using WK.Data;

namespace WK.DataAccess
{
    public class MenuDAL: Base
    {
        public List<MenuData> LoadMenu(string role)
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader datareader = null;
            SqlConnection connect = null;

            List<MenuData> listmenu = null;

            try
            {
                #region 1. เชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2. การยิง query sql
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT MENU_CODE, MENU_NAME FROM MENU INNER JOIN ROLE ON (ROLE.ROLE_CODE = MENU.MENU_CODE) WHERE ROLE.ROLE_NAME = '"+role+"' ";
                #endregion

                #region 3. รีเทินผลลัพท์
                datareader = command.ExecuteReader();

                if (datareader != null && datareader.HasRows)
                {
                    listmenu = new List<MenuData>();

                    while (datareader.Read())
                    {
                        MenuData data = new MenuData();

                        data.Code = datareader["MENU_CODE"].ToString().Trim();
                        data.Name = datareader["MENU_NAME"].ToString().Trim();

                        listmenu.Add(data);
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
                if ((datareader != null) && (!datareader.IsClosed))
                {
                    datareader = null;
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
            return listmenu;
        }//LoadMenu

    }
}