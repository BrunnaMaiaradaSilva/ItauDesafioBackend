using System;

namespace Itau.DesafioBackend.Utils
{
    public interface IDataDeMovimentacaoConversor
    {
        DateTime Converter(string conteudo);
    }
}
