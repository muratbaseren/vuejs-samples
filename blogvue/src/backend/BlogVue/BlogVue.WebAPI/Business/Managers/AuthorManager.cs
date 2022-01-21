using AutoMapper;
using BlogVue.WebAPI.DataAccess.Mongo.Repositories;
using BlogVue.WebAPI.Entities;
using MFramework.Services.Business.Abstract;
using MFramework.Services.Business.Mongo.Abstract;
using MongoDB.Bson;

namespace BlogVue.WebAPI.Business.Managers
{
    public interface IAuthorManager : IManager<Author, ObjectId>
    {

    }

    public partial class AuthorManager : MongoManager<Author, IMongoAuthorRepository>, IAuthorManager
    {
        public AuthorManager(IMongoAuthorRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
