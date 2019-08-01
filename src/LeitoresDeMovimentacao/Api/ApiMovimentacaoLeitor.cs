using Itau.DesafioBackend.Core;
using Itau.DesafioBackend.LeitoresDeMovimentacao.Abstracoes;
using Itau.DesafioBackend.LeitoresDeMovimentacao.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Api
{
    /// <summary>
    /// Retorna as movimentações bancárias de uma api. A api precisa retornar um array em JSON.
    /// </summary>
    public class ApiMovimentacaoLeitor : ILeitorDeMovimentacao
    {
        private readonly DadosDaApi _dadosDaApi;
        private readonly IApiRequisitor _apiRequisitor;

        public ApiMovimentacaoLeitor(DadosDaApi dadosDaApi)
            : this(dadosDaApi, new HttpClientApiRequisitor())
        {
        }

        public ApiMovimentacaoLeitor(
            DadosDaApi dadosDaApi,
            IApiRequisitor apiRequisitor)
        {
            _dadosDaApi = dadosDaApi;
            _apiRequisitor = apiRequisitor;
        }

        public async Task<IReadOnlyList<Movimentacao>> RetornarMovimentacoesAsync()
        {
            Task<string> pagamentosResponseTask = RetornarConteudoDaApi(_dadosDaApi.EndpointPagamentos);
            Task<string> recebimentosResponseTask = RetornarConteudoDaApi(_dadosDaApi.EndpointRecebimentos);

            await Task.WhenAll(pagamentosResponseTask, recebimentosResponseTask).ConfigureAwait(false);

            Task<IReadOnlyList<Movimentacao>> movimentacoesPagamentoTask = RetornarMovimentacoesPorJsonAsync(pagamentosResponseTask.Result);
            Task<IReadOnlyList<Movimentacao>> movimentacoesRecebimentosTask = RetornarMovimentacoesPorJsonAsync(recebimentosResponseTask.Result);

            await Task.WhenAll(movimentacoesPagamentoTask, movimentacoesRecebimentosTask).ConfigureAwait(false);

            List<Movimentacao> movimentacoes = new List<Movimentacao>();
            movimentacoes.AddRange(movimentacoesPagamentoTask.Result);
            movimentacoes.AddRange(movimentacoesRecebimentosTask.Result);

            return movimentacoes;
        }

        private Task<string> RetornarConteudoDaApi(string endpoint)
        {
            return _apiRequisitor.FazerRequisicaoAsync(_dadosDaApi.UrlBase, endpoint);
        }

        private Task<IReadOnlyList<Movimentacao>> RetornarMovimentacoesPorJsonAsync(string json)
        {
            return new JsonMovimentacaoLeitor(json).RetornarMovimentacoesAsync();
        }
    }
}
