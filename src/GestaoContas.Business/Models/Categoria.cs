namespace GestaoContas.Business.Models
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Padrao { get; set; }
    }
}
