using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace investiment.manager.api.Models.Investment
{
    public class InvestmentValue
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? ExpiryDate { get; set; }
        
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
    }
}