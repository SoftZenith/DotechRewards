using DotechRewards.Models;
using System;
using System.Web.Mvc;

namespace DotechRewards.Controllers
{
    public class AdminUsrController : Controller
    {
        // GET: AdminUsr
        public ActionResult Index()
        {

            return View();
        }

        /// <summary>
        /// Agrega usuario a lista de usuarios, recibe una instancia de la clase Usuario.
        /// </summary>
        [HttpPost]
        public ActionResult AddUsuario(Usuario usuarioPost) {
            AdminModel admin = new AdminModel();
            admin.addUsuario(usuarioPost);
            return RedirectToAction("Index","AdminUsr");
        }

        /// <summary>
        /// Elimina usuario de la lista de usuario, recibe id del usuario a eliminar.
        /// </summary>
        [HttpPost]
        public ActionResult DelUsuario(int idUsuario) {
            AdminModel admin = new AdminModel();
            admin.delUsuario(idUsuario);
            return RedirectToAction("Index","AdminUsr");
        }

        /// <summary>
        /// Asigna puntos a un usuario, requiere el usuario, la descripción de la actividad, y los puntos que se asignan por esa actividad.
        /// </summary>
        [HttpPost]
        public ActionResult AsignarPuntos(string nUsuario, string descripcion, int puntos) {
            AdminModel admin = new AdminModel();
            int idUsuario = admin.getIdByNombre(nUsuario);
            try {
                idUsuario = Convert.ToInt32(nUsuario);
            } catch (Exception ex) { 
            
            }
            admin.AsignarPuntos(idUsuario, 0, descripcion, puntos,0);
            return null;
        }

        /// <summary>
        /// Asigna puntos a un usuario por evento al que asistió, requiere el usuario, el id del evento y los puntos que se asignaran a ese evento.
        /// </summary>
        [HttpPost]
        public ActionResult AsignarPuntosE(string nUsuario, int idEvento, int puntos)
        {
            AdminModel admin = new AdminModel();
            int idUsuario = 0;
            idUsuario = admin.getIdByNombre(nUsuario);
            try
            {
                idUsuario = Convert.ToInt32(idUsuario);
            }
            catch (Exception ex) { 
                
            }
            if (idUsuario <= 0)
            {

            }
            else
            {
                admin.AsignarPuntos(idUsuario, idEvento, "", puntos, 0);
            }
            return null;
        }

        /// <summary>
        /// Cobra o resta puntos a un usuario, requiere el usuario, la descripción por la que se restan puntos y los puntos a restar.
        /// </summary>
        [HttpPost]
        public int CobrarPuntos(int idUsuario, string descripcion, int puntos) {
            AdminModel admin = new AdminModel();
            int puntosActuales = admin.validaPuntos(idUsuario);
            int activacion = admin.getActivacionPuntos(idUsuario);
            if (activacion == 0)
            {
                return -2;
            }
            if (puntosActuales < puntos) {
                return -1;
            }
            admin.CobrarPuntos(idUsuario, 0, descripcion, puntos);
            return 0;
        }

        /// <summary>
        /// Obtiene JSON con el historia de asignación/cobro de puntos de un usuario, requiere el usuario.
        /// </summary>
        [HttpPost]
        public JsonResult getHistorialUsuario(string usuario) {
            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.getHistorialUsuario(usuario);

            return Json(usuarioModel.eventos, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int getPuntosTotales(string usuario) {

            UsuarioModel usuarioModel = new UsuarioModel();
            int puntos = usuarioModel.getTotalPuntos(usuario);

            return puntos;
        }

    }
}