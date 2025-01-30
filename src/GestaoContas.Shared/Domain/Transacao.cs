namespace GestaoContas.Shared.Domain
{
    public class Transacao
    {

        public Guid Id { get; private set; }
        public TipoTransacao TipoTransacao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime? Data { get; private set; } 
        public string Descricao { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }
        public Categoria Categoria { get; private set; }
    }
}
