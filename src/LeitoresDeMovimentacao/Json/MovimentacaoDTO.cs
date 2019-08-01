using System.Runtime.Serialization;

namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Json
{
    /// <summary>
    /// Representa o JSON de uma movimentação
    /// </summary>
    [DataContract]
    internal sealed class MovimentacaoDTO
    {
#pragma warning disable IDE1006 // Naming Styles
        [DataMember]
        public string data { get; set; }

        [DataMember]
        public string descricao { get; set; }

        [DataMember]
        public string valor { get; set; }

        [DataMember]
        public string categoria { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
