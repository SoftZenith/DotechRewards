using ClosedXML.Excel;
using DotechRewards.Util;
using DotechRewards.Util.Database;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DotechRewards.Models
{
    public class AdminEvenModel
    {
        /// <summary>
        /// Obtiene DataTable con la lista de confirmación para un evento, recibe id del evento.
        /// </summary>
        public DataTable getListaConfirmacion(int idEvento)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter ad;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_GET_LISTA_CONFIRMACION", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idEvento", SqlDbType.Int).Value = idEvento;

                    ad = new SqlDataAdapter(cmd);
                    ad.Fill(dt);
                    cnn.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return dt;
                }/*
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }*/
            }

        }

        public DataTable getListaGenerica(int idEvento) {
            DataTable dt = new DataTable();
            SqlDataAdapter ad;
            using (SqlConnection cnn = Context.Connect())
            {
                try
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SP_GET_LISTA_GENERICA", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@idEvento", SqlDbType.Int).Value = idEvento;

                    ad = new SqlDataAdapter(cmd);
                    ad.Fill(dt);
                    cnn.Close();
                    return dt;
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                    return dt;
                }/*
                finally
                {
                    cnn.Close();
                    cnn.Dispose();
                }*/
            }
        }

        /// <summary>
        /// Lee archivo .xlsx de lista de asistencia y asigna los puntos individualmente.
        /// </summary>
        public Retorno leerListaAsistencia(string nombreArchivo) {

            string fileName = "~/Content/Listas/" + nombreArchivo;
            using (var excelWorkbook = new XLWorkbook(nombreArchivo))
            {
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();

                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() > 1)
                    {
                        try {
                            int idUsuario = Convert.ToInt32(dataRow.Cell(1).Value);
                            int idEvento = Convert.ToInt32(dataRow.Cell(2).Value);
                            string nombre = dataRow.Cell(4).Value.ToString();
                            string evento = dataRow.Cell(5).Value.ToString();
                            int puntos = Convert.ToInt32(dataRow.Cell(6).Value);
                            string lugar = dataRow.Cell(7).Value.ToString();
                            int confirmacion = dataRow.Cell(8).Value != null ? (dataRow.Cell(8).Value.ToString() == "SI" ? 1 : 0) : 0; //Esta fila
                            object confirmaobj = dataRow.Cell(8).Value;
                            int personas = 0;
                            if (dataRow.Cell(9).Value != null && !String.IsNullOrWhiteSpace(dataRow.Cell(9).Value.ToString())) {
                                personas = Convert.ToInt32(dataRow.Cell(9).Value.ToString());
                            }

                            int asistencia = 0;
                            try
                            {
                                asistencia = dataRow.Cell(10).Value != null ? (dataRow.Cell(10).Value.ToString().ToUpper() == "SI" ? 1 : 0) : 0; //Esta fila
                                //personas = dataRow.Cell(9).Value != null ? Convert.ToInt32(dataRow.Cell(9).ToString()) : 0; //Esta fila
                            }
                            catch (Exception ex)
                            {

                            }
                            //Validar si evento ya se cargo
                            AdminModel admin = new AdminModel();
                            UsuarioModel confi = new UsuarioModel();
                            if (confirmacion == 1)
                            {
                                confi.AddConfirmacion(confirmacion, personas, idEvento, admin.getUsuario(idUsuario));
                            }
                            if (asistencia == 1)
                            {
                                admin.AsignarPuntos(idUsuario, idEvento, evento, puntos, personas);
                            }
                        } catch (Exception ex) {
                            return new Retorno() {
                                estatus = false,
                                mensaje = "Archivo corrupto uno o más datos no estan en el formato correcto"
                            };
                        }
                    }
                }
            }

            return new Retorno() {
                estatus = true,
                mensaje = ""
            };
        }

        public Retorno leerListaActividadesExtra(string nombreArchivo) {
            string fileName = "~/Content/Listas/" + nombreArchivo;
            using (var excelWorkbook = new XLWorkbook(nombreArchivo))
            {
                var nonEmptyDataRows = excelWorkbook.Worksheet(1).RowsUsed();

                foreach (var dataRow in nonEmptyDataRows)
                {
                    //for row number check
                    if (dataRow.RowNumber() > 1)
                    {
                        try
                        {
                            string usuario = dataRow.Cell(1).Value.ToString();
                            string actividad = dataRow.Cell(2).Value.ToString();
                            string fecha = dataRow.Cell(3).Value.ToString();
                            int puntos = Convert.ToInt32(dataRow.Cell(4).Value);
                            
                            //Asignar puntos a usuario
                            AdminModel admin = new AdminModel();
                            int idUsuario = admin.getIdusuario(usuario);
                            if (idUsuario == 0) {
                                return new Retorno()
                                {
                                    estatus = false,
                                    mensaje = "Usuario no valido en renglon: " + dataRow.RowNumber()
                                };
                            }
                            admin.AsignarPuntosActividadExtra(idUsuario,actividad,puntos,fecha);
                        }
                        catch (Exception ex)
                        {
                            return new Retorno()
                            {
                                estatus = false,
                                mensaje = "Archivo corrupto uno o más datos no estan en el formato correcto"
                            };
                        }
                    }
                }
            }

            return new Retorno()
            {
                estatus = true,
                mensaje = ""
            };
        }

    }
}