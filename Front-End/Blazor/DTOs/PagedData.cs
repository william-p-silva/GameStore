namespace Blazor.DTOs
{
    public class PagedData<T>
    {
        public int Total { get; set; }
        public List<T> Data { get; set; } = new List<T>(); //Onde estão seus produtos
    }
}
