using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GestaoContas.Data.Contexts
{
    //Mudar quando implementar o Identity para não gerar, ainda, as tabelas
    //public class ApplicationDbContext : IdentityDbContext
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (!optionsBuilder.IsConfigured)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Contexts");
                string pathToConfigFile = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "src\\GestaoContas.Shared\\CommonConfigurations\\SharedSettings.json");
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(path)                
                .AddJsonFile(pathToConfigFile)
                .Build();
                optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly((typeof(ApplicationDbContext).Assembly));            
        }
    }
}
