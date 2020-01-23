using DotechRewards.Models;
using System.Web.Mvc;

namespace DotechRewards.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            //UsuarioModel UsuarioM = new UsuarioModel().getRecompensa();
            return View();
        }

        [HttpPost]
        public int Cambiar(string user, string pass) {
            return UsuarioModel.cambiar(user, pass);
        }

        public ActionResult activarPuntos(string usuario) {
            UsuarioModel.ActivarPuntos(usuario);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Confirmar(int asistentes = 0, string idUsr = "", int idEventoF = 0)
        {
            //llamar modelo para modificar tabla
            int confirmacion = 0;
            UsuarioModel confi = new UsuarioModel();
            if (asistentes != 0) {
                confirmacion = 1;
            }
            confi.AddConfirmacion(confirmacion, asistentes, idEventoF, idUsr);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ConfirmarLink(string idUsuario, int idEvento) {
            UsuarioModel confi = new UsuarioModel();
            confi.AddConfirmacion(1, 1, idEvento, idUsuario);
            return RedirectToAction("Index");
        }
    }
}
