using System;
using Blog.Core.Base;

namespace Blog.Entity.Entities
{
    public class Visitor : IBaseEntity
    {
		public Visitor()
		{

		}
		public Visitor(string ipAddress, string userAgent)
		{
			IpAddress = ipAddress;
			UserAgent = userAgent;
		}

		public int Id { get; set; }
		public string IpAddress { get; set; }
        public string UserAgent { get; set; }
		public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

        public ICollection<PostVisitor>? PostVisitors { get; set; }

    }
}

