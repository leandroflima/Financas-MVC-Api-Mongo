using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FinancasMVC.Models
{
    public class ModelBase
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}
