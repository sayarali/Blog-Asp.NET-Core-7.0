using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Entity.DTOs.About;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService aboutService;
        private readonly IMapper mapper;

        public AboutController(IAboutService aboutService, IMapper mapper)
        {
            this.aboutService = aboutService;
            this.mapper = mapper;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var about = await aboutService.GetAboutAsync();
            if (about == null)
            {
                return View(new AboutDto { });
            }
            else
            {
                return View(mapper.Map<AboutDto>(about));
            }

        }


        public async Task<IActionResult> Update(AboutDto aboutDto)
        {
            await aboutService.SaveAboutAsync(aboutDto);
            return RedirectToAction("Index");
        }
    }
}

