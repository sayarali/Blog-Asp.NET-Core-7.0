using System;
using Blog.Core.Base;

namespace Blog.Entity.Entities
{
	public class Comment : BaseEntity
	{
		public string Content { get; set; }
		public Guid? UserId { get; set; }
		public TUser? User { get; set; }
		public string? Email { get; set; }
		public string? Name { get; set; }
		public Guid PostId { get; set; }
		public Post Post { get; set; }

	}
}

