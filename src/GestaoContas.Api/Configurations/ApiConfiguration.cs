using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace GestaoContas.Api.Configurations
{
    public static class ApiConfiguration
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(Program));

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                        options.SuppressModelStateInvalidFilter = true
                );            

            builder.Services
                .AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(2);
                }).AddApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });
            

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;

            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Total",
                              builder =>
                                  builder
                                   .WithOrigins("http://localhost:4200")
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                                   .AllowCredentials());
            });

            return builder;
        }
    }
}
