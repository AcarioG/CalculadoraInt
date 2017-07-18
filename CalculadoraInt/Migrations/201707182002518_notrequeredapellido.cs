namespace CalculadoraInt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notrequeredapellido : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clientes", "Apellido", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clientes", "Apellido", c => c.String(nullable: false));
        }
    }
}
