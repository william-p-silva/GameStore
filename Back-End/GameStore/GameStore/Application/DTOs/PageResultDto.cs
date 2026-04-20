using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameStore.Application.DTOs
{
    public class PageResultDto<T>
    {
        public int Total { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling((double)Total / PageSize);
        public List<T>? Data { get; set; }
        

        public PageResultDto() { }

        public PageResultDto(List<T> data, int total, int page, int pageSize)
        {
            Data = data;
            Total = total;
            Page = page;
            PageSize = pageSize;
        }
    }
}
