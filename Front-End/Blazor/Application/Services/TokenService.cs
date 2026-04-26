namespace Blazor.Application.Services
{
    public class TokenService
    {
        private readonly LocalStorageService _storage;

        public TokenService(LocalStorageService storage)
        {
            _storage = storage;
        }

        public async Task SalvarToken(string token)
        {
            await _storage.SetItem("authToken", token);
        }

        public async Task<string> ObterToken()
        {
            return await _storage.GetItem("authToken");
        }

        public async Task Logout()
        {
            await _storage.RemoveItem("authToken");
        }
    }
}
