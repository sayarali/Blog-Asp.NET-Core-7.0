using System;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Http;

namespace Blog.Entity.DTOs.Posts
{
	public class UpdatePostDto
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public Guid? ImageId { get; set; }
		public Image? Image { get; set; }

 		public Guid CategoryId { get; set; }
        public IList<CategoryDto> Categories { get; set; }
        public bool IsActive { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

