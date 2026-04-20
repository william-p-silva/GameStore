using GameStore.Application.DTOs.Categoria;
using GameStore.Application.DTOs.Produto;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Services
{
    public class CategoriaService
    {
        private readonly AppDbContext _context;
        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CategoriaResponseDto> Criar(CategoriaCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome da categoria inválido");
            var categoria = new Categoria
            {
                Nome = dto.Nome
            };
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return new CategoriaResponseDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome,
            };
        }

        public async Task<List<CategoriaResponseDto>> Listar()
        {
            return await _context.Categorias.Select(c => new CategoriaResponseDto
            {
                Nome = c.Nome,
                Id = c.Id,
                Produtos = c.Produtos.Select(p => new ProdutoResumoDto
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Estoque = p.Estoque,
                    Ativo = p.Ativo
                }).ToList()
            }).ToListAsync();
        }

        public async Task<CategoriaResponseDto?> BuscarId(int id)
        {
            return await _context.Categorias
                .Where(c => c.Id == id)
                .Select(c => new CategoriaResponseDto
                {
                    Nome = c.Nome,
                    Id = c.Id,
                    Produtos = c.Produtos.Select(p => new ProdutoResumoDto
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        Preco = p.Preco,
                        Estoque = p.Estoque,
                        Ativo = p.Ativo
                    }).ToList()
                }).FirstOrDefaultAsync();
        }

        public async Task Atualizar(int id, CategoriaUpdateDto dto)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");

            categoria.Nome = dto.Nome.Trim();

            await _context.SaveChangesAsync();
        }

        public async Task Remover(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                throw new ArgumentException("Categoria não encontrada");

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();
        }
    }
}
