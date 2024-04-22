using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace investiment.manager.api.Models.User
{
    public class UserInvestor
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        protected string Id { get; set; }
        public string Email { get; set; }
        public string Name{ get; set; }
        public string DocumentId { get; set; }
        public DateTime Birthday { get; set; }

    }
}
