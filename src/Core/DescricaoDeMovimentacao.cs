using System;

namespace Itau.DesafioBackend.Core
{
    /// <summary>
    /// Representa a descrição de uma movimentação bancária
    /// </summary>
    public sealed class DescricaoDeMovimentacao
    {
        private readonly string _descricao;

        public DescricaoDeMovimentacao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new ArgumentException("A descrição não pode ser vazia", nameof(descricao));

            _descricao = descricao;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DescricaoDeMovimentacao);
        }

        public bool Equals(DescricaoDeMovimentacao descricaoDeMovimentacao)
        {
            if (descricaoDeMovimentacao == null)
            {
                return false;
            }

            if (ReferenceEquals(this, descricaoDeMovimentacao))
            {
                return true;
            }

            return descricaoDeMovimentacao._descricao == _descricao;
        }

        public static bool operator ==(DescricaoDeMovimentacao item1, DescricaoDeMovimentacao item2)
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

        public static bool operator !=(DescricaoDeMovimentacao item1, DescricaoDeMovimentacao item2)
        {
            return !(item1 == item2);
        }

        public override int GetHashCode()
        {
            return _descricao.GetHashCode();
        }

        public static implicit operator string(DescricaoDeMovimentacao descricaoDeMovimentacao)
        {
            return descricaoDeMovimentacao.ToString();
        }

        public override string ToString()
        {
            return _descricao?.ToString();
        }
    }
}
