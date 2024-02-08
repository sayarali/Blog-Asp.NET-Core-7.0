using System;
using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class TUserRoleMap : IEntityTypeConfiguration<TUserRole>
    {
        public void Configure(EntityTypeBuilder<TUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("UserRoles");

            builder.HasData(
                new TUserRole
                {
                    RoleId = Guid.Parse("5301fc14-043b-43e7-b551-e838b2c86036"),
                    UserId = Guid.Parse("1e153110-5d6c-48a3-ade5-02f09d72eccf"),
                }
            );
        }
    }
}

