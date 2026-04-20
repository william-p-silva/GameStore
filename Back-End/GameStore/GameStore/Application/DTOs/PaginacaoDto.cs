namespace GameStore.Application.DTOs
{
    public class PaginacaoDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public void Normalizar()
        {
            if(Page <= 0) Page = 1;
            if (PageSize <= 0) PageSize = 10;
            if (PageSize > 50) PageSize = 50;
        }
    }
}
