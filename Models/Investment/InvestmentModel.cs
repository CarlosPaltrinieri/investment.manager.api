using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace investiment.manager.api.Models.Investment
{
    public class InvestmentModel
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
        public InvestmentValue Value { get; set; }
    }
}
