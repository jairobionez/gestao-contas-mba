using GestaoContas.Business.Models;

public class Orcamento : Entity
{
    public Orcamento() { }

    public Orcamento(decimal limite, Guid? categoriaId, Guid usuarioId)
        : base()
    {
        Limite = limite;
        CategoriaId = categoriaId;
        UsuarioId = usuarioId;
    }

    public Orcamento(Guid id, decimal limite, Guid? categoriaId, Guid usuarioId)
        : this(limite, categoriaId, usuarioId) // Fixing constructor call
    {
        Id = id;
    }
    public decimal Limite { get; private set; }
    public Guid? CategoriaId { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Categoria? Categoria { get; private set; }
    public Usuario? Usuario { get; private set; }
}
