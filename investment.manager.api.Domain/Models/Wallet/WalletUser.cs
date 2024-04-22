using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace investment.manager.api.Domain.Models.Wallet
{
    public class WalletUser
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        protected string Id { get; set; }
        public string IdUser { get; set; }
        public string IdInvestment { get; set; }
        public string Email { get; set; }
    }
}
