using GameStore.Application.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace GameStore.Helpers
{
    public static class QueryableExtensions
    {
        public static async Task<PageResultDto<TDestino>> ToPagedResultAsync<TOrigem, TDestino>(
      this IQueryable<TOrigem> query,
      int page,
      int pageSize,
      Expression<Func<TOrigem, TDestino>> selector
  )
        {
            // Normalização
            if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 10;
            if (pageSize > 50) pageSize = 50;

            // Total
            var total = await query.CountAsync();

            // Paginação
            var data = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(selector)
                .ToListAsync();

            return new PageResultDto<TDestino>(data, total, page, pageSize);
        }
    }
}
