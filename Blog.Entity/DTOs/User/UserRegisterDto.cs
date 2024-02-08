using System;
using Microsoft.AspNetCore.Http;

namespace Blog.Entity.DTOs.User
{
	public class UserRegisterDto
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Password { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}

