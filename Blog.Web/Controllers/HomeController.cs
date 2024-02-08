using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Blog.Web.Models;
using Blog.Service.Services.Abstracts;
using Blog.Service.Services.Concretes;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;

namespace Blog.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPostService postService;
    private readonly IImageService imageService;
    private readonly IContactService contactService;
    private readonly IAboutService aboutService;

    public HomeController(
        ILogger<HomeController> logger,
        IPostService postService,
        IImageService imageService,
        IContactService contactService,
        IAboutService aboutService)
    {
        _logger = logger;
        this.postService = postService;
        this.imageService = imageService;
        this.contactService = contactService;
        this.aboutService = aboutService;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var posts = await postService.GetAllPostsPagingAsync(categoryId: null, currentPage: page, pageSize: 5, isAscending: false);
        foreach (var item in posts.Posts)
        {
            if(item.User.ImageId != null)
            {
                item.User.Image = await imageService.GetImageByIdAsync(item.User.ImageId.Value);
            }
            
        }
        return View(posts);
    }

   
  
    public async Task<IActionResult> AboutMe()
    {
        var about = await aboutService.GetAboutAsync();
        return View(about);
    }
    public IActionResult ContactMe()
    {
        return View();
    }

    public async Task<IActionResult> SendContact(Contact contact)
    {
        await contactService.AddContactMessageAsync(contact);
        return RedirectToAction("ContactMe");
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

