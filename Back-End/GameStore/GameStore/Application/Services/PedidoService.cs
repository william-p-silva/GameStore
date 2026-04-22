using GameStore.Application.DTOs;
using GameStore.Application.DTOs.Pedido;
using GameStore.Domain.Entities;
using GameStore.Helpers;
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

        public async Task<PedidoResponseDto> CriarPedido(int userId)
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
            var usuario = await _context.Usuarios.Where(i => i.Id == userId).Select(o => o.Nome).FirstOrDefaultAsync();

            pedido.Total = total;

            _context.Pedidos.Add(pedido);

            _context.CarrinhoItems.RemoveRange(carrinho.Itens);

            await _context.SaveChangesAsync();
            return new PedidoResponseDto
            {

                SubTotal = total,
                Id = pedido.Id,
                CriadoEm = pedido.CriadoEm,
                Status = pedido.Status,
                UsuarioId = pedido.UsuarioId,
                UsuarioNome = usuario,
                Itens = pedido.Itens.Select(i => new PedidoItemResponseDto
                {
                    NomeProduto = i.NomeProduto,
                    PrecoUnitario = i.PrecoUnitario,
                    ProdutoId = i.ProdutoId,
                    Quantidade = i.Quantidade
                }).ToList()
            };
        }

        public async Task<PageResultDto<PedidoResponseDto>> Listar(PedidofiltroDto filtro, int userId)
        {
            filtro.Normalizar();
            var query = _context.Pedidos
                .Include(e => e.Itens)
                .Where(p => p.UsuarioId == userId);

            if (!string.IsNullOrWhiteSpace(filtro.Status))
                query = query.Where(e => e.Status == filtro.Status);

            return await query.ToPagedResultAsync(
                filtro.Page,
                filtro.PageSize,
                pedido => new PedidoResponseDto
                {
                    Id = pedido.Id,
                    CriadoEm = pedido.CriadoEm,
                    Status = pedido.Status,
                    UsuarioId = pedido.UsuarioId,
                    SubTotal = pedido.Total,

                    UsuarioNome = pedido.Usuario.Nome,
                    Itens = pedido.Itens.Select(i => new PedidoItemResponseDto
                    {
                        NomeProduto = i.NomeProduto,
                        PrecoUnitario = i.PrecoUnitario,
                        ProdutoId = i.ProdutoId,
                        Quantidade = i.Quantidade
                    }).ToList()
                });
        }

        public async Task<PedidoResponseDto> BuscarId(int pedidoId, int userId)
        {
            var pedido = await _context.Pedidos
                .Include(i => i.Itens)
                .Include(u => u.Usuario)
                .FirstOrDefaultAsync(p => p.Id == pedidoId && p.UsuarioId == userId);

            if (pedido == null)
                throw new ArgumentException("Pedido não encontrado");

            return new PedidoResponseDto
            {
                Id = pedido.Id,
                CriadoEm = pedido.CriadoEm,
                Status = pedido.Status,
                UsuarioId = pedido.UsuarioId,
                UsuarioNome = pedido.Usuario.Nome,
                SubTotal = pedido.Total,
                Itens = pedido.Itens.Select(i => new PedidoItemResponseDto
                {
                    NomeProduto = i.NomeProduto,
                    PrecoUnitario = i.PrecoUnitario,
                    ProdutoId = i.ProdutoId,
                    Quantidade = i.Quantidade
                }).ToList()
            };
        }

        public async Task CancelarPedido(int pedidoId, int userId)
        {
            var pedido = await _context.Pedidos
                .Include(i => i.Itens)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(p => p.Id == pedidoId && p.UsuarioId == userId);

            if (pedido == null)
                throw new ArgumentException("Pedido não encontrado");

            if (pedido.Status == "Entregue" || pedido.Status == "Enviado")
                throw new ArgumentException("Pedido não pode ser cancelado");

            if (pedido.Status == "Cancelado")
                throw new ArgumentException("Pedido cancelado");

            foreach (var item in pedido.Itens)
            {
                var produto = await _context.Produtos.FindAsync(item.ProdutoId);
                if (produto != null)
                    produto.Estoque += item.Quantidade;
            }

            pedido.Status = "Cancelado";

            await _context.SaveChangesAsync();
        }

        public async Task AtualizarStatus(int pedidoId, AtualizarStatusPedidoDto dto)
        {
            var pedido = await _context.Pedidos
               .Include(i => i.Itens)
               .Include(e => e.Usuario)
               .FirstOrDefaultAsync(p => p.Id == pedidoId);

            if (pedido == null)
                throw new ArgumentException("Pedido não encontrado");

            if (pedido.Status == "Cancelado")
                throw new ArgumentException("Pedido Cancelado não pode ser alterado");

            if (pedido.Status == "Entregue")
                throw new ArgumentException("Pedido já finalizado");

            if (!TransicaoValida(pedido.Status, dto.NovoStatus))
                throw new InvalidOperationException($"Transição inválida: {pedido.Status} → {dto.NovoStatus}");

            pedido.Status = dto.NovoStatus;

            await _context.SaveChangesAsync();
        }

        private bool TransicaoValida(string atual, string novo)
        {
            return (atual, novo) switch
            {
                ("Pendente", "Pago") => true,
                ("Pendente", "Cancelado") => true,

                ("Pago", "Enviado") => true,
                ("Pago", "Cancelado") => true,

                ("Enviado", "Entregue") => true,

                _ => false
            };
        }
    }
}
