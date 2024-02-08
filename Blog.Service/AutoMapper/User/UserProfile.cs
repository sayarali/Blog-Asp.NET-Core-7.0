using System;
using AutoMapper;
using Blog.Entity.DTOs.Posts;
using Blog.Entity.DTOs.User;
using Blog.Entity.Entities;

namespace Blog.Service.AutoMapper.User
{
	public class UserProfile : Profile
	{
        public UserProfile()
        {
            CreateMap<UserDto, TUser>().ReverseMap();
            CreateMap<AddUserDto, TUser>().ReverseMap();
            CreateMap<UpdateUserDto, TUser>().ReverseMap();
            CreateMap<UserProfileDto, TUser>().ReverseMap();
            CreateMap<UserRegisterDto, TUser>().ReverseMap();
        }
    }
}

