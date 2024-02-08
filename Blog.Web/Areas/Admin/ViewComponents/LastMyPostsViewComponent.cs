using System;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Areas.Admin.ViewComponents
{
	public class LastMyPostsViewComponent : ViewComponent
	{
        private readonly IPostService postService;

        public LastMyPostsViewComponent(IPostService postService)
		{
            this.postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await postService.GetLastMyPosts();
            return View(posts);
        }
    }
}

