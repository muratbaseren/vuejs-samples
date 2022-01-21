using BlogVue.WebAPI.DataAccess.Mongo.Context;
using BlogVue.WebAPI.Entities;
using MFramework.Services.DataAccess.Mongo.Attributes;
using MFramework.Services.DataAccess.Mongo.Repository.Abstract;

namespace BlogVue.WebAPI.DataAccess.Mongo.Repositories
{
    public interface IMongoAlbumRepository : IMongoRepository<Album>
    {
    }

    [Collection("albums")]
    public class MongoAlbumRepository : MongoRepository<Album>, IMongoAlbumRepository
    {
        public MongoAlbumRepository(MongoDBContext mongoDbContext) : base(mongoDbContext)
        {
        }
    }
}
