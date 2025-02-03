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
            var claims = await _userManager.GetClaimsAsync(user!);
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Id == user.Id);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user!.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user!.Email!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpocheDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpocheDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64));
            claims.Add(new Claim("nome", usuario?.Nome));

            foreach (var role in roles)            
                claims.Add(new Claim("role", role));
            

            var identityClaims = new ClaimsIdentity(claims);


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo!);



            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = identityClaims,
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.HorasParaExpirar),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private static long ToUnixEpocheDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}
