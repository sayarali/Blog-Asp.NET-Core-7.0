using System;
namespace Blog.Entity.DTOs.Categories
{
	public class CategoryDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool IsActive { get; set; }
	}
}

