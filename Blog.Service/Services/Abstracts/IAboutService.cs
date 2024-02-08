using System;
using Blog.Entity.DTOs.About;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Abstracts
{
	public interface IAboutService
	{
        Task<About> GetAboutAsync();
        Task SaveAboutAsync(AboutDto aboutDto);
    }
}

