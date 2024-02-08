using System;
using System.Reflection;
using Blog.DataAccess.Context;
using Blog.DataAccess.Repositories.Abstracts;
using Blog.DataAccess.Repositories.Concretes;
using Blog.DataAccess.UnitOfWorks;
using Blog.Service.Services.Abstracts;
using Blog.Service.Services.Concretes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Service.Extensions
{
	public static class ServiceLayerExtensions
	{
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IVisitorService, VisitorService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IAboutService, AboutService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(assembly);

            return services;
        }
    }
}

