using System;
using Blog.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.DataAccess.Mappings
{
	public class AboutMap : IEntityTypeConfiguration<About>
    {
		
        public void Configure(EntityTypeBuilder<About> builder)
        {
        
        }
    }
}

