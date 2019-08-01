using Itau.DesafioBackend.LeitoresDeMovimentacao.Abstracoes;
using Itau.DesafioBackend.LeitoresDeMovimentacao.Api;
using Itau.DesafioBackend.LeitoresDeMovimentacao.Log;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Itau.DesafioBackend.Aplicacao.Console
{
    internal static class Program
    {
        private static async Task Main()
        {
            string conteudoLog = File.ReadAllText("logmovimentacao.txt");

            LogMovimentacaoLeitor logMovimentacaoLeitor = new LogMovimentacaoLeitor(conteudoLog);

            ApiMovimentacaoLeitor apiMovimentacaoLeitor = new ApiMovimentacaoLeitor(new DadosDaApi
            {
                UrlBase = "https://my-json-server.typicode.com/cairano/backend-test",
                EndpointPagamentos = "pagamentos",
                EndpointRecebimentos = "recebimentos"
            });

            RelatorioMovimentacaoServico relatorioMovimentacaoServico = new RelatorioMovimentacaoServico(new ILeitorDeMovimentacao[]
            {
                logMovimentacaoLeitor,
                apiMovimentacaoLeitor
            });

            RelatorioMovimentacao relatorioMovimentacao = await relatorioMovimentacaoServico.RetornarRelatorioAsync().ConfigureAwait(false);

            ExibirGastosPorCategoria(relatorioMovimentacao);
            ExibirCategoriaComMaisGastos(relatorioMovimentacao);
            ExibirMesComMaisGastos(relatorioMovimentacao);
            ExibirValoresDeMovimentacaoDoCliente(relatorioMovimentacao);
            ExibirLogDeMovimentacoes(relatorioMovimentacao);

            System.Console.ReadKey();
        }

        private static void ExibirGastosPorCategoria(RelatorioMovimentacao relatorioMovimentacao)
        {
            ExibirTitulo("Gastos por categoria");

            foreach (KeyValuePair<string, decimal> gastosPorCategoriaPar in relatorioMovimentacao.GastosPorCategoria)
            {
                string categoria = RetornarStringComTamanhoFixo(gastosPorCategoriaPar.Key, 20);
                string gasto = RetornarStringComTamanhoFixo($"R$ {gastosPorCategoriaPar.Value}", 10);

                System.Console.WriteLine("{0}\t{1}", categoria, gasto);
            }
        }

        private static void ExibirCategoriaComMaisGastos(RelatorioMovimentacao relatorioMovimentacao)
        {
            ExibirTitulo("Categoria com mais gastos");

            System.Console.WriteLine(relatorioMovimentacao.CategoriaComMaisGastos);
        }

        private static void ExibirMesComMaisGastos(RelatorioMovimentacao relatorioMovimentacao)
        {
            ExibirTitulo("Mês com mais gastos");

            string nomeDoMes = CultureInfo.CurrentUICulture.DateTimeFormat.GetMonthName(relatorioMovimentacao.MesComMaisGastos);

            System.Console.WriteLine(nomeDoMes);
        }

        private static void ExibirValoresDeMovimentacaoDoCliente(RelatorioMovimentacao relatorioMovimentacao)
        {
            ExibirTitulo("Valores de movimentação do cliente");

            System.Console.WriteLine("Quanto gastou: R$ {0}", relatorioMovimentacao.QuantoGastou);
            System.Console.WriteLine("Quanto recebeu: R$ {0}", relatorioMovimentacao.QuantoRecebeu);
            System.Console.WriteLine("Quanto movimentou: R$ {0}", relatorioMovimentacao.MovimentacaoTotal);
        }

        private static void ExibirLogDeMovimentacoes(RelatorioMovimentacao relatorioMovimentacao)
        {
            ExibirTitulo("Movimentações");

            foreach (var movimentacao in relatorioMovimentacao.Movimentacoes)
            {
                string descricao = RetornarStringComTamanhoFixo(movimentacao.Descricao, 30);
                string valor = RetornarStringComTamanhoFixo($"R$ {movimentacao.Valor.ToString("N")}", 10);

                System.Console.WriteLine("{0:dd/MM} - {1}\t\t{2}\t\t{3}", movimentacao.Data, descricao, valor, movimentacao.Categoria);
            }
        }

        private static void ExibirTitulo(string titulo)
        {
            const int tamanhoDosDivisores = 50;
            const char caractereDosDivisores = '-';

            System.Console.WriteLine(string.Empty.PadLeft(tamanhoDosDivisores, caractereDosDivisores));
            System.Console.WriteLine(titulo);
            System.Console.WriteLine(string.Empty.PadLeft(tamanhoDosDivisores, caractereDosDivisores));
        }

        private static string RetornarStringComTamanhoFixo(string str, int quantidadeDeCaracteres)
        {
            if (str.Length > quantidadeDeCaracteres)
            {
                return str.Substring(0, quantidadeDeCaracteres);
            }

            return string.Concat(str, "".PadLeft(quantidadeDeCaracteres - str.Length));
        }
    }
}
