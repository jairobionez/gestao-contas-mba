using GestaoContas.Business.Models;

namespace GestaoContas.Business.Interfaces
{
    public interface IUsuarioService : IDisposable
    {
        Task<bool> Adicionar(Usuario usuario);
    }
}
