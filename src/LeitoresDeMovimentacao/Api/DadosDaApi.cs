namespace Itau.DesafioBackend.LeitoresDeMovimentacao.Api
{
    /// <summary>
    /// Representa as configurações da api
    /// </summary>
    public sealed class DadosDaApi
    {
        /// <summary>
        /// Url base da api
        /// </summary>
        public string UrlBase { get; set; }

        /// <summary>
        /// Endpoint de pagamentos
        /// </summary>
        public string EndpointPagamentos { get; set; }

        /// <summary>
        /// Endpoint de recebimentos
        /// </summary>
        public string EndpointRecebimentos { get; set; }
    }
}
