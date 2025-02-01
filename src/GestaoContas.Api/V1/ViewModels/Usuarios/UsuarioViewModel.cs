using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.V1.ViewModels.Usuarios
{
    public class UsuarioViewModel
    {
        [Key]
        [Required(ErrorMessage = "O campo {0} é necessário")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O campo {0} é necessário")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres")]
        public string? Nome { get; set; }
    }
}
