using Blog.Entity.DTOs.Categories;
using Blog.Entity.DTOs.Posts;
using Blog.Service.Services.Abstracts;
using Blog.Service.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            List<CategoryDto> categoryDto = await categoryService.GetAllCategoriesAsync();
            return View(categoryDto);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View(new CategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryDto categoryDto)
        {
            await categoryService.CreateCategoryAsync(categoryDto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid categoryId)
        {
            await categoryService.DeleteCategoryAsync(categoryId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var category = await categoryService.GetCategoryByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            await categoryService.UpdateCategoryAsync(categoryDto);
            return RedirectToAction("Index");
        }
    }
}

