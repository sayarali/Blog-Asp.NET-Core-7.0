using System;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;

namespace Blog.Service.Services.Concretes
{
	public class CommentService : ICommentService
	{
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageService imageService;
        private readonly IPostService postService;
        private readonly IUserService userService;

        public CommentService(IUnitOfWork unitOfWork, IImageService imageService, IPostService postService, IUserService userService)
		{
            this.unitOfWork = unitOfWork;
            this.imageService = imageService;
            this.postService = postService;
            this.userService = userService;
        }

        public async Task SaveCommentAsync(Comment comment)
        {
            await unitOfWork.GetRepository<Comment>().AddAsync(comment);
            await unitOfWork.SaveAsync();
        }

		public async Task<List<Comment>> GetAllCommentsAsync(Guid postId)
		{
            var comments = await unitOfWork.GetRepository<Comment>().GetAllAsync(x => x.PostId == postId && x.IsActive, u => u.User);
            foreach (var item in comments)
            {
                if(item.UserId != null)
                {
                    if (item.User.ImageId != null)
                    {
                        item.User.Image = await imageService.GetImageByIdAsync(item.User.ImageId.Value);
                    }
                }
              
            }
            return comments;
        }


        public async Task<List<Comment>> GetAllMyPostsCommentsAsync()
        {
            var user = await userService.GetCurrentUserAsync();
            var comments = await unitOfWork.GetRepository<Comment>().GetAllAsync(x => x.Post.UserId == user.Id,p => p.Post, u => u.User);
            foreach (var item in comments)
            {
                if (item.UserId != null)
                {
                    if (item.User.ImageId != null)
                    {
                        item.User.Image = await imageService.GetImageByIdAsync(item.User.ImageId.Value);
                    }
                }

            }
            comments = comments.OrderByDescending(x => x.CreatedTime).ToList();
            return comments;
        }

        public async Task SetCommentStatus(Guid id, bool status)
        {
            var comment = await unitOfWork.GetRepository<Comment>().GetByGuidAsync(id);
            comment.IsActive = status;
            await unitOfWork.GetRepository<Comment>().UpdateAsync(comment);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteComment(Guid id)
        {
            var comment = await unitOfWork.GetRepository<Comment>().GetByGuidAsync(id);
            await unitOfWork.GetRepository<Comment>().DeleteAsync(comment);
            await unitOfWork.SaveAsync();
        }

    }
}

