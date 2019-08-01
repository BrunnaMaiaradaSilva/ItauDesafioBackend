using Itau.DesafioBackend.Core;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Itau.DesafioBackend.Utils
{
    public class ValorDeMovimentacaoConversor : IValorDeMovimentacaoConversor
    {
        private static readonly CultureInfo _cultureInfo = new CultureInfo("pt-BR");

        public ValorDeMovimentacao Converter(string conteudo)
        {
            return new ValorDeMovimentacao(decimal.Parse(Regex.Replace(conteudo, @"\s+", ""), _cultureInfo));
        }
    }
}
