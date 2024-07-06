using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }// MongoDb de ilişkisel veri tabanı olmadığı için ve identy olaylarında id'ler int değilde string türde tutuluyor. Bir guid değer atanıyor. Bunun id olduğunu belirtmek için yukarıda ki iki attribute(BsonId, BsonRepresentation) kullanılır. BsonRepresentation da bu id'nin benzersiz olduğunu belirtmek için "BsonType.ObjectId" ile yaparız.
        public string CategoryName { get; set; }
    }
}
