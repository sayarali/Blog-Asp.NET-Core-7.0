using System;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Abstracts
{
	public interface ICategoryService
	{
		Task<CategoryDto> GetCategoryByIdAsync(Guid Id);
		Task<List<CategoryDto>> GetActiveCategoriesAsync();
        Task<List<CategoryDto>> GetAllCategoriesAsync();
        Task CreateCategoryAsync(CategoryDto categoryDto);
        Task UpdateCategoryAsync(CategoryDto categoryDto);
        Task DeleteCategoryAsync(Guid id);

    }
}

