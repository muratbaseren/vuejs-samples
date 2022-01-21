using BlogVue.WebAPI.DataAccess.Mongo.Context;
using BlogVue.WebAPI.Entities;
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;

namespace BlogVue.WebAPI.DataAccess.Mongo.Repositories
{
    public interface IMongoBlogPostRepository : IMongoRepository<BlogPost>
    {
    }

    [Collection("blogposts")]
    public class MongoBlogPostRepository : MongoRepository<BlogPost>, IMongoBlogPostRepository
    {
        public MongoBlogPostRepository(MongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
