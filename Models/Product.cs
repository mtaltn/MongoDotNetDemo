using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotNetDemo.Models
{
    public class Product
    {
        /*
         *[BsonRepresentation(BsonType.ObjectId)]
         * eğer değeri string tanımlamak istersen bunu yazmak zorundasın 
         * mongoda id ler ObjectId türünde ve not int olmuyorlar ona dikkat et 
         * */
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string? ProductName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CategoryId { get; set; }
    }
}
