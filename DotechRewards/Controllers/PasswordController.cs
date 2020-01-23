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

            ViewBag.Message = UUID_user;
            return View("Index");
        }

        [HttpPost]
        public ActionResult ChangePassword(string password, string uuid)
        {

            return null;
        }

    }
}