using System;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.ViewComponents
{
	public class MostViewedMyViewComponent : ViewComponent
	{
        private readonly IPostService postService;

        public MostViewedMyViewComponent(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await postService.GetMyMostViewedAll();
            return View(posts);
        }
    }
}

