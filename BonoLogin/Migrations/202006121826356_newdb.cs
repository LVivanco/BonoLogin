namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newdb : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FlujoBonistas", "ResultadoBonoId", "dbo.ResultadoBonoes");
            DropForeignKey("dbo.FlujoEmisors", "ResultadoBonoId", "dbo.ResultadoBonoes");
            DropForeignKey("dbo.FlujoEmisorEscs", "ResultadoBonoId", "dbo.ResultadoBonoes");
            DropIndex("dbo.FlujoBonistas", new[] { "ResultadoBonoId" });
            DropIndex("dbo.FlujoEmisors", new[] { "ResultadoBonoId" });
            DropIndex("dbo.FlujoEmisorEscs", new[] { "ResultadoBonoId" });
            AddColumn("dbo.ResultadoBonoes", "Bono", c => c.String());
            AddColumn("dbo.ResultadoBonoes", "BonoIndexado", c => c.String());
            AddColumn("dbo.ResultadoBonoes", "Cupon", c => c.String());
            AddColumn("dbo.ResultadoBonoes", "Cuota", c => c.String());
            AddColumn("dbo.ResultadoBonoes", "Prima", c => c.String());
            AddColumn("dbo.ResultadoBonoes", "Escudo", c => c.String());
            AddColumn("dbo.ResultadoBonoes", "FlujoEmisor", c => c.String());
            AddColumn("dbo.ResultadoBonoes", "FlujoEmisorEsc", c => c.String());
            AddColumn("dbo.ResultadoBonoes", "FlujoBonista", c => c.String());
            DropTable("dbo.FlujoBonistas");
            DropTable("dbo.FlujoEmisors");
            DropTable("dbo.FlujoEmisorEscs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FlujoEmisorEscs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        Monto = c.Double(nullable: false),
                        ResultadoBonoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FlujoEmisors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        Monto = c.Double(nullable: false),
                        ResultadoBonoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FlujoBonistas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        Monto = c.Double(nullable: false),
                        ResultadoBonoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.ResultadoBonoes", "FlujoBonista");
            DropColumn("dbo.ResultadoBonoes", "FlujoEmisorEsc");
            DropColumn("dbo.ResultadoBonoes", "FlujoEmisor");
            DropColumn("dbo.ResultadoBonoes", "Escudo");
            DropColumn("dbo.ResultadoBonoes", "Prima");
            DropColumn("dbo.ResultadoBonoes", "Cuota");
            DropColumn("dbo.ResultadoBonoes", "Cupon");
            DropColumn("dbo.ResultadoBonoes", "BonoIndexado");
            DropColumn("dbo.ResultadoBonoes", "Bono");
            CreateIndex("dbo.FlujoEmisorEscs", "ResultadoBonoId");
            CreateIndex("dbo.FlujoEmisors", "ResultadoBonoId");
            CreateIndex("dbo.FlujoBonistas", "ResultadoBonoId");
            AddForeignKey("dbo.FlujoEmisorEscs", "ResultadoBonoId", "dbo.ResultadoBonoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FlujoEmisors", "ResultadoBonoId", "dbo.ResultadoBonoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FlujoBonistas", "ResultadoBonoId", "dbo.ResultadoBonoes", "Id", cascadeDelete: true);
        }
    }
}
