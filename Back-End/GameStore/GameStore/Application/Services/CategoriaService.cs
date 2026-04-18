using GameStore.Application.DTOs;
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

        public async Task<Categoria> Criar(CategoriaCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ArgumentException("Nome da categoria inválido");
            var categoria = new Categoria
            {
                Nome = dto.Nome
            };
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return categoria;
        }

        public async Task<List<Categoria>> Listar()
        {
            return await _context.Categorias.ToListAsync();
        }

        public async Task<Categoria?> BuscarId(int id)
        {
            return await _context.Categorias.FindAsync(id);
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
