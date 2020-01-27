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

        public static DataTable getListaConfirmacion(int idEvento)
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

        public static Retorno leerListaAsistencia(string nombreArchivo) {

            string fileName = @"C:\Users\BHN_R\source\repos\DotechRewards\DotechRewards\Content\Listas\" + nombreArchivo;
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
                            string nombre = dataRow.Cell(3).Value.ToString();
                            string evento = dataRow.Cell(4).Value.ToString();
                            int puntos = Convert.ToInt32(dataRow.Cell(5).Value);
                            string lugar = dataRow.Cell(6).Value.ToString();
                            int confirmacion = Convert.ToInt32(dataRow.Cell(7).Value);
                            int personas = Convert.ToInt32(dataRow.Cell(8).Value);
                            int asistencia = 0;
                            try
                            {
                                Convert.ToInt32(dataRow.Cell(9).Value);
                                asistencia = 1;
                            }
                            catch (Exception ex)
                            {

                            }
                            //Validar si evento ya se cargo
                            AdminModel.AsignarPuntos(idUsuario, idEvento, evento, puntos);
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

    }
}