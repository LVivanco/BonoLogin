namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datosemisor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosEmisors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(nullable: false),
                        Ianual = c.Double(nullable: false),
                        Ir = c.Double(nullable: false),
                        Pprima = c.Double(nullable: false),
                        Pestructuracion = c.Double(nullable: false),
                        Pcolocacion = c.Double(nullable: false),
                        Pflotacion = c.Double(nullable: false),
                        PCavali = c.Double(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DatosEmisors", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.DatosEmisors", new[] { "UserId" });
            DropTable("dbo.DatosEmisors");
        }
    }
}
