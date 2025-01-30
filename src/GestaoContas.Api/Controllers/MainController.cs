using AutoMapper;
using GestaoContas.Api.Models;
using GestaoContas.Shared.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestaoContas.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        //private readonly INotificador _notificador;
        //public readonly IUser AppUser;

        protected Guid UsuarioId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        protected readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public MainController(        
            UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> jwtSettings)
        {            
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;            
        }

        //protected MainController(INotificador notificador,
        //                         IUser appUser)
        //{
        //    _notificador = notificador;
        //    AppUser = appUser;

        //    if (appUser.IsAuthenticated())
        //    {
        //        UsuarioId = appUser.GetUserId();
        //        UsuarioAutenticado = true;
        //    }
        //}

        protected bool OperacaoValida()
        {
            return true; //!_notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = new object() // _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(errorMsg);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            //_notificador.Handle(new Notificacao(mensagem));
        }

        protected async Task<string> GetJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user!);

            var claims = new List<Claim>();
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, await _userManager.GetUserIdAsync(user!)));

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo!);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.HorasParaExpirar),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
