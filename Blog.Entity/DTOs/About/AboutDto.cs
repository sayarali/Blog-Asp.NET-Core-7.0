using System;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Http;

namespace Blog.Entity.DTOs.About
{
	public class AboutDto
	{
        public Guid Id { get; set; }
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}

