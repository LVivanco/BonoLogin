namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DatosBonoes", "Pestructuracion", c => c.Double(nullable: false));
            DropColumn("dbo.DatosBonoes", "Pestimacion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DatosBonoes", "Pestimacion", c => c.Double(nullable: false));
            DropColumn("dbo.DatosBonoes", "Pestructuracion");
        }
    }
}
