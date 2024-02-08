
using AutoMapper;
using Blog.Entity.DTOs.Auth;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<TUser> userManager;
        private readonly SignInManager<TUser> signInManager;
        private readonly IMapper mapper;

        public AuthController(UserManager<TUser> userManager, SignInManager<TUser> signInManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var username = await userManager.FindByNameAsync(userLoginDto.UserName);
                var email = await userManager.FindByEmailAsync(userLoginDto.UserName);
                if (username != null)
                {
                    var result = await signInManager.PasswordSignInAsync(username, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (User.IsInRole("Admin"))
                            return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        else
                            return RedirectToAction("Index", "Home", new { Area = "" });
                    } else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı ya da parola yanlış. Lütfen tekrar deneyiniz.");
                        return View();
                    }
                }
                else if (email != null)
                {
                    var result = await signInManager.PasswordSignInAsync(email, userLoginDto.Password, userLoginDto.RememberMe, false);
                    if (result.Succeeded)
                    {
                        if (User.IsInRole("Admin"))
                            return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        else
                            return RedirectToAction("Index", "Home", new { Area = "" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kullanıcı adı ya da parola yanlış. Lütfen tekrar deneyiniz.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı ya da parola yanlış. Lütfen tekrar deneyiniz.");
                    return View();
                }
            } else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (ModelState.IsValid)
            {
                TUser user = mapper.Map<TUser>(userRegisterDto);
                var result = await userManager.CreateAsync(user, userRegisterDto.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "KULLANICI");
                    var loginResult = await signInManager.PasswordSignInAsync(user, userRegisterDto.Password, false, false);
                    if (loginResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { Area = "" });
                    } else
                    {
                        return View();
                    }
                } else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new {Area = ""});
        }

        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}

