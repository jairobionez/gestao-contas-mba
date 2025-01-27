namespace GestaoContas.Api.Models
{
    public class AdicionarTransacaoModel
    {
        public decimal Valor { get; set; }
        public string? Descricao { get; set; }
        public Guid? CategoriaId { get; set; }
        public TipoTranscao Tipo { get; set; }
        public DateTime? Data { get; set; } = DateTime.Now;
    }

}
