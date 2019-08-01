using System;
using Xunit;

namespace Itau.DesafioBackend.Utils.TestesUnitarios
{
    public class DataDeMovimentacaoConversorTestes
    {
        [Theory]
        [InlineData("17-Fev", 17, 2)]
        [InlineData("11/jul", 11, 7)]
        [InlineData("24-jun", 24, 6)]
        [InlineData("30 / jul", 30, 7)]
        [InlineData("21-Mar", 21, 3)]
        [InlineData("29-May", 29, 5)]
        [InlineData("26-Apr", 26, 4)]
        public void Converter_Deve_RetornarUmaDataDeMovimentacaoCorreta(string conteudo, int diaEsperado, int mesEsperado)
        {
            DataDeMovimentacaoConversor dataDeMovimentacaoConversor = new DataDeMovimentacaoConversor();

            DateTime dataDeMovimentacao = dataDeMovimentacaoConversor.Converter(conteudo);

            Assert.Equal(diaEsperado, dataDeMovimentacao.Day);
            Assert.Equal(mesEsperado, dataDeMovimentacao.Month);
        }
    }
}
