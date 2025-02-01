namespace GestaoContas.Shared.Domain
{
    public  class Orcamento : Entity
    {
        public int Mes { get; set; }
        public int Ano { get; set; }
        public decimal Limite { get; set; }
        public Guid? CategoriaId { get; set; }
        public Guid UsuarioId {get; set; }
        public Categoria Categoria { get; set; }
        public Usuario Usuario { get; set; }
    }
}
