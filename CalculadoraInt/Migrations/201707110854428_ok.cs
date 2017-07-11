namespace CalculadoraInt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ok : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        Cedula = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteID);
            
            CreateTable(
                "dbo.Prestamoes",
                c => new
                    {
                        PrestamoID = c.Int(nullable: false, identity: true),
                        Monto = c.Double(nullable: false),
                        Plazo = c.Int(nullable: false),
                        Interes = c.Single(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        ClienteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PrestamoID)
                .ForeignKey("dbo.Clientes", t => t.ClienteID, cascadeDelete: true)
                .Index(t => t.ClienteID);
            
            CreateTable(
                "dbo.Cuotas",
                c => new
                    {
                        CuotasID = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        Cuota = c.Double(nullable: false),
                        Interes = c.Double(nullable: false),
                        Amortiz_Principal = c.Double(nullable: false),
                        Amortiz_Total = c.Double(nullable: false),
                        Capital_Pendiente = c.Double(nullable: false),
                        Estado = c.Boolean(nullable: false),
                        PrestamoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CuotasID)
                .ForeignKey("dbo.Prestamoes", t => t.PrestamoID, cascadeDelete: true)
                .Index(t => t.PrestamoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cuotas", "PrestamoID", "dbo.Prestamoes");
            DropForeignKey("dbo.Prestamoes", "ClienteID", "dbo.Clientes");
            DropIndex("dbo.Cuotas", new[] { "PrestamoID" });
            DropIndex("dbo.Prestamoes", new[] { "ClienteID" });
            DropTable("dbo.Cuotas");
            DropTable("dbo.Prestamoes");
            DropTable("dbo.Clientes");
        }
    }
}
