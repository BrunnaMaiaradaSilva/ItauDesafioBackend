using System.Globalization;
using System.Linq;
using System.Text;

namespace Itau.DesafioBackend.Core
{
    /// <summary>
    /// Representa a categoria (definida pelo usuário) de uma movimentação bancária
    /// </summary>
    public sealed class CategoriaDeMovimentacao
    {
        private readonly string _nome;

        /// <summary>
        /// Cria uma nova categoria de movimentação.
        /// </summary>
        /// <param name="nome">O nome da categoria. Seu valor será capitalizado e terá seus acentos removidos. Por exemplo: "diversão" torna-se "Diversao". Se for uma string sem caracteres, tornará-se "Outros"</param>
        public CategoriaDeMovimentacao(string nome)
        {
            _nome = nome ?? string.Empty;
            _nome = RetornarNomeNormalizado(_nome);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CategoriaDeMovimentacao);
        }

        public bool Equals(CategoriaDeMovimentacao categoriaDeMovimentacao)
        {
            if (categoriaDeMovimentacao == null)
            {
                return false;
            }

            if (ReferenceEquals(this, categoriaDeMovimentacao))
            {
                return true;
            }

            return categoriaDeMovimentacao._nome == _nome;
        }

        public static bool operator ==(CategoriaDeMovimentacao item1, CategoriaDeMovimentacao item2)
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

        public static bool operator !=(CategoriaDeMovimentacao item1, CategoriaDeMovimentacao item2)
        {
            return !(item1 == item2);
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(_nome))
            {
                return 0;
            }

            return _nome.GetHashCode();
        }

        public static implicit operator string(CategoriaDeMovimentacao categoriaDeMovimentacao)
        {
            return categoriaDeMovimentacao.ToString();
        }

        public override string ToString()
        {
            return _nome?.ToString();
        }

        private static string RetornarNomeNormalizado(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                nome = "Outros";
            }

            string semAcentos = RetornarStringSemAcentos(nome);
            return semAcentos.First().ToString().ToUpper() + semAcentos.Substring(1);
        }

        private static string RetornarStringSemAcentos(string str)
        {
            string normalizedString = str.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);

                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
