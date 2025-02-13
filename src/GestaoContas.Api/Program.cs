using GestaoContas.Api.Configurations;
using GestaoContas.Api.Helpers;
using GestaoContas.Shared.CommonConfigurations;
using Microsoft.AspNetCore.Authentication;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Asp.Versioning.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);


builder
    .AddSharedsConfiguration()
    .AddIdentityConfiguration()
    .AddApiConfiguration()
    .AddSwaggerConfiguration()    
    .AddContextConfiguration()    
    .ResolveDependencies();

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();


app.UseHttpsRedirection();

app.UseCors("Total");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerConfig(apiVersionDescriptionProvider);
}

app.UseDbMigrationHelper();

app.Run();
