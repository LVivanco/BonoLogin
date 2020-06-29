namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultadoBonoes", "FlujoFechas", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResultadoBonoes", "FlujoFechas");
        }
    }
}
