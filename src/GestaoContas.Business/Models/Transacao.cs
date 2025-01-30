namespace GestaoContas.Business.Models
{
    public class Transacao:Entity
    {
        public Guid IdCategoria { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public Categoria Categoria { get; set; }
    }
}
