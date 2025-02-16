namespace GestaoContas.Api.V2.ViewModels.Orcamento
{
    public class OrcamentoViewModel
    {
        public Guid Id { get; set; }
        public decimal Limite { get; set; }
        public Guid? CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }
        public string? CategoriaNome { get; set; }
        public string? UsuarioNome { get; set; }
    }
}

