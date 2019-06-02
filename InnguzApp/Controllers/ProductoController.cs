using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using InnguzApp.ContextoDatos;

namespace InnguzApp.Controllers
{
    public class ProductoController : Controller
    {
        Base_DatosDataContext bd = new Base_DatosDataContext();
        // GET: Producto
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }
            IEnumerable<Productos_view> Lista = (from pv in bd.Productos_view select pv);
            return View(Lista);
        }

        // GET: Producto/Details/5
        public ActionResult Details(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var productov = (from pv in bd.Productos_view where pv.id == id select pv).Single();
            return View(productov);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var tiposProducto = (from tp in bd.Tipo_Producto select tp).ToList();
            var categorias = (from c in bd.Categorias select c).ToList();

            ViewBag.productTypes = tiposProducto;
            ViewBag.categories = categorias;
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Productos modelo)
        {
            try
            {
       
                DateTime fecha = DateTime.Now;
                modelo.UsuarioInserta = Session["login"].ToString();
                modelo.FechaInserta = fecha;

                bd.Productos.InsertOnSubmit(modelo);
                bd.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }
            var producto = (from p in bd.Productos where p.id == id select p).Single();
            var tiposProducto = (from tp in bd.Tipo_Producto select tp).ToList();
            var categorias = (from c in bd.Categorias select c).ToList();

            ViewBag.productTypes = tiposProducto;
            ViewBag.categories = categorias;

            return View(producto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Productos modelo)
        {
            try
            {
                modelo.UsuarioActualiza = Session["login"].ToString();
                bd.SP_Actualizar_Producto(modelo.id, modelo.Nombre, modelo.Descripcion, modelo.Precio, modelo.Tipo_Producto_Id, modelo.Categoria_Id, modelo.UsuarioActualiza);
                bd.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Producto/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }
            var productov = (from pv in bd.Productos_view where pv.id == id select pv).Single();
            return View(productov);
        }

        // POST: Producto/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var producto = (from p in bd.Productos where p.id == id select p).Single();
                bd.Productos.DeleteOnSubmit(producto);
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
