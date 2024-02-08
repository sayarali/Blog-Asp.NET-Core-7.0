using System;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Abstracts
{
	public interface IImageService
	{
        Task<Guid> UploadImage(string fileName, string fileType, byte[] data);
        Task<Image> GetImageByIdAsync(Guid imageId);
    }
}

