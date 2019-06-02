using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using InnguzApp.ContextoDatos;

namespace InnguzApp.Controllers
{
    public class VentaController : Controller
    {

        Base_DatosDataContext bd = new Base_DatosDataContext();
        // GET: Venta
        public ActionResult Index()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            IEnumerable<Ventas_view> Lista = (from vv in bd.Ventas_view select vv).ToList();
            return View(Lista);
        }

        // GET: Venta/Details/5
        public ActionResult Details(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var venta = (from vv in bd.Ventas_view where vv.Id == id select vv).Single();
            return View(venta);
        }

        // GET: Venta/Create
        public ActionResult Create()
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var productos = (from p in bd.Productos select p).ToList();

            ViewBag.Products = productos;

            return View();
        }

        // POST: Venta/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, Ventas modelo)
        {
            try
            {
                //Fecha y usuario
                DateTime fecha = DateTime.Now;
                modelo.FechaInserta = fecha;
                modelo.UsuarioInserta = Session["login"].ToString();
                var idUser = (from u in bd.Usuarios where u.Usuario == Session["login"].ToString() select u.Id).Single();
                modelo.Usuario_id = idUser;


                //Verificar que sea el mismo periodo o crear uno nuevo
                var mes = fecha.Month;
                var año = fecha.Year;

                var period = (from per in bd.Periodos where per.mes == mes && per.año == año select per).SingleOrDefault();

                if(period != null)
                {
                    modelo.periodo_id = period.id_periodo;
                } else
                {
                    Periodos p = new Periodos
                    {
                        mes = mes,
                        año = año,
                        estado = 1
                    };

                    bd.Periodos.InsertOnSubmit(p);
                    bd.SubmitChanges();

                    bd.SP_Actualizar_Periodo(period.id_periodo, 2);
                    bd.SubmitChanges();
                    var periodo = (from per in bd.Periodos where p.mes == mes && p.año == año select per).SingleOrDefault();
                    modelo.periodo_id = period.id_periodo;
                }
                decimal IVA = 0.13m;
                modelo.IVA = modelo.Monto * IVA;
                modelo.Total = (modelo.Monto * modelo.Cantidad) + modelo.IVA;

                bd.Ventas.InsertOnSubmit(modelo);
                bd.SubmitChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Venta/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var venta = (from v in bd.Ventas where v.Id == id select v).Single();
            var productos = (from p in bd.Productos select p).ToList();

            ViewBag.Products = productos;

            return View(venta);
        }

        // POST: Venta/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, Ventas modelo)
        {
            try
            {

                decimal IVA = 0.13m;
                modelo.IVA = modelo.Monto * IVA;
                modelo.Total = (modelo.Cantidad * modelo.Monto) + modelo.IVA;
                modelo.UsuarioActualiza = Session["login"].ToString();
                bd.SP_Actualizar_Venta(modelo.Id, modelo.Descripcion, modelo.Cantidad, modelo.Monto, modelo.IVA, modelo.Total, modelo.Producto_Servicio_id, modelo.UsuarioActualiza);
                bd.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Venta/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["login"] == null)
            {
                return Redirect("~/Login/Login");
            }

            var venta = (from vv in bd.Ventas_view where vv.Id == id select vv).Single();

            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var venta = (from v in bd.Ventas where v.Id == id select v).Single();
                bd.Ventas.DeleteOnSubmit(venta);
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
