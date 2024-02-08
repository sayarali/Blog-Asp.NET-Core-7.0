using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Entity.DTOs.Categories;
using Blog.Entity.DTOs.Posts;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Blog.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Yazar")]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly ICategoryService categoryService;
        private readonly IImageService imageService;

        public PostController(IPostService postService, ICategoryService categoryService, IImageService imageService)
        {
            this.postService = postService;
            this.categoryService = categoryService;
            this.imageService = imageService;
        }


        public async Task<IActionResult> Index()
        {
            var postsDtos = await postService.GetAllMyPosts();
            return View(postsDtos);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var categories = await categoryService.GetActiveCategoriesAsync();
            return View(new AddPostDto { Categories = categories });
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddPostDto postDto)
        {
            if (postDto.ImageFile != null && postDto.ImageFile.Length > 0)
            {
                MemoryStream ms = new MemoryStream();
                postDto.ImageFile.CopyTo(ms);
                
                byte[] imageData = ms.ToArray();
                Guid imageId = await imageService.UploadImage(postDto.Title, postDto.ImageFile.ContentType, imageData);
                postDto.ImageId = imageId;
                await postService.CreatePostAsync(postDto);
                return RedirectToAction("Index");
            } else
            {
                await postService.CreatePostAsync(postDto);
                return RedirectToAction("Index");
            }
            

            //var categories = await categoryService.GetActiveCategoriesAsync();
            //return View(new AddPostDto { Categories = categories });
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var post = await postService.GetPostForUpdateByIdAsync(id);
            post.Categories = await categoryService.GetActiveCategoriesAsync(); ;
            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdatePostDto updatePostDto)
        {
            if (updatePostDto.ImageFile != null && updatePostDto.ImageFile.Length > 0)
            {
                MemoryStream ms = new MemoryStream();
                updatePostDto.ImageFile.CopyTo(ms);

                byte[] imageData = ms.ToArray();
                Guid imageId = await imageService.UploadImage(updatePostDto.Title, updatePostDto.ImageFile.ContentType, imageData);
                updatePostDto.ImageId = imageId;
                await postService.UpdatePostAsync(updatePostDto);
                return RedirectToAction("Index");
            }
            else
            {
                await postService.UpdatePostAsync(updatePostDto);
                return RedirectToAction("Index");
            }
            //var post = await postService.UpdatePostAsync(updatePostDto);
            //post.Categories = await categoryService.GetActiveCategoriesAsync(); ;
            //return View(post);
        }

        public async Task<IActionResult> Delete(Guid postId)
        {
            await postService.DeletePostAsync(postId);
            return RedirectToAction("Index");
        }
    }
}

