using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace investiment.manager.api.Models.Investment
{
    public class InvestmentBase
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? UpdatedAt { get; set; }
    }
}