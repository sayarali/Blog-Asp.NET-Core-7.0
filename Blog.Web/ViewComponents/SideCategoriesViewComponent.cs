using System;
using Blog.Entity.DTOs.Categories;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
	public class SideCategoriesViewComponent : ViewComponent
	{
        private readonly ICategoryService categoryService;

        public SideCategoriesViewComponent(ICategoryService categoryService)
		{
            this.categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<CategoryDto> categoryDtos = await categoryService.GetActiveCategoriesAsync();
            return View(categoryDtos);
        }
    }
}

