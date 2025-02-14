using Asp.Versioning;
using AutoMapper;
using GestaoContas.Api.Controllers;
using GestaoContas.Api.V2.ViewModels.Categorias;
using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestaoContas.Api.V2.Controllers
{

    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class CategoriasController : MainController
    {
        private readonly ICategoriaReposotory _repository;
        private readonly ICategoriaService _service;
        public CategoriasController(
            INotificador notificador, 
            IMapper mapper, 
            IUser appUser,
            ICategoriaReposotory repository,
            ICategoriaService service) 
            : base(notificador, mapper, appUser)
        {
            _repository = repository;
            _service = service;
        }

        
        [HttpGet]        
        public async Task<IEnumerable<CategoriaViewModel>> Get()
        {
            return _mapper.Map<IEnumerable<CategoriaViewModel>>(await _repository.Buscar(c=>c.UsuarioId == AppUser.GetId() || c.Padrao));
        }

        
        [HttpGet("id:guid")]        
        public async Task<ActionResult<CategoriaViewModel>> Get(Guid id)
        {
            var categoria = await _repository.ObterPorId(id);

            if(categoria!= null && categoria.UsuarioId != AppUser.GetId())
            {
                NotificarErro("Esta categoria não foi cadastrada por você, nem é uma categoria padrão.");
                return CustomResponse();
            }

            return _mapper.Map<CategoriaViewModel>(categoria);
        }

        
        [HttpPost]        
        public async Task<ActionResult<CategoriaCadastrarViewModel>> Post(CategoriaCadastrarViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if(! await _service.Adicionar(_mapper.Map<Categoria>(model)))
                return CustomResponse();

            return CustomResponse(model);
        }

        
        [HttpPut("id:guid")]        
        public async Task<ActionResult<CategoriaAtualizarViewModel>> Put(Guid id, CategoriaAtualizarViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if(id != model.Id)
            {
                NotificarErro("Id informado não equivalente");
                return CustomResponse();
            }

            if (!await _service.Atualizar(_mapper.Map<Categoria>(model)))
                return CustomResponse();

            return CustomResponse(model);
        }

        
        [HttpDelete("id:guid")]        
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid id)
        {            
            if (!await _service.Remover(id) && !OperacaoValida())
                return CustomResponse();

            return NoContent();
        }
    }
}
