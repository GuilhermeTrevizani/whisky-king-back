using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Maps;

public class AccessGroupPermissionMap : IEntityTypeConfiguration<AccessGroupPermission>
{
    public void Configure(EntityTypeBuilder<AccessGroupPermission> builder)
    {
        builder.ToTable("AccessGroupsPermissions");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.AccessGroupId, x.Permission })
            .IsUnique();

        builder.HasOne(x => x.AccessGroup)
            .WithMany(x => x.AccessGroupsPermissions)
            .HasForeignKey(x => x.AccessGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RegisterUser)
            .WithMany()
            .HasForeignKey(x => x.RegisterUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}