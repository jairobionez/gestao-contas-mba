namespace GestaoContas.Business.Models
{
    public class Transacao : Entity
    {
       public Transacao() { }
        public Transacao(TipoTransacao tipoTransacao, decimal valor, DateTime? data, string? descricao, Guid categoriaId, Guid usuarioId)
            : base()
        {
            TipoTransacao = tipoTransacao;
            Valor = valor;
            Data = data;
            Descricao = descricao;
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;        
        }

        public Transacao(Guid id, TipoTransacao tipoTransacao, decimal valor, DateTime? data, string? descricao, Guid categoriaId, Guid usuarioId)
            :this(tipoTransacao,valor,data,descricao,categoriaId,usuarioId)
        {
            Id = id;
        }

        public TipoTransacao? TipoTransacao { get; private set; }
        public decimal? Valor { get; private set; }
        public DateTime? Data { get; private set; } 
        public string? Descricao { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid UsuarioId { get; private set; }
        public Usuario? Usuario { get; private set; }
        public Categoria? Categoria { get; private set; }

        public void SetUsuarioId(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}
