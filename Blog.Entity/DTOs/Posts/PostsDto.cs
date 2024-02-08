using System;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;

namespace Blog.Entity.DTOs.Posts
{
	public class PostsDto
	{
		public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public CategoryDto Category { get; set; }
        public int ViewCount { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsActive { get; set; }

    }
}

