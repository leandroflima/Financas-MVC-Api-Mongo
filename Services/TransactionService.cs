using FinancasMVC.Contracts;
using FinancasMVC.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace FinancasMVC.Services
{
    public class TransactionService : IService<Transaction>
    {
        private readonly IMongoCollection<Transaction> _collection;

        public TransactionService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _collection = database.GetCollection<Transaction>(nameof(Transaction));
        }

        public void Remove(Guid id)
        {
            _collection.DeleteOne(a => a.Id.Equals(id));
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _collection.Find(TransactionModel => true).ToEnumerable();
        }

        public Transaction GetById(Guid id)
        {
            return _collection.Find(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<Transaction> GetByAccountId(Guid id)
        {
            return _collection.Find(a => a.AccountId.Equals(id)).ToEnumerable();
        }

        public void Insert(Transaction model)
        {
            _collection.InsertOneAsync(model);
        }

        public void Update(Transaction model)
        {
            _collection.ReplaceOne(a => a.Id.Equals(model.Id), model);
        }
    }
}
