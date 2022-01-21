using System.ComponentModel.DataAnnotations;

namespace BlogVue.WebAPI.Common.Enums
{
    public enum BlogPostVisibility
    {
        [Display(Name ="Genel")]
        Public = 0,
        [Display(Name = "Taslak")]
        Draft = 1,
        [Display(Name = "Şifre Korumalı")]
        PasswordProtected = 2
    }
}
