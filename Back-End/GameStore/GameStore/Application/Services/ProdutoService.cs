using GameStore.Application.DTOs;
using GameStore.Application.DTOs.Produto;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ProdutoResponseDto> Criar(ProdutoCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentNullException("Produto Inválido");

            if (dto.Estoque < 0 || dto.Preco <= 0)
                throw new ArgumentException("Estoque ou preço Invalido");

            var categoria = await _context.Categorias.FirstOrDefaultAsync(e => e.Id == dto.CategoriaId);

            if (categoria == null)
                throw new ArgumentNullException("Categoria Inexistente");


            var produto = new Produto
            {
                Nome = dto.Nome.Trim(),
                Descricao = dto.Descricao.Trim(),
                Preco = dto.Preco,
                Estoque = dto.Estoque,
                Ativo = dto.Ativo,
                CategoriaId = dto.CategoriaId,
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
                Ativo = produto.Ativo,
                Descricao = produto.Descricao,
                CategoriaNome = categoria.Nome,
                CategoriaId = produto.CategoriaId
            };

        }

        public async Task<PageResultDto<ProdutoResponseDto>> Listar(ProdutoFiltroDto filtro)
        {
            var query = _context.Produtos.AsQueryable();


            if (!string.IsNullOrWhiteSpace(filtro.Nome))
                query = query.Where(p => p.Nome.Contains(filtro.Nome));
            if (filtro.CategoriaId.HasValue)
                query = query.Where(e => e.CategoriaId == filtro.CategoriaId);
            filtro.Normalizar();

            query = query.Include(p => p.Categoria);
            
            var total = await query.CountAsync();

            query = query.Skip((filtro.Page -1 ) * filtro.PageSize).Take(filtro.PageSize);

            var lista = await query.Select(produto => new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
                Ativo = produto.Ativo,
                Descricao = produto.Descricao,
                CategoriaNome = produto.Categoria.Nome,
                CategoriaId = produto.CategoriaId
            }).ToListAsync();

            return new PageResultDto<ProdutoResponseDto>(lista, total, filtro.Page, filtro.PageSize);
            
        }

        public async Task<ProdutoResponseDto?> BuscarId(int id)
        {
            return await _context.Produtos.Select(produto => new ProdutoResponseDto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
                Ativo = produto.Ativo,
                Descricao = produto.Descricao,
                CategoriaNome = produto.Categoria.Nome,
                CategoriaId = produto.CategoriaId
            }).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Atualizar(int id, ProdutoUpdateDto dto)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                throw new ArgumentException("produto Inexistente");

            if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentNullException("Produto Inválido");

            if (dto.Estoque < 0 || dto.Preco <= 0)
                throw new ArgumentException("Estoque ou preço Invalido");

            var categoria = await _context.Categorias.FirstOrDefaultAsync(e => e.Id == dto.CategoriaId);

            if (categoria == null)
                throw new ArgumentNullException("Categoria Inexistente");
                        
            produto.Nome = dto.Nome;
            produto.Descricao  = dto.Descricao;
            produto.Preco = dto.Preco;
            produto.Estoque = dto.Estoque;
            produto.CategoriaId = dto.CategoriaId;
            produto.Ativo = dto.Ativo;

            await _context.SaveChangesAsync();
        }

        public async Task Remover(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
                throw new ArgumentException("Produto não encontado");

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }


    }
}
