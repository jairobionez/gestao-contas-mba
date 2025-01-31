using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.V1.ViewModels.Categorias
{
    public class CategoriaCriarViewModel
    {        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string? Nome { get; set; }
        [StringLength(500, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 1)]
        public string? Descricao { get; set; }           
    }
}
