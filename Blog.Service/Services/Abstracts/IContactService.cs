using System;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Abstracts
{
	public interface IContactService
	{
        Task AddContactMessageAsync(Contact contact);
        Task<List<Contact>> GetAllContactMessageAsync();
    }
}

