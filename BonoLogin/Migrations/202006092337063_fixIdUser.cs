namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixIdUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.DatosBonoes", new[] { "User_Id" });
            DropColumn("dbo.DatosBonoes", "UserId");
            RenameColumn(table: "dbo.DatosBonoes", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.DatosBonoes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.DatosBonoes", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.DatosBonoes", new[] { "UserId" });
            AlterColumn("dbo.DatosBonoes", "UserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.DatosBonoes", name: "UserId", newName: "User_Id");
            AddColumn("dbo.DatosBonoes", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.DatosBonoes", "User_Id");
        }
    }
}
