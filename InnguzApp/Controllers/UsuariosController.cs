using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.IO;
using System.Text;

using InnguzApp.ContextoDatos;


namespace InnguzApp.Controllers
{
    public class UsuariosController : Controller
    {
        Base_DatosDataContext bd = new Base_DatosDataContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            IEnumerable<Usuarios> Lista = (from u in bd.Usuarios select u).ToList();
            return View(Lista);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            var usuario = (from u in bd.Usuarios where u.Id == id select u).Single();
            var to64 = Convert.ToBase64String(usuario.Foto.ToArray());
            ViewBag.foto = to64;
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Usuarios modelo, HttpPostedFileBase Photo)
        {
            try
            {
                string name = Path.GetFileName(Photo.FileName);
                string extension = Path.GetExtension(name);
                int size = Photo.ContentLength;
                Stream stream = Photo.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                    

                modelo.Foto = bytes;
                DateTime fecha = DateTime.Now;
                modelo.Fecha_registro = fecha;
                bd.Usuarios.InsertOnSubmit(modelo);
                bd.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = (from u in bd.Usuarios where u.Id == id select u).Single();
            var to64 = Convert.ToBase64String(usuario.Foto.ToArray());
            ViewBag.foto = to64;
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Usuarios modelo, HttpPostedFile Photo)
        {
            try
            {
                string name = Path.GetFileName(Photo.FileName);
                string extension = Path.GetExtension(name);
                int size = Photo.ContentLength;
                Stream stream = Photo.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);


                modelo.Foto = bytes;
                DateTime fecha = DateTime.Now;                
                modelo.Fecha_registro = fecha;                        

                bd.SP_modificar_usuario(modelo.Id,modelo.Usuario,modelo.Nombre,modelo.Apellido,modelo.Posicion,modelo.Telefono,modelo.Correo,modelo.Clave,modelo.Foto,modelo.Fecha_registro);
                bd.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
