using Itau.DesafioBackend.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Log.TestesUnitarios
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
            const string dadosLog = @"Data              Descricao                   Valor               Categoria
21-Mar		  INGRESSO.COM                -159,53             diversao
17-Feb            TAM SITE                    -430,49             viagem";

            LogMovimentacaoLeitor logMovimentacaoLeitor = new LogMovimentacaoLeitor(dadosLog);

            IReadOnlyList<Movimentacao> movimentacoes = await logMovimentacaoLeitor.RetornarMovimentacoesAsync().ConfigureAwait(false);

            Assert.Equal(2, movimentacoes.Count);

            AssertMovimentacao(movimentacoes[0], 21, 3, "INGRESSO.COM", -159.53m, "Diversao");
            AssertMovimentacao(movimentacoes[1], 17, 2, "TAM SITE", -430.49m, "Viagem");
        }
    }
}
