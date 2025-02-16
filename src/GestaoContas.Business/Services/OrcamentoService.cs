using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoContas.Business.Services
{
    public class OrcamentoService : BaseService, IOrcamentoService
    {
        private readonly IOrcamentoRepository _repository;
        private readonly IUser _user;

        public OrcamentoService(
            IOrcamentoRepository repository,
            INotificador notificador,
            IUser user)
            : base(notificador)
        {
            _repository = repository;
            _user = user;
        }

        public async Task<bool> Adicionar(Orcamento orcamento)
        {
            if (_user == null || !_user.IsAuthenticated())
            {
                Notificar("Orçamento só pode ser adicionado por um usuário autenticado.");
                return false;
            }

            orcamento = new Orcamento(orcamento.Limite, orcamento.CategoriaId, _user.GetId());

            if (!ExecutarValidacao(new OrcamentoValidation(), orcamento)) return false;

            if (await OrcamentoExistente(orcamento.CategoriaId, orcamento.UsuarioId))
            {
                Notificar("Já existe um orçamento cadastrado para esta categoria.");
                return false;
            }

            await _repository.Adicionar(orcamento);
            return true;
        }

        public async Task<bool> Atualizar(Orcamento orcamento)
        {
            if (_user == null || !_user.IsAuthenticated())
            {
                Notificar("Orçamento só pode ser alterado por um usuário autenticado.");
                return false;
            }

            if (!ExecutarValidacao(new OrcamentoValidation(), orcamento)) return false;

            var orcamentoExistente = (await _repository.Buscar(o => o.Id == orcamento.Id)).FirstOrDefault();

            if (orcamentoExistente == null)
            {
                Notificar("Não existe orçamento com o Id informado.");
                return false;
            }

            if (orcamentoExistente.UsuarioId != _user.GetId())
            {
                Notificar("Este usuário não tem permissão para alterar este orçamento.");
                return false;
            }

            orcamento = new Orcamento(orcamento.Id, orcamento.Limite, orcamento.CategoriaId, _user.GetId());

            await _repository.Atualizar(orcamento);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_user == null || !_user.IsAuthenticated())
            {
                Notificar("Orçamento só pode ser removido por um usuário autenticado.");
                return false;
            }

            var orcamentoExistente = (await _repository.Buscar(o => o.Id == id)).FirstOrDefault();

            if (orcamentoExistente == null)
            {
                Notificar("Orçamento não encontrado.");
                return false;
            }

            if (orcamentoExistente.UsuarioId != _user.GetId())
            {
                Notificar("Este usuário não pode excluir este orçamento.");
                return false;
            }

            await _repository.Remover(id);
            return true;
        }

        private async Task<bool> OrcamentoExistente(Guid? categoriaId, Guid usuarioId)
        {
            var orcamento = await _repository.Buscar(o => o.CategoriaId == categoriaId && o.UsuarioId == usuarioId);
            return orcamento.Any();
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
