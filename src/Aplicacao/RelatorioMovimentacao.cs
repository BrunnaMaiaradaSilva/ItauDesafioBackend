using Itau.DesafioBackend.Core;
using Itau.DesafioBackend.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Itau.DesafioBackend.Aplicacao
{
    public sealed class RelatorioMovimentacao
    {
        /// <summary>
        /// A lista de movimentações ordenadas por data, decrescente
        /// </summary>
        public IReadOnlyList<Movimentacao> Movimentacoes { get; private set; }

        /// <summary>
        /// Um dicionário que separa os gastos por categoria. A chave é o nome da categoria
        /// </summary>
        public IReadOnlyDictionary<string, decimal> GastosPorCategoria { get; private set; }

        /// <summary>
        /// O nome da categoria com mais gastos
        /// </summary>
        public string CategoriaComMaisGastos { get; private set; }

        /// <summary>
        /// O número do mês com mais gastos
        /// </summary>
        public int MesComMaisGastos { get; private set; }

        /// <summary>
        /// Quanto o usuário gastou
        /// </summary>
        public decimal QuantoGastou { get; private set; }

        /// <summary>
        /// Quanto o usuário recebeu
        /// </summary>
        public decimal QuantoRecebeu { get; private set; }

        /// <summary>
        /// Quanto de dinheiro foi movimentado
        /// </summary>
        public decimal MovimentacaoTotal { get; private set; }

        private RelatorioMovimentacao()
        {
        }

        /// <summary>
        /// Cria um novo relatório
        /// </summary>
        /// <param name="movimentacoes">A lista com as movimentações bancárias</param>
        /// <returns></returns>
        public static RelatorioMovimentacao Criar(IReadOnlyList<Movimentacao> movimentacoes)
        {
            if (movimentacoes.Count == 0)
            {
                return null;
            }

            List<Movimentacao> movimentacoesOrdenadasPorData = movimentacoes.OrderByDescending(m => m.Data).ToList();
            Dictionary<string, decimal> gastosPorCategoria = new Dictionary<string, decimal>();
            Dictionary<int, decimal> gastosPorMes = new Dictionary<int, decimal>();
            decimal quantoGastou = 0;
            decimal quantoRecebeu = 0;
            decimal movimentacaoTotal = 0;

            foreach (Movimentacao movimentacao in movimentacoesOrdenadasPorData)
            {
                AtualizarGastosPorCategoria(gastosPorCategoria, movimentacao);
                AtualizarGastosPorMes(gastosPorMes, movimentacao);

                decimal valorAbsoluto = Math.Abs(movimentacao.Valor);

                if (movimentacao.Valor < 0)
                    quantoGastou += valorAbsoluto;
                else
                    quantoRecebeu += valorAbsoluto;

                movimentacaoTotal += valorAbsoluto;
            }

            return new RelatorioMovimentacao
            {
                Movimentacoes = movimentacoesOrdenadasPorData,
                GastosPorCategoria = gastosPorCategoria,
                CategoriaComMaisGastos = RetornarCategoriaComMaisGastos(gastosPorCategoria),
                MesComMaisGastos = RetornarMesComMaisGastos(gastosPorMes),
                QuantoGastou = quantoGastou,
                QuantoRecebeu = quantoRecebeu,
                MovimentacaoTotal = movimentacaoTotal
            };
        }

        private static void AtualizarGastosPorCategoria(Dictionary<string, decimal> gastosPorCategoria, Movimentacao movimentacao)
        {
            if (movimentacao.Valor > 0)
            {
                return;
            }

            if (!gastosPorCategoria.TryGetValue(movimentacao.Categoria, out decimal valor))
            {
                gastosPorCategoria.Add(movimentacao.Categoria, valor);
            }

            valor += Math.Abs(movimentacao.Valor);
            gastosPorCategoria[movimentacao.Categoria] = valor;
        }

        private static void AtualizarGastosPorMes(Dictionary<int, decimal> gastosPorMes, Movimentacao movimentacao)
        {
            if (movimentacao.Valor > 0)
            {
                return;
            }

            if (!gastosPorMes.TryGetValue(movimentacao.Data.Month, out decimal valor))
            {
                gastosPorMes.Add(movimentacao.Data.Month, valor);
            }

            valor += Math.Abs(movimentacao.Valor);
            gastosPorMes[movimentacao.Data.Month] = valor;
        }

        private static string RetornarCategoriaComMaisGastos(Dictionary<string, decimal> gastosPorCategoria)
        {
            return gastosPorCategoria.OrderByDescending(x => x.Value).First().Key;
        }

        private static int RetornarMesComMaisGastos(Dictionary<int, decimal> gastosPorMes)
        {
            return gastosPorMes.OrderByDescending(x => x.Value).First().Key;
        }
    }
}
