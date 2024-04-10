using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Persistence.Contexts.MongoContext
{
    public class MongoDbContext<T> : IMongoDbContext<T>
    {

        private readonly IMongoDatabase db;
        private readonly IMongoCollection<T> mongoCollection;

        public MongoDbContext()
        {
            var client = new MongoClient();
            db = client.GetDatabase("VisitorSiteDb");
            mongoCollection = db.GetCollection<T>(typeof(T).Name);
        }

        public IMongoCollection<T> GetCollection()
        {
            return mongoCollection;
        }
    }
}
