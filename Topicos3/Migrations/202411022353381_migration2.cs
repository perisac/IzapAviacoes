namespace Topicos3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Aviaos", "Tripulacao_Id", "dbo.Tripulacaos");
            DropIndex("dbo.Rotas", new[] { "Aviao_Id" });
            AlterColumn("dbo.Rotas", "Aviao_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Rotas", "Aviao_Id");
            AddForeignKey("dbo.Aviaos", "Tripulacao_Id", "dbo.Tripulacaos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Aviaos", "Tripulacao_Id", "dbo.Tripulacaos");
            DropIndex("dbo.Rotas", new[] { "Aviao_Id" });
            AlterColumn("dbo.Rotas", "Aviao_Id", c => c.Int());
            CreateIndex("dbo.Rotas", "Aviao_Id");
            AddForeignKey("dbo.Aviaos", "Tripulacao_Id", "dbo.Tripulacaos", "Id", cascadeDelete: true);
        }
    }
}
