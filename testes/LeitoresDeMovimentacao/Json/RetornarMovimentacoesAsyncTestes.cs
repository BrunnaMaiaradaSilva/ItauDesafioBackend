using Itau.DesafioBackend.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Json.TestesUnitarios
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
            const string dadosJson = @"[
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

            JsonMovimentacaoLeitor jsonMovimentacaoLeitor = new JsonMovimentacaoLeitor(dadosJson);

            IReadOnlyList<Movimentacao> movimentacoes = await jsonMovimentacaoLeitor.RetornarMovimentacoesAsync().ConfigureAwait(false);

            Assert.Equal(2, movimentacoes.Count);

            AssertMovimentacao(movimentacoes[0], 11, 7, "Auto Posto Shell", -50m, "Transporte");
            AssertMovimentacao(movimentacoes[1], 24, 6, "Ofner", -23.80m, "Transporte");
        }
    }
}
