using System.Reflection;
using System.Security.Claims;
using Blog.Core.Base;
using Blog.DataAccess.Extensions;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<TUser, TRole, Guid, TUserClaim, TUserRole, TUserLogin, TRoleClaim, TUserToken>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AppDbContext() {
            
        }
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options) {
            this.httpContextAccessor = httpContextAccessor;
        }
        DbSet<Post> Posts { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Visitor> Visitors { get; set; }
        DbSet<PostVisitor> PostVisitors { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<About> About { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(entry => entry.Entity is IBaseEntity && (entry.State == EntityState.Added || entry.State == EntityState.Modified));
         
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedTime = DateTime.UtcNow;
                    if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                        ((BaseEntity)entry.Entity).CreatedBy = GetUserId();
                } else if (entry.State == EntityState.Modified)
                {
                    ((BaseEntity)entry.Entity).ChangedTime = DateTime.UtcNow;
                    if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                        ((BaseEntity)entry.Entity).ChangedBy = GetUserId();
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        private Guid GetUserId()
        {
            var user = httpContextAccessor.HttpContext.User;
            return user.GetUserId();
        }
    }
}