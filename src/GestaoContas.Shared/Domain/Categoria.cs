using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Shared.Domain
{
    public class Categoria : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set;}
        public bool Padrao { get; set;}
        public bool Ativo { get; set;}
        public IEnumerable<Transacao> Transacoes { get; set; }
        public IEnumerable<Orcamento> Orcamentos { get; set; }

        public void Atualizar(Categoria categoria)
        {
            this.Nome = categoria.Nome;
            this.Descricao = categoria.Descricao;
        }
    }
}
