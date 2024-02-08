using System;
using Blog.Entity.Entities;

namespace Blog.Entity.DTOs.User
{
	public class UserDto
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public Image? Image { get; set; }
        public string Role { get; set; }
    }
}

