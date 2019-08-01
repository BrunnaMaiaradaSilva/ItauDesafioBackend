using Itau.DesafioBackend.Core;
using Itau.DesafioBackend.LeitoresDeMovimentacao.Abstracoes;
using Itau.DesafioBackend.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Json
{
    /// <summary>
    /// Retorna uma lista de movimentações a partir de uma string de um array em JSON
    /// </summary>
    public class JsonMovimentacaoLeitor : ILeitorDeMovimentacao
    {
        private static readonly Encoding _encoding = Encoding.Default;

        private readonly string _conteudo;
        private readonly IDataDeMovimentacaoConversor _dataDeMovimentacaoConversor;
        private readonly IValorDeMovimentacaoConversor _valorDeMovimentacaoConversor;

        /// <summary>
        /// Cria uma nova instância do <see cref="JsonMovimentacaoLeitor"/>
        /// </summary>
        /// <param name="conteudo">Uma string representando um array de movimentações</param>
        public JsonMovimentacaoLeitor(string conteudo)
            : this(conteudo, new DataDeMovimentacaoConversor(), new ValorDeMovimentacaoConversor())
        {
        }

        public JsonMovimentacaoLeitor(
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
            List<Movimentacao> movimentacoes =
                ConverterJsonParaDTOs()
                .Select(ConverterDTOParaMovimentacao)
                .ToList();

            return Task.FromResult<IReadOnlyList<Movimentacao>>(movimentacoes);
        }

        private MovimentacaoDTO[] ConverterJsonParaDTOs()
        {
            using (MemoryStream memoryStream = new MemoryStream(_encoding.GetBytes(_conteudo)))
            {
                DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(MovimentacaoDTO[]));
                return dataContractJsonSerializer.ReadObject(memoryStream) as MovimentacaoDTO[];
            }
        }

        private Movimentacao ConverterDTOParaMovimentacao(MovimentacaoDTO movimentacaoDTO)
        {
            DateTime data = _dataDeMovimentacaoConversor.Converter(movimentacaoDTO.data);
            DescricaoDeMovimentacao descricao = new DescricaoDeMovimentacao(movimentacaoDTO.descricao);
            ValorDeMovimentacao valor = _valorDeMovimentacaoConversor.Converter(movimentacaoDTO.valor);
            CategoriaDeMovimentacao categoria = new CategoriaDeMovimentacao(movimentacaoDTO.categoria);

            return new Movimentacao(data, descricao, valor, categoria);
        }
    }
}
