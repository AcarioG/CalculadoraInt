using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalculadoraInt.Models;
using System.Data.Entity;
using System.Net;
using CalculadoraInt.DF;

namespace CalculadoraInt.Controllers
{
    public class PrestamoController : Controller
    {
        private CalcInt db = new CalcInt();

        // GET: Prestamo
        public ActionResult Index()
        {
            var prestamo = db.Prestamo.Include(p => p.Cliente);
            prestamo = prestamo.Where(p => p.Estado == true);
            return View(prestamo.ToList());
        }

        // GET: Prestamo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();

            }
            return View(prestamo);
        }

        // GET: Prestamo/Create
        public ActionResult Create()
        {
            ViewBag.ClienteID = new SelectList(db.Cliente, "ClienteID", "Nombre");
            return View();
        }

        // POST: Prestamo/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "PrestamoID,Monto,Plazo,Interes,Estado,ClienteID")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {


                db.Prestamo.Add(prestamo);

                db.SaveChanges();
                int id_prestamo = prestamo.PrestamoID;

                Cuotas cuotas = new Cuotas();
                double capital = prestamo.Monto;
                double interes = prestamo.Interes / 12 / 100;
                int plazo = prestamo.Plazo;

                double cuota = capital * (interes / (1 - Math.Pow((1 + interes), plazo * -1)));
                double interes_mensual = 0;
                double amortizacion_total = 0;

                for (int i = 1; i <= plazo; i++)
                {
                    interes_mensual = interes * capital;
                    capital = capital - cuota + interes_mensual;

                    amortizacion_total += cuota - interes_mensual;
                    double amortizacion = cuota - interes_mensual;

                    cuotas.Periodo = i;
                    cuotas.Cuota = Math.Round(cuota, 1);
                    cuotas.Interes = Math.Round(interes_mensual, 2);
                    cuotas.Amortiz_Principal = Math.Round(amortizacion, 2);
                    cuotas.Amortiz_Total = Math.Round(amortizacion_total, 2);
                    cuotas.Capital_Pendiente = Math.Round(capital, 2);
                    cuotas.PrestamoID = prestamo.PrestamoID;
                    new CuotasController().Create(cuotas);
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nombre", prestamo.ClienteID);
            return View(prestamo);
        }

        // GET: Prestamo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nombre", prestamo.ClienteID);
            return View(prestamo);
        }

        // POST: Prestamo/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "PrestamoID,Monto,Plazo,Interes,Estado,ClienteID")] Prestamo prestamo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prestamo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteID = new SelectList(db.Cliente, "ID", "Nombre", prestamo.ClienteID);
            return View(prestamo);
        }

        // GET: Prestamo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestamo prestamo = db.Prestamo.Find(id);
            if (prestamo == null)
            {
                return HttpNotFound();
            }
            return View(prestamo);
        }

        // POST: Prestamo/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Prestamo prestamo = db.Prestamo.Find(id);
            db.Prestamo.Remove(prestamo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
