using System;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.DTOs.About;
using Blog.Entity.DTOs.Posts;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;

namespace Blog.Service.Services.Concretes
{
	public class AboutService : IAboutService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageService imageService;

        public AboutService(IUnitOfWork unitOfWork, IImageService imageService)
		{
            this.unitOfWork = unitOfWork;
            this.imageService = imageService;
        }
        public async Task<About> GetAboutAsync()
        {
            var list = await unitOfWork.GetRepository<About>().GetAllAsync(null, x => x.Image);
            return list != null && list.Any() ? list.First() : null;

        }

        public async Task SaveAboutAsync(AboutDto aboutDto)
        {
            About about = await GetAboutAsync();
            bool firstSave = false;
            if (about == null)
            {
                about = new About { };
                firstSave = true;
            }
            about.Title = aboutDto.Title;
            about.Content = aboutDto.Content;
            if (aboutDto.ImageFile != null && aboutDto.ImageFile.Length > 0)
            {
                MemoryStream ms = new MemoryStream();
                aboutDto.ImageFile.CopyTo(ms);

                byte[] imageData = ms.ToArray();
                Guid imageId = await imageService.UploadImage(aboutDto.Title, aboutDto.ImageFile.ContentType, imageData);
                about.ImageId = imageId;
            }
            if (firstSave)
            {
                await unitOfWork.GetRepository<About>().AddAsync(about);
            }
            else
            {
                await unitOfWork.GetRepository<About>().UpdateAsync(about);
            }

            await unitOfWork.SaveAsync();
        }


    }
}

