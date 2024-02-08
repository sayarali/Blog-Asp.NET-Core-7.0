﻿using System;
using System.Linq.Expressions;
using Blog.DataAccess.Context;
using Blog.DataAccess.Repositories.Abstracts;
using Blog.Core.Base;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Repositories.Concretes
{
	public class Repository<T> : IRepository<T> where T : class, IBaseEntity, new()
	{
        private readonly AppDbContext dbContext;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private DbSet<T> Table { get => dbContext.Set<T>(); }

        public async Task<T> AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await Table.AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
        {
            return await Table.CountAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, Object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            if (predicate != null) query = query.Where(predicate);
            if (includeProperties.Any())
                foreach (var item in includeProperties)
                    query = query.Include(item);
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);

            if (includeProperties.Any())
                foreach (var item in includeProperties)
                    query = query.Include(item);
            return await query.SingleAsync();
        }

        public async Task<T> GetByGuidAsync(Guid id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }
    }
}

