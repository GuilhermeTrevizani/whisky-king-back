using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.ModelConfigurations;

public class CategoryDetailModelConfiguration : IEntityTypeConfiguration<CategoryDetail>
{
    public void Configure(EntityTypeBuilder<CategoryDetail> builder)
    {
        builder.ToTable("CategoriesDetails");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.CategoryId, x.Detail })
            .IsUnique();

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RegisterUser)
            .WithMany()
            .HasForeignKey(x => x.RegisterUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}