using GestaoContas.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.V2.ViewModels.Transacoes
{
    public class TransacaoAtualizarViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public TipoTransacao TipoTransacao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime? Data { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid? CategoriaId { get; set; }
    }
}
