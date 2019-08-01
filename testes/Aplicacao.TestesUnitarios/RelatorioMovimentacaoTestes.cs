using Itau.DesafioBackend.Core;
using System;
using System.Collections.Generic;
using Xunit;

namespace Itau.DesafioBackend.Aplicacao.TestesUnitarios
{
    public class RelatorioMovimentacaoTestes
    {
        [Fact]
        public void Criar_Deve_RetornarNull_Quando_NaoHouveremMovimentacoes()
        {
            List<Movimentacao> movimentacoes = new List<Movimentacao>();

            RelatorioMovimentacao relatorioMovimentacao = RelatorioMovimentacao.Criar(movimentacoes);

            Assert.Null(relatorioMovimentacao);
        }

        [Fact]
        public void Criar_Deve_RetornarUmRelatorioCorreto()
        {
            List<Movimentacao> movimentacoes = new List<Movimentacao>
            {
                new Movimentacao(
                    data: new DateTime(1,1,1),
                    descricao: new DescricaoDeMovimentacao("descricao"),
                    valor: new ValorDeMovimentacao(-3),
                    categoria: new CategoriaDeMovimentacao("Categoria")
                ),

                new Movimentacao(
                    data: new DateTime(1,2,3),
                    descricao: new DescricaoDeMovimentacao("descricao"),
                    valor: new ValorDeMovimentacao(7),
                    categoria: new CategoriaDeMovimentacao("cátegória")
                )
            };

            RelatorioMovimentacao relatorioMovimentacao = RelatorioMovimentacao.Criar(movimentacoes);

            Assert.Equal(2, relatorioMovimentacao.Movimentacoes.Count);
            Assert.Equal("Categoria", relatorioMovimentacao.CategoriaComMaisGastos);
            Assert.Equal(1, relatorioMovimentacao.MesComMaisGastos);
            Assert.Equal(3, relatorioMovimentacao.QuantoGastou);
            Assert.Equal(7, relatorioMovimentacao.QuantoRecebeu);
            Assert.Equal(10, relatorioMovimentacao.MovimentacaoTotal);
        }
    }
}
