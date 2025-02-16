using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GestaoContas.Data.Repositories
{
    public class TransacaoRepsitory : Repository<Transacao>, ITransacaoRepository
    {
        public TransacaoRepsitory(ApplicationDbContext context) : base(context)
        {            
        }

        public async Task<IEnumerable<Transacao>> BuscarCompleto(Expression<Func<Transacao, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Include(t => t.Categoria).Where(predicate).ToListAsync();
        }

        public async Task<Transacao?> ObterCompletoPorId(Guid id)
        {
            return await _dbSet.AsNoTracking().Include(t=>t.Categoria).FirstOrDefaultAsync(t=>t.Id.Equals(id));
        }
    }
}
