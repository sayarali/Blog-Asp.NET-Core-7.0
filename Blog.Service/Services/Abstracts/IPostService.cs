using System;
using Blog.Entity.DTOs.Posts;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Abstracts
{
    public interface IPostService
    {
        Task<Post> GetPostByIdAsync(Guid Id);
        Task<List<PostsDto>> GetAllMyPosts();
        Task<PostsPageDto> GetAllPostsPagingAsync(Guid? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false);
        Task CreatePostAsync(AddPostDto postDto);
        Task<UpdatePostDto> GetPostForUpdateByIdAsync(Guid Id);
        Task<UpdatePostDto> UpdatePostAsync(UpdatePostDto updatePostDto);
        Task DeletePostAsync(Guid id);
        Task<List<MostViewPostsDto>> GetMostViewedPostAsync();
        Task<SearchResultDto> Search(string s, int currentPage = 1, int pageSize = 5, bool isAscending = false);
        Task<List<TitleDatePostDto>> GetLastMyPosts();
        Task<List<MostViewPostsDto>> GetMostViewedThisMonth();
        Task<List<MostViewPostsDto>> GetMyMostViewedAll();
    }
}

