namespace Blazor.Application.DTOs
{
    public class ApiResponseSemPage<T> where T : class, new()
    {
        public bool Sucesso { get; set; }
        public T Dados { get; set; } = new T();
        public List<string> Erros { get; set; } = new List<string>();
    }
}
