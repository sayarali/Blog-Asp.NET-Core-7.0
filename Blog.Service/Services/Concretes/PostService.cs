using System;
using Blog.Entity.Entities;
using Blog.DataAccess.UnitOfWorks;

using Blog.Service.Services.Abstracts;
using Blog.Entity.DTOs.Posts;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Security.Claims;
using Blog.Service.Extensions;
using Blog.DataAccess.Extensions;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;
using Blog.DataAccess.Context;

namespace Blog.Service.Services.Concretes
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IImageService imageService;
        private readonly ClaimsPrincipal user;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.imageService = imageService;
            user = httpContextAccessor.HttpContext.User;
        }

        public async Task<Post> GetPostByIdAsync(Guid Id)
        {
            var post = await unitOfWork.GetRepository<Post>().GetAsync(x => x.Id == Id, x => x.Category, x => x.Image, x => x.User);
            if (post.User.ImageId != null)
            {
                post.User.Image = await imageService.GetImageByIdAsync(post.User.ImageId.Value);
            }
            return post;
        }

        public async Task<List<PostsDto>> GetAllMyPosts()
        {
            if (user != null)
            {
                var posts = await unitOfWork.GetRepository<Post>()
                    .GetAllAsync(x => x.UserId == user.GetUserId(), x => x.Category);

                if (posts != null && posts.Any())
                {
                    posts = posts.OrderByDescending(x => x.CreatedTime).ToList();

                    return mapper.Map<List<PostsDto>>(posts);
                }
            }
            return null;
        }

        public async Task<PostsPageDto> GetAllPostsPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;

            List<Post> posts = categoryId == null ?
                await unitOfWork.GetRepository<Post>().GetAllAsync(x => x.IsActive && x.Category.IsActive, c => c.Category, i => i.Image, u => u.User) :
                await unitOfWork.GetRepository<Post>().GetAllAsync(x => x.CategoryId == categoryId && x.IsActive == x.Category.IsActive, c => c.Category, i => i.Image, u => u.User, u => u.User.Image);

            List<Post> sortedPosts = isAscending
                ? posts.OrderBy(x => x.CreatedTime).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : posts.OrderByDescending(x => x.CreatedTime).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PostsPageDto
            {
                Posts = mapper.Map<List<SmallPostDto>>(sortedPosts),
                CategoryId = categoryId == null ? null : categoryId.Value,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = posts.Count,
                IsAscending = isAscending
            };
        }

        public async Task CreatePostAsync(AddPostDto postDto)
        {
            Post post = new Post
            {
                Title = postDto.Title,
                Content = postDto.Content,
                CategoryId = postDto.CategoryId,
                UserId = user.GetUserId(),
                ImageId = postDto.ImageId,
                IsActive = postDto.IsActive
            };
            await unitOfWork.GetRepository<Post>().AddAsync(post);
            await unitOfWork.SaveAsync();
        }

        public async Task<UpdatePostDto> GetPostForUpdateByIdAsync(Guid Id)
        {
            var post = await unitOfWork.GetRepository<Post>().GetAsync(x=>x.Id == Id, x=>x.Category, x=>x.Image);

            var map = mapper.Map<UpdatePostDto>(post);
            return map;
        }

        public async Task<UpdatePostDto> UpdatePostAsync(UpdatePostDto updatePostDto)
        {
            var post = await unitOfWork.GetRepository<Post>().GetByGuidAsync(updatePostDto.Id);
            post.Title = updatePostDto.Title;
            post.Content = updatePostDto.Content;
            post.CategoryId = updatePostDto.CategoryId;
            if (updatePostDto.ImageId != null)
            {
                post.ImageId = updatePostDto.ImageId;
            }
            post.IsActive = updatePostDto.IsActive;
            var updatedPost = await unitOfWork.GetRepository<Post>().UpdateAsync(post);
            await unitOfWork.SaveAsync();
            var map = mapper.Map<UpdatePostDto>(updatedPost);
            return map;
        }

        public async Task DeletePostAsync(Guid id)
        {
            Post post = await unitOfWork.GetRepository<Post>().GetByGuidAsync(id);
            await unitOfWork.GetRepository<Post>().DeleteAsync(post);
            await unitOfWork.SaveAsync();
        }

        public async Task<List<MostViewPostsDto>> GetMostViewedPostAsync()
        {
            var posts = await unitOfWork.GetRepository<Post>()
                    .GetAllAsync(x => x.IsActive && x.Category.IsActive);

            posts = posts.OrderByDescending(x => x.ViewCount).Take(5).ToList();

            return mapper.Map<List<MostViewPostsDto>>(posts);
        }

        public async Task<SearchResultDto> Search(string s, int currentPage, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;

            List<Post> posts =
                await unitOfWork.GetRepository<Post>().GetAllAsync(x => x.IsActive && x.Category.IsActive && (x.Title.ToLower().Contains(s.ToLower()) || x.Content.ToLower().Contains(s.ToLower()) || x.Category.Name.ToLower().Contains(s.ToLower())), c => c.Category, i => i.Image, u => u.User);

            List<Post> sortedPosts = isAscending
                ? posts.OrderBy(x => x.CreatedTime).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList()
                : posts.OrderByDescending(x => x.CreatedTime).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new SearchResultDto
            {
                SearchText = s,
                Posts = mapper.Map<List<SmallPostDto>>(sortedPosts),
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = posts.Count,
                IsAscending = isAscending
            };
        }

        public async Task<List<TitleDatePostDto>> GetLastMyPosts()
        {
            if (user != null)
            {
                var posts = await unitOfWork.GetRepository<Post>()
                    .GetAllAsync(x => x.UserId == user.GetUserId(), x => x.Category);

                if (posts != null && posts.Any())
                {
                    posts = posts.OrderByDescending(x => x.CreatedTime).Take(5).ToList();

                    return mapper.Map<List<TitleDatePostDto>>(posts);
                }
            }
            return null;
        }

        public async Task<List<MostViewPostsDto>> GetMostViewedThisMonth()
        {
            if (user != null)
            {
                DateTime now = DateTime.UtcNow;
                DateTime firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
                DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

                var postVisitors = await unitOfWork.GetRepository<PostVisitor>().GetAllAsync(
                    x => x.CreatedTime >= firstDayOfMonth.ToUniversalTime() && x.CreatedTime <= lastDayOfMonth.ToUniversalTime() && x.Post.UserId == user.GetUserId(),
                    p => p.Post, v => v.Visitor);


                if (postVisitors != null && postVisitors.Any())
                {
                    List<MostViewPostsDto> mostViewPostsDtos = new();
                    for (int i = 0; i < postVisitors.Count; i++)
                    {
                        MostViewPostsDto existingPost = mostViewPostsDtos.FirstOrDefault(x => x.Id == postVisitors[i].Post.Id);
                        if (existingPost != null)
                        {
                            existingPost.ViewCount++;
                        }
                        else
                        {
                            MostViewPostsDto mostViewPostsDto = new MostViewPostsDto
                            {
                                Id = postVisitors[i].Post.Id,
                                Title = postVisitors[i].Post.Title,
                                ViewCount = 1
                            };
                            mostViewPostsDtos.Add(mostViewPostsDto);
                        }
                    }
                    mostViewPostsDtos = mostViewPostsDtos.OrderByDescending(x => x.ViewCount).ToList();
                    return mostViewPostsDtos;
                }
            }
            return null;
        }

        public async Task<List<MostViewPostsDto>> GetMyMostViewedAll()
        {
            if (user != null)
            {
                var posts = await unitOfWork.GetRepository<Post>()
                  .GetAllAsync(x => x.IsActive && x.Category.IsActive && x.UserId == user.GetUserId());

                posts = posts.OrderByDescending(x => x.ViewCount).Take(5).ToList();

                return mapper.Map<List<MostViewPostsDto>>(posts);
            }
            return null;
        }
    }
}

