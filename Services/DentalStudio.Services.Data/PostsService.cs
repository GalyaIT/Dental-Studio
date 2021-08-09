namespace DentalStudio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DentalStudio.Data.Common.Repositories;
    using DentalStudio.Data.Models;
    using DentalStudio.Services.Mapping;
    using DentalStudio.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class PostsService : IPostsService
    {
        private readonly IDeletableEntityRepository<Post> postsRepository;

        public PostsService(IDeletableEntityRepository<Post> postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public async Task<int> Create(PostServiceModel postServiceModel, string userid)
        {
            var post = new Post
            {
                Content = postServiceModel.Content,
                Title = postServiceModel.Title,
                ImageUrl = postServiceModel.ImageUrl,
                UserId = userid,
            };

            await this.postsRepository.AddAsync(post);
            await this.postsRepository.SaveChangesAsync();
            return post.Id;

        }

        public async Task<bool> Delete(PostServiceModel postServiceModel)
        {
            Post post = await this.postsRepository.All().FirstOrDefaultAsync(p => p.Id == postServiceModel.Id);

            if (post == null)
            {
                throw new ArgumentNullException(
                   string.Format(nameof(post)));
            }

            this.postsRepository.Delete(post);

            int result = await this.postsRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<int> Edit(int id, PostServiceModel postServiceModel)
        {
            Post post = await this.postsRepository.All().FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                throw new ArgumentNullException(
                   string.Format(nameof(post)));
            }

            post.Title = postServiceModel.Title;
            post.Content = postServiceModel.Content;
            post.ImageUrl = postServiceModel.ImageUrl;

            this.postsRepository.Update(post);
            await this.postsRepository.SaveChangesAsync();

            return post.Id;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var query = this.postsRepository.All().To<PostServiceModel>()
                 .OrderByDescending(p => p.CreatedOn);

            return query.To<T>().ToList();
        }

        public async Task<T> GetById<T>(int id)
        {
            var post = await this.postsRepository
              .All()
              .Where(p => p.Id == id).To<PostServiceModel>()
              .FirstOrDefaultAsync();

            //if (post == null)
            //{
            //    throw new ArgumentNullException(
            //        string.Format(nameof(post)));
            //}

            var postServiceModel = AutoMapperConfig.MapperInstance.Map<T>(post);

            return postServiceModel;
        }

        public string GetUrl(int id)
        {
            var post = this.postsRepository.All().FirstOrDefault(p => p.Id == id);
            var url = post.ImageUrl;
            return url;
        }

        public int GetCount()
        {
            return this.postsRepository.All().Count();
        }

        public IEnumerable<T> GetItemsPerPage<T>(int? take = null, int skip = 0)
        {
            var query = this.postsRepository.All().To<PostServiceModel>()
                 .OrderByDescending(x => x.CreatedOn)
                 .Skip(skip);
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }
    }
}
