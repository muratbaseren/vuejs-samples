using BlogVue.WebAPI.Models.Abstract;

namespace BlogVue.WebAPI.Models
{
    public class CategoryQuery : ModelBase<string>
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
        public string ParentId { get; set; }
    }

    public class CategoryCreate
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
        public string ParentId { get; set; }
    }

    public class CategorySelection<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }

    public class CategoryUpdate
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
        public string ParentId { get; set; }
    }
}
