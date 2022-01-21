using BlogVue.WebAPI.DataAccess.Mongo.Context;
using BlogVue.WebAPI.Entities;
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;

namespace BlogVue.WebAPI.DataAccess.Mongo.Repositories
{
    public interface IMongoAuthorRepository : IMongoRepository<Author>
    {
    }

    [Collection("authors")]
    public class MongoAuthorRepository : MongoRepository<Author>, IMongoAuthorRepository
    {
        public MongoAuthorRepository(MongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
