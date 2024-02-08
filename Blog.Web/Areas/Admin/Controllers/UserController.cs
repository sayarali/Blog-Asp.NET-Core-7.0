using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Blog.Service.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }
        
        public async Task<IActionResult> Index()
        {
            List<UserDto> userDtos = await userService.GetAllUsersAsync();
            return View(userDtos);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            List<TRole> roles = await roleService.GetAllRolesAsync();
            return View(new AddUserDto { Roles = roles});
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddUserDto addUserDto)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<IdentityError> errors = await userService.SaveUserAsync(addUserDto);
                if (errors == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in errors)
                        ModelState.AddModelError("", error.Description);
                     
                    List<TRole> roles = await roleService.GetAllRolesAsync();
                    return View(new AddUserDto { Roles = roles });
                }
            } else
            {
                List<TRole> roles = await roleService.GetAllRolesAsync();
                return View(new AddUserDto { Roles = roles });
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid userId)
        {
            UpdateUserDto user = await userService.GetUserByIdForUpdateAsync(userId);
            user.Roles = await roleService.GetAllRolesAsync();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<IdentityError> errors = await userService.UpdateUserAsync(updateUserDto);
                if (errors == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in errors)
                        ModelState.AddModelError("", error.Description);

                    UpdateUserDto user = await userService.GetUserByIdForUpdateAsync(updateUserDto.Id);
                    user.Roles = await roleService.GetAllRolesAsync();
                    return View(user);
                }
            } else
            {
                UpdateUserDto user = await userService.GetUserByIdForUpdateAsync(updateUserDto.Id);
                user.Roles = await roleService.GetAllRolesAsync();
                return View(user);
            }
        }

        public async Task<IActionResult> Delete(Guid userId)
        {
            await userService.DeleteUserAsync(userId);
            return RedirectToAction("Index");
        }
    }
}

