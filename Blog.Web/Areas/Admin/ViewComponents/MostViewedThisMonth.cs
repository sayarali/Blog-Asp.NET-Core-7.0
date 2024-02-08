using System;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.ViewComponents
{
	public class MostViewedThisMonth : ViewComponent
	{
        private readonly IPostService postService;

        public MostViewedThisMonth(IPostService postService)
		{
            this.postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await postService.GetMostViewedThisMonth();
            return View(posts);
        }
    }
}

