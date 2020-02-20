using DotechRewards.Util.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace DotechRewards.Models
{
    public class AdminModel
    {

        public List<Usuario> usuarios;
        public List<Evento> eventos;
        public List<Producto> productos;

        public AdminModel getUsuarios()
        {
            List<Usuario> usuarios = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                    SqlCommand cmd = new SqlCommand("SP_GET_USUARIOS", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    usuarios = new List<Usuario>();
                    while (reader.Read())
                    {
                        usuarios.Add(new Usuario(
                                Convert.ToInt32(reader["idUsuario"].ToString()),
                                reader["nombre"].ToString(),
                                reader["usuario"].ToString(),
                                reader["puesto"].ToString(),
                                reader["fecha_entrada"].ToString().Substring(0, 10),
                                reader["cumpleanios"].ToString().Substring(0, 10),
                                Convert.ToInt32(reader["puntos"].ToString())
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

            AdminModel UsuarioM = new AdminModel();
            this.usuarios = usuarios;
            return UsuarioM;
        }

        /// <summary>
        /// Obtiene todos los eventos activos en una lista de tipo Eventos.
        /// </summary>
        public AdminModel getEventos()
        {
            List<Evento> eventos = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    //SqlCommand cmd = new SqlCommand("select * from DR_CAT_USUARIO where usuario = '"+user+"' and contrasena = '"+pass+"'",cnn);
                    SqlCommand cmd = new SqlCommand("SP_GET_EVENTOS", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    eventos = new List<Evento>();
                    while (reader.Read())
                    {
                        /*eventos.Add(new Evento(
                                Convert.ToInt16(reader["idActividad"].ToString()),
                                reader["nombre"].ToString(),
                                reader["lugar"].ToString(),
                                reader["fecha"].ToString().Substring(0, 10),
                                Convert.ToInt16(reader["asistentes"].ToString()),
                                reader["imagen"].ToString(),
                                Convert.ToInt16(reader["puntos"].ToString()),
                                reader["url"].ToString()
                            ));*/
                        eventos.Add(new Evento()
                        {
                            idEvento = Convert.ToInt16(reader["idActividad"].ToString()),
                            nombre = reader["nombre"].ToString(),
                            lugar = reader["lugar"].ToString(),
                            fecha = reader["fecha"].ToString().Substring(0, 10),
                            asistentes = Convert.ToInt16(reader["asistentes"].ToString()),
                            imagen = reader["imagen"].ToString(),
                            puntos = Convert.ToInt16(reader["puntos"].ToString()),
                            url = reader["url"].ToString(),
                            confirmacion = Convert.ToInt16(reader["confirmacion"].ToString()) == 1 ? true : false,
                            confirmados = Convert.ToInt32(reader["confirmados"].ToString())
                        }); 
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

            AdminModel UsuarioM = new AdminModel();
            this.eventos = eventos;
            return UsuarioM;
        }

        /// <summary>
        /// Obtiene lista de productos/beneficios en una lista de tipo Producto.
        /// </summary>
        public AdminModel getProductos()
        {
            List<Producto> productos = null;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();
                    
                    SqlCommand cmd = new SqlCommand("SP_GET_PRODUCTOS", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();
                    productos = new List<Producto>();
                    while (reader.Read())
                    {
                        productos.Add(new Producto(
                                Convert.ToInt16(reader["idRecompensa"].ToString()),
                                reader["nombre"].ToString(),
                                reader["descripcion"].ToString(),
                                Convert.ToInt16(reader["puntos"].ToString()),
                                reader["imagen"].ToString()
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

            AdminModel UsuarioM = new AdminModel();
            this.productos = productos;
            return UsuarioM;
        }

        /// <summary>
        /// Agrega un usuario a la lista de usuarios, requiere una instancia de la clase Usuario.
        /// </summary>
        public int addUsuario(Usuario usuarioPost) {
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_ADD_USUARIO", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = usuarioPost.idUsuario;
                    cmd.Parameters.Add("@usuario", SqlDbType.NVarChar).Value = usuarioPost.usuario;
                    cmd.Parameters.Add("@nombre", SqlDbType.NVarChar).Value = usuarioPost.nombre;
                    cmd.Parameters.Add("@puesto", SqlDbType.NVarChar).Value = usuarioPost.puesto;
                    cmd.Parameters.Add("@fecha_entrada", SqlDbType.Date).Value = usuarioPost.fecha_entrada;
                    cmd.Parameters.Add("@cumpleaños", SqlDbType.Date).Value = usuarioPost.cumpleaños;
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return 1;
            }
        }

        /// <summary>
        /// Elimina producto de la lista de productos.
        /// </summary>
        public bool delUsuario(int idUsuario) {
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_DEL_USUARIO", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;

                    SqlDataReader reader = cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return false;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return true;
            }
        }

        public bool delEvento(int idEvento)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_DEL_EVENTO", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@idEvento", SqlDbType.Int).Value = idEvento;

                    SqlDataReader reader = cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return false;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return true;
            }
        }

        public bool delProducto(int idProducto)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_DEL_PRODUCTO", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@idRecompensa", SqlDbType.Int).Value = idProducto;

                    SqlDataReader reader = cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return false;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return true;
            }
        }

        /// <summary>
        /// Agrega producto a la lista de productos.
        /// </summary>
        public int AddProducto(Producto productoPos, string imagen)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                int res = 0;
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_ADD_PRODUCTO", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@idProducto", SqlDbType.Int).Value = productoPos.idProducto;
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = productoPos.nombre;
                    cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = productoPos.descripcion;
                    cmd.Parameters.Add("@puntos", SqlDbType.Int).Value = productoPos.puntos;
                    cmd.Parameters.Add("@imagen", SqlDbType.VarChar).Value = imagen;

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
        /// Agrega un evento a la lista de eventos.
        /// </summary>
        public int AddEvento(Evento evento, String imagen)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                int res = 0;
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_ADD_EVENTO", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@idEvento", SqlDbType.Int).Value = evento.idEvento;
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = evento.nombre;
                    cmd.Parameters.Add("@lugar", SqlDbType.VarChar).Value = evento.lugar;
                    cmd.Parameters.Add("@fecha", SqlDbType.Date).Value = evento.fecha;
                    cmd.Parameters.Add("@asistentes", SqlDbType.Int).Value = evento.asistentes;
                    cmd.Parameters.Add("@confirmacion", SqlDbType.Int).Value = evento.confirmacion ? 1 : 0;
                    cmd.Parameters.Add("@imagen", SqlDbType.VarChar).Value = imagen;
                    cmd.Parameters.Add("@puntos", SqlDbType.Int).Value = evento.puntos;
                    cmd.Parameters.Add("@url", SqlDbType.VarChar).Value = evento.url == null ? "" : evento.url;

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
        /// Obtiene id de usuario basado en el nombre de usuario nombre.apellido
        /// </summary>
        public int getIdusuario(string nombreUsuario)
        {
            int idUsuario = 0;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_GET_ID_USUARIO", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombreUsuario;

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        idUsuario = Convert.ToInt16(reader["idUsuario"].ToString());
                    }

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return idUsuario;
                }
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }
                return idUsuario;
            }
        }

        /// <summary>
        /// Asigna puntos a un usuario, si el idActividad es 0, requiere la descripción.
        /// </summary>
        public int AsignarPuntos(int idUsuario, int idActividad, String descripcion, int puntos)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                int res = 0;
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_ASIGNAR_ACTIVIDAD", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
                    cmd.Parameters.Add("@idActividad", SqlDbType.Int).Value = idActividad;
                    cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = descripcion;
                    cmd.Parameters.Add("@puntos", SqlDbType.Int).Value = puntos;

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
        /// Resta puntos a un usuario, si el idActividad es 0, requiere descripción.
        /// </summary>
        public int CobrarPuntos(int idUsuario, int idActividad, String descripcion, int puntos)
        {
            using (SqlConnection cnn = Context.Connect())
            {
                int res = 0;
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SP_COBRAR_ACTIVIDAD", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = idUsuario;
                    cmd.Parameters.Add("@idRecompensa", SqlDbType.Int).Value = idActividad;
                    cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = descripcion;
                    cmd.Parameters.Add("@puntos", SqlDbType.Int).Value = puntos;

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

    }

    public class Usuario
    {
        public Usuario(int idUsuario, string usuario, string puesto, string fecha_entrada, string cumpleaños, int puntos)
        {
            this.idUsuario = idUsuario;
            this.usuario = usuario;
            this.puesto = puesto;
            this.fecha_entrada = fecha_entrada;
            this.cumpleaños = cumpleaños;
            this.puntos = puntos;
        }

        public Usuario(int idUsuario, string nombre, string usuario, string puesto, string fecha_entrada, string cumpleaños, int puntos)
        {
            this.idUsuario = idUsuario;
            this.nombre = nombre;
            this.usuario = usuario;
            this.puesto = puesto;
            this.fecha_entrada = fecha_entrada;
            this.cumpleaños = cumpleaños;
            this.puntos = puntos;
        }

        public Usuario()
        {

        }

        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string usuario { get; set; }
        public string puesto { get; set; }
        public string fecha_entrada { get; set; }
        public string cumpleaños { get; set; }
        public int puntos { get; set; }

    }

    public class Evento {
        public Evento(int idEvento, string nombre, string lugar, string fecha, int asistentes, string imagen, int puntos)
        {
            this.idEvento = idEvento;
            this.nombre = nombre;
            this.lugar = lugar;
            this.fecha = fecha;
            this.asistentes = asistentes;
            this.imagen = imagen;
            this.puntos = puntos;
        }

        

        public Evento(int idEvento, string nombre, string lugar, string fecha, int asistentes, string imagen, int puntos, string url)
        {
            this.idEvento = idEvento;
            this.nombre = nombre;
            this.lugar = lugar;
            this.fecha = fecha;
            this.asistentes = asistentes;
            this.imagen = imagen;
            this.puntos = puntos;
            this.url = url;
        }

        public Evento(int idEvento, string nombre, string lugar, string fecha, int asistentes, string imagen)
        {
            this.idEvento = idEvento;
            this.nombre = nombre;
            this.lugar = lugar;
            this.fecha = fecha;
            this.asistentes = asistentes;
            this.imagen = imagen;
        }

        public Evento(int idEvento, string nombre, string lugar, string fecha, int asistentes, string imagen, bool confirmacion, string url, int puntos) : this(idEvento, nombre, lugar, fecha, asistentes, imagen)
        {
            this.confirmacion = confirmacion;
            this.url = url;
            this.puntos = puntos;
        }

        public Evento(string fecha, string nombre, int puntos)
        {
            this.nombre = nombre;
            this.fecha = fecha;
            this.puntos = puntos;
        }

        public Evento(int idEvento, string nombre, string fecha, int puntos)
        {
            this.idEvento = idEvento;
            this.nombre = nombre;
            this.fecha = fecha;
            this.puntos = puntos;
        }

        public Evento(int puntos)
        {
            this.puntos = puntos;
        }

        public Evento()
        {

        }

        public int idEvento { get; set; }
        [Required(ErrorMessage = "Ingresa un nombre")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Ingresa un lugar")]
        public string lugar { get; set; }
        [Required(ErrorMessage = "Ingresa una fecha")]
        public string fecha { get; set; }
        public int asistentes { get; set; }
        public string imagen { get; set; }
        public Boolean confirmacion { get; set; }
        public string url { get; set; }
        public int puntos { get; set; }
        public int confirmados { get; set; }
    }

    public class Producto {
        public Producto(int idProducto, string nombre, string descripcion, int puntos, string imagen)
        {
            this.idProducto = idProducto;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.puntos = puntos;
            this.imagen = imagen;
        }

        public Producto()
        {
        }

        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int puntos { get; set; }
        public string imagen { get; set; }
    }
}