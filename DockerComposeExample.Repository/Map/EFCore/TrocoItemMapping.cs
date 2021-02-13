using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DockerComposeExample.Domain.Models;

namespace DockerComposeExample.Repository.Map.EFCore
{
    public class TrocoItemMapping : IEntityTypeConfiguration<TrocoItem>
    {
        public void Configure(EntityTypeBuilder<TrocoItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Quantidade)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(p => p.ValorItem)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(p => p.TipoItemTroco)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(c => c.Pagamento)
                .WithMany(c => c.TrocoItems);

            builder.ToTable("TrocoItem");
        }
    }
}
