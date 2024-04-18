using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace investiment.manager.api.Models.Investment
{
    public class InvestmentValue
    {
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? UpdatedAt { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? ExpiryDate { get; set; }
        public string Description { get; set; }
        public string TypeInvestment { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string Name { get; set; }
    }
}