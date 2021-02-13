using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DockerComposeExample.Domain.Models;

namespace DockerComposeExample.Repository.Map.EFCore
{
    public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ValorPagamento)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.ValorPagoCliente)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.HasMany(f => f.TrocoItems)
                .WithOne(p => p.Pagamento)
                .HasForeignKey(p => p.IdPagamento);

            builder.ToTable("Pagamento");
        }
    }
}
