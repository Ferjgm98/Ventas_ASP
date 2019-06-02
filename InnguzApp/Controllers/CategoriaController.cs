using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using InnguzApp.ContextoDatos;

namespace InnguzApp.Controllers
{
    public class CategoriaController : Controller
    {

        Base_DatosDataContext bd = new Base_DatosDataContext();
        // GET: Categoria
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            IEnumerable<Categorias> Lista = (from c in bd.Categorias select c).ToList();
            return View(Lista);
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var categoria = (from c in bd.Categorias where c.id == id select c).Single();

            return View(categoria);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Categorias modelo)
        {
            try
            {
                modelo.UsuarioInserta = Session["login"].ToString();
                DateTime feha = DateTime.Now;
                modelo.FechaInserta = feha;

                bd.Categorias.InsertOnSubmit(modelo);
                bd.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var categoria = (from c in bd.Categorias where c.id == id select c).Single();

            return View(categoria);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Categorias modelo)
        {
            try
            {
                modelo.UsuarioActualiza = Session["login"].ToString();
                bd.SP_ActualizarCategorias(modelo.id, modelo.Nombre, modelo.UsuarioActualiza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int id)
        {

            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var categoria = (from c in bd.Categorias where c.id == id select c).Single();
            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var categoria = (from c in bd.Categorias where c.id == id select c).Single();
                bd.Categorias.DeleteOnSubmit(categoria);
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
