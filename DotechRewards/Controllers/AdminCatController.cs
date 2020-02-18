using DotechRewards.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace DotechRewards.Controllers
{
    public class AdminCatController : Controller
    {
        // GET: AdminCat
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProducto(Producto productoPos) {
            String imagen = "";
            try{
                    HttpPostedFileBase file = Request.Files[0];
                    file = Request.Files[0];
                    string ruta = Server.MapPath("~/Content/images/");
                    ruta += file.FileName;
                    file.SaveAs(ruta);

                    imagen = file.FileName.Substring(0, file.FileName.Length);
            }catch (Exception ex) {
                imagen = productoPos.imagen;
            }
            AdminModel model = new AdminModel();
            model.AddProducto(productoPos, imagen);
            return RedirectToAction("Index", "AdminCat");
        }

        [HttpPost]
        public ActionResult UploadPDFCatalogo(HttpPostedFileBase nombrePDF) {

            try
            {
                HttpPostedFileBase file = Request.Files[0];
                file = Request.Files[0];
                string ruta = Server.MapPath("~/catalogos/");
                ruta += "catalogo.pdf";
                file.SaveAs(ruta);
            }
            catch (Exception ex)
            {
                
            }

            return null;
        }

        [HttpPost]
        public ActionResult DelProducto(int idProducto) {
            AdminModel.delProducto(idProducto);
            return RedirectToAction("Index","AdminCat");
        }

    }
}