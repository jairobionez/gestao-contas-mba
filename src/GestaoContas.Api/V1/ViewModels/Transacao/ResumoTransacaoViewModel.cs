namespace GestaoContas.Api.V1.ViewModels.Transacao
{
    public class ResumoTransacaoViewModel
    {
        public decimal? Saldo { get; set; }
        public decimal? Receitas { get; set; }
        public decimal? Despesas { get; set; }
    }
}
