using System;
using Blog.Core.Base;

namespace Blog.Entity.Entities
{
	public class Image : BaseEntity
	{
		public string FileName { get; set; }
		public string FileType { get; set; }
        public byte[]? Data { get; set; }

        public ICollection<Post> Posts { get; set; }
		public ICollection<TUser> Users { get; set; }
	}
}

