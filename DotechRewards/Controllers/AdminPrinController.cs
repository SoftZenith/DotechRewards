using DotechRewards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotechRewards.Controllers
{
    public class AdminPrinController : Controller
    {
        // GET: AdminPrin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getUsuarios() {
            AdminModel admin = new AdminModel();
            admin.getUsuarios();
            return Json(admin.usuarios, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getEventos() {
            AdminModel admin = new AdminModel();
            admin.getEventos();
            return Json(admin.eventos, JsonRequestBehavior.AllowGet);
        }
    }
}