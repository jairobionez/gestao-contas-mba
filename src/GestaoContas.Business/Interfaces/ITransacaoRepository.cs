using GestaoContas.Business.Models;
using System.Linq.Expressions;

namespace GestaoContas.Business.Interfaces
{
    public interface ITransacaoRepository : IRepository<Transacao>
    {
        Task<IEnumerable<Transacao>> BuscarCompleto(Expression<Func<Transacao, bool>> predicate);
        Task<Transacao?> ObterCompletoPorId(Guid id);
    }
}
