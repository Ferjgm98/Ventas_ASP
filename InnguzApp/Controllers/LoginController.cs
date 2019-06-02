using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InnguzApp.ContextoDatos;

namespace InnguzApp.Controllers
{
    public class LoginController : Controller
    {
        Base_DatosDataContext bd = new Base_DatosDataContext();
        // GET: Login
        InnguzWC.IusuarioClient LoginUsuario = new InnguzWC.IusuarioClient();
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(FormCollection collection, InnguzWC.LoginUser modelo)
        {
            try
            {
                var existentUser = LoginUsuario.Login(modelo);
                var usuario = (from u in bd.Usuarios where u.Id == existentUser.id_user select u).Single();
                var imagen = Convert.ToBase64String(usuario.Foto.ToArray());
                
                Session["login"] = existentUser.usuario;
                Session["nombre"] = existentUser.nombre;
                Session["apellido"] = existentUser.apellido;
                Session["imagen"] = imagen;

                return View("~/Views/Dashboard/Index.cshtml");
            } catch
            {
                ViewBag.message = "Los datos no son validos. Intenta de nuevo";
                return View();
            }
        }

        public ActionResult Lougout()
        {
            Session["login"] = null;
            Session.Clear();
            Session.Abandon();
            return Redirect("Login");
        }

    }
}