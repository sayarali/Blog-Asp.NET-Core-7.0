using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPostService postService;

        public SearchController(IPostService postService)
        {
            this.postService = postService;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(string s, int page = 1)
        {
            var result = await postService.Search(s: s, currentPage: page, pageSize: 10, isAscending: false);
            
            return View(result);
        }
    }
}

