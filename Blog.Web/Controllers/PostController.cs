using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Entity.DTOs.Posts;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IMapper mapper;
        private readonly IVisitorService visitorService;

        public PostController(IPostService postService, IMapper mapper, IVisitorService visitorService)
        {
            this.postService = postService;
            this.mapper = mapper;
            this.visitorService = visitorService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index(Guid id)
        {
            var post = await postService.GetPostByIdAsync(id);
            await visitorService.PostViewCountIncrement(id);
            return View(mapper.Map<SmallPostDto>(post));
        }
    }
}

