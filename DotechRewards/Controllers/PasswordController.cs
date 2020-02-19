using DotechRewards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotechRewards.Controllers
{
    public class PasswordController : Controller
    {
        // GET: Password
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ResetPassword(string UUID) {

            string UUID_user = UUID;

            //Revisar caducidad del enlace
            PasswordModel pm = new PasswordModel();
            bool valido = pm.GetDateURL(UUID);

            ViewBag.Message = UUID_user;
            ViewBag.Valido = valido;
            return View("Index");
        }

        [HttpPost]
        public ActionResult ChangePassword(string password, string uuid)
        {
            PasswordModel pass = new PasswordModel();
            pass.ResetPassword(uuid, password);
            return RedirectToAction("Index", "Home");
        }

    }
}