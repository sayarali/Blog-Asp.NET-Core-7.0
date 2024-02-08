using System;
using AutoMapper;
using Blog.Entity.DTOs.Posts;
using Blog.Entity.Entities;

namespace Blog.Service.AutoMapper.Posts
{
	public class PostProfile : Profile
	{
		public PostProfile()
		{
			CreateMap<PostsDto, Post>().ReverseMap();
			CreateMap<UpdatePostDto, Post>().ReverseMap();
            CreateMap<SmallPostDto, Post>().ReverseMap();
            CreateMap<MostViewPostsDto, Post>().ReverseMap();
            CreateMap<TitleDatePostDto, Post>().ReverseMap();
        }
	}
}

