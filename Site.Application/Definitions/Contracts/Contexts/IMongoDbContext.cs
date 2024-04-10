using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Site.Application.Definitions.Contracts.Contexts
{
    public interface IMongoDbContext<T>
    {
        public IMongoCollection<T> GetCollection();
    }
}
