using MongoDB.Bson;
using MongoDotNetDemo.Models;

namespace MongoDotNetDemo.Services.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<Models.Category>> GetAllAsyc();

        Task<Models.Category> GetById(string id);

        Task CreateAsync(Models.Category category);

        Task UpdateAsync(string id, Models.Category category);

        Task DeleteAsync(string id);
    }
}