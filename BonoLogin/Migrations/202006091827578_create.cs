namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DatosBonoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Metodo = c.String(nullable: false),
                        ValNominal = c.Double(nullable: false),
                        ValComercial = c.Double(nullable: false),
                        AnosVida = c.Int(nullable: false),
                        TipoAno = c.String(nullable: false),
                        FrecPago = c.String(nullable: false),
                        Tea = c.Double(nullable: false),
                        Tdea = c.Double(nullable: false),
                        Ianual = c.Double(nullable: false),
                        Ir = c.Double(nullable: false),
                        Pprima = c.Double(nullable: false),
                        Pestimacion = c.Double(nullable: false),
                        Pcolocacion = c.Double(nullable: false),
                        Pflotacion = c.Double(nullable: false),
                        PCavali = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                        ResultadoBono_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResultadoBonoes", t => t.ResultadoBono_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ResultadoBono_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.PGracias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipo = c.String(nullable: false),
                        Periodo = c.Int(nullable: false),
                        DatosBonoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DatosBonoes", t => t.DatosBonoId, cascadeDelete: true)
                .Index(t => t.DatosBonoId);
            
            CreateTable(
                "dbo.ResultadoBonoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Van = c.Double(nullable: false),
                        Va = c.Double(nullable: false),
                        Tcea = c.Double(nullable: false),
                        Tir = c.Double(nullable: false),
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResultadoBonoes", t => t.ResultadoBonoId, cascadeDelete: true)
                .Index(t => t.ResultadoBonoId);
            
            CreateTable(
                "dbo.FlujoEmisors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        Monto = c.Double(nullable: false),
                        ResultadoBonoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResultadoBonoes", t => t.ResultadoBonoId, cascadeDelete: true)
                .Index(t => t.ResultadoBonoId);
            
            CreateTable(
                "dbo.FlujoEmisorEscs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Periodo = c.Int(nullable: false),
                        Monto = c.Double(nullable: false),
                        ResultadoBonoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResultadoBonoes", t => t.ResultadoBonoId, cascadeDelete: true)
                .Index(t => t.ResultadoBonoId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.DatosBonoes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DatosBonoes", "ResultadoBono_Id", "dbo.ResultadoBonoes");
            DropForeignKey("dbo.FlujoEmisorEscs", "ResultadoBonoId", "dbo.ResultadoBonoes");
            DropForeignKey("dbo.FlujoEmisors", "ResultadoBonoId", "dbo.ResultadoBonoes");
            DropForeignKey("dbo.FlujoBonistas", "ResultadoBonoId", "dbo.ResultadoBonoes");
            DropForeignKey("dbo.PGracias", "DatosBonoId", "dbo.DatosBonoes");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.FlujoEmisorEscs", new[] { "ResultadoBonoId" });
            DropIndex("dbo.FlujoEmisors", new[] { "ResultadoBonoId" });
            DropIndex("dbo.FlujoBonistas", new[] { "ResultadoBonoId" });
            DropIndex("dbo.PGracias", new[] { "DatosBonoId" });
            DropIndex("dbo.DatosBonoes", new[] { "User_Id" });
            DropIndex("dbo.DatosBonoes", new[] { "ResultadoBono_Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.FlujoEmisorEscs");
            DropTable("dbo.FlujoEmisors");
            DropTable("dbo.FlujoBonistas");
            DropTable("dbo.ResultadoBonoes");
            DropTable("dbo.PGracias");
            DropTable("dbo.DatosBonoes");
        }
    }
}
