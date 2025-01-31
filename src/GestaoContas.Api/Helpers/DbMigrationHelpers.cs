using GestaoContas.Shared.Data.Contexts;
using GestaoContas.Shared.Domain;
using GestaoContas.Shared.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

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

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();


            if (env.IsDevelopment() || env.IsEnvironment("Docker"))
            {
                await context.Database.MigrateAsync();

                await EnsureSeedProducts(context, roleManager, userManager);
            }

        }
        private static async Task EnsureSeedProducts(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (context.Users.Any())
                return;

            // Seed Roles
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new ApplicationRole("Admin"));
            }

            // Seed Users
            var adminId = Guid.NewGuid();
            if (!context.Users.Any(u => u.UserName == "admin@teste.com"))
            {
                var adminUser = new ApplicationUser
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
            }

            await context.Usuarios.AddAsync(new Usuario
            {
                Id = adminId,
                Nome = "Admin User",
            });

            var userId = Guid.NewGuid();
            if (!context.Users.Any(u => u.UserName == "teste@teste.com"))
            {
                var normalUser = new ApplicationUser
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

            await context.Usuarios.AddAsync(new Usuario
            {
                Id = userId,
                Nome = "Teste User",
            });
            await context.SaveChangesAsync();

            // Seed Categories
            var categoriaOneId = Guid.NewGuid();
            var categoriaTwoId = Guid.NewGuid();
            var categoriaThreeId = Guid.NewGuid();

            if (!context.Categorias.Any())
            {
                await context.Categorias.AddRangeAsync(new List<Categoria>
                {
                    new Categoria
                    {
                        Id = categoriaOneId,
                        Nome = "Alimentação",
                        Descricao = "Categoria de Alimentação",
                        Padrao = true,
                        Ativo = true
                    },
                    new Categoria
                    {
                        Id = categoriaTwoId,
                        Nome = "Transporte",
                        Descricao = "Categoria de Transporte",
                        Padrao = true,
                        Ativo = true
                    },
                    new Categoria
                    {
                        Id = categoriaThreeId,
                        Nome = "Salario",
                        Descricao = "Salario Mensal",
                        Padrao = true,
                        Ativo = true
                    }
                });

                await context.SaveChangesAsync();
            }

            // Seed Transactions
            if (!context.Transacoes.Any())
            {
                await context.Transacoes.AddRangeAsync(new List<Transacao>
                {
                    new Transacao
                    {
                        Id = Guid.NewGuid(),
                        Data = DateTime.UtcNow.AddDays(-5),
                        Valor = 20000.00M,
                        Descricao = "Salário Admin",
                        TipoTransacao = TipoTransacao.Entrada,
                        CategoriaId = categoriaOneId,
                        UsuarioId = adminId
                    },
                    new Transacao
                    {
                        Id = Guid.NewGuid(),
                        Data = DateTime.UtcNow.AddDays(-5),
                        Valor = 150.75M,
                        Descricao = "Compra no supermercado",
                        TipoTransacao = TipoTransacao.Saida,
                        CategoriaId = categoriaOneId,
                        UsuarioId = adminId
                    },
                    new Transacao
                    {
                        Id = Guid.NewGuid(),
                        Data = DateTime.UtcNow.AddDays(-2),
                        Valor = 10000.00M,
                        Descricao = "Salario Teste",
                        TipoTransacao = TipoTransacao.Entrada,
                        CategoriaId = categoriaTwoId,
                        UsuarioId = userId
                    },
                    new Transacao
                    {
                        Id = Guid.NewGuid(),
                        Data = DateTime.UtcNow.AddDays(-2),
                        Valor = 50.25M,
                        Descricao = "Passagem de ônibus",
                        TipoTransacao = TipoTransacao.Saida,
                        CategoriaId = categoriaTwoId,
                        UsuarioId = userId
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}

