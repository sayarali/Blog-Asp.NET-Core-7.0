using System;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Abstracts
{
	public interface ICommentService
	{
        Task SaveCommentAsync(Comment comment);
        Task<List<Comment>> GetAllCommentsAsync(Guid postId);
        Task<List<Comment>> GetAllMyPostsCommentsAsync();
        Task SetCommentStatus(Guid id, bool status);
        Task DeleteComment(Guid id);
    }
}

