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
        public DbSet<Carrinho> Carrinho => Set<Carrinho>();
        public DbSet<CarrinhoItem> CarrinhoItems => Set<CarrinhoItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Carrinho>()
                .HasKey(c => c.Id);
            // 1 usuário -> 1 carrinho
            modelBuilder.Entity<Carrinho>()
                .HasOne(c => c.Usuario)
                .WithOne()
                .HasForeignKey<Carrinho>(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
            // 1 carrinho -> muitos itens
            modelBuilder.Entity<Carrinho>()
                .HasMany(c => c.Itens)
                .WithOne(i => i.Carrinho)
                .HasForeignKey(i => i.CarrinhoId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<CarrinhoItem>()
                .HasKey(i => i.Id);
            // NÃO pode ser unique → um carrinho tem vários itens
            modelBuilder.Entity<CarrinhoItem>()
                .HasIndex(i => new { i.CarrinhoId, i.ProdutoId })
                .IsUnique(); // impede produto duplicado no carrinho
            // relacionamento com produto
            modelBuilder.Entity<CarrinhoItem>()
                .HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);



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
                entity.HasOne(e => e.Categoria).WithMany(c => c.Produtos).HasForeignKey(p => p.CategoriaId).OnDelete(DeleteBehavior.Restrict); 
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            });
        }
    }
}
