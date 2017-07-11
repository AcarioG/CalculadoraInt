namespace CalculadoraInt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class telefononorequerido : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clientes", "Telefono", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clientes", "Telefono", c => c.String(nullable: false));
        }
    }
}
