using System;
using Blog.DataAccess.Repositories.Abstracts;
using Blog.Core.Base;

namespace Blog.DataAccess.UnitOfWorks
{
	public interface IUnitOfWork : IAsyncDisposable
	{
		IRepository<T> GetRepository<T>() where T : class, IBaseEntity, new();

		Task<int> SaveAsync();
		int Save();
	}
}

