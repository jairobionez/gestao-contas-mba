using GestaoContas.Business.Models;
using System.Linq.Expressions;

namespace GestaoContas.Business.Interfaces
{
    public interface ICategoriaReposotory:IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> BuscarCompleto(Expression<Func<Categoria, bool>> predicate);
    }
}
