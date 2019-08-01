using Itau.DesafioBackend.Core;
using Itau.DesafioBackend.LeitoresDeMovimentacao.Abstracoes;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Itau.DesafioBackend.Aplicacao.TestesUnitarios
{
    public class RelatorioMovimentacaoServicoTestes
    {
        [Fact]
        public async Task RetornarRelatorioAsync_Deve_ChamarRetornarRelatorioAsyncDeCadaLeitor()
        {
            Mock<ILeitorDeMovimentacao> mockLeitorDeMovimentacao1 = new Mock<ILeitorDeMovimentacao>();
            mockLeitorDeMovimentacao1
                .Setup(x => x.RetornarMovimentacoesAsync())
                .ReturnsAsync(new List<Movimentacao>());

            Mock<ILeitorDeMovimentacao> mockLeitorDeMovimentacao2 = new Mock<ILeitorDeMovimentacao>();
            mockLeitorDeMovimentacao2
                .Setup(x => x.RetornarMovimentacoesAsync())
                .ReturnsAsync(new List<Movimentacao>());

            RelatorioMovimentacaoServico relatorioServico = new RelatorioMovimentacaoServico(new ILeitorDeMovimentacao[] {
                mockLeitorDeMovimentacao1.Object,
                mockLeitorDeMovimentacao2.Object
            });

            RelatorioMovimentacao relatorio = await relatorioServico.RetornarRelatorioAsync().ConfigureAwait(false);

            mockLeitorDeMovimentacao1.Verify(x => x.RetornarMovimentacoesAsync(), Times.Once());
            mockLeitorDeMovimentacao2.Verify(x => x.RetornarMovimentacoesAsync(), Times.Once());
        }
    }
}
