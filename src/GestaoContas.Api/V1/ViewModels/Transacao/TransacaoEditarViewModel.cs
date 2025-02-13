using GestaoContas.Shared.Domain;
using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.V1.ViewModels.Transacao
{
    public class TransacaoEditarViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TipoTransacao TipoTransacao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? Data { get; set; }
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CategoriaId { get; set; }
    }
}
