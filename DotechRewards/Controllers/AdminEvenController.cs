using ClosedXML.Excel;
using DotechRewards.Models;
using DotechRewards.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace DotechRewards.Controllers
{
    public class AdminEvenController : Controller
    {
        public int exitoSubirLista = 2;

        // GET: AdminEven
        public ActionResult Index()
        {
            ViewBag.idEvento = 0;
            ViewBag.exitoso = exitoSubirLista;
            return View();
        }

        /// <summary>
        /// Agrega un evento al listado de eventos, recibe una instancia de la clase Evento.
        /// La imagen se guarda en ~/Content/images/
        /// </summary>
        [HttpPost]
        public ActionResult AddEvento(Evento evento) {

            String imagen = "";

            try
            {
                HttpPostedFileBase file = Request.Files[0];
                file = Request.Files[0];
                string ruta = Server.MapPath("~/Content/images/");
                ruta += file.FileName;
                file.SaveAs(ruta);
                imagen = file.FileName.Substring(0, file.FileName.Length);

                evento.imagen = imagen;
            }
            catch (Exception ex) {
                imagen = evento.imagen;
            }
            AdminModel model = new AdminModel();
            model.AddEvento(evento, imagen);
            return RedirectToAction("Index", "AdminEven");
        }

        /// <summary>
        /// Elimina un evento de la lista de eventos.
        /// </summary>
        public ActionResult DelEvento(int idEvento) {
            AdminModel admin = new AdminModel();
            admin.delEvento(idEvento);
            return RedirectToAction("Index", "AdminEven");
        }

        /// <summary>
        /// Genera y descarga archivo con extensión .xlxs con la lista de asistentes, recibe id del evento del que se desea generar la lista.
        /// </summary>
        public ActionResult DescargarLista(int idEvento = 6) {
            if (idEvento == 0)
                return null;
            DataTable dt = new DataTable();
            AdminEvenModel evento = new AdminEvenModel();
            dt = evento.getListaConfirmacion(idEvento);
            //dt.TableName = "AR_LOG_IMPLEMENTACION";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "REPORTES");
                
                /*ws.Cell("Z1").Value = "Yes";
                ws.Cell("Z2").Value = "No";
                ws.Cell("G2").DataValidation.List(ws.Range("Z1:Z2"));*/

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string nombre_archivo = "attachment;filename=ListaAsistencia_" + DateTime.Now.Day.ToString() +"-"+ DateTime.Now.Month.ToString() +"-"+ DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() +":"+ DateTime.Now.Minute.ToString() + ".xlsx";
                Response.AddHeader("Content-Disposition", nombre_archivo);

                //Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }

            }

            return RedirectToAction("Index");
        }

        public ActionResult DescargarListaGeneric(int idEvento) {
            if (idEvento == 0)
                return null;
            DataTable dt = new DataTable();
            
            AdminEvenModel evento = new AdminEvenModel();
            dt = evento.getListaGenerica(idEvento);
            //dt.TableName = "AR_LOG_IMPLEMENTACION";

            var options = new List<string>();
            int max_acompañantes = 0;
            try
            {
                max_acompañantes = (int)dt.Rows[0].ItemArray[2];
            }
            catch (Exception ex) { 
                
            }
            for (int i = 0; i <= max_acompañantes; i++)
            {
                options.Add(i.ToString());
            }
            var validOptions = $"\"{String.Join(",", options)}\"";

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(dt, "LISTA ASISTENCIA");

                ws.Cell("Z1").Value = "SI";
                ws.Cell("Z2").Value = "NO";

                for (int i = 0; i < dt.Rows.Count; i++) {
                    ws.Cell("H2").CellBelow(i).DataValidation.List(ws.Range("Z1:Z2"));
                    ws.Cell("I2").CellBelow(i).DataValidation.List(validOptions, true);
                    ws.Cell("J2").CellBelow(i).DataValidation.List(ws.Range("Z1:Z2"));
                    string cellN = "H" + (i + 2);
                    string cellAsistio = "J" + (i + 2);
                    try
                    {
                        if (ws.Cell(cellN).GetString() == "1")
                        {
                            ws.Cell(cellN).Value = "SI";
                        }
                        else{
                            ws.Cell(cellN).Value = "NO";
                        }

                        if (ws.Cell(cellAsistio).GetString() == "1")
                        {
                            ws.Cell(cellAsistio).Value = "SI";
                        }else{
                            ws.Cell(cellAsistio).Value = "NO";
                        }

                    }
                    catch (Exception ex) { 
                    
                    }

                }

                for (int i = 0; i < dt.Rows.Count; i++)
                { 
                    //ws.Cell("H2")    
                }

                //ws.Cell("G2:G11").DataValidation.List(ws.Range("Z1:Z2"));

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                string nombre_archivo = "attachment;filename=ListaAsistencia_" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ".xlsx";
                Response.AddHeader("Content-Disposition", nombre_archivo);

                //Response.AddHeader("content-disposition", "attachment;filename=GridView.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }

            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Sube archivo de lista de asistencia con extension .xlxs, el archivo se toma del post request y se guarda en ~/Content/lista/
        /// </summary>
        [HttpPost]
        public ActionResult SubirLista() {
            string nombreArchivo = "";
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                file = Request.Files[0];
                string pathListas = WebConfigurationManager.AppSettings["pathListas"].ToString();
                string ruta = Server.MapPath(pathListas);
                ruta += file.FileName;
                file.SaveAs(ruta);
                nombreArchivo = file.FileName.Substring(0, file.FileName.Length);
                
                AdminEvenModel evento = new AdminEvenModel();
                Retorno retorno = new Retorno();
                retorno.estatus = false;
                
                if (file.FileName.Contains("Actividades_extra")) //Si inicia nombre con actividades_extra es archivo de actividades extra
                {
                    retorno = evento.leerListaActividadesExtra(ruta);
                }
                else {
                    //Leer archivo lista de asistencia
                    retorno = evento.leerListaAsistencia(ruta);
                }
                if (retorno.estatus)
                {
                    exitoSubirLista = 1;
                    ViewBag.exitoso = 1;
                }
                else {
                    exitoSubirLista = 0;
                    ViewBag.exitoso = 0;
                }
                return View("Index");
            }
            catch (Exception ex) {
                exitoSubirLista = 0;
                ViewBag.exitoso = 0;
                return RedirectToAction("Index","AdminEven");
            }
        }

    }
}