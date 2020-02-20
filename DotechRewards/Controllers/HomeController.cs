using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using DotechRewards.Models;
using DotechRewards.Util.User;

namespace DotechRewards.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }

        [HttpPost]
        public ActionResult login(string usuario, string contrasena) {
            HomeModel home = new HomeModel();
            if (home.login(usuario, contrasena) == 1)
            {
                UserData.User = usuario;
                UserData.Admin = home.getPermisos(usuario).ToString();
                UserData.Nombre = home.getNombreC(usuario).ToString();
                //FormsAuthentication.SetAuthCookie(usuario, false);
                return RedirectToAction("Index", "Usuario");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public bool SendEmail(string user)
        {
            HomeModel home = new HomeModel();
            string pass = home.recuperarContrasena(user);
            string UIDD_url = "http://localhost:58322/Password/ResetPassword?UUID=";

            if (pass == "")
            {
                return false;
            }
            else {
                //Genear UUID
                String UUID = Guid.NewGuid().ToString();
                UIDD_url += UUID;
                //Generar url con UUID
                PasswordModel password = new PasswordModel();
                password.AddResetPassword(UUID, user);
            }

            try
            {
                string senderEmail = WebConfigurationManager.AppSettings["email"].ToString(); //"bhn.rock@gmail.com"; 
                string senderPassword = WebConfigurationManager.AppSettings["password"].ToString(); //"yiesajpdikycokfg";
                string smtpClient = WebConfigurationManager.AppSettings["smtpClient"].ToString(); //"smtp.gmail.com"
                int smtpPort = Convert.ToInt32(WebConfigurationManager.AppSettings["emailPort"].ToString()); //587

                SmtpClient client = new SmtpClient(smtpClient, smtpPort);
                /*
                string senderEmail = "rewards@dotech.com.mx";
                string senderPassword = "rewards2020";
                SmtpClient client = new SmtpClient("Smtpout.secureserver.net", 80);*/

                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                string bodyMailMessage = "Ingresa a la siguiente dirección para restablecer tu contraseña: " + UIDD_url
                                            + "<br>"
                                            + "<br>"
                                            + "<b>Si no solicitaste reestablecer tu contraseña, por favor ponte en contacto con los administradores del sistema.</b>";

                MailMessage mailMessage = new MailMessage(senderEmail, user+"@dotech.com.mx", "Restablecer contraseña", bodyMailMessage);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;

                //new Thread(() => { client.Send(mailMessage); }).Start();
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

    }
}