using Itau.DesafioBackend.Core;
using Itau.DesafioBackend.LeitoresDeMovimentacao.Abstracoes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itau.DesafioBackend.Aplicacao
{
    /// <summary>
    /// Gerencia o relatório de movimentação bancária
    /// </summary>
    public class RelatorioMovimentacaoServico
    {
        private readonly IEnumerable<ILeitorDeMovimentacao> _leitoresDeMovimentacao;

        /// <summary>
        /// Cria uma nova instância do <see cref="RelatorioMovimentacaoServico"/>
        /// </summary>
        /// <param name="leitoresDeMovimentacao">Uma lista de leitores de movimentaçao bancária.</param>
        public RelatorioMovimentacaoServico(
            IEnumerable<ILeitorDeMovimentacao> leitoresDeMovimentacao)
        {
            _leitoresDeMovimentacao = leitoresDeMovimentacao;
        }

        /// <summary>
        /// Retorna um relatório de movimentaçao bancária. Usa os leitores que foram passados no construtor para agregar as movimentações.
        /// </summary>
        /// <returns>O relatório de movimentação bancária</returns>
        public async Task<RelatorioMovimentacao> RetornarRelatorioAsync()
        {
            List<Movimentacao> movimentacoes = new List<Movimentacao>();

            foreach (ILeitorDeMovimentacao leitorDeMovimentacao in _leitoresDeMovimentacao)
            {
                movimentacoes.AddRange(await leitorDeMovimentacao.RetornarMovimentacoesAsync().ConfigureAwait(false));
            }

            return RelatorioMovimentacao.Criar(movimentacoes);
        }
    }
}
