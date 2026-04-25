using Microsoft.JSInterop;


namespace Blazor.Services
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _js;

        public LocalStorageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SetItem(string key, string value)
        {
            await _js.InvokeVoidAsync("localStorageHelper.set", key, value);
        }

        public async Task<string> GetItem(string key)
        {
            return await _js.InvokeAsync<string>("localStorageHelper.get", key);
        }

        public async Task RemoveItem(string key)
        {
            await _js.InvokeVoidAsync("localStorageHelper.remove", key);
        }
    }
}
