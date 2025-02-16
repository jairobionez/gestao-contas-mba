using GestaoContas.Business.Models;
using System.Linq.Expressions;

namespace GestaoContas.Business.Interfaces
{
    public interface IOrcamentoRepository : IRepository<Orcamento>
    {
        Task<IEnumerable<Orcamento>> BuscarOrcamentoCompleto(Expression<Func<Orcamento, bool>> predicate);
        Task<Orcamento?> ObterOrcamentoCompletoPorId(Guid id);
    }
}
