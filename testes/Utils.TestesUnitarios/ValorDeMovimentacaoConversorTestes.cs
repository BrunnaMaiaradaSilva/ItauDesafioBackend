using Itau.DesafioBackend.Core;
using Xunit;

namespace Itau.DesafioBackend.Utils.TestesUnitarios
{
    public class ValorDeMovimentacaoConversorTestes
    {
        [Theory]
        [InlineData("-50,00", -50)]
        [InlineData("-45,10", -45.10)]
        [InlineData("- 28,08", -28.08)]
        public void Converter_Deve_RetornarUmValorDeMovimentacaoCorreto(string conteudo, decimal valorEsperado)
        {
            ValorDeMovimentacaoConversor valorDeMovimentacaoConversor = new ValorDeMovimentacaoConversor();

            ValorDeMovimentacao valorDeMovimentacao = valorDeMovimentacaoConversor.Converter(conteudo);

            Assert.Equal(valorEsperado, valorDeMovimentacao);
        }
    }
}
