using FinancasMVC.Contracts;
using FinancasMVC.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace FinancasMVC.Services
{
    public class AccountService : IService<Account>
    {
        private readonly IMongoCollection<Account> _collection;

        public AccountService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _collection = database.GetCollection<Account>(nameof(Account));
        }

        public void Remove(Guid id)
        {
            _collection.DeleteOne(a => a.Id.Equals(id));
        }

        public IEnumerable<Account> GetAll()
        {
            return _collection.Find(AccountModel => true).ToEnumerable();
        }

        public Account GetById(Guid id)
        {
            return _collection.Find(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public void Insert(Account model)
        {
            _collection.InsertOneAsync(model);
        }

        public void Update(Account model)
        {
            _collection.ReplaceOne(a => a.Id.Equals(model.Id), model);
        }
    }
}
