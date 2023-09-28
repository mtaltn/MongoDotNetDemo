  using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDotNetDemo.Models;

namespace MongoDotNetDemo.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Models.Product> _products;
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public ProductService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _products = mongoDatabase.GetCollection<Models.Product>(dbSettings.Value.ProductsCollectionName);
        }

        //public async Task<IEnumerable<Models.Product>> GetAllAsyc() =>
        //    await _products.Find(_ => true).ToListAsync();

        public async Task<IEnumerable<Models.Product>> GetAllAsyc()
        {
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$lookup",new BsonDocument
                {
                    {"from", "CategoryCollection"},
                    {"localField", "CategoryId"},
                    {"foreignField", "_id"},
                    {"as", "product_category"}
                }),
                new BsonDocument("$unwind", "$product_category"),
                new BsonDocument("$project", new BsonDocument
                {
                    {"_id", 1 },
                    {"CategoryId", 1 },
                    {"ProductName", 1 },
                    {"CategoryName", "$product_category.CategoryName" },
                })
            };

            var result = await _products.Aggregate<Models.Product>(pipeline).ToListAsync();
            return result;
        }

        public async Task<Models.Product> GetById(string id) =>
            await _products.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Models.Product Product) =>
            await _products.InsertOneAsync(Product);

        public async Task UpdateAsync(string id, Models.Product Product) =>
            await _products
            .ReplaceOneAsync(a => a.Id == Product.Id, Product);

        public async Task DeleteAsync(string id) =>
            await _products.DeleteOneAsync(a => a.Id == id);


    }
}
