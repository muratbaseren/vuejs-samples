using AutoMapper;
using BlogVue.WebAPI.DataAccess.Mongo.Repositories;
using BlogVue.WebAPI.Entities;
using MFramework.Services.Business.Abstract;
using MFramework.Services.Business.Mongo.Abstract;
using MongoDB.Bson;

namespace BlogVue.WebAPI.Business.Managers
{
    public interface ITagManager : IManager<Tag, ObjectId>
    {

    }

    public partial class TagManager : MongoManager<Tag, IMongoTagRepository>, ITagManager
    {
        public TagManager(IMongoTagRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
