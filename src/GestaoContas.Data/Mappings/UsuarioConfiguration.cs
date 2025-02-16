using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GestaoContas.Business.Models;


namespace GestaoContas.Data.Contexts
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(u => u.Email).HasColumnType("varchar(100)").IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();            
        }
    }
}
