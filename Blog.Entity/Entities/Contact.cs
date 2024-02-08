using System;
using Blog.Core.Base;

namespace Blog.Entity.Entities
{
	public class Contact : IBaseEntity
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string Message { get; set; }
		public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
	}
}

