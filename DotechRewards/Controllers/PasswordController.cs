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

        /// <summary>
        /// Te envia a formulario para reestablecer contraseña asociada al UUID que se recibe de la URL, si, solo si, no han pasado más de 12hrs desde que se genero el UUID. 
        /// </summary>
        public ActionResult ResetPassword(string UUID) {

            string UUID_user = UUID;

            //Revisar caducidad del enlace
            PasswordModel pm = new PasswordModel();
            bool valido = pm.GetDateURL(UUID);
            ViewBag.Message = UUID_user;
            ViewBag.Valido = valido;
            return View("Index");
        }

        /// <summary>
        /// Cambiar contraseña de usuario al que se le envio la URL con UUID.
        /// </summary>
        [HttpPost]
        public ActionResult ChangePassword(string password, string uuid)
        {
            PasswordModel pass = new PasswordModel();
            pass.ResetPassword(uuid, password);
            return RedirectToAction("Index", "Home");
        }

    }
}