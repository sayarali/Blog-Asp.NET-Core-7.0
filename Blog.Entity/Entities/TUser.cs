using System;
using Microsoft.AspNetCore.Identity;

namespace Blog.Entity.Entities
{
	public class TUser : IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }

		public Guid? ImageId { get; set;}
		public Image? Image { get; set; }

		public ICollection<Post> Posts { get; set; }

    }
}

