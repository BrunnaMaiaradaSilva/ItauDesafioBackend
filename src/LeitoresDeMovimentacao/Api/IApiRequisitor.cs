using System.Threading.Tasks;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Api
{
    /// <summary>
    /// Representa um objeto que faz requisição à uma api
    /// </summary>
    public interface IApiRequisitor
    {
        /// <summary>
        /// Faz uma requisição à api
        /// </summary>
        /// <param name="urlBase">A url base da api</param>
        /// <param name="endpoint">O endpoint</param>
        /// <returns>O conteúdo da api</returns>
        Task<string> FazerRequisicaoAsync(string urlBase, string endpoint);
    }
}
