namespace GestaoContas.Api.Models
{
    public class TransacaoModel : BaseModel
    {
        public decimal Valor { get; set; }
        public string? Descricao { get; set; }
        public CategoriaModel? Categoria { get; set; }
        public TipoTranscao Tipo { get; set; }
        public DateTime? Data { get; set; } = DateTime.Now;
    }

    public enum TipoTranscao {
        Entrada,
        Saida           
    }

}
