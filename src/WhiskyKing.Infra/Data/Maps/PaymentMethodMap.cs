﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WhiskyKing.Domain.Entities;

namespace WhiskyKing.Infra.Data.Maps;

public class PaymentMethodMap : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.ToTable("PaymentMethods");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(25)
            .IsRequired();

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