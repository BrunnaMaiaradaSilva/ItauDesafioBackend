using System;

namespace Itau.DesafioBackend.Core
{
    /// <summary>
    /// Representa o valor de uma movimentação bancária
    /// </summary>
    public sealed class ValorDeMovimentacao
    {
        private const decimal ValorMinimo = -1_000_000;
        private const decimal ValorMaximo = 1_000_000;

        /// <summary>
        /// Valor original, que foi passado no construtor da classe
        /// </summary>
        public decimal Valor { get; }

        public ValorDeMovimentacao(decimal valor)
        {
            if (valor < ValorMinimo)
                throw new ArgumentException($"Valor não pode ser menor que {ValorMinimo}", nameof(valor));

            if (valor > ValorMaximo)
                throw new ArgumentException($"Valor não pode ser maior que {ValorMaximo}", nameof(valor));

            //Não faz sentido uma movimentação ter valor 0
            if (valor == 0)
                throw new ArgumentException($"Valor não pode ser igual a zero", nameof(valor));

            Valor = valor;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ValorDeMovimentacao);
        }

        public bool Equals(ValorDeMovimentacao valorDeMovimentacao)
        {
            if (valorDeMovimentacao == null)
            {
                return false;
            }

            if (ReferenceEquals(this, valorDeMovimentacao))
            {
                return true;
            }

            return valorDeMovimentacao.Valor == Valor;
        }

        public static bool operator ==(ValorDeMovimentacao item1, ValorDeMovimentacao item2)
        {
            if (ReferenceEquals(item1, item2))
            {
                return true;
            }

            if ((object)item1 == null)
            {
                return false;
            }

            return item1.Equals(item2);
        }

        public static bool operator !=(ValorDeMovimentacao item1, ValorDeMovimentacao item2)
        {
            return !(item1 == item2);
        }

        public static implicit operator decimal(ValorDeMovimentacao valorDeMovimentacao)
        {
            return valorDeMovimentacao.Valor;
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }

        public override string ToString()
        {
            return Valor.ToString();
        }

        public string ToString(string format)
        {
            return Valor.ToString(format);
        }
    }
}
