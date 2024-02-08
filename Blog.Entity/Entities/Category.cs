using System;
using Blog.Core.Base;

namespace Blog.Entity.Entities
{
	public class Category : BaseEntity
	{
		public string Name { get; set; }

		public ICollection<Post> Posts { get; set; }
	} 
}

