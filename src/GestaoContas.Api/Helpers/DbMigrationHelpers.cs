using GestaoContas.Business.Models;
using GestaoContas.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestaoContas.Api.Helpers
{
    public static class DbMigrationHelperExtension
    {
        public static void UseDbMigrationHelper(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).Wait();
        }
    }
    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }
        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser<Guid>>>();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                await context.Database.MigrateAsync();

                await EnsureSeedProducts(context, roleManager, userManager);
            }

        }
        private static async Task EnsureSeedProducts(ApplicationDbContext context, RoleManager<IdentityRole<Guid>> roleManager, UserManager<IdentityUser<Guid>> userManager)
        {
            if (context.Users.Any())
                return;

            // Seed Roles
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            }

            // Seed Users
            var adminId = Guid.NewGuid();
            if (!context.Users.Any(u => u.UserName == "admin@teste.com"))
            {
                var adminUser = new IdentityUser<Guid>
                {
                    Id = adminId,
                    UserName = "admin@teste.com",
                    NormalizedUserName = "ADMIN@TESTE.COM",
                    Email = "admin@teste.com",
                    NormalizedEmail = "ADMIN@TESTE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                adminUser.PasswordHash = userManager.PasswordHasher.HashPassword(adminUser, "Admin@123");

                await userManager.CreateAsync(adminUser);
                await userManager.AddToRoleAsync(adminUser, "Admin");
                await userManager.AddClaimsAsync(adminUser,
                    new List<Claim>() {
                        new Claim(ClaimName.Categoria, $"{ClaimValue.Cadastrar},{ClaimValue.Editar},{ClaimValue.Excluir}", ClaimValueTypes.String),
                        new Claim(ClaimName.Orcamento, $"{ClaimValue.Cadastrar},{ClaimValue.Editar},{ClaimValue.Excluir}", ClaimValueTypes.String),
                        new Claim(ClaimName.Transacao, $"{ClaimValue.Cadastrar},{ClaimValue.Editar},{ClaimValue.Excluir}", ClaimValueTypes.String)
                    });
            }

            await context.Usuarios.AddAsync(new Usuario(adminId, "Admin User", "admin@teste.com"));

            var userId = Guid.NewGuid();
            if (!context.Users.Any(u => u.UserName == "teste@teste.com"))
            {
                var normalUser = new IdentityUser<Guid>
                {
                    Id = userId,
                    UserName = "teste@teste.com",
                    NormalizedUserName = "TESTE@TESTE.COM",
                    Email = "teste@teste.com",
                    NormalizedEmail = "TESTE@TESTE.COM",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                normalUser.PasswordHash = userManager.PasswordHasher.HashPassword(normalUser, "Teste@123");

                await userManager.CreateAsync(normalUser);
            }

            await context.Usuarios.AddAsync(new Usuario(userId, "Teste User", "teste@teste.com"));
            await context.SaveChangesAsync();

            // Seed Categories
            var categoriaOneId = Guid.NewGuid();
            var categoriaTwoId = Guid.NewGuid();
            var categoriaThreeId = Guid.NewGuid();

            if (!context.Categorias.Any())
            {
                await context.Categorias.AddRangeAsync(new List<Categoria>
                {
                    new Categoria(categoriaOneId,"Alimentação","Categoria de Alimentação",true,true),
                    new Categoria(categoriaTwoId,"Transporte","Categoria de Transporte",true,true),
                    new Categoria(categoriaThreeId,"Salario","Salario Mensal",true,true)
                });
                await context.SaveChangesAsync();
            }

            // Seed Transactions
            if (!context.Transacoes.Any())
            {
                await context.Transacoes.AddRangeAsync(new List<Transacao>
                {
                    new Transacao(TipoTransacao.Entrada,20000.00M,DateTime.UtcNow.AddDays(-5),"Salário Admin",categoriaOneId,adminId),
                    new Transacao(TipoTransacao.Saida,150.75M,DateTime.UtcNow.AddDays(-5),"Compra no supermercado",categoriaOneId,adminId),
                    new Transacao(TipoTransacao.Entrada,50.25M,DateTime.UtcNow.AddDays(-2),"Salario Teste",categoriaTwoId,userId),
                    new Transacao(TipoTransacao.Saida,10000.00M,DateTime.UtcNow.AddDays(-2),"Passagem de ônibus",categoriaTwoId,userId)
                });

                await context.SaveChangesAsync();

                if (!context.Orcamentos.Any())
                {
                    await context.Orcamentos.AddRangeAsync(new List<Orcamento>
                    {
                        new Orcamento(1000M,categoriaOneId,adminId),
                        new Orcamento(150M,categoriaTwoId,adminId),
                        new Orcamento(500M,categoriaOneId,userId)

                    });
                }
                await context.SaveChangesAsync();
            }
        }
    }
}


