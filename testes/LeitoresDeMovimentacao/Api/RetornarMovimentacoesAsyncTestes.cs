using Itau.DesafioBackend.Core;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Api.TestesUnitarios
{
    public class RetornarMovimentacoesAsyncTestes
    {
        private void AssertMovimentacao(Movimentacao movimentacao, int dia, int mes, string descricao, decimal valor, string categoria)
        {
            Assert.Equal(dia, movimentacao.Data.Day);
            Assert.Equal(mes, movimentacao.Data.Month);
            Assert.Equal(descricao, movimentacao.Descricao);
            Assert.Equal(valor, movimentacao.Valor);
            Assert.Equal(categoria, movimentacao.Categoria);
        }

        [Fact]
        public async Task Deve_RetornarAsMovimentacoesCorretmente()
        {
            const string dadosJsonPagamentos = @"[
    {
      ""data"": ""11 / jul"",
      ""descricao"": ""Auto Posto Shell"",
      ""moeda"": ""R$"",
      ""valor"": ""-50,00"",
      ""categoria"": ""transporte""
    },
    {
      ""data"": ""24/jun"",
      ""descricao"": ""Ofner"",
      ""moeda:"": ""R$"",
      ""valor"": ""-23,80"",
      ""categoria"": ""transporte""
    }]";

            const string dadosJsonRecebimentos = @"[
    {
    ""data"": ""10 / jul"",
    ""descricao"": ""Marcelo B."",
    ""moeda"": ""R$"",
    ""valor"": ""50,00""
  },
  {
    ""data"": ""04 / jul"",
    ""descricao"": ""Antonio C."",
    ""moeda"": ""R$"",
    ""valor"": ""15,00""
  }]";

            DadosDaApi dadosDaApi = new DadosDaApi
            {
                EndpointPagamentos = "EndpointPagamentos",
                EndpointRecebimentos = "EndpointRecebimentos",
                UrlBase = "UrlBase"
            };

            Mock<IApiRequisitor> mockApiRequisitor = new Mock<IApiRequisitor>();
            mockApiRequisitor.Setup(x => x.FazerRequisicaoAsync(dadosDaApi.UrlBase, dadosDaApi.EndpointPagamentos))
                             .ReturnsAsync(dadosJsonPagamentos);

            mockApiRequisitor.Setup(x => x.FazerRequisicaoAsync(dadosDaApi.UrlBase, dadosDaApi.EndpointRecebimentos))
                             .ReturnsAsync(dadosJsonRecebimentos);

            ApiMovimentacaoLeitor jsonMovimentacaoLeitor = new ApiMovimentacaoLeitor(dadosDaApi, mockApiRequisitor.Object);

            IReadOnlyList<Movimentacao> movimentacoes = await jsonMovimentacaoLeitor.RetornarMovimentacoesAsync().ConfigureAwait(false);

            Assert.Equal(4, movimentacoes.Count);

            AssertMovimentacao(movimentacoes[0], 11, 7, "Auto Posto Shell", -50m, "Transporte");
            AssertMovimentacao(movimentacoes[1], 24, 6, "Ofner", -23.80m, "Transporte");
            AssertMovimentacao(movimentacoes[2], 10, 7, "Marcelo B.", 50, "Outros");
            AssertMovimentacao(movimentacoes[3], 04, 7, "Antonio C.", 15, "Outros");
        }
    }
}
