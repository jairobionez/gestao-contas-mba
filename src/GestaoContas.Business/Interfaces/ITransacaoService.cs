using GestaoContas.Business.Models;
using GestaoContas.Business.Models.DTOs;

namespace GestaoContas.Business.Interfaces
{
    public interface ITransacaoService
    {
        Task<bool> Adicionar(Transacao transacao);
        Task<bool> Atualizar(Transacao transacao);
        Task<bool> Remover(Guid id);
        Task<DashboardDTO> GetDadosDash(Guid usuarioId, DateTime dataInicial, DateTime dataFim);
    }
}
