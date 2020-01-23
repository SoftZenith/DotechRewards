using DotechRewards.Models;
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

        [HttpPost]
        public ActionResult AddUsuario(Usuario usuarioPost) {
            AdminModel.addUsuario(usuarioPost);
            return RedirectToAction("Index","AdminUsr");
        }

        [HttpPost]
        public ActionResult DelUsuario(int idUsuario) {
            AdminModel.delUsuario(idUsuario);
            return RedirectToAction("Index","AdminUsr");
        }

        [HttpPost]
        public ActionResult AsignarPuntos(string nUsuario, string descripcion, int puntos) {
            int idUsuario = AdminModel.getIdusuario(nUsuario);
            AdminModel.AsignarPuntos(idUsuario, 0, descripcion, puntos);
            return null;
        }

        [HttpPost]
        public ActionResult AsignarPuntosE(string nUsuario, int idEvento, int puntos)
        {
            int idUsuario = AdminModel.getIdusuario(nUsuario);
            AdminModel.AsignarPuntos(idUsuario, idEvento,"", puntos);
            return null;
        }

        [HttpPost]
        public ActionResult CobrarPuntos(int idUsuario, string descripcion, int puntos) {
            AdminModel.CobrarPuntos(idUsuario, 0, descripcion, puntos);
            return null;
        }

        [HttpPost]
        public JsonResult getHistorialUsuario(string usuario) {
            UsuarioModel usuarioModel = new UsuarioModel();
            usuarioModel.getHistorialUsuario(usuario);

            return Json(usuarioModel.eventos, JsonRequestBehavior.AllowGet);
        }

    }
}