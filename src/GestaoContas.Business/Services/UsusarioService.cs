using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Business.Models.Validations;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace GestaoContas.Business.Services
{
    public class UsusarioService : BaseService, IUsuarioService
    {
        private IUsuarioRepository _repository;
        

        public UsusarioService(IUsuarioRepository repository, INotificador notificador)
            :base(notificador)
        {
            _repository = repository;
        }

        public async Task<bool> Adicionar(Usuario usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return false;

            if(_repository.Buscar(u=>u.Email == usuario.Email).Result.Any())
            {
                Notificar("Já existe usuário com o email informado");
                return false;
            }

            await _repository.Adicionar(usuario);
            return true;
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
