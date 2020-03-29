using FinancasMVC.Contracts;
using FinancasMVC.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace FinancasMVC.Services
{
    public class SubGroupService : IService<SubGroup>
    {
        private readonly IMongoCollection<SubGroup> _collection;

        public SubGroupService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _collection = database.GetCollection<SubGroup>(nameof(SubGroup));
        }

        public void Remove(Guid id)
        {
            _collection.DeleteOne(a => a.Id.Equals(id));
        }

        public IEnumerable<SubGroup> GetAll()
        {
            return _collection.Find(SubGroupModel => true).ToEnumerable();
        }

        public SubGroup GetById(Guid id)
        {
            return _collection.Find(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public IEnumerable<SubGroup> GetByGroupId(Guid id)
        {
            return _collection.Find(a => a.GroupId.Equals(id)).ToEnumerable();
        }

        public void Insert(SubGroup model)
        {
            _collection.InsertOneAsync(model);
        }

        public void Update(SubGroup model)
        {
            _collection.ReplaceOne(a => a.Id.Equals(model.Id), model);
        }
    }
}
