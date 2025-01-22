using GestaoContas.Api.Models;
using GestaoContas.Data.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GestaoContas.Api.Configurations
{
    public static class IdentityConfiguration
    {
        public static WebApplicationBuilder AddIdentityConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddRoles<IdentityRole>()
                            .AddEntityFrameworkStores<ApplicationDbContext>();

            var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings?.Segredo!);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = jwtSettings?.Emissor,
                        ValidAudience = jwtSettings?.Audiencia
                    };
                });
            return builder;
        }
    }
}
