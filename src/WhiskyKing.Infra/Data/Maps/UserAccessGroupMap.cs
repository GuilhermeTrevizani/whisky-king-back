using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Maps;

public class UserAccessGroupMap : IEntityTypeConfiguration<UserAccessGroup>
{
    public void Configure(EntityTypeBuilder<UserAccessGroup> builder)
    {
        builder.ToTable("UsersAccessGroups");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserId, x.AccessGroupId })
            .IsUnique();

        builder.HasOne(x => x.User)
            .WithMany(x => x.UsersAccessGroups)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AccessGroup)
            .WithMany(x => x.UsersAccessGroups)
            .HasForeignKey(x => x.AccessGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RegisterUser)
            .WithMany()
            .HasForeignKey(x => x.RegisterUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}