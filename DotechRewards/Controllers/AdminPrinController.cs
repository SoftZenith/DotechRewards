﻿using DotechRewards.Models;
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

        /// <summary>
        /// Retorn JSON con lista usuarios.
        /// </summary>
        [HttpGet]
        public JsonResult getUsuarios() {
            AdminModel admin = new AdminModel();
            admin.getUsuarios();
            return Json(admin.usuarios, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Retorna JSON con lista de eventos.
        /// </summary>
        [HttpGet]
        public JsonResult getEventos() {
            AdminModel admin = new AdminModel();
            admin.getEventosAsistencia();
            //admin.eventos = admin.eventos.Where(banner => DateTime.Parse(banner.fecha).Month >= DateTime.Now.Month && DateTime.Parse(banner.fecha).Month <= DateTime.Now.Month + 2).ToList();
            //this.banners = this.banners.Where(banner => DateTime.Parse(banner.fecha).Month >= DateTime.Now.Month && DateTime.Parse(banner.fecha).Month <= DateTime.Now.Month + 2).ToList();
            return Json(admin.eventos, JsonRequestBehavior.AllowGet);
        }
    }
}