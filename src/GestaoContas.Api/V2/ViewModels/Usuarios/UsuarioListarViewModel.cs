using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.V2.ViewModels.Usuarios
{
    public class UsuarioListarViewModel
    {    
        public string? Nome { get; set; }    
        public string? Email { get; set; }
    }
}
