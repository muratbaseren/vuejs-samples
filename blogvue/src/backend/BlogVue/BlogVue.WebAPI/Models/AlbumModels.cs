using BlogVue.WebAPI.Models.Abstract;
using System.ComponentModel.DataAnnotations;

namespace BlogVue.WebAPI.Models
{
    public class AlbumQuery : ModelBase<string>
    {
        public string Name { get; set; }
        public bool NoSales { get; set; }
        public string NoSalesString { get; set; }
        public string Description { get; set; }
    }

    public partial class AlbumCreate
    {
        [Required]
        public string Name { get; set; }
        public bool NoSales { get; set; }
        public string Description { get; set; }
    }

    public partial class AlbumUpdate
    {
        [Required]
        public string Name { get; set; }
        public bool NoSales { get; set; }
        public string Description { get; set; }
    }
}
