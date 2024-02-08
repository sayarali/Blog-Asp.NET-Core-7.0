using System;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;

namespace Blog.Service.Services.Concretes
{
	public class ContactService : IContactService
	{
        private readonly IUnitOfWork unitOfWork;

        public ContactService(IUnitOfWork unitOfWork)
		{
            this.unitOfWork = unitOfWork;
        }

        public async Task AddContactMessageAsync(Contact contact)
        {
            await unitOfWork.GetRepository<Contact>().AddAsync(contact);
            unitOfWork.Save();
        }

        public async Task<List<Contact>> GetAllContactMessageAsync()
        {
            var contacts = await unitOfWork.GetRepository<Contact>().GetAllAsync();
            return contacts.OrderByDescending(x => x.CreatedTime).ToList();
        }
    }
}


