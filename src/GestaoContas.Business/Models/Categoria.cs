using System.ComponentModel.DataAnnotations;

namespace GestaoContas.Business.Models
{
    public class Categoria : Entity
    {
        public Categoria()
        {
            Id = Guid.NewGuid();
            Padrao = false;
        }
        public Categoria(string? nome, string? descricao, bool padrao, bool ativo)
            : base()
        {
            Nome = nome;
            Descricao = descricao;
            Padrao = padrao;
            Ativo = ativo;
        }

        public Categoria(Guid id, string? nome, string? descricao, bool padrao, bool ativo)
            :this(nome,descricao,padrao,ativo)
        {
            Id = id;
        }



        public string? Nome { get; private set; }
        public string? Descricao { get; private set;}
        public bool Padrao { get; private set;}
        public bool Ativo { get; private set;}
        public Guid? UsuarioId { get; private set; }

        public IEnumerable<Transacao>? Transacoes { get; private set; }
        public IEnumerable<Orcamento>? Orcamentos { get; private set; }

        public void Atualizar(Categoria categoria)
        {
            this.Nome = categoria.Nome;
            this.Descricao = categoria.Descricao;
        }

        public void SetUsuarioId(Guid usuarioId)
        {
            this.UsuarioId = usuarioId;
        }
    }
}
