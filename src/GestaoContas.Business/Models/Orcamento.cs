namespace GestaoContas.Business.Models
{
    public  class Orcamento : Entity
    {
        public Orcamento() { }

        public Orcamento(int mes, int ano, decimal limite, Guid? categoriaId, Guid usuarioId)
            :base()
        {
            Mes = mes;
            Ano = ano;
            Limite = limite;
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;
        }
        public Orcamento(Guid id, int mes, int ano, decimal limite, Guid? categoriaId, Guid usuarioId)
           :this(mes,ano,limite,categoriaId,usuarioId)
        {
            Id = id;
        }

        public int Mes { get; private set; }
        public int Ano { get; private set; }
        public decimal Limite { get; private set; }
        public Guid? CategoriaId { get; private set; }
        public Guid UsuarioId {get; private set; }
        public Categoria? Categoria { get; private set; }
        public Usuario? Usuario { get; private set; }
    }
}
