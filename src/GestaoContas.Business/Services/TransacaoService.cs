using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Business.Models.Validations;

namespace GestaoContas.Business.Services
{
    public class TransacaoService : BaseService, ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly ICategoriaReposotory _categoriaRepository;
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly IUser _usuario;

        public TransacaoService(
            INotificador notificador,
            IUser usuario,
            ITransacaoRepository transacaoRepository,
            ICategoriaReposotory categoriaRepository,
            IOrcamentoRepository orcamentoRepository) : base(notificador)
        {
            _transacaoRepository = transacaoRepository;
            _usuario = usuario;
            _categoriaRepository = categoriaRepository;
            _orcamentoRepository = orcamentoRepository;
        }

        public async Task<bool> Adicionar(Transacao transacao)
        {
            if (_usuario == null || !_usuario.IsAuthenticated())
            {
                Notificar("Transação só pode ser adicionada por um usuário autenticado");
                return false;
            }

            if (!ExecutarValidacao(new TransacaoValidation(), transacao)) return false;

            var categoria = await _categoriaRepository.Buscar(c =>
                c.Id == transacao.CategoriaId &&
                (c.UsuarioId == _usuario.GetId() || c.Padrao));

            if (!categoria.Any())
            {
                Notificar("Categoria inexistente ou não pertence ao usuário logado");
                return false;
            }

            var orcamento = await _orcamentoRepository.Buscar(o =>
                o.CategoriaId == transacao.CategoriaId && o.UsuarioId == _usuario.GetId());

            if (orcamento.Any())
            {
                var totalGasto = await _transacaoRepository.Buscar(t =>
                    t.CategoriaId == transacao.CategoriaId && t.UsuarioId == _usuario.GetId());

                decimal totalValorGasto = totalGasto.Sum(t => t.Valor);

                if (totalValorGasto + transacao.Valor > orcamento.First().Limite)
                {
                    Notificar("Aviso: O orçamento desta categoria foi excedido!");
                }
            }

            transacao.SetUsuarioId(_usuario.GetId());

            await _transacaoRepository.Adicionar(transacao);

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

            if(_transacaoRepository.Buscar(t=>t.Id == transacao.Id && t.UsuarioId == _usuario.GetId()).Result.FirstOrDefault() == null)
            {
                Notificar("Transacao inexistente inexistente ou não pertence ao usuário logado");
                return false;
            }

            transacao.SetUsuarioId(_usuario.GetId());

            await _transacaoRepository.Atualizar(transacao);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_usuario == null || !_usuario.IsAuthenticated())
            {
                Notificar("Transação só pode ser removida por um usuário autenticado");
                return false;
            }

            if (_transacaoRepository.Buscar(t => t.Id == id && t.UsuarioId == _usuario.GetId()).Result.FirstOrDefault() == null)
            {
                Notificar("Transacao inexistente inexistente ou não pertence ao usuário logado");
                return false;
            }

            await _transacaoRepository.Remover(id);
            return true;
        }
    }
}
