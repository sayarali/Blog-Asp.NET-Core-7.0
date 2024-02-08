using System;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
	public class TUserMap : IEntityTypeConfiguration<TUser>
	{
		public void Configure(EntityTypeBuilder<TUser> builder)
        {
            // Primary key
            builder.HasKey(u => u.Id);

            // Indexes for "normalized" username and email, to allow efficient lookups
            builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // Maps to the AspNetUsers table
            builder.ToTable("Users");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each User can have many UserClaims
            builder.HasMany<TUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            // Each User can have many UserLogins
            builder.HasMany<TUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            // Each User can have many UserTokens
            builder.HasMany<TUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            // Each User can have many entries in the UserRole join table
            builder.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var admin = new TUser
            {
                Id = Guid.Parse("1e153110-5d6c-48a3-ade5-02f09d72eccf"),
                UserName = "alisayar",
                NormalizedUserName = "ALISAYAR",
                Email = "as@alisayar.com",
                NormalizedEmail = "AS@ALISAYAR.COM",
                PhoneNumber = "+905374539631",
                FirstName = "Ali",
                LastName = "Sayar",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            admin.PasswordHash = CreatePasswordHash(admin, "123456");


            builder.HasData(
               admin
            );
        }
        private string CreatePasswordHash(TUser user, string password)
        {
            var passwordHasher = new PasswordHasher<TUser>();
            return passwordHasher.HashPassword(user, password);
        }
    }
}

