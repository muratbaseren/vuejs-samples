using BlogVue.WebAPI.Entities.Abstract;
using BlogVue.WebAPI.Models.Abstract;
using MongoDB.Bson;

namespace BlogVue.WebAPI.Entities
{
    public class Author : Entity<ObjectId>, IMongoEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; }
        public bool IsConfirmed { get; set; }
        public string Token { get; set; }
    }
}
