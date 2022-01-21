using AutoMapper;
using BlogVue.WebAPI.DataAccess.Mongo.Repositories;
using BlogVue.WebAPI.Entities;
using MFramework.Services.Business.Abstract;
using MFramework.Services.Business.Mongo.Abstract;
using MongoDB.Bson;

namespace BlogVue.WebAPI.Business.Managers
{
    public interface ICategoryManager : IManager<Category, ObjectId>
    {

    }

    public partial class CategoryManager : MongoManager<Category, IMongoCategoryRepository>, ICategoryManager
    {
        public CategoryManager(IMongoCategoryRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
