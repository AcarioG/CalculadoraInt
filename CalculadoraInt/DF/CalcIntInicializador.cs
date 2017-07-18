using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CalculadoraInt.Models;
using System.Data.Entity;

namespace CalculadoraInt.DF
{
    public class CalcIntInicializador : System.Data.Entity.DropCreateDatabaseIfModelChanges<CalcInt>
    {
        protected override void Seed(CalcInt context)
        {
            var clientes = new List<Clientes>
            {
                new Clientes {Nombre= "Wilmer", Telefono= "809-292-2131", Cedula= "402700760772", Direccion= "Sabana Perdida"}
            };

            clientes.ForEach(s => context.Cliente.Add(s));
            context.SaveChanges();
        }

    }
}