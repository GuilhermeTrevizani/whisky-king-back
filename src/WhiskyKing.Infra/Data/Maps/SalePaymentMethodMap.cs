using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Maps;

public class SalePaymentMethodMap : IEntityTypeConfiguration<SalePaymentMethod>
{
    public void Configure(EntityTypeBuilder<SalePaymentMethod> builder)
    {
        builder.ToTable("SalesPaymentMethods");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Value)
            .HasPrecision(18, 2);

        builder.HasOne(x => x.Sale)
            .WithMany(x => x.SalesPaymentMethods)
            .HasForeignKey(x => x.SaleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PaymentMethod)
            .WithMany()
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RegisterUser)
            .WithMany()
            .HasForeignKey(x => x.RegisterUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}