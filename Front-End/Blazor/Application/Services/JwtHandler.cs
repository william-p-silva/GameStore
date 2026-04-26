using System.Net.Http.Headers;
using Blazor.Application.Services;

namespace Blazor.Application.Services
{
    public class JwtHandler : DelegatingHandler
    {
        private readonly TokenService _tokenSvc;

        public JwtHandler(TokenService tokenSvc)
        {
            _tokenSvc = tokenSvc;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Obtém o token do LocalStorage através do seu TokenService
            var token = await _tokenSvc.ObterToken();

            // Se o token existir, adiciona no cabeçalho Authorization
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
