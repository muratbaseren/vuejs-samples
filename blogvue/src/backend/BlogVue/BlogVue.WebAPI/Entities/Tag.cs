using BlogVue.WebAPI.Entities.Abstract;
using BlogVue.WebAPI.Models.Abstract;
using MongoDB.Bson;

namespace BlogVue.WebAPI.Entities
{
    public class Tag : Entity<ObjectId>, IMongoEntity
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
    }
}
