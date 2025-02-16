using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Api.V2.ViewModels.Categorias
{
    public class CategoriaViewModel
    {
        [Key]
        public Guid Id { get; set; }        
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Padrao { get; set; }
    }
}
