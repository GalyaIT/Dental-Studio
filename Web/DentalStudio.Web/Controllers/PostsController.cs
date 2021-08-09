namespace DentalStudio.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Common;
    using DentalStudio.Data.Models;
    using DentalStudio.Services;
    using DentalStudio.Services.Data;
    using DentalStudio.Services.Models;
    using DentalStudio.Web.ViewModels.Posts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PostsController : BaseController
    {
        private readonly IPostsService postsService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
           IPostsService postsService,
           ICloudinaryService cloudinaryService,
           UserManager<ApplicationUser> userManager)
        {
            this.postsService = postsService;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
        }

        // Create Post
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateInputModel model)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string imageUrl = await this.cloudinaryService.UploadPhotoAsync(
                                  model.ImageUrl,
                                  "Blog",
                                  GlobalConstants.CloudFolderForBlogPhotos);

            var postServiceModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<PostServiceModel>(model);
            postServiceModel.ImageUrl = imageUrl;
            var postId = await this.postsService.Create(postServiceModel, user.Id);

            this.TempData["InfoMessage"] = InfoMessages.PostCreateSuccessMessage;
            return this.RedirectToAction("All");
        }

        // Edit Post
        [HttpGet(Name = "Edit")]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await this.postsService.GetById<PostServiceModel>(id);
            if (post == null)
            {
                return this.NotFound();
            }

            var postEditViewModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<PostEditViewModel>(post);
            return this.View(postEditViewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostEditViewModel postEditViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(postEditViewModel);
            }

            var url = this.postsService.GetUrl(id);

            string imageUrl = await this.cloudinaryService.UploadPhotoAsync(
                postEditViewModel.ImageUrl,
                "Blog",
                GlobalConstants.CloudFolderForBlogPhotos);

            var postServiceModel = Services.Mapping.AutoMapperConfig.MapperInstance.Map<PostServiceModel>(postEditViewModel);
            if (postEditViewModel.ImageUrl == null)
            {
                postServiceModel.ImageUrl = url;
            }
            else
            {
                postServiceModel.ImageUrl = imageUrl;
            }

            var postId = await this.postsService.Edit(id, postServiceModel);
            this.TempData["InfoMessage"] = InfoMessages.PostEditSuccessMessage;
            return this.RedirectToAction("Details", "Posts", new { id = postId });
        }

        // Post Details
        [HttpGet(Name = "Details")]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var post = await this.postsService.GetById<PostDetailsViewModel>(id);
            if (post == null)
            {
                return this.NotFound();
            }

            return this.View(post);
        }

        // Delete Post
        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await this.postsService.GetById<PostServiceModel>(id);
            if (post == null)
            {
                return this.NotFound();
            }

            await this.postsService.Delete(post);
            this.TempData["InfoMessage"] = InfoMessages.PostDeleteSuccessMessage;
            return this.RedirectToAction("All");
        }

        [AllowAnonymous]
        public IActionResult All(int page = 1)
        {
            var posts = this.postsService.GetAll<PostViewModel>().ToList();
            var count = this.postsService.GetCount();
            var model = new AllPostViewModel
            {
                BlogPosts = posts,
            };
            model.BlogPosts = this.postsService.GetItemsPerPage<PostViewModel>(Constraints.ItemsPerPage, (page - 1) * Constraints.ItemsPerPage).ToList();
            model.PagesCount = (int)Math.Ceiling((double)count / Constraints.ItemsPerPage);
            if (model.PagesCount == 0)
            {
                model.PagesCount = 1;
            }

            model.CurrentPage = page;
            return this.View(model);
        }
    }
}
