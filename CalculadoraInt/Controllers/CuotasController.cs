using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalculadoraInt.Models;
using CalculadoraInt.DF;
using System.Net;
using System.Data.Entity;

namespace CalculadoraInt.Controllers
{
    public class CuotasController : Controller
    {
        private CalcInt db = new CalcInt();

        // GET: Cuotas
        public ActionResult Index()
        {
            var cuotas = db.Cuotas.Include(c => c.Prestamo);
            return View(cuotas.ToList());
        }

        // GET: Cuotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuotas cuotas = db.Cuotas.Find(id);
            if (cuotas == null)
            {
                return HttpNotFound();
            }
            return View(cuotas);
        }

        // GET: Cuotas/Create
        public ActionResult Create()
        {
            ViewBag.PrestamoID = new SelectList(db.Prestamo, "PrestamoID", "PrestamoID");
            return View();
        }

        // POST: Cuotas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CuotasID,Periodo,Cuota,Interes,Amortiz_Principal,Amortiz_Total,Capital_Pendiente,Estado,PrestamoID")] Cuotas cuotas)
        {
            if (ModelState.IsValid)
            {
                db.Cuotas.Add(cuotas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PrestamoID = new SelectList(db.Prestamo, "PrestamoID", "PrestamoID", cuotas.PrestamoID);
            return View(cuotas);
        }

        // GET: Cuotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuotas cuotas = db.Cuotas.Find(id);
            if (cuotas == null)
            {
                return HttpNotFound();
            }
            ViewBag.PrestamoID = new SelectList(db.Prestamo, "PrestamoID", "PrestamoID", cuotas.PrestamoID);
            return View(cuotas);
        }

        // POST: Cuotas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CuotasID,Periodo,Cuota,Interes,Amortiz_Principal,Amortiz_Total,Capital_Pendiente,Estado,PrestamoID")] Cuotas cuotas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuotas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PrestamoID = new SelectList(db.Prestamo, "PrestamoID", "PrestamoID", cuotas.PrestamoID);
            return View(cuotas);
        }

        // GET: Cuotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuotas cuotas = db.Cuotas.Find(id);
            if (cuotas == null)
            {
                return HttpNotFound();
            }
            return View(cuotas);
        }

        // POST: Cuotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Cuotas cuotas = db.Cuotas.Find(id);
            db.Cuotas.Remove(cuotas);
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
