using System;
using Blog.Entity.DTOs.Categories;
using Blog.Service.Services.Abstracts;
using Blog.Service.Services.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.ViewComponents
{
	public class CommentsViewComponent : ViewComponent
	{
        private readonly ICommentService commentService;

        public CommentsViewComponent(ICommentService commentService)
		{
            this.commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id)
        {
            var comments = await commentService.GetAllCommentsAsync(id);
            return View(comments);
        }
    }
}

