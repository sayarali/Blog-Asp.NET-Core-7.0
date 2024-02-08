using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Yazar")]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var comments = await commentService.GetAllMyPostsCommentsAsync();
            return View(comments);
        }


        public async Task<IActionResult> SetActive(Guid commentId)
        {
            await commentService.SetCommentStatus(commentId, true);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SetDeactive(Guid commentId)
        {
            await commentService.SetCommentStatus(commentId, false);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid commentId)
        {
            await commentService.DeleteComment(commentId);
            return RedirectToAction("Index");
        }
    }
}

