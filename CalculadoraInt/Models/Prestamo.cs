using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using CalculadoraInt.Models;

namespace CalculadoraInt.Models
{
    public class Prestamo
    {

        public Prestamo()
        {
            Estado = true;
        }
        [Key]
        public int PrestamoID { get; set; }
        [Required]
        public double Monto { get; set; }
        [Required]
        public int Plazo { get; set; }
        [Required]
        public float Interes { get; set; }
        public bool Estado { get; set; }
        public int ClienteID { get; set; }


        public virtual Clientes Cliente { get; set; }
        public virtual ICollection<Cuotas> Cuotas { get; set; }
    }
}