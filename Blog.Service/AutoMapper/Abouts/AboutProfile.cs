using System;
using AutoMapper;
using Blog.Entity.DTOs.About;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.Entities;

namespace Blog.Service.AutoMapper.Abouts
{
	public class AboutProfile : Profile
	{
		public AboutProfile()
		{
            CreateMap<AboutDto, About>().ReverseMap();
        }
	}
}

