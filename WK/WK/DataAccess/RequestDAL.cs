﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using WK.Data;
using WK.DataAccess;

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
                command.CommandText = "INSERT INTO REQUEST (REQUEST_ID, APPROVE_ID, [SUBJECT], [DESCRIPTION], CREATE_BY, CREATE_DATE, PATH_FILE) VALUES('" + data.RequestID + "', '" + data.ApproveID + "', '" + data.Subject + "', '" + data.Description + "', '" + data.CreateBy + "', CONVERT(datetime, '" + DateTime.Now.ToString("dd/MM/yyyy") + "', 103), '" + data.Path_File + "'); ";
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

        public bool RequestInsert(RequestData data, string req_Id)
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
                command.CommandText = "INSERT INTO REQUEST (REQUEST_ID, APPROVE_ID, [SUBJECT], [DESCRIPTION], CREATE_BY, CREATE_DATE, PATH_FILE) VALUES('"+ req_Id + "', '"+data.ApproveID+"', '"+data.Subject+"', '"+data.Description+"', '"+data.CreateBy+"', CONVERT(datetime, '"+DateTime.Now.ToString("dd/MM/yyyy")+"', 103), '"+data.Path_File+"'); ";
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

        public List<RequestData> GetRequest(string username)
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            SqlConnection connect = null;

            List<RequestData> listresult = null;

            try
            {
                #region 1. เชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2. การยิง query sql               
                command.CommandType = CommandType.Text;
                command.CommandText = "select * from request where Approve_id = '"+username+"' ";
                #endregion

                #region 3. รีเทินผลลัพท์
                reader = command.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    listresult = new List<RequestData>();

                    while (reader.Read())
                    {
                        RequestData data = new RequestData();

                        data.RequestID = reader["request_id"].ToString();
                        data.ApproveID = reader["approve_id"].ToString();
                        data.Subject = reader["subject"].ToString();
                        data.Description = reader["description"].ToString();
                        data.CreateBy = reader["create_by"].ToString().Trim();
                        //data.CreateDate = Convert.ToDateTime(reader["create_date"].ToString()).ToString("dd/MM/yyyy");
                        data.CreateDate = reader["create_date"].ToString();
                        data.Path_File = reader["path_file"].ToString();

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
        }//GetRequest

        public List<RequestData> GetRequest(string[] data_arr)
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            SqlConnection connect = null;

            List<RequestData> listresult = null;

            try
            {
                #region 1. เชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2. การยิง query sql
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetRequestByCondition";

                SqlParameter parameterReqId = new SqlParameter();
                parameterReqId = new SqlParameter("@ReqId", data_arr[0]);
                parameterReqId.Direction = ParameterDirection.Input;
                parameterReqId.DbType = DbType.String;
                command.Parameters.Add(parameterReqId);

                SqlParameter parameterSubject = new SqlParameter();
                parameterSubject = new SqlParameter("@Subject", data_arr[1]);
                parameterSubject.Direction = ParameterDirection.Input;
                parameterSubject.DbType = DbType.String;
                command.Parameters.Add(parameterSubject);

                SqlParameter parameterDateNow = new SqlParameter();
                parameterDateNow = new SqlParameter("@DateNow", data_arr[2]);
                parameterDateNow.Direction = ParameterDirection.Input;
                parameterDateNow.DbType = DbType.String;
                command.Parameters.Add(parameterDateNow);

                #endregion

                #region 3. รีเทินผลลัพท์
                reader = command.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    listresult = new List<RequestData>();

                    while (reader.Read())
                    {
                        RequestData data = new RequestData();

                        data.RequestID = reader["request_id"].ToString();
                        data.ApproveID = reader["approve_id"].ToString();
                        data.Subject = reader["subject"].ToString();
                        data.Description = reader["description"].ToString();
                        data.CreateBy = reader["create_by"].ToString();
                        data.CreateDate = Convert.ToDateTime(reader["create_date"].ToString()).ToString("dd/MM/yyyy");
                        data.Path_File = reader["path_file"].ToString();

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
        }//GetRequest

        public DataSet GetRequest(string dt, object k)
        {
            DataSet resutl = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand();
            SqlConnection connect = null;           

            try
            {
                #region 1. เชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2. การยิง query sql               
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetRequestAll";

                SqlParameter param = new SqlParameter();
                param = new SqlParameter("@DateNow", dt);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;

                command.Parameters.Add(param);
                #endregion

                #region 3. รีเทินผลลัพท์
                adapter.SelectCommand = command;
                adapter.Fill(resutl);
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
            return resutl;
        }//GetRequest

        public RequestData GetRequestId(string request_id)
        {
            SqlCommand command = new SqlCommand();
            SqlDataReader reader = null;
            SqlConnection connect = null;

            RequestData data = null;

            try
            {
                #region 1. เชื่อมต่อฐานข้อมูล
                connect = this.GetConnection();
                command.Connection = connect;
                #endregion

                #region 2. การยิง query sql

                command.CommandType = CommandType.Text;
                command.CommandText = " select * from request where REQUEST_ID = '"+ request_id + "' ";
                #endregion

                #region 3. รีเทินผลลัพท์
                reader = command.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data = new RequestData();

                        data.RequestID = reader["request_id"].ToString().Trim();
                        data.Subject = reader["subject"].ToString().Trim();
                        data.Description = reader["description"].ToString().Trim();
                        data.Note = reader["note"].ToString();
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

        public bool RequestUpdate(RequestData data)
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
                command.CommandText = "UPDATE REQUEST SET REQUEST_ID = '" + data.RequestID + "', [SUBJECT] = '" + data.Subject + "', [DESCRIPTION] = '" + data.Description + "', [NOTE] = '" + data.Note + "', [STATUS] = '" + data.Status + "' WHERE REQUEST_ID = '" + data.RequestID + "' ";

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

        public bool RequestRejectInsert(RequestData data)
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

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[INSERT_REQUEST_REJECT]";

                SqlParameter request_id_param = new SqlParameter();
                request_id_param = new SqlParameter("@REQUEST_ID", data.RequestID);
                request_id_param.Direction = ParameterDirection.Input;
                request_id_param.DbType = DbType.String;
                command.Parameters.Add(request_id_param);

                SqlParameter create_date_param = new SqlParameter();
                create_date_param = new SqlParameter("@DATENOW", DateTime.Now.ToString("dd/MM/yyyy"));
                create_date_param.Direction = ParameterDirection.Input;
                create_date_param.DbType = DbType.String;
                command.Parameters.Add(create_date_param);

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

        public bool RequestDelete(string request_id)
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
                command.CommandText = "DELETE FROM REQUEST WHERE REQUEST_ID = '"+ request_id + "' ";

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