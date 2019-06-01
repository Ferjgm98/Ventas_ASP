using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InnguzApp.Controllers
{
    public class LoginController : Controller
    {
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
                var test = LoginUsuario.Login(modelo);
                ViewBag.tester = test.usuario;

                return View("~/Views/Dashboard/Index.cshtml");
            } catch
            {
                ViewBag.message = "Los datos no son validos. Intenta de nuevo";
                return View();
            }
        }
    }
}