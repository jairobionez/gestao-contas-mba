using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Business.Models.DTOs;
using GestaoContas.Business.Models.Validations;

namespace GestaoContas.Business.Services
{
    public class CategoriaService : BaseService, ICategoriaService
    {
        private ICategoriaReposotory _repository;
        private IUser _user;

        public CategoriaService (
            ICategoriaReposotory repository, 
            INotificador notificador,
            IUser user)
            :base(notificador)
        {
            _repository = repository;
            _user = user;
        }

        public async Task<bool> Adicionar(Categoria categoria)
        {
            if(_user == null || !_user.IsAuthenticated())
            {
                Notificar("Categoria só pode ser adicionada por um usuário autenticado");
                return false;
            }

            if(categoria.Padrao)
            {
                Notificar("Categoria padrão não pode ser adicionada");
                return false;
            }

            categoria.SetUsuarioId(_user.GetId());

            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return false;

            if (NomeExistente(categoria.Nome!)) return false;

            await _repository.Adicionar(categoria);

            return true;
        }

        public async Task<bool> Atualizar(Categoria categoria)
        {
            if (_user == null || !_user.IsAuthenticated())
            {
                Notificar("Categoria só pode ser alterada por um usuário autenticado");
                return false;
            }

            if (!ExecutarValidacao(new CategoriaValidation(), categoria)) return false;

            var categoriaExistente =  _repository.Buscar(c => c.Id == categoria.Id).Result.FirstOrDefault();

            if(categoriaExistente == null)
            {
                Notificar("Não existe categoria com o Id informado");
                return false;
            }

            if (categoriaExistente.Padrao)
            {
                Notificar("Esta é uma categoria padrão e não pode ser alterada");
                return false;
            }

            if (categoriaExistente.UsuarioId != _user.GetId())
            {
                Notificar("Este usuário não pode alterar esta categoria");
                return false;
            }

            if (NomeExistente(categoria.Nome!, categoria.Id)) return false;

            categoria.SetUsuarioId(_user.GetId());

            await _repository.Atualizar(categoria);

            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            if (_user == null || !_user.IsAuthenticated())
            {
                Notificar("Categoria excluída por um usuário autenticado");
                return false;
            }

            var categoriaExistente = _repository.BuscarCompleto(c => c.Id == id).Result.FirstOrDefault();
            
            if (categoriaExistente == null) return false;
            
            if (categoriaExistente.Padrao)
            {
                Notificar("Esta é uma categoria padrão e não pode ser removida");
                return false;
            }

            if (categoriaExistente.UsuarioId != _user.GetId())
            {
                Notificar("Este usuário não pode excluir esta categoria");
                return false;
            }

            if (categoriaExistente.Transacoes != null)
            {
                Notificar("Esta categoria possui uma ou mais transações associadas");
                return false;
            }

            await _repository.Remover(id);
            return true;
        }


        public void Dispose()
        {
            _repository.Dispose();
        }

        private bool NomeExistente(string nome, Guid? categoriaId = null)
        {
            if (_repository.Buscar(c => c.Nome == nome && c.UsuarioId == _user.GetId() && (!categoriaId.HasValue || !c.Id.Equals(categoriaId.Value) )).Result.Any())
            {
                Notificar($"Categoria {nome} já existente");
                return true;
            }
            return false;
        }

        private bool CategoriaPadrao(Guid id)
        {
            return _repository.Buscar(c => c.Id == id && c.Padrao).Result.Any();
        }


    }
}
