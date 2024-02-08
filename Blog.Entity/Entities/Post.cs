using System;
using Blog.Core.Base;

namespace Blog.Entity.Entities
{
	public class Post : BaseEntity
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public int ViewCount { get; set; } = 0;

		public Guid CategoryId { get; set; }
		public Category Category { get; set; }

		public Guid? ImageId { get; set; }
		public Image? Image { get; set; }

		public Guid UserId { get; set; }
		public TUser User { get; set; }

        public ICollection<Comment>? Comments { get; set; }
        public ICollection<PostVisitor>? PostVisitors { get; set; }
    }
}

