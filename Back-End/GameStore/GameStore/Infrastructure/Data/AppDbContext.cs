using Microsoft.EntityFrameworkCore;
using GameStore.Domain.Entities;

namespace GameStore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios => Set<Usuario>();
        public DbSet<Produto> Produtos => Set<Produto>();
        public DbSet<Categoria> Categorias => Set<Categoria>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.SenhaHash).IsRequired();
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Descricao).HasMaxLength(300);
                entity.Property(e => e.Preco).IsRequired().HasColumnType("decimal(10,2)");
                entity.HasOne(e => e.Categoria).WithMany(c => c.Produtos).HasForeignKey(p => p.CategoriaId).OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            });
        }
    }
}
