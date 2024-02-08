using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Blog.Service.Services.Concretes;
using Blog.Web.ViewComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Controllers
{
   
    public class ProfileController : Controller
    {
        private readonly IUserService userService;
        private readonly SignInManager<TUser> signInManager;

        public ProfileController(IUserService userService, SignInManager<TUser> signInManager)
        {
            this.userService = userService;
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> Index(string username)
        {
            UserDto userDto = await userService.GetUserByUsernameAsync(username);
            return View(userDto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            UserProfileDto userProfileDto = await userService.GetCurrentUserProfileDtoAsync();
            return View(userProfileDto);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(UserProfileDto userProfileDto)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<IdentityError> errors = await userService.UpdateProfileAsync(userProfileDto);
                if (errors == null)
                {
                    TUser user = await userService.GetCurrentUserAsync();
                    await signInManager.RefreshSignInAsync(user);

                    return RedirectToAction("Index", new { username = userProfileDto.UserName });
                }
                else
                {
                    foreach (var error in errors)
                        ModelState.AddModelError("", error.Description);

                    UserProfileDto use = await userService.GetCurrentUserProfileDtoAsync();
                    if (use.Image != null)
                    {
                        userProfileDto.Image = use.Image;
                    }
                   
                    return View(userProfileDto);
                }
            }
            else
            {
                return View(userProfileDto);
            }
           
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            TUser user = await userService.GetCurrentUserAsync();
            return View(new ChangePasswordDto { Id = user.Id });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (ModelState.IsValid)
            {
                if (model.NewPassword == model.ConfirmPassword)
                {
                    IEnumerable<IdentityError> errors = await userService.ChangePasswordAsync(model);
                    if (errors == null)
                    {
                        TUser user = await userService.GetCurrentUserAsync();
                        await signInManager.RefreshSignInAsync(user);
                        return RedirectToAction("Index", new { username = user.UserName });
                    }
                    else
                    {
                        foreach (var error in errors)
                            ModelState.AddModelError("", error.Description);

                        UserProfileDto use = await userService.GetCurrentUserProfileDtoAsync();
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Parolalar eşleşmiyor.");
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
          
        }


    }
}

