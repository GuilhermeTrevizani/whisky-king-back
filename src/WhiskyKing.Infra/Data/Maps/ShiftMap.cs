﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Maps;

public class ShiftMap : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.ToTable("Shifts");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.RegisterUser)
            .WithMany()
            .HasForeignKey(x => x.RegisterUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}