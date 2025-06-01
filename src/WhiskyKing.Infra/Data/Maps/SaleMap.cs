using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Maps;

public class SaleMap : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Shift)
            .WithMany()
            .HasForeignKey(x => x.ShiftId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RegisterUser)
            .WithMany()
            .HasForeignKey(x => x.RegisterUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.LastChangeUser)
            .WithMany()
            .HasForeignKey(x => x.LastChangeUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}