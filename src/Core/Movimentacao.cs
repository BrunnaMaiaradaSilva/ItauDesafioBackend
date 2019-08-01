using System;

namespace Itau.DesafioBackend.Core
{
    /// <summary>
    /// Representa os dados de uma movimentação bancária
    /// </summary>
    public sealed class Movimentacao
    {
        /// <summary>
        /// Data da movimentação
        /// </summary>
        public DateTime Data { get; }

        /// <summary>
        /// Descrição da movimentação
        /// </summary>
        public DescricaoDeMovimentacao Descricao { get; }

        /// <summary>
        /// Valor da movimentação
        /// </summary>
        public ValorDeMovimentacao Valor { get; }

        /// <summary>
        /// Categoria da movimentação
        /// </summary>
        public CategoriaDeMovimentacao Categoria { get; }

        public Movimentacao(DateTime data, DescricaoDeMovimentacao descricao, ValorDeMovimentacao valor, CategoriaDeMovimentacao categoria)
        {
            Data = data;
            Descricao = descricao ?? throw new ArgumentNullException(nameof(descricao));
            Valor = valor ?? throw new ArgumentNullException(nameof(valor));
            Categoria = categoria ?? throw new ArgumentNullException(nameof(categoria));
        }
    }
}
