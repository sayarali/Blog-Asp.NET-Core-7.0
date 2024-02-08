using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Yazar")]
    public class HomeController : Controller
    {
        private readonly UserManager<TUser> userManager;

        public HomeController(UserManager<TUser> userManager)
        {
            this.userManager = userManager;
        }

     
        public async Task<IActionResult> Index()
        {
            TUser user = await userManager.GetUserAsync(User);
            return View(user);
        }
    }
}

