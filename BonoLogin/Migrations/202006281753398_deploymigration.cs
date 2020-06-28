namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deploymigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultadoBonoes", "TceaBonista", c => c.Double(nullable: false));
            AddColumn("dbo.ResultadoBonoes", "TirBonista", c => c.Double(nullable: false));
            AddColumn("dbo.ResultadoBonoes", "TceaEmisor", c => c.Double(nullable: false));
            AddColumn("dbo.ResultadoBonoes", "TirEmisor", c => c.Double(nullable: false));
            DropColumn("dbo.ResultadoBonoes", "Tcea");
            DropColumn("dbo.ResultadoBonoes", "Tir");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResultadoBonoes", "Tir", c => c.Double(nullable: false));
            AddColumn("dbo.ResultadoBonoes", "Tcea", c => c.Double(nullable: false));
            DropColumn("dbo.ResultadoBonoes", "TirEmisor");
            DropColumn("dbo.ResultadoBonoes", "TceaEmisor");
            DropColumn("dbo.ResultadoBonoes", "TirBonista");
            DropColumn("dbo.ResultadoBonoes", "TceaBonista");
        }
    }
}
