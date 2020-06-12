namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class name : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DatosBonoes", "Nombre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DatosBonoes", "Nombre");
        }
    }
}
