using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CalculadoraInt.Models;
using CalculadoraInt.DF;
using System.Net;

namespace CalculadoraInt.Controllers
{
    public class CobroController : Controller
    {
        private CalcInt db = new CalcInt();

        // GET: Clientes
        public ActionResult Clientes(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.CedulaSortParm = sortOrder == "cedula_asc" ? "cedula_desc" : "cedula_asc";

            var clientes = from s in db.Cliente
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                clientes = clientes.Where(s => s.Nombre.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    clientes = clientes.OrderByDescending(s => s.Nombre);
                    break;
                case "cedula_asc":
                    clientes = clientes.OrderBy(s => s.Cedula);
                    break;
                case "cedula_desc":
                    clientes = clientes.OrderBy(s => s.Cedula);
                    break;
                default:
                    clientes = clientes.OrderBy(s => s.Nombre);
                    break;
            }
            return View(clientes.ToList());
        }

        // GET: Cliente/Details/5 Prestamos de Cliente
        public ActionResult Prestamos(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cobro/Detalle_Prestamo/5
        public ActionResult Detalle_Prestamo(int? id)
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


        // GET: Cuotas/Delete/5
        public ActionResult Pagar_Cuota(int? id)
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
        [HttpPost, ActionName("Pagar_Cuota")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var cuotas = (from s in db.Cuotas where s.CuotasID == id select s).FirstOrDefault();
            db.Cuotas.Remove(cuotas);
            db.SaveChanges();
            return RedirectToAction("Detalle_Prestamo", new { id = cuotas.PrestamoID });
        }

        // GET: Prestamo/Delete/5
        public ActionResult Saldar_Prestamo(int? id)
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
        [HttpPost, ActionName("Saldar_Prestamo")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed2(int id)
        {
            Prestamo prestamo = db.Prestamo.Find(id);
            db.Prestamo.Remove(prestamo);
            db.SaveChanges();
            return RedirectToAction("Prestamos", new { id = prestamo.ClienteID });
        }
    }
}
