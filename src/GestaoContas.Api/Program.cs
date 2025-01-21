using GestaoContas.Api.Configurations;
using GestaoContas.Shared.CommonConfigurations;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddSharedsConfiguration()
    .AddApiConfiguration()
    .AddSwaggerConfiguration()
    .AddContextConfiguration()
    .AddIdentityConfiguration();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
