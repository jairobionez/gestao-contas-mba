using GestaoContas.Api.V1.ViewModels.Categorias;
using GestaoContas.Shared.Domain;

namespace GestaoContas.Api.V1.ViewModels.Transacao
{
    public class TransacaoViewModel
    {
        public Guid Id { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime? Data { get; set; }
        public string Descricao { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public CategoriaViewModel Categoria { get; set; }
    }
}
