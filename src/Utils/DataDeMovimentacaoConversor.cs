using System;
using System.Globalization;
using System.Linq;

namespace Itau.DesafioBackend.Utils
{
    public class DataDeMovimentacaoConversor : IDataDeMovimentacaoConversor
    {
        private static readonly CultureInfo[] _cultureInfoSuportadas = new CultureInfo[]
        {
            new CultureInfo("pt-BR"),
            new CultureInfo("en-US")
        };

        private static readonly char[] _separadores = new char[]
        {
            '/',
            '-'
        };

        public DateTime Converter(string conteudo)
        {
            string[] split = conteudo.Split(_separadores);
            string diaStr = split[0].Trim();
            string mesStr = split[1].Trim();

            int dia = int.Parse(diaStr);

            if (!TentarRetornarNumeroDoMesPeloNomeAbreviado(mesStr, out int mes))
            {
                //TODO: Tratar se o mês for inválido (não foi encontrado)
            }

            return new DateTime(1, mes, dia);
        }

        private bool TentarRetornarNumeroDoMesPeloNomeAbreviado(string nomeAbreviado, out int numeroDoMes)
        {
            numeroDoMes = 0;

            foreach (CultureInfo cultureInfo in _cultureInfoSuportadas)
            {
                var dadosDoMes = cultureInfo.DateTimeFormat.AbbreviatedMonthNames
                    .Select((mes, index) => new { mes, numero = index + 1 })
                    .FirstOrDefault(x => x.mes.Equals(nomeAbreviado, StringComparison.OrdinalIgnoreCase));

                if (dadosDoMes != null)
                {
                    numeroDoMes = dadosDoMes.numero;
                    return true;
                }
            }

            return false;
        }
    }
}
