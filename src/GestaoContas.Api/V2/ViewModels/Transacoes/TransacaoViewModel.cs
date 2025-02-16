using GestaoContas.Api.V2.ViewModels.Categorias;
using GestaoContas.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.V2.ViewModels.Transacoes
{
    public class TransacaoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public DateTime? Data { get;  set; }
        public string? Descricao { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }

        public CategoriaViewModel? Categoria { get; set; }
    }
}
