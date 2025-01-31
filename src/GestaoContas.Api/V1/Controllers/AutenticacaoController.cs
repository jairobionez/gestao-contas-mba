using GestaoContas.Api.Controllers;
using GestaoContas.Api.Models;
using GestaoContas.Api.V1.ViewModels.Autenticacoes;
using GestaoContas.Shared.Data.Contexts;
using GestaoContas.Shared.Domain;
using GestaoContas.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestaoContas.Api.V1.Controllers
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/autenticacao")]
    public class AutenticacaoController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationDbContext _context;
        public AutenticacaoController(
            SignInManager<IdentityUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            ApplicationDbContext context)
            :base(userManager, jwtSettings)
        {
            _signInManager = signInManager;            
            _context = context;
        }
                        

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

            var result = await _signInManager.PasswordSignInAsync(_userManager.FindByEmailAsync(model.Email!).Result!, model.Senha!, false, true);

            if (!result.Succeeded) return Problem("Usuário ou senha incorretos, tente novamente", statusCode: StatusCodes.Status400BadRequest);

            return Ok(await GetJwt(model.Email!));
        }        
    }
}
