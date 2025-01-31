using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Shared.Domain
{
    public class Categoria
    {
        public Categoria()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }        
        public string Nome { get; private set; }
        public string Descricao { get; private set;}
        public bool Padrao { get; private set;}

        public void Atualizar(Categoria categoria)
        {
            this.Nome = categoria.Nome;
            this.Descricao = categoria.Descricao;
        }
    }
}
