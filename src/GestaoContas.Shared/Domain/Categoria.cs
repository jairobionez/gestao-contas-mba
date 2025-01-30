namespace GestaoContas.Shared.Domain
{
    public class Categoria
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set;}
        public bool Padrao { get; private set;}
    }
}
