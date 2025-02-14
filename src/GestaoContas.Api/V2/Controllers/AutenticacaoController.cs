using Asp.Versioning;
using AutoMapper;
using GestaoContas.Api.Controllers;
using GestaoContas.Api.Extensions.Jwts;
using GestaoContas.Api.V2.ViewModels.Autenticacoes;
using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Models;
using GestaoContas.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GestaoContas.Api.V2.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[Controller]")]
    public class AutenticacaoController : MainSignInController
    {
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly IUsuarioService _usuarioService;
        public AutenticacaoController(
            SignInManager<IdentityUser<Guid>> signInManager,
            UserManager<IdentityUser<Guid>> userManager,
            IOptions<JwtSettings> jwtSettings,
            ApplicationDbContext context,
            INotificador notificador, 
            IMapper mapper, 
            IUsuarioService usuarioService,
            IUser appUser) 
            : base(userManager, jwtSettings, context, notificador, mapper, appUser)
        {
            _signInManager = signInManager;
            _usuarioService= usuarioService;
        }

        
        [HttpPost("login")]        
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user == null) return CustomResponse("Usuário ou senha incorretos, tente novamente");

            var result = await _signInManager.PasswordSignInAsync(user, model.Senha!, false, true);

            if (!result.Succeeded) return CustomResponse("Usuário ou senha incorretos, tente novamente");

            if (result.IsLockedOut) return CustomResponse("Usuário bloqueado por tentativas inválidas");
                        
            return CustomResponse(GetJwt(user.Email!).Result);
        }

        
        [HttpPost("cadastrar")]        
        public async Task<ActionResult> CadastrarAsync(CadastrarViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            

            var user = new IdentityUser<Guid>()
            {
                UserName = model.Nome,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Senha!);
            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    NotificarErro(error.Description);
            else
            {

                await _usuarioService.Adicionar(new Usuario(Guid.Parse(await _userManager.GetUserIdAsync(user)), model.Nome, model.Email));
                await _signInManager.SignInAsync(user, false);
            }

            return CustomResponse(model);
        }

    }
}
