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

        public string GeneratePassword(int length)
        {
            string fuente = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] fuenteArray = fuente.ToCharArray();
            StringBuilder resultado = new StringBuilder();
            Random random = new Random();
            for(int i = 0; i<= length; i++)
            {
                resultado.Append(fuenteArray[random.Next(fuenteArray.Length)]);
            }

            return resultado.ToString();
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            if(Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            IEnumerable<Usuarios> Lista = (from u in bd.Usuarios select u).ToList();
            return View(Lista);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }
            var usuario = (from u in bd.Usuarios where u.Id == id select u).Single();
            var to64 = Convert.ToBase64String(usuario.Foto.ToArray());
            ViewBag.foto = to64;
            return View(usuario);
        }

        // GET: Usuarios/Registrado/id
        public ActionResult Registrado(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var usuario = (from u in bd.Usuarios where u.Id == id select u).Single();
            var to64 = Convert.ToBase64String(usuario.Foto.ToArray());
            ViewBag.foto = to64;
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Usuarios modelo, HttpPostedFileBase Photo)
        {
            try
            {

                //Recibiendo datos: creando formato de fecha y foto
                string name = Path.GetFileName(Photo.FileName);
                string extension = Path.GetExtension(name);
                int size = Photo.ContentLength;
                Stream stream = Photo.InputStream;
                BinaryReader binaryReader = new BinaryReader(stream);
                byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                   
                modelo.Foto = bytes;
                DateTime fecha = DateTime.Now;
                modelo.Fecha_registro = fecha;


                // Generate password y salvando datos
                modelo.Clave = GeneratePassword(12);

                bd.Usuarios.InsertOnSubmit(modelo);
                bd.SubmitChanges();

                ViewBag.user = modelo.Usuario;
                ViewBag.clave = modelo.Clave;
                ViewBag.foto = modelo.Foto;

                var user = (from u in bd.Usuarios where u.Correo == modelo.Correo select u.Id).Single(); 

                return RedirectToAction("Registrado", new { id = user });
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }
            var usuario = (from u in bd.Usuarios where u.Id == id select u).Single();
            var to64 = Convert.ToBase64String(usuario.Foto.ToArray());
            ViewBag.foto = to64;
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Usuarios modelo, HttpPostedFileBase Photo)
        {
            try
            {
                var fotoActual = (from u in bd.Usuarios where u.Id == id select u.Foto).Single().ToArray();
                modelo.Foto = fotoActual;

                if (Photo != null)
                {
                    string name = Path.GetFileName(Photo.FileName);
                    string extension = Path.GetExtension(name);
                    int size = Photo.ContentLength;
                    Stream stream = Photo.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] bytes = binaryReader.ReadBytes((int)stream.Length);
                    modelo.Foto = bytes;
                }


                DateTime fecha = DateTime.Now;                
                modelo.Fecha_registro = fecha;                        

                bd.SP_modificar_usuario(modelo.Id,modelo.Usuario,modelo.Nombre,modelo.Apellido,modelo.Posicion,modelo.Telefono,modelo.Correo,modelo.Clave,modelo.Foto);
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
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }
            var usuario = (from u in bd.Usuarios where u.Id == id select u).Single();
            var to64 = Convert.ToBase64String(usuario.Foto.ToArray());
            ViewBag.foto = to64;
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var usuario = (from u in bd.Usuarios where u.Id == id select u).Single();
                bd.Usuarios.DeleteOnSubmit(usuario);
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
