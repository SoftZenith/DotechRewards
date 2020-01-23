using DotechRewards.Util.Database;
using System.Data.SqlClient;
using System.Data;
using System;

namespace DotechRewards.Models
{
    public class HomeModel
    {
        public static int login(string user, string pass)
        {
            int res = 0;
            
            using (SqlConnection cnn = Context.Connect())
            {
                cnn.Open();
               
                //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                SqlCommand cmd = new SqlCommand("SP_LOGIN", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user;
                cmd.Parameters.Add("@pass", SqlDbType.VarChar).Value = pass;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    res = 1;
                }
                else {
                    res = -1;
                }
                cnn.Close();
                cnn.Dispose();
            }//using para crear conexión con metodo de clase Context
            return res;
        }//login()

        public static int getPermisos(string user) {
            int res = 0;

            using (SqlConnection cnn = Context.Connect())
            {
                cnn.Open();

                //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                SqlCommand cmd = new SqlCommand("SP_GET_PERMISOS", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        res = reader.GetInt32(0);
                    }
                }
                else
                {
                    res = -1;
                }
                cnn.Close();
                cnn.Dispose();
            }//using para crear conexión con metodo de clase Context
            return res;
        }

        public static string getNombreC(string user)
        {
            string nombre = "";

            using (SqlConnection cnn = Context.Connect())
            {
                cnn.Open();

                //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                SqlCommand cmd = new SqlCommand("SP_GET_NOMBRE", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@user", SqlDbType.VarChar).Value = user;

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        nombre = reader.GetString(0);
                    }
                }
                else
                {
                    nombre = "";
                }
                cnn.Close();
                cnn.Dispose();
            }//using para crear conexión con metodo de clase Context
            return nombre;
        }

        public static string recuperarContrasena(string user)
        {
            string pass = "";

            using (SqlConnection cnn = Context.Connect())
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select contrasena from DR_CAT_USUARIO where usuario = '" + user + "'", cnn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        pass = reader.GetString(0);
                    }
                }
                else
                {
                    pass = "";
                }
                cnn.Close();
                cnn.Dispose();
            }//using para crear conexión con metodo de clase Context
            return pass;
        }//login()

    }//class HomeModel
}