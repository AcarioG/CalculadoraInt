namespace CalculadoraInt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sinapellido : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clientes", "Telefono", c => c.String(nullable: false));
            DropColumn("dbo.Clientes", "Apellido");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clientes", "Apellido", c => c.String());
            AlterColumn("dbo.Clientes", "Telefono", c => c.String());
        }
    }
}
