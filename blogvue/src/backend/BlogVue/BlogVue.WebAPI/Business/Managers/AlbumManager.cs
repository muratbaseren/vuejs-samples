using AutoMapper;
using BlogVue.WebAPI.DataAccess.Mongo.Repositories;
using BlogVue.WebAPI.Entities;
using MFramework.Services.Business.Abstract;
using MFramework.Services.Business.Mongo.Abstract;
using MongoDB.Bson;

namespace BlogVue.WebAPI.Business.Managers
{
    public interface IAlbumManager : IManager<Album, ObjectId>
    {

    }


    public partial class AlbumManager : MongoManager<Album, IMongoAlbumRepository>, IAlbumManager
    {
        public AlbumManager(IMongoAlbumRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
