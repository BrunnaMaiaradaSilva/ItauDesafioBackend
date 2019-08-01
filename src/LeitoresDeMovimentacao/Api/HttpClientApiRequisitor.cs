using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Api
{
    /// <summary>
    /// Implementação usando <see cref="HttpClient"/>
    /// </summary>
    public class HttpClientApiRequisitor : IApiRequisitor, IDisposable
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public Task<string> FazerRequisicaoAsync(string urlBase, string endpoint)
        {
            return _httpClient.GetStringAsync(string.Concat(urlBase, "/", endpoint));
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
        }
    }
}
