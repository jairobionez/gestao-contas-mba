using Asp.Versioning;
using AutoMapper;
using GestaoContas.Api.Controllers;
using GestaoContas.Api.V2.ViewModels.Categorias;
using GestaoContas.Api.V2.ViewModels.Dashboard;
using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Business.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GestaoContas.Api.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class DashboardController : MainController
    {
        private readonly ITransacaoService _service;
        public DashboardController(
            INotificador notificador,
            IMapper mapper,
            IUser appUser,
            ITransacaoService service)
            : base(notificador, mapper, appUser)
        {
            _service = service;
        }

        [HttpPost("indicadores")]
        public async Task<ActionResult<DashboardDTO>> Post([FromBody] DashboardFiltroViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var usuarioId = AppUser.GetId();

            var resultado = await _service.GetDadosDash(usuarioId, model.DataInicial, model.DataFinal);

            return CustomResponse(resultado);
        }
    }
}
