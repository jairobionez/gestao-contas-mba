using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GestaoContas.Shared.Domain;

namespace GestaoContas.Shared.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Nome).HasColumnType("varchar(100)").IsRequired();
        }
    }
}
