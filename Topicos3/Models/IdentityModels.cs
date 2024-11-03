using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Topicos3.Models
{
    // É possível adicionar dados do perfil do usuário adicionando mais propriedades na sua classe ApplicationUser, visite https://go.microsoft.com/fwlink/?LinkID=317594 para obter mais informações.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Observe que a authenticationType deve corresponder a uma definida em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Adicionar declarações do usuário personalizadas aqui
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Comissario> Comissarios { get; set; }
        public DbSet<Piloto> Pilotos { get; set; }
        public DbSet<Tripulacao> Tripulacoes { get; set; }
        public DbSet<Aviao> Avioes { get; set; }
        public DbSet<Rota> Rotas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Configuração da relação muitos-para-muitos entre Comissario e Tripulacao
            modelBuilder.Entity<Tripulacao>()
                .HasMany(t => t.Comissarios)
                .WithMany()
                .Map(m =>
                {
                    m.ToTable("TripulacaoComissario");
                    m.MapLeftKey("TripulacaoId");
                    m.MapRightKey("ComissarioId");
                });

            // Configuração da relação um-para-um entre Tripulacao e Piloto
            modelBuilder.Entity<Tripulacao>()
                .HasRequired(t => t.Piloto)
                .WithMany()
                .HasForeignKey(t => t.PilotoId)
                .WillCascadeOnDelete(false);

            // Configuração da relação um-para-um entre Tripulacao e CoPiloto
            modelBuilder.Entity<Tripulacao>()
                .HasRequired(t => t.CoPiloto)
                .WithMany()
                .HasForeignKey(t => t.CoPilotoId)
                .WillCascadeOnDelete(false);

            // Configuração da relação um-para-muitos entre Aviao e Rota
            modelBuilder.Entity<Aviao>()
                .HasMany(a => a.Rotas)
                .WithRequired(r => r.Aviao)
                .WillCascadeOnDelete(false);

            // Configuração da relação obrigatória entre Aviao e Tripulacao
            modelBuilder.Entity<Aviao>()
                .HasRequired(a => a.Tripulacao)
                .WithMany()
                .WillCascadeOnDelete(false);

        }
    }
}