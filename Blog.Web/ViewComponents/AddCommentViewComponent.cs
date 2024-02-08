using System;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
	public class AddCommentViewComponent : ViewComponent
	{
        private readonly ICommentService commentService;

        public AddCommentViewComponent(ICommentService commentService)
		{
            this.commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            return View(new Comment { PostId = id});
        }

        
    }
}

