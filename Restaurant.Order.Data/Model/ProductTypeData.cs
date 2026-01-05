using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant.Order.Data.Model;

public class ProductTypeData
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string? Nome { get; set; }

}
