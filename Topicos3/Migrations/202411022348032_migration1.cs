namespace Topicos3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aviaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Modelo = c.String(nullable: false, maxLength: 100),
                        Autonomia = c.Double(nullable: false),
                        AnoFabricacao = c.Int(nullable: false),
                        Capacidade = c.Int(nullable: false),
                        VelocidadeMedia = c.Double(nullable: false),
                        HorasVoo = c.Double(nullable: false),
                        UltimaManutencao = c.DateTime(nullable: false),
                        Tripulacao_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tripulacaos", t => t.Tripulacao_Id, cascadeDelete: true)
                .Index(t => t.Tripulacao_Id);
            
            CreateTable(
                "dbo.Rotas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Origem = c.String(nullable: false, maxLength: 100),
                        Destino = c.String(nullable: false, maxLength: 100),
                        Distancia = c.Double(nullable: false),
                        TempoEstimado = c.Time(nullable: false, precision: 7),
                        Aviao_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aviaos", t => t.Aviao_Id)
                .Index(t => t.Aviao_Id);
            
            CreateTable(
                "dbo.Tripulacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PilotoId = c.Int(nullable: false),
                        CoPilotoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pilotoes", t => t.CoPilotoId)
                .ForeignKey("dbo.Pilotoes", t => t.PilotoId)
                .Index(t => t.PilotoId)
                .Index(t => t.CoPilotoId);
            
            CreateTable(
                "dbo.Comissarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        DataNascimento = c.DateTime(nullable: false),
                        Cpf = c.String(nullable: false, maxLength: 14),
                        AnosExperiencia = c.Int(nullable: false),
                        UltimoTreinamento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pilotoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100),
                        DataNascimento = c.DateTime(nullable: false),
                        Cpf = c.String(nullable: false, maxLength: 14),
                        TipoLicenca = c.String(nullable: false, maxLength: 50),
                        TempoExperiencia = c.Int(nullable: false),
                        HorasVoo = c.Double(nullable: false),
                        UltimaAvaliacaoMedica = c.DateTime(nullable: false),
                        UltimoTreinamento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                "dbo.TripulacaoComissario",
                c => new
                    {
                        TripulacaoId = c.Int(nullable: false),
                        ComissarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TripulacaoId, t.ComissarioId })
                .ForeignKey("dbo.Tripulacaos", t => t.TripulacaoId, cascadeDelete: true)
                .ForeignKey("dbo.Comissarios", t => t.ComissarioId, cascadeDelete: true)
                .Index(t => t.TripulacaoId)
                .Index(t => t.ComissarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Aviaos", "Tripulacao_Id", "dbo.Tripulacaos");
            DropForeignKey("dbo.Tripulacaos", "PilotoId", "dbo.Pilotoes");
            DropForeignKey("dbo.Tripulacaos", "CoPilotoId", "dbo.Pilotoes");
            DropForeignKey("dbo.TripulacaoComissario", "ComissarioId", "dbo.Comissarios");
            DropForeignKey("dbo.TripulacaoComissario", "TripulacaoId", "dbo.Tripulacaos");
            DropForeignKey("dbo.Rotas", "Aviao_Id", "dbo.Aviaos");
            DropIndex("dbo.TripulacaoComissario", new[] { "ComissarioId" });
            DropIndex("dbo.TripulacaoComissario", new[] { "TripulacaoId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tripulacaos", new[] { "CoPilotoId" });
            DropIndex("dbo.Tripulacaos", new[] { "PilotoId" });
            DropIndex("dbo.Rotas", new[] { "Aviao_Id" });
            DropIndex("dbo.Aviaos", new[] { "Tripulacao_Id" });
            DropTable("dbo.TripulacaoComissario");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Pilotoes");
            DropTable("dbo.Comissarios");
            DropTable("dbo.Tripulacaos");
            DropTable("dbo.Rotas");
            DropTable("dbo.Aviaos");
        }
    }
}
