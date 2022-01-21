using BlogVue.WebAPI.DataAccess.Mongo.Context;
using BlogVue.WebAPI.Entities;
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;

namespace BlogVue.WebAPI.DataAccess.Mongo.Repositories
{
    public interface IMongoTagRepository : IMongoRepository<Tag>
    {
    }

    [Collection("tags")]
    public class MongoTagRepository : MongoRepository<Tag>, IMongoTagRepository
    {
        public MongoTagRepository(MongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
