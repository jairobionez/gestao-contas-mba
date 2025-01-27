using GestaoContas.Api.Configurations;
using GestaoContas.Shared.CommonConfigurations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddSharedsConfiguration()
    .AddApiConfiguration()
    .AddSwaggerConfiguration()
    .AddContextConfiguration()
    .AddIdentityConfiguration();

//TODO alterar para configuration
builder.Services.ResolveDependencies();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerConfig(apiVersionDescriptionProvider);
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
