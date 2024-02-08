using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entity.DTOs.Categories;
using Blog.Service.Services.Abstracts;
using Blog.Service.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IPostService postService;
        private readonly IImageService imageService;

        public CategoryController(IPostService postService, IImageService imageService)
        {
            this.postService = postService;
            this.imageService = imageService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(Guid id, int page = 1)
        {
            var posts = await postService.GetAllPostsPagingAsync(categoryId: id, currentPage: page, pageSize: 5, isAscending: false);
            foreach (var item in posts.Posts)
            {
                if (item.User.ImageId != null)
                {
                    item.User.Image = await imageService.GetImageByIdAsync(item.User.ImageId.Value);
                }

            }
            return View(posts);
        }
    }
}

