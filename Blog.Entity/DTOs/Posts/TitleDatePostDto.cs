using System;
namespace Blog.Entity.DTOs.Posts
{
	public class TitleDatePostDto
	{
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}

