using FinancasMVC.Contracts;
using FinancasMVC.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace FinancasMVC.Services
{
    public class GroupService : IService<Group>
    {
        private readonly IMongoCollection<Group> _collection;

        public GroupService(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _collection = database.GetCollection<Group>(nameof(Group));
        }

        public void Remove(Guid id)
        {
            _collection.DeleteOne(a => a.Id.Equals(id));
        }

        public IEnumerable<Group> GetAll()
        {
            return _collection.Find(GroupModel => true).ToEnumerable();
        }

        public Group GetById(Guid id)
        {
            return _collection.Find(a => a.Id.Equals(id)).FirstOrDefault();
        }

        public void Insert(Group model)
        {
            _collection.InsertOneAsync(model);
        }

        public void Update(Group model)
        {
            _collection.ReplaceOne(a => a.Id.Equals(model.Id), model);
        }
    }
}
