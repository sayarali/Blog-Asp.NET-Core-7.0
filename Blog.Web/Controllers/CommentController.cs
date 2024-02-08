using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IUserService userService;

        public CommentController(ICommentService commentService, IUserService userService)
        {
            this.commentService = commentService;
            this.userService = userService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(Comment comment)
        {
            if (User.Identity.IsAuthenticated)
            {
                comment.UserId = (await userService.GetCurrentUserAsync()).Id;
            }
            await commentService.SaveCommentAsync(comment);
            return RedirectToAction("Index", "Post", new { id = comment.PostId });
        }
    }
}

