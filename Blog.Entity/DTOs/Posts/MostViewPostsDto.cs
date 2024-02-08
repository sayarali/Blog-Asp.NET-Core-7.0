using System;
using Blog.Entity.DTOs.Categories;

namespace Blog.Entity.DTOs.Posts
{
	public class MostViewPostsDto
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int ViewCount { get; set; }
    }
}

