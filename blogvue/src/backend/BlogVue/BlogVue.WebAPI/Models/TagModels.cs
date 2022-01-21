using BlogVue.WebAPI.Models.Abstract;

namespace BlogVue.WebAPI.Models
{
    public class TagQuery : ModelBase<string>
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
    }

    public class TagCreate
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
    }

    public class TagSelection<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
    }

    public class TagUpdate
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Slug { get; set; }
    }
}
