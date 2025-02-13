using GestaoContas.Business.Models;

namespace GestaoContas.Business.Interfaces
{
    public interface ITransacaoService
    {
        Task<bool> Adicionar(Transacao transacao);
        Task<bool> Atualizar(Transacao transacao);
        Task<bool> Remover(Guid id);
    }
}
