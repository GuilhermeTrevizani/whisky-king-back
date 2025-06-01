using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Maps;

public class MerchandiseMap : IEntityTypeConfiguration<Merchandise>
{
    public void Configure(EntityTypeBuilder<Merchandise> builder)
    {
        builder.ToTable("Merchandises");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Merchandises)
            .HasForeignKey(x => x.CategoryId)
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