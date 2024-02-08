using System;
using Blog.Core.Base;

namespace Blog.Entity.Entities
{
	public class PostVisitor : IBaseEntity
	{
		public PostVisitor()
		{
		}
        public PostVisitor(Guid postId, int visitorId)
        {
			PostId = postId;
			VisitorId = visitorId;
        }

        public Guid PostId { get; set; }
		public Post Post { get; set; }
		public int VisitorId { get; set; }
		public Visitor Visitor { get; set; }
		public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
	}
}

