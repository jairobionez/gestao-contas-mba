using Asp.Versioning;
using AutoMapper;
using GestaoContas.Api.Controllers;
using GestaoContas.Api.V2.ViewModels.Transacoes;
using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using GestaoContas.Api.Extensions.Expressions;

namespace GestaoContas.Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class TransacaoController : MainController
    {
        private readonly ITransacaoRepository _repositorio;
        private readonly ITransacaoService _servico;
        public TransacaoController(
            INotificador notificador, 
            IMapper mapper, 
            IUser appUser, 
            ITransacaoRepository repositorio, 
            ITransacaoService servico) 
            : base(notificador, mapper, appUser)
        {
            _repositorio = repositorio;
            _servico = servico;
        }

        [HttpGet]
        public async Task<IEnumerable<TransacaoViewModel>> Get()
        {
            return _mapper.Map<IEnumerable<TransacaoViewModel>>(await _repositorio.Buscar(c => c.UsuarioId == AppUser.GetId()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TransacaoViewModel>> Get(Guid id)
        {
            var transacao = await _repositorio.ObterPorId(id);

            if (transacao != null && transacao.UsuarioId != AppUser.GetId())
            {
                NotificarErro("Esta transacao não foi cadastrada por você.");
                return CustomResponse();
            }

            return _mapper.Map<TransacaoViewModel>(transacao);
        }


        [HttpPost]
        public async Task<ActionResult<TransacaoCadastrarViewModel>> Post(TransacaoCadastrarViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (!await _servico.Adicionar(_mapper.Map<Transacao>(model)))
                return CustomResponse();

            return CustomResponse(model);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<TransacaoAtualizarViewModel>> Put(Guid id, TransacaoAtualizarViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != model.Id)
            {
                NotificarErro("Id informado não equivalente");
                return CustomResponse();
            }

            if (!await _servico.Atualizar(_mapper.Map<Transacao>(model)))
                return CustomResponse();

            return CustomResponse(model);
        }


        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!await _servico.Remover(id) && !OperacaoValida())
                return CustomResponse();

            return NoContent();
        }

        [HttpGet("Busca")]
        public async Task<IEnumerable<TransacaoViewModel>> Get([FromQuery]TransacaoFiltroViewModel busca)
        {
            var e = await _repositorio.Buscar(
                t=>
                t.UsuarioId.Equals(AppUser.GetId())
                && (!busca.Tipo.HasValue || (busca.Tipo.HasValue && t.TipoTransacao.Equals((TipoTransacao)busca.Tipo.Value)))
                &&(!busca.DataInicial.HasValue || (busca.DataInicial.HasValue && t.Data.HasValue && t.Data.Value.Date >= busca.DataInicial.Value.Date))
                &&(!busca.DataFinal.HasValue || (busca.DataFinal.HasValue && t.Data.HasValue && t.Data.Value.Date <= busca.DataFinal.Value.Date))
                && (!busca.ValorInicial.HasValue || (busca.ValorInicial.HasValue && t.Valor >= busca.ValorInicial.Value))
                &&(!busca.ValorFinal.HasValue || (busca.ValorFinal.HasValue && t.Valor <= busca.ValorFinal.Value))
                &&(!busca.CategoriaId.HasValue || (busca.CategoriaId.HasValue && t.CategoriaId.Equals(busca.CategoriaId.Value)))
                &&(!busca.Tipo.HasValue ||(busca.Tipo.HasValue && t.TipoTransacao.Equals((TipoTransacao)busca.Tipo.Value)))
                );

            return _mapper.Map<IEnumerable<TransacaoViewModel>>(e);
        }
    }
}

