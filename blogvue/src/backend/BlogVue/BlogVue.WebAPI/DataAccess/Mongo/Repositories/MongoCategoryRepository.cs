using BlogVue.WebAPI.DataAccess.Mongo.Context;
using BlogVue.WebAPI.Entities;
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;

namespace BlogVue.WebAPI.DataAccess.Mongo.Repositories
{
    public interface IMongoCategoryRepository : IMongoRepository<Category>
    {
    }

    [Collection("categories")]
    public class MongoCategoryRepository : MongoRepository<Category>, IMongoCategoryRepository
    {
        public MongoCategoryRepository(MongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
