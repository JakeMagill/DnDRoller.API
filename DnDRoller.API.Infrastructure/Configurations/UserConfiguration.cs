using System;
using System.Collections.Generic;
using System.Text;
using DnDRoller.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnDRoller.API.Infrastructure.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Firstname)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(u => u.Lastname).HasMaxLength(20);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.PasswordSalt)
                .IsRequired();

            builder.Property(u => u.ModifiedBy)
                .IsRequired();

            builder.Property(u => u.ModifiedDate)
                .IsRequired();

            builder.Property(u => u.CreatedBy)
                .IsRequired();

            builder.Property(u => u.CreatedDate)
                .IsRequired();

            builder.Property(u => u.IsDeleted)
                .IsRequired();
        }
    }
}
