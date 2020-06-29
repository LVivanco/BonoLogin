namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dates4je : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultadoBonoes", "Ip", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResultadoBonoes", "Ip");
        }
    }
}
