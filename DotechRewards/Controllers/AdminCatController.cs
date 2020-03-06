using DotechRewards.Models;
using System;
using System.Web;
using System.Web.Configuration;
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

        /// <summary>
        /// Agrega un producto/beneficio al catalogo de productos/beneficios, recibe una instancia de la clase Producto.
        /// </summary>
        [HttpPost]
        public ActionResult AddProducto(Producto productoPos) {
            String imagen = "";
            try{
                    HttpPostedFileBase file = Request.Files[0];
                    file = Request.Files[0];
                    string ruta = Server.MapPath("/Rewards/Content/images/");
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

        /// <summary>
        /// Subir archivo en formato PDF para guardar en ~/catalogos
        /// </summary>
        [HttpPost]
        public ActionResult UploadPDFCatalogo(HttpPostedFileBase nombrePDF) {
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                file = Request.Files[0];
                string pathCatalogo = WebConfigurationManager.AppSettings["pathCatalogos"].ToString();
                string ruta = Server.MapPath(pathCatalogo);
                ruta += "catalogo.pdf";
                file.SaveAs(ruta);
            }
            catch (Exception ex)
            {
                
            }

            return null;
        }
        /// <summary>
        /// Elimina un producto/beneficio del catálogo productos/beneficios, recibe el id del producto/beneficio.
        /// </summary>
        [HttpPost]
        public ActionResult DelProducto(int idProducto) {
            AdminModel admin = new AdminModel();
            admin.delProducto(idProducto);
            return RedirectToAction("Index","AdminCat");
        }

    }
}