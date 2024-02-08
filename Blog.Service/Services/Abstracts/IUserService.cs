using System;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Service.Services.Abstracts
{
	public interface IUserService
	{
        Task<UpdateUserDto> GetUserByIdForUpdateAsync(Guid userId);
        Task<UserDto> GetUserByUsernameAsync(string username);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<IEnumerable<IdentityError>> SaveUserAsync(AddUserDto addUserDto);
        Task<IEnumerable<IdentityError>> UpdateUserAsync(UpdateUserDto updateUserDto);
        Task DeleteUserAsync(Guid userId);
        Task<TUser> GetCurrentUserAsync();
        Task<UserProfileDto> GetCurrentUserProfileDtoAsync();
        Task<IEnumerable<IdentityError>> UpdateProfileAsync(UserProfileDto userProfileDto);
        Task<IEnumerable<IdentityError>> ChangePasswordAsync(ChangePasswordDto model);
    }
}

