using Asp.Versioning;
using AutoMapper;
using GestaoContas.Api.Controllers;
using GestaoContas.Api.V2.ViewModels.Orcamento;
using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestaoContas.Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OrcamentosController : MainController
    {
        private readonly IOrcamentoRepository _orcamentoRepository;
        private readonly IOrcamentoService _orcamentoService;
        private readonly IMapper _mapper;

        public OrcamentosController(
            INotificador notificador,
            IMapper mapper,
            IUser appUser,
            IOrcamentoRepository orcamentoRepository,
            IOrcamentoService orcamentoService)
            : base(notificador, mapper, appUser)
        {
            _orcamentoRepository = orcamentoRepository;
            _orcamentoService = orcamentoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<OrcamentoViewModel>> Get()
        {
            var orcamentos = await _orcamentoRepository.BuscarOrcamentoCompleto(o => o.UsuarioId == AppUser.GetId());
            return _mapper.Map<IEnumerable<OrcamentoViewModel>>(orcamentos);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrcamentoViewModel>> Get(Guid id)
        {
            var orcamento = await _orcamentoRepository.ObterOrcamentoCompletoPorId(id);

            if (orcamento == null || orcamento.UsuarioId != AppUser.GetId())
            {
                NotificarErro("Este orçamento não foi cadastrado por você.");
                return CustomResponse();
            }

            return _mapper.Map<OrcamentoViewModel>(orcamento);
        }

        [HttpPost]
        public async Task<ActionResult<OrcamentoViewModel>> Post(OrcamentoViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var orcamento = _mapper.Map<Orcamento>(model);
            await _orcamentoService.Adicionar(orcamento);

            return CustomResponse(_mapper.Map<OrcamentoViewModel>(orcamento));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<OrcamentoViewModel>> Put(Guid id, OrcamentoViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (id != model.Id)
            {
                NotificarErro("Id informado não equivalente.");
                return CustomResponse();
            }

            if (!await _orcamentoService.Atualizar(_mapper.Map<Orcamento>(model)))
                return CustomResponse();

            return CustomResponse(model);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!await _orcamentoService.Remover(id) && !OperacaoValida())
                return CustomResponse();

            return NoContent();
        }
    }
}
