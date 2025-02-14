using GestaoContas.Api.Extensions.Identity;
using GestaoContas.Business.Interfaces;
using GestaoContas.Business.Notificacoes;
using GestaoContas.Business.Services;
using GestaoContas.Data.Repositories;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GestaoContas.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder ResolveDependencies(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<ICategoriaReposotory, CategoriaRepository>();
            builder.Services.AddScoped<ITransacaoRepository, TransacaoRepsitory>();

            builder.Services.AddScoped<IUsuarioService, UsusarioService>();
            builder.Services.AddScoped<ICategoriaService, CategoriaService>();
            builder.Services.AddScoped<ITransacaoService, TransacaoService>();


            builder.Services.AddScoped<INotificador, Notificador>();

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IUser, AspNetUser>();
            return builder;
        }
    }
}
