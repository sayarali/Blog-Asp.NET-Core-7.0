using System;
using Blog.Entity.DTOs.Categories;
using Microsoft.AspNetCore.Http;

namespace Blog.Entity.DTOs.Posts
{
	public class AddPostDto
	{
		public string Title { get; set; }
        public string Content { get; set; }
        public Guid? ImageId { get; set; }
        public Guid CategoryId { get; set; }
        public IList<CategoryDto> Categories { get; set; }
        public bool IsActive { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

