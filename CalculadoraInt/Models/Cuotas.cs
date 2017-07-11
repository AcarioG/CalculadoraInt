using System;
using System.Collections.Generic;
using CalculadoraInt.Models;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CalculadoraInt.Models
{
    public class Cuotas
    {
        public Cuotas()
        {
            Estado = true;
        }
        [Key]
        public int CuotasID { get; set; }
        public int Periodo { get; set; }
        public double Cuota { get; set; }
        public double Interes { get; set; }
        public double Amortiz_Principal { get; set; }
        public double Amortiz_Total { get; set; }
        public double Capital_Pendiente { get; set; }
        public bool Estado { get; set; }
        public int PrestamoID { get; set; }

        public virtual Prestamo Prestamo { get; set; }
    }
}