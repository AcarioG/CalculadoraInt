using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using CalculadoraInt.Models;

namespace CalculadoraInt.DF
{
    public class CalcInt  : DbContext
    {
        public CalcInt() : base("DefaultConnection")
        {

        }
        public DbSet<Clientes> Cliente { get; set; }
        public DbSet<Prestamo> Prestamo { get; set; }
        public DbSet<Cuotas> Cuotas { get; set; }
    }
}