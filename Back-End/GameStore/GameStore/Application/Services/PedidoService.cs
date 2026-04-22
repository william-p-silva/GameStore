using GameStore.Domain.Entities;
using GameStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Services
{
    public class PedidoService
    {
        private readonly AppDbContext _context;
        public PedidoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CriarPedido(int userId)
        {
            var carrinho = await _context.Carrinhos
                .Include(e => e.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(p => p.UsuarioId == userId);

            if (carrinho == null || !carrinho.Itens.Any())
                throw new ArgumentException("Carrinho inexistente");

            var pedido = new Pedido
            {
                UsuarioId = userId,
                Status = "Pendente",
                CriadoEm = DateTime.UtcNow,
                Itens = new List<PedidoItem>()
            };

            decimal total = 0;

            foreach (var item in carrinho.Itens)
            {
                if (item.Produto.Estoque < item.Quantidade)
                    throw new ArgumentException("Estoque indisponivel");

                var pedidoItem = new PedidoItem
                {
                    NomeProduto = item.Produto.Nome,
                    PrecoUnitario = item.PrecoUnitario,
                    Quantidade = item.Quantidade,
                    ProdutoId = item.ProdutoId
                };

                item.Produto.Estoque -= item.Quantidade;

                total += item.Quantidade * item.PrecoUnitario;

                pedido.Itens.Add(pedidoItem);
            }

            pedido.Total = total;

            _context.Pedidos.Add(pedido);

            _context.CarrinhoItems.RemoveRange(carrinho.Itens);

            await _context.SaveChangesAsync();
            return pedido.Id;
        }
    }
}
