namespace GestaoContas.Api.Models
{
    public class FiltroTransacaoModel
    {
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public decimal? ValorInicial { get; set; }
        public decimal? ValorFinal { get; set; }
        public Guid? CategoriaId { get; set; }
        public TipoTranscao? Tipo { get; set; }
    }
}
