using GameStore.Application.DTOs.Carrinho;
using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Services
{
    public class CarrinhoService
    {
        private readonly AppDbContext _context;
        public CarrinhoService(AppDbContext context)
        {
            _context = context;
        }

        private async Task<Carrinho> ObterCarrinhoInterno(int userId)
        {
            var carrinho = await _context.Carrinhos
                .Include(i => i.Itens)
                .ThenInclude(f => f.Produto)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(e => e.UsuarioId == userId);

            if (carrinho == null)
            {
                carrinho = new Carrinho
                {
                    UsuarioId = userId,
                };
                _context.Carrinhos.Add(carrinho);
                await _context.SaveChangesAsync();
            }
            return carrinho;
        }

        public async Task<CarrinhoResponseDto> ObterOuCriarCarrinho(int userId)
        {
            var carrinho = await ObterCarrinhoInterno(userId);


            carrinho.Itens ??= new List<CarrinhoItem>();

            var itensDto = carrinho.Itens.Select(i => new CarrinhoItemResponseDto
            {
                NomeProduto = i.Produto.Nome,
                PrecoUnitario = i.PrecoUnitario,
                ProdutoId = i.Produto.Id,
                Quantidade = i.Quantidade,
                SubTotal = i.Quantidade * i.PrecoUnitario,
                Estoque = i.Produto.Estoque
            }).ToList();

            var totalItens = carrinho.Itens.Sum(e => e.Quantidade);
            var totalCarrinho = carrinho.Itens.Sum(i => i.Quantidade * i.PrecoUnitario);


            return new CarrinhoResponseDto
            {
                Id = carrinho.Id,
                Itens = itensDto,
                TotalCarrinho = totalCarrinho,
                TotalItens = totalItens,
                UsuarioEmail = carrinho.Usuario?.Email ?? "",
                UsuarioId = carrinho.UsuarioId
            };

        }

        public async Task<CarrinhoResponseDto> AdicionarProduto(CarrinhoAdcionarItemDto dto, int userId)
        {
            if (dto.Quantidade <= 0)
                throw new ArgumentException("Quantidade Inválida");

            var produto = await _context.Produtos
                .Select(p => new {p.Id, p.Preco, p.Estoque})
                .FirstOrDefaultAsync(p => p.Id == dto.ProdutoID);
            if (produto == null)
                throw new ArgumentException("produto Inexistente");

            var carrinho = await ObterCarrinhoInterno(userId);

            var itemExistentes = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == dto.ProdutoID);
            if (itemExistentes != null)
            {
                var novaQtd = itemExistentes.Quantidade + dto.Quantidade;
                if (produto.Estoque < novaQtd)
                    throw new ArgumentException("Estoque insuficiente");
                itemExistentes.Quantidade = novaQtd;
            }
            else
            {
                if (produto.Estoque < dto.Quantidade)
                    throw new ArgumentException("Estoque insuficiente");
                var newItem = new CarrinhoItem
                {
                    CarrinhoId = carrinho.Id,
                    ProdutoId = produto.Id,
                    Quantidade = dto.Quantidade,
                    PrecoUnitario = produto.Preco
                };
                _context.CarrinhoItems.Add(newItem);
            }
            await _context.SaveChangesAsync();
            return await ObterOuCriarCarrinho(userId);
        }

        public async Task<CarrinhoResponseDto> RemoverProduto(CarrinhoRemoverItemDto dto, int userId)
        {
            var carrinho = await ObterCarrinhoInterno(userId);

            var item = carrinho.Itens.FirstOrDefault(p => p.ProdutoId == dto.ProdutoId);

            if (item == null)
                throw new ArgumentException("Item Inexistente");

            _context.CarrinhoItems.Remove(item);
            await _context.SaveChangesAsync();
            return await ObterOuCriarCarrinho(userId);
        }

        public async Task<CarrinhoResponseDto> AtualizarItemCarrinho(CarrinhoAtualizarItemDto dto, int userId)
        {
            var carrinho = await ObterCarrinhoInterno(userId);
            var item = carrinho.Itens.FirstOrDefault(i => i.ProdutoId == dto.ProdutId);

            if (item == null)
                throw new ArgumentException("Produto inexistente");

            var novaQtn = dto.Quantidade;

            if (novaQtn <= 0)
            {
                _context.CarrinhoItems.Remove(item);
                await _context.SaveChangesAsync();
                return await ObterOuCriarCarrinho(userId);
            }

            var produto = await _context.Produtos
                .Select(p => new { p.Id, p.Estoque })
                .FirstOrDefaultAsync(e => e.Id == dto.ProdutId);

            if (produto == null)
                throw new ArgumentException("Produto invalido");

            if (produto.Estoque < novaQtn)
                throw new ArgumentException("Estoque insuficiente");

            item.Quantidade = novaQtn;
            await _context.SaveChangesAsync();
            return await ObterOuCriarCarrinho(userId);
        }
    }
}
