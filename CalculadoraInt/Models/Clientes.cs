using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CalculadoraInt.Models
{
    public class Clientes
    {
        [Key]
        public int ClienteID { get; set; }
        [Required]
        public String Nombre { get; set; }
        [Required]
        public String Apellido { get; set; }
        [Required]
        public String Direccion { get; set; }
        
        public String Telefono { get; set; }
        [Required]
        public String Cedula { get; set; }

        public virtual ICollection<Prestamo> Prestamo { get; set; }
    }
}