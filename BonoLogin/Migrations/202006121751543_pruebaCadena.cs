namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pruebaCadena : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultadoBonoes", "Amortizacion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResultadoBonoes", "Amortizacion");
        }
    }
}
