using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Data.Contexts;

namespace GestaoContas.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        
        private INotificador _notificador;
        public UsuarioRepository(ApplicationDbContext context, INotificador notificador) : base(context)
        {        
            _notificador = notificador;
        }
    }
}
