using ClosedXML.Excel;
using DotechRewards.Models;
using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace DotechRewards.Controllers
{
    public class AdminEvenController : Controller
    {
        // GET: AdminEven
        public ActionResult Index()
        {
            ViewBag.idEvento = 0;

            return View();
        }
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

        public ActionResult DelEvento(int idEvento) {
            AdminModel.delEvento(idEvento);
            return RedirectToAction("Index", "AdminEven");
        }

        public ActionResult DescargarLista(int idEvento = 6) {
            if (idEvento == 0)
                return null;
            DataTable dt = new DataTable();
            dt = AdminEvenModel.getListaConfirmacion(idEvento);
            //dt.TableName = "AR_LOG_IMPLEMENTACION";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "REPORTES");

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

        [HttpPost]
        public ActionResult SubirLista() {
            string nombreArchivo = "";
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                file = Request.Files[0];
                string ruta = Server.MapPath("~/Content/Listas/");
                ruta += file.FileName;
                file.SaveAs(ruta);
                nombreArchivo = file.FileName.Substring(0, file.FileName.Length);

                //Leer archivo desde Model
                AdminEvenModel.leerListaAsistencia(nombreArchivo);
                return RedirectToAction("Index");
            }
            catch (Exception ex) {
                return RedirectToAction("Index");
            }
        }

    }
}