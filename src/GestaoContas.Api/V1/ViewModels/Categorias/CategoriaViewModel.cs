using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.V1.ViewModels.Categorias
{
    public class CategoriaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }        
    }
}
