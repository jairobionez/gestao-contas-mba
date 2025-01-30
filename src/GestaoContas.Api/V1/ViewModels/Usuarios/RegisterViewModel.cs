using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.V1.ViewModels.Usuarios
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O campo {0} é necessário")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é necessário.")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? Senha { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirmação de senha")]
        //[Compare("Password", ErrorMessage = "Os campos Senha e Confirmação de senha não correspondem.")]
        //public string? ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.")]
        public string? Nome { get; set; }
    }
}
