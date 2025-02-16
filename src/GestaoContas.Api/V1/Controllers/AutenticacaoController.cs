using Asp.Versioning;
using AutoMapper;
using GestaoContas.Api.Controllers;
using GestaoContas.Api.Extensions.Jwts;
using GestaoContas.Api.V1.ViewModels.Autenticacoes;
using GestaoContas.Business.Interfaces;
using GestaoContas.Data.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GestaoContas.Api.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0",Deprecated = true)]
    [Route("api/v{version:apiVersion}/autenticacao")]
    public class AutenticacaoController : MainSignInController
    {
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;                
        public AutenticacaoController(
            SignInManager<IdentityUser<Guid>> signInManager,
            UserManager<IdentityUser<Guid>> userManager,
            IOptions<JwtSettings> jwtSettings,
            ApplicationDbContext context,
            INotificador notificador, IMapper mapper, IUser appUser) 
            :base(userManager, jwtSettings, context, notificador, mapper, appUser)
        {
            _signInManager = signInManager;            
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user == null) return Problem("Usuário ou senha incorretos, tente novamente", statusCode: StatusCodes.Status400BadRequest);
            
            var result = await _signInManager.PasswordSignInAsync(user, model.Senha!, false, true);

            if (!result.Succeeded) return Problem("Usuário ou senha incorretos, tente novamente", statusCode: StatusCodes.Status400BadRequest);

            return Ok(GetJwt(model.Email!).Result.AccessToken);
        }        
    }
}
