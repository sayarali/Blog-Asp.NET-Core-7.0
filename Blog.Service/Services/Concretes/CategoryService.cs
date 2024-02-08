using System;
using System.Security.Claims;
using AutoMapper;
using Blog.DataAccess.Extensions;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.DTOs.Posts;
using Blog.Entity.Entities;
using Blog.Service.Extensions;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Blog.Service.Services.Concretes
{
	public class CategoryService : ICategoryService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetActiveCategoriesAsync()
        {
            List<Category> categories = await unitOfWork.GetRepository<Category>().GetAllAsync(x => x.IsActive);
            return mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            List<Category> categories = await unitOfWork.GetRepository<Category>().GetAllAsync();
            return mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid Id)
        {
            Category category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(Id);
            return mapper.Map<CategoryDto>(category);
        }

        public async Task CreateCategoryAsync(CategoryDto categoryDto)
        {
            Category category = new Category
            {
                Name = categoryDto.Name,
                IsActive = categoryDto.IsActive
            };
            await unitOfWork.GetRepository<Category>().AddAsync(category);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(categoryDto.Id);
            category.Name = categoryDto.Name;
            category.IsActive = categoryDto.IsActive;
            await unitOfWork.GetRepository<Category>().UpdateAsync(category);
            await unitOfWork.SaveAsync();
            
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            Category category = await unitOfWork.GetRepository<Category>().GetByGuidAsync(id);
            await unitOfWork.GetRepository<Category>().DeleteAsync(category);
            await unitOfWork.SaveAsync();
        }
    }
}

