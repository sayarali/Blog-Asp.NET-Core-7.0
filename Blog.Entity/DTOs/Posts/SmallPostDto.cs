using System;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;

namespace Blog.Entity.DTOs.Posts
{
	public class SmallPostDto
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public CategoryDto Category { get; set; }
        public int ViewCount { get; set; }
        public TUser User { get; set; }
        public Image Image { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

