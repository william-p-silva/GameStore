namespace Blazor.DTOs
{
    public class ApiResponse<T>
    {
        public bool Sucesso { get; set; }
        public PagedData<T> Dados { get; set; } = new PagedData<T>();
        public List<string> Erros { get; set; } = new List<string>();
    }
}
