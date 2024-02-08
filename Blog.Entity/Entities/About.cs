using System;
using Blog.Core.Base;

namespace Blog.Entity.Entities
{
	public class About : BaseEntity
	{
		public Guid ImageId { get; set; }
		public Image Image { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
	}
}

