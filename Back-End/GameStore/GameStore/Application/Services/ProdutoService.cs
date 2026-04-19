using GameStore.Application.DTOs;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Services
{
    public class ProdutoService
    {
        private readonly AppDbContext _context;
        private readonly CategoriaService _service;
        public ProdutoService(AppDbContext context, CategoriaService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<Produto> Criar(ProdutoCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome) || string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ArgumentNullException("Produto Inválido");

            if (dto.Estoque < 0 || dto.Preco <= 0)
                throw new ArgumentException("Estoque ou preço Invalido");

            var categoria = await _service.BuscarId(dto.CategoriaId);

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
            return produto;

        }

        public async Task<List<Produto>> Listar()
        {
            return await _context.Produtos
                .Include(e => e.Categoria).ToListAsync();
        }

        public async Task<Produto?> BuscarId(int id)
        {
            return await _context.Produtos
                .Include(e => e.Categoria).FirstOrDefaultAsync(p => p.Id == id);
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
            
            var categoria = await _service.BuscarId(dto.CategoriaId);

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
