using System;
using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
    public class PostVisitorMap : IEntityTypeConfiguration<PostVisitor>
    {
        public void Configure(EntityTypeBuilder<PostVisitor> builder)
        {
            builder.HasKey(x => new { x.PostId, x.VisitorId });
        }
    }
}


