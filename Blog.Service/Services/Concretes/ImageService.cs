using System;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Blog.Service.Services.Concretes
{
	public class ImageService : IImageService
	{
        private readonly IUnitOfWork unitofWork;

        public ImageService(IUnitOfWork unitofWork)
		{
            this.unitofWork = unitofWork;
        }

        public async Task<Guid> UploadImage(string fileName, string fileType, byte[] data)
        {
            var image = new Image { FileName = fileName, FileType = fileType, Data = data };
            Image savedImage = await unitofWork.GetRepository<Image>().AddAsync(image);
            await unitofWork.SaveAsync();
            return savedImage.Id;
        }

        public async Task<Image> GetImageByIdAsync(Guid imageId)
        {
            return await unitofWork.GetRepository<Image>().GetByGuidAsync(imageId);
          
        }
    }
}

