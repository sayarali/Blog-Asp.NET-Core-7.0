using System;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Http;

namespace Blog.Service.Services.Concretes
{
	public class VisitorService : IVisitorService
	{
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IPostService postService;
        private readonly IUnitOfWork unitOfWork;

        public VisitorService(IHttpContextAccessor httpContextAccessor, IPostService postService, IUnitOfWork unitOfWork)
		{
            this.httpContextAccessor = httpContextAccessor;
            this.postService = postService;
            this.unitOfWork = unitOfWork;
        }

        public async Task PostViewCountIncrement(Guid postId)
        {
            try
            {
                var ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                var postVisitors = await unitOfWork.GetRepository<PostVisitor>().GetAllAsync(null, x => x.Visitor, y => y.Post);
                var post = await postService.GetPostByIdAsync(postId);
                var visitor = await unitOfWork.GetRepository<Visitor>().GetAsync(x => x.IpAddress == ipAddress);

                if (!postVisitors.Any(x => x.PostId == postId && x.VisitorId == visitor.Id))
                {
                    await unitOfWork.GetRepository<PostVisitor>().AddAsync(new PostVisitor(postId, visitor.Id));
                    post.ViewCount += 1;
                    await unitOfWork.GetRepository<Post>().UpdateAsync(post);
                    unitOfWork.Save();
                }
            }
            catch (Exception ex)
            {

            }
        }
	}
}

