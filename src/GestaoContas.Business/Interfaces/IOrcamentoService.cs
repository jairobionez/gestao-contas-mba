using GestaoContas.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.Business.Interfaces
{
    public interface IOrcamentoService : IDisposable
    {
        Task<bool> Adicionar(Orcamento orcamento);
        Task<bool> Atualizar(Orcamento orcamento);
        Task<bool> Remover(Guid id);
    }
}
