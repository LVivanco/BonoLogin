namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class result : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DatosBonoes", "ResultadoBono_Id", "dbo.ResultadoBonoes");
            DropIndex("dbo.DatosBonoes", new[] { "ResultadoBono_Id" });
            AddColumn("dbo.ResultadoBonoes", "DatosBonoId", c => c.Int(nullable: false));
            CreateIndex("dbo.ResultadoBonoes", "DatosBonoId");
            AddForeignKey("dbo.ResultadoBonoes", "DatosBonoId", "dbo.DatosBonoes", "Id", cascadeDelete: true);
            DropColumn("dbo.DatosBonoes", "ResultadoBono_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DatosBonoes", "ResultadoBono_Id", c => c.Int());
            DropForeignKey("dbo.ResultadoBonoes", "DatosBonoId", "dbo.DatosBonoes");
            DropIndex("dbo.ResultadoBonoes", new[] { "DatosBonoId" });
            DropColumn("dbo.ResultadoBonoes", "DatosBonoId");
            CreateIndex("dbo.DatosBonoes", "ResultadoBono_Id");
            AddForeignKey("dbo.DatosBonoes", "ResultadoBono_Id", "dbo.ResultadoBonoes", "Id");
        }
    }
}
