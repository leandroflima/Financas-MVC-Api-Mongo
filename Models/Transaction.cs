using FinancasMVC.Models.Enum;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace FinancasMVC.Models
{
    public class Transaction : ModelBase
    {
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreateOn { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime PaymentOn { get; set; }

        public string AccountId { get; set; }

        public string SubGroupId { get; set; }

        public decimal Value { get; set; }

        public TransactionType Type { get; set; }

        public TransactionStatus Status { get; set; }
    }
}
