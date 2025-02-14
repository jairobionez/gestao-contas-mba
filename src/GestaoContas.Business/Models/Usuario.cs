using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Business.Models
{
    public class Usuario : Entity
    {

        public Usuario() { }

        public Usuario(Guid id, string? nome, string? email)
            :base(id)
        {
            Nome = nome;
            Email = email;            
        }

        public string? Nome { get; private set; }        
        public string? Email { get; private set; }
        public IEnumerable<Transacao>? Transacoes { get; private set; }
        public IEnumerable<Orcamento>? Orcamentos { get; private set; }
    }
}
