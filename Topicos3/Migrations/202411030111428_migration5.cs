namespace Topicos3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tripulacaos", "NomeTripulacao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tripulacaos", "NomeTripulacao");
        }
    }
}
