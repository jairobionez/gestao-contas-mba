using GestaoContas.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoContas.Shared.Data.Configurations
{
    public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacao");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.TipoTransacao)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(t => t.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Data)
                .IsRequired(false);

            builder.Property(t => t.Descricao)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(t => t.CategoriaId)
                .IsRequired();

            builder.Property(t => t.UsuarioId)
                .IsRequired();

            builder.HasOne(t => t.Usuario)
                .WithMany(u => u.Transacoes) 
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(t => t.Categoria)
                .WithMany(c => c.Transacoes)
                .HasForeignKey(t => t.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
