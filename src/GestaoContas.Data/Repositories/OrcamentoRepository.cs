using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Data.Contexts;

namespace GestaoContas.Data.Repositories
{
    public class OrcamentoRepository : Repository<Orcamento>, IOrcamentoRepository
    {
        protected OrcamentoRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
