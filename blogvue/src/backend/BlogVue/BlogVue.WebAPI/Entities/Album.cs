using BlogVue.WebAPI.Entities.Abstract;
using BlogVue.WebAPI.Models.Abstract;
using MongoDB.Bson;

namespace BlogVue.WebAPI.Entities
{
    public class Album : Entity<ObjectId>, IMongoEntity
    {
        public string Name { get; set; }
        public bool NoSales { get; set; }
        public string Description { get; set; }
    }
}
