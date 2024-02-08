using System;
namespace Blog.Entity.DTOs.User
{
	public class ChangePasswordDto
	{
        public Guid Id { get; set; }
        public String CurrentPassword { get; set; }
        public String NewPassword { get; set; }
        public String ConfirmPassword { get; set; }
    }
}

