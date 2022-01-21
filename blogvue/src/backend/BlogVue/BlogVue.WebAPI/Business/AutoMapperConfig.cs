using AutoMapper;
using BlogVue.WebAPI.Common;
using BlogVue.WebAPI.Entities;
using BlogVue.WebAPI.Models;
using MFramework.Services.Common.Extensions;
using MongoDB.Bson;

namespace BlogVue.WebAPI.Business
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            AlbumsMappings();
            CategoriesMappings();
            TagsMappings();
            AuthorsMappings();
            BlogPostsMappings();
        }

        private void BlogPostsMappings()
        {
            CreateMap<BlogPost, BlogPostQuery>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()))
                .ForMember(
                    dest => dest.VisibilityString,
                    opts => opts.MapFrom((x, y) => x.Visibility.GetEnumDisplayName()))
                .ForMember(
                    dest => dest.StateString,
                    opts => opts.MapFrom((x, y) => x.State.GetEnumDisplayName()));

            CreateMap<BlogPostCreate, BlogPost>().ReverseMap();
            CreateMap<BlogPostCreate, BlogPostCreateUpdateExtend<ObjectId>>()
                .ForMember(x => x.Categories, x => x.Ignore())
                .ForMember(x => x.Tags, x => x.Ignore());
            CreateMap<BlogPostCreateUpdateExtend<ObjectId>, BlogPost>().ReverseMap();
            CreateMap<BlogPostUpdate, BlogPost>().ReverseMap();
            CreateMap<BlogPostUpdate, BlogPostCreateUpdateExtend<ObjectId>>()
                .ForMember(x => x.Categories, x => x.Ignore())
                .ForMember(x => x.Tags, x => x.Ignore());
        }

        private void AuthorsMappings()
        {
            CreateMap<Author, AuthorQuery>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()))
                .ForMember(
                    dest => dest.IsLockedString,
                    opts => opts.MapFrom((x, y) => x.IsLocked.Text("Kilitli", "Aktif")))
                .ForMember(
                    dest => dest.IsConfirmedString,
                    opts => opts.MapFrom((x, y) => x.IsConfirmed.Text("Onaylı", "Onaylanmamış")))
                .ForMember(
                    dest => dest.StateString,
                    opts => opts.MapFrom((x, y) => x.State.GetEnumDisplayName()));

            CreateMap<AuthorCreate, Author>().ReverseMap();
            CreateMap<AuthorUpdate, Author>().ReverseMap();

            CreateMap<Author, AuthorSelection<ObjectId>>().ReverseMap();
            CreateMap<Author, AuthorSelection<string>>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()));

            CreateMap<AuthorSelection<ObjectId>, AuthorSelection<string>>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()));
        }

        private void TagsMappings()
        {
            CreateMap<Tag, TagQuery>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()))
                .ForMember(
                    dest => dest.StateString,
                    opts => opts.MapFrom((x, y) => x.State.GetEnumDisplayName()));

            CreateMap<TagCreate, Tag>().ReverseMap();
            CreateMap<TagUpdate, Tag>().ReverseMap();

            CreateMap<Tag, TagSelection<ObjectId>>().ReverseMap();
            CreateMap<Tag, TagSelection<string>>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()));

            CreateMap<TagSelection<ObjectId>, TagSelection<string>>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()));

            CreateMap<TagSelection<string>, TagSelection<ObjectId>>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToObjectId()));
        }

        private void CategoriesMappings()
        {
            CreateMap<Category, CategoryQuery>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()))
                .ForMember(
                    dest => dest.ParentId,
                    opts => opts.MapFrom((x, y) => x.ParentId.ToString()))
                .ForMember(
                    dest => dest.StateString,
                    opts => opts.MapFrom((x, y) => x.State.GetEnumDisplayName()));

            CreateMap<CategoryCreate, Category>()
                .ForMember(
                    dest => dest.ParentId,
                    opts => opts.MapFrom((x, y) => x.ParentId.ToObjectId()));
            CreateMap<CategoryUpdate, Category>()
                .ForMember(
                    dest => dest.ParentId,
                    opts => opts.MapFrom((x, y) => x.ParentId.ToObjectId()));

            CreateMap<Category, CategorySelection<ObjectId>>().ReverseMap();
            CreateMap<Category, CategorySelection<string>>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()));

            CreateMap<CategorySelection<ObjectId>, CategorySelection<string>>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()));

            CreateMap<CategorySelection<string>, CategorySelection<ObjectId>>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToObjectId()));
        }

        private void AlbumsMappings()
        {
            CreateMap<Album, AlbumQuery>()
                .ForMember(
                    dest => dest.Id,
                    opts => opts.MapFrom((x, y) => x.Id.ToString()))
                .ForMember(
                    dest => dest.StateString,
                    opts => opts.MapFrom((x, y) => x.State.GetEnumDisplayName()))
                .ForMember(
                    dest => dest.NoSalesString,
                    opts => opts.MapFrom((x, y) => x.NoSales ? "Satış Dışı" : "Satışta"));

            CreateMap<AlbumCreate, Album>().ReverseMap();
            CreateMap<AlbumUpdate, Album>().ReverseMap();
        }
    }
}
