using GestaoContas.Business.Models;

namespace GestaoContas.Api.V2.ViewModels.Transacoes
{
    public class TransacaoFiltroViewModel
    {
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public decimal? ValorInicial { get; set; }
        public decimal? ValorFinal { get; set; }
        public Guid? CategoriaId { get; set; }
        public TipoTransacao? Tipo { get; set; }
    }
}
