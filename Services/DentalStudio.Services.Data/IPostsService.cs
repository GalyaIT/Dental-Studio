namespace DentalStudio.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DentalStudio.Services.Models;

    public interface IPostsService
    {
        Task<int> Create(PostServiceModel post, string userId);

        IEnumerable<T> GetAll<T>();

        Task<T> GetById<T>(int id);

        Task<int> Edit(int id, PostServiceModel post);

        string GetUrl(int id);

        Task<bool> Delete(PostServiceModel patient);

        IEnumerable<T> GetItemsPerPage<T>(int? take = null, int skip = 0);

        int GetCount();
    }
}
