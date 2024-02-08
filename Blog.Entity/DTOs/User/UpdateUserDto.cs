﻿using System;
using Blog.Entity.Entities;

namespace Blog.Entity.DTOs.User
{
	public class UpdateUserDto
	{
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public List<TRole>? Roles { get; set; }
    }
}

