using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.ModelConfigurations;

public class SaleMerchandiseModelConfiguration : IEntityTypeConfiguration<SaleMerchandise>
{
    public void Configure(EntityTypeBuilder<SaleMerchandise> builder)
    {
        builder.ToTable("SalesMerchandises");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Quantity)
            .HasPrecision(18, 2);

        builder.Property(x => x.Price)
            .HasPrecision(18, 2);

        builder.Property(x => x.Detail)
            .HasMaxLength(1000);

        builder.HasOne(x => x.Sale)
            .WithMany(x => x.SalesMerchandises)
            .HasForeignKey(x => x.SaleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Merchandise)
            .WithMany()
            .HasForeignKey(x => x.MerchandiseId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RegisterUser)
            .WithMany()
            .HasForeignKey(x => x.RegisterUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}