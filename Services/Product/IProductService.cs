using MongoDB.Bson;
using MongoDotNetDemo.Models;

namespace MongoDotNetDemo.Services.Product
{
    public interface IProductService
    {
        Task<IEnumerable<Models.Product>> GetAllAsyc();

        Task<Models.Product> GetById(string id);

        Task CreateAsync(Models.Product product);

        Task UpdateAsync(string id, Models.Product product);

        Task DeleteAsync(string id);
    }
}