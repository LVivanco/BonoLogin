namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tasasperiodo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultadoBonoes", "Tep", c => c.Double(nullable: false));
            AddColumn("dbo.ResultadoBonoes", "Tdep", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResultadoBonoes", "Tdep");
            DropColumn("dbo.ResultadoBonoes", "Tep");
        }
    }
}
