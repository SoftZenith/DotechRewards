using DotechRewards.Util.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DotechRewards.Models
{
    public class PasswordModel
    {

        public string UUID { get; set; }


        public bool GetDateURL(string uuid) {
            using (SqlConnection cnn = Context.Connect())
            {
                bool res = false;
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_GET_DATE_URL_RESET", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@uuid", SqlDbType.VarChar).Value = uuid;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read()) {
                        res = true;
                    }
                }
                catch (Exception ex)
                {
                    res = false;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return res;
            }
        }

        public static bool AddResetPassword(string uuid, string usuario)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                bool res = false;
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_SET_RESETE_PASSWORD", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@uuid", SqlDbType.VarChar).Value = uuid;
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;

                    SqlDataReader reader = cmd.ExecuteReader();
                    res = true;
                }catch (Exception ex)
                {
                    res = false;
                }finally{
                    cnn.Close();
                    cnn.Dispose();
                }
                return res;
            }
        }

        public static bool ResetPassword(string uuid, string password)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                bool res = false;
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_GET_RESET_PASSWORD", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@uuid", SqlDbType.VarChar).Value = uuid;
                    cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;

                    SqlDataReader reader = cmd.ExecuteReader();
                    res = true;
                }
                catch (Exception ex)
                {
                    res = false;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return res;
            }
        }
    }
}