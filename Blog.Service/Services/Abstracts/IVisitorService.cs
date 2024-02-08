using System;
namespace Blog.Service.Services.Abstracts
{
	public interface IVisitorService 
	{
        Task PostViewCountIncrement(Guid postId);
    }
}

