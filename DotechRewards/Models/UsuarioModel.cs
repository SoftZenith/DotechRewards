using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DotechRewards.Util.Database;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;

namespace DotechRewards.Models
{
    public class UsuarioModel
    {

        public int idUsuario { get; set; }
        [Required(ErrorMessage = "Usuario requerido.")]
        [Display(Name = "Usuario")]
        [DataType(DataType.Text)]
        public string usuario { get; set; }
        [Required(ErrorMessage = "Contraseña requerido.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contrasena { get; set; }
        public int puntos { get; set; }
        public int adminFlg { get; set; }
        public int activoFlg { get; set; }
        public int estatus { get; set; }

        public List<banner> banners;

        public List<recompensa> recompensas;

        public List<Evento> eventos;

        public List<banner> getBanners() {
            return this.banners == null ? new List<banner>() : this.banners;
        }

        public List<recompensa> getRecompensas() {
            return this.recompensas;
        }

        public void setBanners(List<banner> banners) {
            this.banners = banners;
        }

        public void setRecompensas(List<recompensa> recompensas) {
            this.recompensas = recompensas;
        }

        /// <summary>
        /// Valida credenciales de acceso de usuario
        /// </summary>
        public int login(string user, string pass)
        {
            int res = 0;

            using (SqlConnection cnn = Context.Connect())
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '" + user + "' and contrasena = '" + pass + "'", cnn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    res = 1;
                }
                else
                {
                    res = -1;
                }
                cnn.Close();
                cnn.Dispose();
            }//using para crear conexión con metodo de clase Context
            return res;
        }//login()
        /// <summary>
        /// Obtiene la lista de banners que se mostraran en la pantalla principal de usuario.
        /// </summary>
        public UsuarioModel getBanner() {
            List<banner> bannerPrincipal = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                    SqlCommand cmd = new SqlCommand("SP_GET_BANNER", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    bannerPrincipal = new List<banner>();
                    while (reader.Read())
                    {
                        var fechaBefore = reader["fecha"].ToString();
                        var fecha3 = fechaBefore.Split(' ');
                        var descomponer = fecha3[1].Split(':');
                        fechaBefore = fecha3[0] + " " + descomponer[0] + ":" + descomponer[1] + " " + fecha3[2];
                        bannerPrincipal.Add(new banner(
                            Convert.ToInt16(reader["idActividad"].ToString()),
                            reader["imagen"].ToString(),
                            reader["nombre"].ToString(),
                            fechaBefore,
                            reader["lugar"].ToString(),
                            Convert.ToInt16(reader["confirmacion"].ToString()),
                            Convert.ToInt16(reader["asistentes"].ToString()),
                            reader["url"].ToString()
                            ));
                    }
                    
                }
                catch (Exception ex) {
                    System.Console.WriteLine(ex);
                }
                finally {
                    cnn.Close();
                    cnn.Dispose();
                }
            }

            UsuarioModel UsuarioM = new UsuarioModel();
            UsuarioM.setBanners(bannerPrincipal);
            this.banners = bannerPrincipal;
            //this.banners = this.banners.Where(banner => DateTime.Parse(banner.fecha).Month >= DateTime.Now.Month && DateTime.Parse(banner.fecha).Month <= DateTime.Now.Month + 2).ToList();
            return UsuarioM;
        }
        /// <summary>
        /// Obtiene la lista de banners por usuario.
        /// </summary>
        public UsuarioModel getBanner(String user)
        {
            List<banner> bannerPrincipal = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_GET_BANNER_USR", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = user;

                    SqlDataReader reader = cmd.ExecuteReader();
                    bannerPrincipal = new List<banner>();
                    while (reader.Read())
                    {
                        var fechaBefore = reader["fecha"].ToString();
                        var fecha3 = fechaBefore.Split(' ');
                        var descomponer = fecha3[1].Split(':');
                        fechaBefore = fecha3[0] + " " + descomponer[0] + ":" + descomponer[1] + " " + fecha3[2] + " " + fecha3[3];
                        bannerPrincipal.Add(new banner(
                            Convert.ToInt32(reader["idUsuario"] is DBNull ? 0 : reader["idUsuario"]),
                            Convert.ToInt32(reader["asistencia"] is DBNull ? 0 : reader["asistencia"]),
                            Convert.ToInt16(reader["idActividad"].ToString()),
                            reader["imagen"].ToString(),
                            reader["nombre"].ToString(),
                            fechaBefore,
                            reader["lugar"].ToString(),
                            Convert.ToInt16(reader["confirmacion"].ToString()),
                            Convert.ToInt16(reader["asistentes"].ToString()),
                            reader["url"].ToString()
                            ));
                    }

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }

            UsuarioModel UsuarioM = new UsuarioModel();
            UsuarioM.setBanners(bannerPrincipal);
            this.banners = bannerPrincipal;
            //this.banners = this.banners.Where(banner => DateTime.Parse(banner.fecha).Month >= DateTime.Now.Month && DateTime.Parse(banner.fecha).Month <= DateTime.Now.Month + 2).ToList();
            return UsuarioM;
        }
        /// <summary>
        /// Obtiene tiene la lista de recompensas para mostrar en carrusel inferior.
        /// </summary>
        public UsuarioModel getRecompensa()
        {
            List<recompensa> bannerRecompensas = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                    SqlCommand cmd = new SqlCommand("SP_GET_RECOMPENSAS", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;


                    SqlDataReader reader = cmd.ExecuteReader();
                    bannerRecompensas = new List<recompensa>();
                    while (reader.Read())
                    {
                        bannerRecompensas.Add(new recompensa(
                            reader["nombre"].ToString(),
                            reader["descripcion"].ToString(),
                            Convert.ToInt16(reader["puntos"].ToString()),
                            reader["imagen"].ToString()));
                    }

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }

            UsuarioModel UsuarioM = new UsuarioModel();
            UsuarioM.setRecompensas(bannerRecompensas);
            this.recompensas = bannerRecompensas;
            return UsuarioM;
        }
        /// <summary>
        /// Obtiene lista de eventos.
        /// </summary>
        public UsuarioModel getEventos(string user)
        {
            List<Evento> eventos = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                    SqlCommand cmd = new SqlCommand("SP_GET_HISTORIAL_ACTIVIDADES", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = user;
                    SqlDataReader reader = cmd.ExecuteReader();
                    eventos = new List<Evento>();
                    while (reader.Read())
                    {
                        string fecha_mes = "";
                        try
                        {
                            //CultureInfo esMX = new CultureInfo("es-MX");
                            string fecha_normal = reader["fecha"].ToString().Substring(0, 10).Replace("/","-");
                            DateTime dt = DateTime.Parse(fecha_normal);
                            fecha_mes = dt.ToString("dd-MMM-yyyy").Replace(".", "");
                            string[] fecha_mes_arreglo = fecha_mes.Split('-');
                            fecha_mes_arreglo[1] = fecha_mes_arreglo[1][0].ToString().ToUpper() + fecha_mes_arreglo[1][1] + fecha_mes_arreglo[1][2];
                            fecha_mes = String.Join("-",fecha_mes_arreglo);
                        }
                        catch (Exception ex) {
                            fecha_mes = ex.ToString();
                        }
                        eventos.Add(new Evento(
                                //reader["fecha"].ToString().Substring(0, 10),
                                fecha_mes,
                                reader["nombre"].ToString(),
                                Convert.ToInt16(reader["PUNTOS"].ToString())
                            ));
                    }

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }

            UsuarioModel UsuarioM = new UsuarioModel();
            this.eventos = eventos;
            return UsuarioM;
        }
        /// <summary>
        /// Obtiene historial de asignación/cobro de puntos de un usuario
        /// </summary>
        public UsuarioModel getHistorialUsuario(string user)
        {
            List<Evento> eventos = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                    SqlCommand cmd = new SqlCommand("SP_GET_HISTORIAL_USUARIO", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = user;
                    SqlDataReader reader = cmd.ExecuteReader();
                    eventos = new List<Evento>();
                    while (reader.Read())
                    {
                        eventos.Add(new Evento(
                                Convert.ToInt16(reader["idUsuario"].ToString()),
                                reader["nombre"].ToString(),
                                reader["fecha"].ToString().Substring(0, 10),
                                Convert.ToInt16(reader["PUNTOS"].ToString())
                            ));
                    }

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }

            UsuarioModel UsuarioM = new UsuarioModel();
            this.eventos = eventos;
            return UsuarioM;
        }

        /// <summary>
        /// Obtiene total de puntos para un usuario.
        /// </summary>
        public int getTotalPuntos(string user)
        {
            List<Evento> eventos = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                    SqlCommand cmd = new SqlCommand("SP_GET_TOTAL_PUNTOS", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = user;
                    SqlDataReader reader = cmd.ExecuteReader();

                    System.Console.WriteLine(this.puntos + " total del puntos");
                    while (reader.Read()) { 

                        try
                        {
                            this.puntos = Convert.ToInt32(reader["PUNTOS"].ToString());
                        }
                        catch (Exception e)
                        {
                            this.puntos = 0;
                        }
                        
                    }
                    return this.puntos;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return this.puntos;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
        }
        /// <summary>
        /// Obtiene bandera para saber si estan activados los puntos.
        /// </summary>
        public void getActivacionStatus(string user)
        {
            List<Evento> eventos = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_GET_ACTIVACION_PUNTOS", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = user;
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        this.estatus = Convert.ToInt16(reader["ESTATUS"].ToString());
                    }

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
            }
        }

        /// <summary>
        /// Cambia contraseña de usuario.
        /// </summary>
        public int cambiar(string user, string pass)
        {
            int res = 0;
            try
            {
                using (SqlConnection cnn = Context.Connect())
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("update DR_CAT_USUARIO " +
                        "set contrasena = '" + pass + "' where usuario = '" + user + "'", cnn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    res = 1;
                    cnn.Close();
                    cnn.Dispose();
                }//using para crear conexión con metodo de clase Context
            }
            catch (Exception ex) {
                res = -1;
            }
            return res;
        }//login()

        /// <summary>
        /// Agrega confirmación a evento de un usuario
        /// </summary>
        public int AddConfirmacion(int confirmacion, int asistentes, int idActividad, string usuario)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                int res = 0;
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_ADD_CONFIRMACION", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                    cmd.Parameters.Add("@asistencia", SqlDbType.Int).Value = asistentes;
                    cmd.Parameters.Add("@idactividad", SqlDbType.Int).Value = idActividad;
                    cmd.Parameters.Add("@confirmacion", SqlDbType.Int).Value = confirmacion;

                    SqlDataReader reader = cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    res = 0;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                    res = 1;
                }
                return res;
            }
        }

        /// <summary>
        /// Activa puntos de usuario en B.D. 
        /// </summary>
        public int ActivarPuntos(string usuario)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                int res = 0;
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_ACTIVAR_PUNTOS", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;

                    SqlDataReader reader = cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    res = 0;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                    res = 1;
                }
                return res;
            }
        }
    }//class UsuarioModel

    public class banner {

        public banner(string imagen, string nombre, string fecha, string lugar, int registro) {
            this.imagen = imagen;
            this.nombre = nombre;
            this.fecha = fecha;
            this.lugar = lugar;
            this.registro = registro;
        }

        public banner(int idEvento, string imagen, string nombre, string fecha, string lugar, int registro, int asistentes, string url)
        {
            this.idEvento = idEvento;
            this.imagen = imagen;
            this.nombre = nombre;
            this.fecha = fecha;
            this.lugar = lugar;
            this.registro = registro;
            this.asistentes = asistentes;
            this.url = url;
        }
        public banner(int idUsuario, int asistentesUsr, int idEvento, string imagen, string nombre, string fecha, string lugar, int registro, int asistentes)
        {
            this.idUsuario = idUsuario;
            this.asistentesUsr = asistentesUsr;
            this.idEvento = idEvento;
            this.imagen = imagen;
            this.nombre = nombre;
            this.fecha = fecha;
            this.lugar = lugar;
            this.registro = registro;
            this.asistentes = asistentes;
        }
        public banner(int idUsuario, int asistentesUsr, int idEvento, string imagen, string nombre, string fecha, string lugar, int registro, int asistentes, string url) : this(idUsuario, asistentesUsr, idEvento, imagen, nombre, fecha, lugar, registro, asistentes)
        {
            this.url = url;
        }
        public int idUsuario { get; set; }
        public int asistentesUsr { get; set; }
        public int idEvento { get; set; }
        public string imagen { get; set; }
        public string nombre { get; set; }
        public string fecha { get; set; }
        public string lugar { get; set; }
        public int registro { get; set; }
        public int asistentes { get; set; }
        public string url { get; set; }
    }

    public class recompensa {
        public recompensa(string nombre, string descripcion, int puntos, string imagen)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.puntos = puntos;
            this.imagen = imagen;
        }

        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int puntos { get; set; }
        public string imagen { get; set; }

    }
}