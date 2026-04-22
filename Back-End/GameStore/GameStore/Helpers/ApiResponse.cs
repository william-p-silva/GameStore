namespace GameStore.Helpers
{
    public class ApiResponse<T>
    {
        public bool Sucesso { get; set; }
        public T? dados { get; set; }
        public List<string> Erros { get; set; } = new();

        public static ApiResponse<T> Ok(T dados)
        {
            return new ApiResponse<T>
            {
                Sucesso = true,
                dados = dados
            };
        }

        public static ApiResponse<T> Fail(string erro)
        {
            return new ApiResponse<T>
            {
                Sucesso = false,
                Erros = new List<string> { erro }
            };
        }
    }
}
