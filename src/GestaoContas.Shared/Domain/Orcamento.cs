namespace GestaoContas.Shared.Domain
{
    public  class Orcamento : Entity
    {
        public Guid Id { get; private set; }
        public int Mes { get; private set; }
        public int Ano { get; private set; }
        public decimal Limite { get; private set; }
        public Guid? CategoriaId { get; private set; }
        public Guid UsuarioId {get; private set; }
        public Categoria Categoria { get; private set; }
        public Usuario Usuario { get; private set; }
    }
}
