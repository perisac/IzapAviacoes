namespace Topicos3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Rotas", name: "Aviao_Id", newName: "AviaoId");
            RenameIndex(table: "dbo.Rotas", name: "IX_Aviao_Id", newName: "IX_AviaoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Rotas", name: "IX_AviaoId", newName: "IX_Aviao_Id");
            RenameColumn(table: "dbo.Rotas", name: "AviaoId", newName: "Aviao_Id");
        }
    }
}
