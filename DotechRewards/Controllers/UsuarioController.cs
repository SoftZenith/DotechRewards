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

        /// <summary>
        /// Cambia contraseña de usuario desde la pantalla principal del usuario, sin enviar correo.
        /// </summary>
        [HttpPost]
        public int Cambiar(string nameuser, string pass) {
            UsuarioModel usuarioM = new UsuarioModel();
            return usuarioM.cambiar(nameuser, pass);
        }

        /// <summary>
        /// Activa los puntos asignados al iniciar sesión por primera vez.
        /// </summary>
        public ActionResult activarPuntos(string usuario) {
            UsuarioModel usuarioM = new UsuarioModel();
            usuarioM.ActivarPuntos(usuario);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Agrega la confirmación de un usuario a un evento, requiere el numero de asistentes o acompañantes, el id de usuario y el id 
        /// </summary>
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

        /// <summary>
        /// Agrega la confirmación de un usuario a un evento a hacer clic sobre un enlace de confirmación que te lleva a otro sitio, 
        /// requiere id del usuario y id del evento al que se confirma la asistencia.
        /// </summary>
        [HttpPost]
        public ActionResult ConfirmarLink(string idUsuario, int idEvento) {
            UsuarioModel confi = new UsuarioModel();
            confi.AddConfirmacion(1, 1, idEvento, idUsuario);
            return RedirectToAction("Index");
        }
    }
}
