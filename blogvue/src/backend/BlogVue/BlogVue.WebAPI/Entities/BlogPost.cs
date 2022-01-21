using BlogVue.WebAPI.Common.Enums;
using BlogVue.WebAPI.Entities.Abstract;
using BlogVue.WebAPI.Models;
using BlogVue.WebAPI.Models.Abstract;
using MongoDB.Bson;
using System.Collections.Generic;

namespace BlogVue.WebAPI.Entities
{
    public class BlogPost : Entity<ObjectId>, IMongoEntity
    {
        public string Title { get; set; }
        public string RawBody { get; set; }
        public string Slug { get; set; }
        public BlogPostVisibility Visibility { get; set; }
        public string Password { get; set; }
        public string Summary { get; set; }
        public AuthorSelection<ObjectId> Author { get; set; }
        public List<CategorySelection<ObjectId>> Categories { get; set; }
        public List<TagSelection<ObjectId>> Tags { get; set; }

        public BlogPost()
        {
            Categories = new List<CategorySelection<ObjectId>>();
            Tags = new List<TagSelection<ObjectId>>();
        }
    }
}
