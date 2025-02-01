using GestaoContas.Api.Models;

namespace GestaoContas.Api.V1.ViewModels.Transacao
{
    public class FiltroTransacaoViewModel
    {
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public decimal? ValorInicial { get; set; }
        public decimal? ValorFinal { get; set; }
        public Guid? CategoriaId { get; set; }
        public GestaoContas.Shared.Domain.TipoTransacao? Tipo { get; set; }
    }
}
