using AutoMapper;
using BlogVue.WebAPI.Common.Enums;
using BlogVue.WebAPI.DataAccess.Mongo.Repositories;
using BlogVue.WebAPI.Entities;
using BlogVue.WebAPI.Models;
using MFramework.Services.DataAccess.Context;
using MFramework.Services.FakeData;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogVue.WebAPI.DataAccess.Mongo.Context
{
    public partial interface IMongoDBInitializer
    {
        IMongoAlbumRepository AlbumRepository { get; }
        IMongoCategoryRepository CategoryRepository { get; }
        IMongoTagRepository TagRepository { get; }
        IMongoAuthorRepository AuthorRepository { get; }
        IMongoBlogPostRepository BlogPostRepository { get; }
    }

    public partial class MongoDBInitializer : DefaultDBInitializer, IMongoDBInitializer, IDBInitializer
    {
        public IMongoAlbumRepository AlbumRepository { get; }
        public IMongoCategoryRepository CategoryRepository { get; }
        public IMongoTagRepository TagRepository { get; }
        public IMongoAuthorRepository AuthorRepository { get; }
        public IMongoBlogPostRepository BlogPostRepository { get; }

        private readonly IMapper _mapper;

        public MongoDBInitializer(IConfiguration configuration, IMongoAlbumRepository albumRepository, IMongoCategoryRepository categoryRepository, IMongoTagRepository tagRepository, IMongoAuthorRepository authorRepository, IMongoBlogPostRepository blogPostRepository, IMapper mapper) : base(configuration)
        {
            AlbumRepository = albumRepository;
            CategoryRepository = categoryRepository;
            TagRepository = tagRepository;
            AuthorRepository = authorRepository;
            BlogPostRepository = blogPostRepository;
            _mapper = mapper;
        }

        public override void Seed()
        {
            AddAlbums();
            AddCategories();
            AddTags();
            AddAuthors();
            AddBlogPosts();
        }

        private void AddBlogPosts()
        {
            if (BlogPostRepository.Any()) return;

            var categories = CategoryRepository
                .List()
                .Select(x => new CategorySelection<ObjectId> { Id = x.Id, Name = x.Name })
                .ToList();

            var tags = TagRepository
                .List()
                .Select(x => new TagSelection<ObjectId> { Id = x.Id, Name = x.Name })
                .ToList();

            var authors = AuthorRepository.List();

            for (int i = 0; i < 25; i++)
            {
                BlogPostVisibility visibility = EnumData.GetElement<BlogPostVisibility>();
                string password = (visibility == BlogPostVisibility.PasswordProtected) ? "123456" : string.Empty;
                List<CategorySelection<ObjectId>> categorySelections = new List<CategorySelection<ObjectId>>();
                
                Enumerable.Range(0, NumberData.GetNumber(1, 3)).ToList()
                    .ForEach(x => categorySelections.Add(CollectionData.GetElement(categories)));

                List<TagSelection<ObjectId>> tagSelections = new List<TagSelection<ObjectId>>();

                Enumerable.Range(0, NumberData.GetNumber(3, 6)).ToList()
                    .ForEach(x => tagSelections.Add(CollectionData.GetElement(tags)));

                BlogPostRepository.Insert(new BlogPost
                {
                    Title = $"Blog post - {NameData.GetDepartmentName()}",
                    RawBody = TextData.GetSentences(NumberData.GetNumber(3,10)),
                    Slug = ObjectId.GenerateNewId().ToString(),
                    Summary = TextData.GetSentence(),
                    Visibility = visibility,
                    Password = password,
                    Author = _mapper.Map<AuthorSelection<ObjectId>>(CollectionData.GetElement<Author>(authors)),
                    Categories = categorySelections,
                    Tags = tagSelections
                });
            }
        }

        private void AddAuthors()
        {
            if (AuthorRepository.Any()) return;

            AuthorRepository.Insert(new Author
            {
                Email = "muratbaseren@gmail.com",
                Name = "K. Murat",
                Surname = "Başeren",
                Password = "123456",
                Username = "muratbaseren",
                IsConfirmed = true,
                IsLocked = true
            });

            AuthorRepository.Insert(new Author
            {
                Email = "ersin.biltekin.57@gmail.com",
                Name = "Ersin",
                Surname = "Biltekin",
                Password = "123456",
                Username = "byertm",
                IsConfirmed = true,
                IsLocked = true
            });
        }

        private void AddTags()
        {
            if (TagRepository.Any()) return;

            for (int i = 0; i < 20; i++)
            {
                var name = NameData.GetCompanyName();

                TagRepository.Insert(new Tag
                {
                    Name = name,
                    Desc = TextData.GetSentence(),
                    Slug = name.ToLower().Replace(" ", "-").Replace("'", "-")
                });
            }
        }

        private void AddCategories()
        {
            if (CategoryRepository.Any()) return;
            List<Category> categories = null;

            for (int i = 0; i < 10; i++)
            {
                var name = NameData.GetCompanyName();

                CategoryRepository.Insert(new Category
                {
                    Name = name,
                    Desc = TextData.GetSentence(),
                    Slug = name.ToLower().Replace(" ", "-").Replace("'", "-"),
                    ParentId = (categories != null) ? CollectionData.GetElement(categories).Id : ObjectId.Empty
                });

                categories = CategoryRepository.List();
            }
        }

        private void AddAlbums()
        {
            if (AlbumRepository.Any()) return;

            for (int i = 0; i < 10; i++)
            {
                AlbumRepository.Insert(new Album
                {
                    Name = NameData.GetCompanyName(),
                    Description = TextData.GetSentence(),
                    NoSales = BooleanData.GetBoolean()
                });
            }
        }
    }
}
