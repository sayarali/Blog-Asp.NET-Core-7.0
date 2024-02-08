using System;
using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class TRoleMap : IEntityTypeConfiguration<TRole>
    {
        public void Configure(EntityTypeBuilder<TRole> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("Roles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<TRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            builder.HasData(new TRole
            {
                Id = Guid.Parse("5301fc14-043b-43e7-b551-e838b2c86036"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new TRole
            {
                Id = Guid.Parse("840e9227-02af-4051-85eb-b3c292f9a5db"),
                Name = "Yazar",
                NormalizedName = "YAZAR",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },
            new TRole
            {
                Id = Guid.Parse("fa3b7c7b-95f7-4495-ab95-6f564c0a4d24"),
                Name = "Kullanıcı",
                NormalizedName = "KULLANICI",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
            );
        }
    }
}

