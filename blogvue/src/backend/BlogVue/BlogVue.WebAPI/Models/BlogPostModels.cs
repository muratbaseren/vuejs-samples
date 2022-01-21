using BlogVue.WebAPI.Common.Enums;
using BlogVue.WebAPI.Models.Abstract;
using System.Collections.Generic;

namespace BlogVue.WebAPI.Models
{
    public class BlogPostQuery : ModelBase<string>
    {
        public string Title { get; set; }
        public string RawBody { get; set; }
        public string Slug { get; set; }
        public BlogPostVisibility Visibility { get; set; }
        public string VisibilityString { get; set; }
        public string Summary { get; set; }
        public AuthorSelection<string> Author { get; set; }
        public List<CategorySelection<string>> Categories { get; set; }
        public List<TagSelection<string>> Tags { get; set; }
    }

    public class BlogPostCreate
    {
        public string Title { get; set; }
        public string RawBody { get; set; }
        public string Slug { get; set; }
        public BlogPostVisibility Visibility { get; set; }
        public string Password { get; set; }
        public string Summary { get; set; }
        public string AuthorId { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }

        public BlogPostCreate()
        {
            Categories = new List<string>();
            Tags = new List<string>();
        }
    }

    public class BlogPostCreateUpdateExtend<T>
    {
        public string Title { get; set; }
        public string RawBody { get; set; }
        public string Slug { get; set; }
        public BlogPostVisibility Visibility { get; set; }
        public string Password { get; set; }
        public string Summary { get; set; }
        public AuthorSelection<T> Author { get; set; }
        public List<CategorySelection<T>> Categories { get; set; }
        public List<TagSelection<T>> Tags { get; set; }

        public BlogPostCreateUpdateExtend()
        {
            Categories = new List<CategorySelection<T>>();
            Tags = new List<TagSelection<T>>();
        }
    }

    public class BlogPostUpdate
    {
        public string Title { get; set; }
        public string RawBody { get; set; }
        public string Slug { get; set; }
        public BlogPostVisibility Visibility { get; set; }
        public string Password { get; set; }
        public string Summary { get; set; }
        public string AuthorId { get; set; }
        public List<string> Categories { get; set; }
        public List<string> Tags { get; set; }

        public BlogPostUpdate()
        {
            Categories = new List<string>();
            Tags = new List<string>();
        }
    }
}
