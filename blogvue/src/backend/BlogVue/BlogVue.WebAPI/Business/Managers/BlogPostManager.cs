using AutoMapper;
using BlogVue.WebAPI.DataAccess.Mongo.Repositories;
using BlogVue.WebAPI.Entities;
using MFramework.Services.Business.Abstract;
using MFramework.Services.Business.Mongo.Abstract;
using MongoDB.Bson;

namespace BlogVue.WebAPI.Business.Managers
{
    public interface IBlogPostManager : IManager<BlogPost, ObjectId>
    {

    }

    public partial class BlogPostManager : MongoManager<BlogPost, IMongoBlogPostRepository>, IBlogPostManager
    {
        public BlogPostManager(IMongoBlogPostRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
