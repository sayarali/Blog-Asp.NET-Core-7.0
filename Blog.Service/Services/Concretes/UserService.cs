using AutoMapper;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Blog.Service.Services.Concretes
{
	public class UserService : IUserService
	{
        private readonly UserManager<TUser> userManager;
        private readonly IMapper mapper;
        private readonly IImageService imageService;
        private readonly IRoleService roleService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(
            UserManager<TUser> userManager,
            IMapper mapper,
            IImageService imageService,
            IRoleService roleService,
            IHttpContextAccessor httpContextAccessor)
		{
            this.userManager = userManager;
            this.mapper = mapper;
            this.imageService = imageService;
            this.roleService = roleService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<UpdateUserDto> GetUserByIdForUpdateAsync(Guid userId)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            return mapper.Map<UpdateUserDto>(user);
        }

        public async Task<UserDto> GetUserByUsernameAsync(string username)
        {
            var user = await userManager.FindByNameAsync(username);
            var userDto = mapper.Map<UserDto>(user);
            userDto.Role = string.Join("", await userManager.GetRolesAsync(user));
            if (userDto.Image != null)
            {
                userDto.Image = await imageService.GetImageByIdAsync(userDto.Image.Id);
            }
            return userDto;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = userManager.Users.ToList();
            var map = mapper.Map<List<UserDto>>(users);

            foreach (var item in map)
            {
                var user = await userManager.FindByIdAsync(item.Id.ToString());
                item.Role = string.Join("", await userManager.GetRolesAsync(user));
                if (item.Image != null)
                {
                    item.Image = await imageService.GetImageByIdAsync(item.Image.Id);
                }
              
            }

            return map;
        }

        public async Task<IEnumerable<IdentityError>> SaveUserAsync(AddUserDto addUserDto)
        {
            var map = mapper.Map<TUser>(addUserDto);

            var result = await userManager.CreateAsync(map, addUserDto.Password);

            if (result.Succeeded)
            {
                var role = await roleService.GetRoleByIdAsync(addUserDto.RoleId);
                await userManager.AddToRoleAsync(map, role.NormalizedName);
                return null;
            } else
            {
                return result.Errors;
            }
        }

        public async Task<IEnumerable<IdentityError>> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            TUser user = await userManager.FindByIdAsync(updateUserDto.Id.ToString());

            string currentRole = string.Join("", await userManager.GetRolesAsync(user));
            
            if (user.Email != updateUserDto.Email)
            {
                string token = await userManager.GenerateChangeEmailTokenAsync(user, updateUserDto.Email);
                await userManager.ChangeEmailAsync(user, updateUserDto.Email, token);
                user.EmailConfirmed = false;
            }
            if (user.PhoneNumber != updateUserDto.PhoneNumber)
            {
                string token = await userManager.GenerateChangePhoneNumberTokenAsync(user, updateUserDto.PhoneNumber);
                await userManager.ChangePhoneNumberAsync(user, updateUserDto.PhoneNumber, token);
                user.PhoneNumberConfirmed = false;
            }
            if (user.UserName != updateUserDto.UserName)
            {
                await userManager.SetUserNameAsync(user, updateUserDto.UserName);
            }
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.SecurityStamp = Guid.NewGuid().ToString();
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                if (currentRole !=  null && currentRole != "")
                {
                    await userManager.RemoveFromRoleAsync(user, currentRole.ToUpper());
                }
                TRole newRole = await roleService.GetRoleByIdAsync(updateUserDto.RoleId);
                await userManager.AddToRoleAsync(user, newRole.NormalizedName);
                return null;
            }
            else
            {
                return result.Errors;
            }
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            TUser user = await userManager.FindByIdAsync(userId.ToString());
            await userManager.DeleteAsync(user);
        }
        public async Task<TUser> GetCurrentUserAsync()
        {
            return await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
        }

        public async Task<UserProfileDto> GetCurrentUserProfileDtoAsync()
        {
            TUser user = await userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            UserProfileDto userProfileDto = mapper.Map<UserProfileDto>(user);
            if(user.ImageId != null)
            {
                userProfileDto.Image = await imageService.GetImageByIdAsync(user.ImageId.Value);
            }
         
            return userProfileDto;
        }

        public async Task<IEnumerable<IdentityError>> UpdateProfileAsync(UserProfileDto userProfileDto)
        {
            TUser user = await userManager.FindByIdAsync(userProfileDto.Id.ToString());
            if (user.UserName != userProfileDto.UserName)
            {
                var res = await userManager.SetUserNameAsync(user, userProfileDto.UserName);
                if (!res.Succeeded)
                {
                    return res.Errors;
                }
            }
            if (user.Email != userProfileDto.Email)
            {
                string token = await userManager.GenerateChangeEmailTokenAsync(user, userProfileDto.Email);
                await userManager.ChangeEmailAsync(user, userProfileDto.Email, token);
                user.EmailConfirmed = false;
            }
            if (user.PhoneNumber != userProfileDto.PhoneNumber)
            {
                string token = await userManager.GenerateChangePhoneNumberTokenAsync(user, userProfileDto.PhoneNumber);
                await userManager.ChangePhoneNumberAsync(user, userProfileDto.PhoneNumber, token);
                user.PhoneNumberConfirmed = false;
            }
           
            if (userProfileDto.ImageFile != null && userProfileDto.ImageFile.Length > 0)
            {
                MemoryStream ms = new MemoryStream();
                userProfileDto.ImageFile.CopyTo(ms);

                byte[] imageData = ms.ToArray();
                Guid imageId = await imageService.UploadImage(userProfileDto.UserName, userProfileDto.ImageFile.ContentType, imageData);
                user.ImageId = imageId;
            }
            user.FirstName = userProfileDto.FirstName;
            user.LastName = userProfileDto.LastName;
            user.SecurityStamp = Guid.NewGuid().ToString();
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                
                return null;
            }
            else
            {
                return result.Errors;
            }

        }

        public async Task<IEnumerable<IdentityError>> ChangePasswordAsync(ChangePasswordDto model)
        {
            TUser user = await userManager.FindByIdAsync(model.Id.ToString());
            if (user != null)
            {
                var result = userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Result.Succeeded)
                {
                    return null;
                } else
                {
                    return result.Result.Errors;

                }
            }
            return null;
        }
    }
}

