using Itau.DesafioBackend.Core;
using Itau.DesafioBackend.LeitoresDeMovimentacao.Abstracoes;
using Itau.DesafioBackend.Utils;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Log
{
    /// <summary>
    /// Retorna uma lista de movimentações a partir de uma string com formato de log
    /// </summary>
    public class LogMovimentacaoLeitor : ILeitorDeMovimentacao
    {
        private readonly string _conteudo;
        private readonly IDataDeMovimentacaoConversor _dataDeMovimentacaoConversor;
        private readonly IValorDeMovimentacaoConversor _valorDeMovimentacaoConversor;

        /// <summary>
        /// Cria uma instância do <see cref="LogMovimentacaoLeitor"/>
        /// </summary>
        /// <param name="conteudo">A string com as movimentações bancárias em formato de log</param>
        public LogMovimentacaoLeitor(string conteudo)
            : this(conteudo, new DataDeMovimentacaoConversor(), new ValorDeMovimentacaoConversor())
        {
        }

        public LogMovimentacaoLeitor(
            string conteudo,
            IDataDeMovimentacaoConversor dataDeMovimentacaoConversor,
            IValorDeMovimentacaoConversor valorDeMovimentacaoConversor)
        {
            _conteudo = conteudo;
            _dataDeMovimentacaoConversor = dataDeMovimentacaoConversor;
            _valorDeMovimentacaoConversor = valorDeMovimentacaoConversor;
        }

        public Task<IReadOnlyList<Movimentacao>> RetornarMovimentacoesAsync()
        {
            List<Movimentacao> movimentacoes = new List<Movimentacao>();

            string[] linhas = _conteudo.Split('\n');

            //não começamos da primeira linha pois os dados de movimentação só começam, de fato, a partir da segunda linha
            for (int linha = 1; linha < linhas.Length; linha++)
            {
                Movimentacao movimentacao = CriarMovimentacao(linhas[linha]);

                movimentacoes.Add(movimentacao);
            }

            return Task.FromResult<IReadOnlyList<Movimentacao>>(movimentacoes);
        }

        private Movimentacao CriarMovimentacao(string conteudo)
        {
            //as colunas são separadas por 2 ou mais espaços e/ou tabs
            string[] colunas = Regex.Split(conteudo, @"\s{2,}");

            DateTime dataDeMovimentacao = _dataDeMovimentacaoConversor.Converter(colunas[0]);
            DescricaoDeMovimentacao descricaoDeMovimentacao = new DescricaoDeMovimentacao(colunas[1]);
            ValorDeMovimentacao valorDeMovimentacao = new ValorDeMovimentacao(_valorDeMovimentacaoConversor.Converter(colunas[2]));
            CategoriaDeMovimentacao categoriaDeMovimentacao = new CategoriaDeMovimentacao(colunas[3].Trim());

            return new Movimentacao(dataDeMovimentacao, descricaoDeMovimentacao, valorDeMovimentacao, categoriaDeMovimentacao);
        }
    }
}
