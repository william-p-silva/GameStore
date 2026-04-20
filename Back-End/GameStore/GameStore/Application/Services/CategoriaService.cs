using GameStore.Application.DTOs;
using GameStore.Application.DTOs.Categoria;
using GameStore.Application.DTOs.Produto;
using GameStore.Domain.Entities;
using GameStore.Helpers;
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

        public async Task<PageResultDto<CategoriaResponseDto>> Listar(CategoriaFiltroDto filtro)
        {
            var query = _context.Categorias.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
                query = query.Where(p => p.Nome.Contains(filtro.Nome));

            var total = await query.CountAsync();

            filtro.Normalizar();

            query = query.Skip((filtro.Page - 1) * filtro.PageSize).Take(filtro.PageSize);



            if (filtro.Produtos == true)
            {
                return await query.ToPagedResultAsync
                    (
                        filtro.Page,
                        filtro.PageSize,
                        c => new CategoriaResponseDto
                        {
                            Nome = c.Nome,
                            Id = c.Id,
                            QuantidadeProdutos = c.Produtos.Count(),
                            Produtos = c.Produtos.Select(p => new ProdutoResumoDto
                            {
                                Id = p.Id,
                                Nome = p.Nome,
                                Preco = p.Preco,
                                Estoque = p.Estoque,
                                Ativo = p.Ativo
                            }).ToList()
                        }
                    );
            }
            else //CASO 2: NÃO trazer produtos, só quantidade
            {
                return await query.ToPagedResultAsync(
                    filtro.Page,
                    filtro.PageSize,
                    c => new CategoriaResponseDto
                {
                    Nome = c.Nome,
                    Id = c.Id,
                    QuantidadeProdutos = c.Produtos.Count()
                });
            }
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
