using Itau.DesafioBackend.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Abstracoes
{
    /// <summary>
    /// Representa um objeto que retorna uma lista de movimentações bancárias
    /// </summary>
    public interface ILeitorDeMovimentacao
    {
        /// <summary>
        /// Retornas as movimentações bancárias
        /// </summary>
        /// <returns>Uma lista com as movimentações bancárias</returns>
        Task<IReadOnlyList<Movimentacao>> RetornarMovimentacoesAsync();
    }
}
