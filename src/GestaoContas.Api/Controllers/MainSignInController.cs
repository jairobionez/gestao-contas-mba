using GestaoContas.Shared.Data.Contexts;
using GestaoContas.Shared.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GestaoContas.Api.Controllers
{
    public abstract class MainSignInController : MainController
    {
        protected readonly UserManager<IdentityUser<Guid>> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationDbContext _context;

        public MainSignInController(
            UserManager<IdentityUser<Guid>> userManager,
            IOptions<JwtSettings> jwtSettings,
            ApplicationDbContext context) : base()
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _context = context;
        }
        protected async Task<string> GetJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user!);
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == user.Id);

            var claims = new List<Claim>();
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, await _userManager.GetUserIdAsync(user!)));
            claims.Add(new Claim(ClaimTypes.Email, email));
            claims.Add(new Claim("nome", usuario?.Nome));

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
