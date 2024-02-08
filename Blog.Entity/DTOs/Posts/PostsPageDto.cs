using System;
using Blog.Entity.Entities;

namespace Blog.Entity.DTOs.Posts
{
	public class PostsPageDto
	{
		public IList<SmallPostDto> Posts { get; set; }
		public Guid? CategoryId { get; set; }
		public virtual int CurrentPage { get; set; } = 1;
		public virtual int PageSize { get; set; } = 5;
		public virtual int TotalCount { get; set; }
		public virtual int TotalPages => (int) Math.Ceiling(decimal.Divide(TotalCount, PageSize));
		public virtual bool ShowPrevious => CurrentPage > 1;
		public virtual bool ShowNext => CurrentPage < TotalPages;
		public virtual bool IsAscending { get; set; }

	}
}

