namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dates2je : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DatosBonoes", "fechaEmision", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DatosBonoes", "fechaEmision");
        }
    }
}
