using BlogVue.WebAPI.Models.Abstract;

namespace BlogVue.WebAPI.Models
{
    public class AuthorQuery : ModelBase<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsLocked { get; set; }
        public string IsLockedString { get; set; }
        public bool IsConfirmed { get; set; }
        public string IsConfirmedString { get; set; }
        public string Token { get; set; }
    }

    public class AuthorSelection<T>
    {
        public T Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
    }

    public class AuthorCreate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }

    public class AuthorUpdate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
