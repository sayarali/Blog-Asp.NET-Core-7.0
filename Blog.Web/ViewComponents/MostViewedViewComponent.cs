using System;
using Blog.Entity.DTOs.Categories;
using Blog.Service.Services.Abstracts;
using Blog.Service.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
	public class MostViewedViewComponent : ViewComponent
	{
        private readonly IPostService postService;

        public MostViewedViewComponent(IPostService postService)
		{
            this.postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await postService.GetMostViewedPostAsync();
            return View(posts);
        }
    }
}

