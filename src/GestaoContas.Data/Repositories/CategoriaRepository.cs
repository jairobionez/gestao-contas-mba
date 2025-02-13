using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestaoContas.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaReposotory
    {

        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Categoria>> BuscarCompleto(Expression<Func<Categoria, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Include(c=>c.Transacoes).Where(predicate).ToListAsync();
        }
    }
}
