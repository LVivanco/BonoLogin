namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class money : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DatosBonoes", "Moneda", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DatosBonoes", "Moneda");
        }
    }
}
