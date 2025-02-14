using GestaoContas.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoContas.Data.Mappings
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nome).HasColumnType("varchar(100)").IsRequired();
            builder.Property(c => c.Descricao).HasColumnType("varchar(500)");
            builder.Property(c => c.Padrao).IsRequired().HasDefaultValue(false);
            builder.Property(c => c.UsuarioId).HasColumnType("varchar(50)");
        }
    }
}
