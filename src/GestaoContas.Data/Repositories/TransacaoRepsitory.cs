using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Data.Contexts;

namespace GestaoContas.Data.Repositories
{
    public class TransacaoRepsitory : Repository<Transacao>, ITransacaoRepository
    {
        public TransacaoRepsitory(ApplicationDbContext context) : base(context)
        {
        }
    }
}
