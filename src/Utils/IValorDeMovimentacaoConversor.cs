using Itau.DesafioBackend.Core;

namespace Itau.DesafioBackend.Utils
{
    public interface IValorDeMovimentacaoConversor
    {
        ValorDeMovimentacao Converter(string conteudo);
    }
}
