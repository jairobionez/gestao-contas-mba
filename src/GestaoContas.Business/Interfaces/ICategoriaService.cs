using GestaoContas.Business.Models;

namespace GestaoContas.Business.Interfaces
{
    public interface ICategoriaService : IDisposable
    {
        Task<bool> Adicionar(Categoria categoria);
        Task<bool> Atualizar(Categoria categoria);
        Task<bool> Remover(Guid id);
    }
}
