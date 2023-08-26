using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDotNetDemo.Models;

namespace MongoDotNetDemo.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Models.Category> _categories;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public CategoryService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _categories = mongoDatabase.GetCollection<Models.Category>(dbSettings.Value.CategoriesCollectionName);
        }

        public async Task<IEnumerable<Models.Category>> GetAllAsyc() =>
            await _categories.Find(_ => true).ToListAsync();

        public async Task<Models.Category> GetById(string id) =>
            await _categories.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Models.Category category) =>
            await _categories.InsertOneAsync(category);

        public async Task UpdateAsync(string id, Models.Category category) =>
            await _categories
            .ReplaceOneAsync(a => a.Id == category.Id, category);

        public async Task DeleteAsync(string id) =>
            await _categories.DeleteOneAsync(a => a.Id == id);


    }
}
