using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using InnguzApp.ContextoDatos;

namespace InnguzApp.Controllers
{
    public class TipoProductoController : Controller
    {
        Base_DatosDataContext bd = new Base_DatosDataContext();
        // GET: TipoProducto
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            IEnumerable<Tipo_Producto> Lista = (from tp in bd.Tipo_Producto select tp).ToList();

            return View(Lista);
        }

        // GET: TipoProducto/Details/5
        public ActionResult Details(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var tipoProducto = (from tp in bd.Tipo_Producto where tp.id == id select tp).Single();
            return View(tipoProducto);
        }

        // GET: TipoProducto/Create
        public ActionResult Create()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            return View();
        }

        // POST: TipoProducto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Tipo_Producto modelo)
        {
            try
            {
                modelo.UsuarioInserta = Session["login"].ToString();
                DateTime fecha = DateTime.Now;
                modelo.FechaInserta = fecha;

                bd.Tipo_Producto.InsertOnSubmit(modelo);
                bd.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoProducto/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var tipoProducto = (from tp in bd.Tipo_Producto where tp.id == id select tp).Single();

            return View(tipoProducto);
        }

        // POST: TipoProducto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Tipo_Producto modelo)
        {
            try
            {
                modelo.UsuarioActualiza = Session["login"].ToString();
                bd.SP_ActualizarTipoProdcuto(modelo.id, modelo.Tipo, modelo.UsuarioActualiza);
                bd.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TipoProducto/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var tipoProducto = (from tp in bd.Tipo_Producto where tp.id == id select tp).Single();
            return View(tipoProducto);
        }

        // POST: TipoProducto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var tipoProducto = (from tp in bd.Tipo_Producto where tp.id == id select tp).Single();

                bd.Tipo_Producto.DeleteOnSubmit(tipoProducto);
                bd.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
