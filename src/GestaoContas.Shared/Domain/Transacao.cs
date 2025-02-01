namespace GestaoContas.Shared.Domain
{
    public class Transacao : Entity
    {
        public TipoTransacao TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime? Data { get; set; } 
        public string Descricao { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public Categoria Categoria { get; set; }
    }
}
