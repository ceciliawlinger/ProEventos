using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contextos
{
    public class ProEventosContext : DbContext
    {

        public ProEventosContext(DbContextOptions<ProEventosContext> options) 
            : base(options){}
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<Palestrante> Palestrantes { get; set; }
        public DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet<RedeSocial> RedesSociais { get; set; }

        // cria a correlação automática de tabelas n para n
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PalestranteEvento>()
                .HasKey(palestranteEvento => new {palestranteEvento.EventoId, palestranteEvento.PalestranteId});

            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Palestrante>()
                .HasMany(p => p.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}