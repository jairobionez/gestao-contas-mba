using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Business.Models.Validations;

namespace GestaoContas.Business.Services
{
    public class TransacaoService : BaseService, ITransacaoService
    {
        private readonly ITransacaoRepository _repository;
        private readonly ICategoriaReposotory _categoriaRepository;
        private readonly IUser _usuario;

        public TransacaoService(
            INotificador notificador,
            ITransacaoRepository transacaoRepository,
            IUser usuario,
            ICategoriaReposotory categoriaRepository) : base(notificador)
        {
            _repository = transacaoRepository;
            _usuario = usuario;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<bool> Adicionar(Transacao transacao)
        {
            if (_usuario == null || !_usuario.IsAuthenticated())
            {
                Notificar("Transação só pode ser adicionada por um usuário autenticado");
                return false;
            }

            if (!ExecutarValidacao(new TransacaoValidation(), transacao)) return false;

            if (_categoriaRepository.Buscar(c=>c.Id == transacao.CategoriaId && (c.UsuarioId == _usuario.GetId() || c.Padrao)).Result.FirstOrDefault() == null)
            {
                Notificar("Categoria inexistente ou não pertence ao usuário logado");
                return false;
            }

            transacao.SetUsuarioId(_usuario.GetId());

            await _repository.Adicionar(transacao);

            return true;
        }

        public async Task<bool> Atualizar(Transacao transacao)
        {
            if (_usuario == null || !_usuario.IsAuthenticated())
            {
                Notificar("Transação só pode ser atualizada por um usuário autenticado");
                return false;
            }

            if (!ExecutarValidacao(new TransacaoValidation(), transacao)) return false;

            if (_categoriaRepository.Buscar(c => c.Id == transacao.CategoriaId && (c.UsuarioId == _usuario.GetId() || c.Padrao)).Result.FirstOrDefault() == null)
            {
                Notificar("Categoria inexistente ou não pertence ao usuário logado");
                return false;
            }

            if(_repository.Buscar(t=>t.Id == transacao.Id && t.UsuarioId == _usuario.GetId()).Result.FirstOrDefault() == null)
            {
                Notificar("Transacao inexistente inexistente ou não pertence ao usuário logado");
                return false;
            }

            transacao.SetUsuarioId(_usuario.GetId());

            await _repository.Atualizar(transacao);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_usuario == null || !_usuario.IsAuthenticated())
            {
                Notificar("Transação só pode ser removida por um usuário autenticado");
                return false;
            }

            if (_repository.Buscar(t => t.Id == id && t.UsuarioId == _usuario.GetId()).Result.FirstOrDefault() == null)
            {
                Notificar("Transacao inexistente inexistente ou não pertence ao usuário logado");
                return false;
            }

            await _repository.Remover(id);
            return true;
        }
    }
}
