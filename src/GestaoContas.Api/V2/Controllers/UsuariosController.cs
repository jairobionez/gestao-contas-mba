using GestaoContas.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GestaoContas.Api.Controllers;
using AutoMapper;
using GestaoContas.Api.V2.ViewModels.Usuarios;
using Asp.Versioning;

namespace GestaoContas.Api.V2.Controllers
{

    //TODO: Excluir, pois não é necesária na aplicação
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class UsuariosController : MainController
    {
        private IUsuarioRepository _usuarioRepository;
        private IUsuarioService _usuarioService;


        public UsuariosController(
            IUsuarioRepository usuarioRepository,
            IUsuarioService usuarioService,
            INotificador notificador, 
            IMapper mapper,
            IUser appUser) 
            : base(notificador, mapper, appUser)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
        }

        
        [HttpGet()]        
        public async Task<IEnumerable<UsuarioListarViewModel>> Get()
        {
            return _mapper.Map<IEnumerable<UsuarioListarViewModel>>(await _usuarioRepository.ObterTodos());
        }

        
        [HttpGet("id:guid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]        
        public async Task<ActionResult<UsuarioListarViewModel>> Get(Guid id)
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            if (usuario == null) return NotFound();
            return _mapper.Map<UsuarioListarViewModel>(usuario);
        }
    }
}
