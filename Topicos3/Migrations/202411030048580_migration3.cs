namespace Topicos3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Aviaos", name: "Tripulacao_Id", newName: "TripulacaoId");
            RenameIndex(table: "dbo.Aviaos", name: "IX_Tripulacao_Id", newName: "IX_TripulacaoId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Aviaos", name: "IX_TripulacaoId", newName: "IX_Tripulacao_Id");
            RenameColumn(table: "dbo.Aviaos", name: "TripulacaoId", newName: "Tripulacao_Id");
        }
    }
}
