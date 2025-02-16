using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestaoContas.Data.Repositories
{
    public class OrcamentoRepository : Repository<Orcamento>, IOrcamentoRepository
    {
        public OrcamentoRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Orcamento>> BuscarOrcamentoCompleto(Expression<Func<Orcamento, bool>> predicate)
        {
            return await _dbSet.AsNoTracking()
                .Include(o => o.Categoria)
                .Include(o => o.Usuario)
                .Where(predicate)
                .ToListAsync();
        }
        public async Task<Orcamento?> ObterOrcamentoCompletoPorId(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Include(o => o.Categoria)
                .Include(o => o.Usuario)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }

}
