using AutoMapper;
using BlogVue.WebAPI.Business.Managers;
using BlogVue.WebAPI.Common;
using BlogVue.WebAPI.Controllers.Abstract;
using BlogVue.WebAPI.Filters;
using BlogVue.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlogVue.WebAPI.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    public partial class BlogPostsController : BaseApiController
    {
        private readonly IBlogPostManager _blogPostManager;
        private readonly IMapper _mapper;

        public BlogPostsController(IBlogPostManager blogPostManager, IMapper mapper)
        {
            _blogPostManager = blogPostManager;
            _mapper = mapper;
        }

        /// <summary>
        /// List all blogPosts.
        /// </summary>
        /// <returns>User list</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseObject<List<BlogPostQuery>>), 200)]
        public IActionResult Get()
        {
            var blogPosts = _blogPostManager.List<BlogPostQuery>();

            return Success("BlogPosts listed.", null, blogPosts);
        }

        /// <summary>
        /// Get specific blogPost.
        /// </summary>
        /// <param name="id">BlogPost id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseObject<List<BlogPostQuery>>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<string>), 404)]
        public IActionResult Get(string id)
        {
            var objectId = ObjectId.Parse(id);
            var blogPost = _blogPostManager.Get<BlogPostQuery>(objectId);

            if (blogPost == null) return NotFound("BlogPost not found.", "BlogPost not found in database.", id);

            return Success("BlogPost found.", null, blogPost);
        }


        /// <summary>
        /// Create a new blogPost
        /// </summary>
        /// <param name="model">BlogPost information</param>
        /// <returns>Results</returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<BlogPostQuery>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<BlogPostCreate>), 500)]
        public IActionResult Post([FromBody] BlogPostCreate model, [FromServices] IAuthorManager authorManager, [FromServices] ICategoryManager categoryManager, [FromServices] ITagManager tagManager)
        {
            try
            {
                BlogPostCreateUpdateExtend<ObjectId> blogPostCreate = _mapper.Map<BlogPostCreateUpdateExtend<ObjectId>>(model);
                blogPostCreate.Author = _mapper.Map<AuthorSelection<ObjectId>>(authorManager.Get(model.AuthorId.ToObjectId()));
                blogPostCreate.Categories = categoryManager.List().Where(x => model.Categories.Contains(x.Id.ToString()))
                    .Select(x => _mapper.Map<CategorySelection<ObjectId>>(x)).ToList();
                blogPostCreate.Tags = tagManager.List().Where(x => model.Tags.Contains(x.Id.ToString()))
                    .Select(x => _mapper.Map<TagSelection<ObjectId>>(x)).ToList();

                var result = _blogPostManager.Create<BlogPostCreateUpdateExtend<ObjectId>, BlogPostQuery>(blogPostCreate);

                if (result != null)
                    return Success("BlogPost added successfully.", null, result);
                else
                    return Error("BlogPost can not be added.", "BlogPost can not insert to db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Update specific blogPost.
        /// </summary>
        /// <param name="id">BlogPost id.</param>
        /// <param name="model">BlogPost model.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<BlogPostUpdate>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<BlogPostUpdate>), 404)]
        [ProducesResponseType(typeof(ApiResponseObject<BlogPostUpdate>), 500)]
        public IActionResult Put(string id, [FromBody] BlogPostUpdate model, [FromServices] IAuthorManager authorManager, [FromServices] ICategoryManager categoryManager, [FromServices] ITagManager tagManager)
        {
            try
            {
                var objectId = ObjectId.Parse(id);
                var blogPost = _blogPostManager.Get(objectId);

                if (blogPost == null) return NotFound<object>("BlogPost not found.", "BlogPost not found in database.", model);

                BlogPostCreateUpdateExtend<ObjectId> blogPostUpdate = _mapper.Map<BlogPostCreateUpdateExtend<ObjectId>>(model);
                blogPostUpdate.Author = _mapper.Map<AuthorSelection<ObjectId>>(authorManager.Get(model.AuthorId.ToObjectId()));
                blogPostUpdate.Categories = categoryManager.List().Where(x => model.Categories.Contains(x.Id.ToString()))
                    .Select(x => _mapper.Map<CategorySelection<ObjectId>>(x)).ToList();
                blogPostUpdate.Tags = tagManager.List().Where(x => model.Tags.Contains(x.Id.ToString()))
                    .Select(x => _mapper.Map<TagSelection<ObjectId>>(x)).ToList();

                var result = _blogPostManager.Update<BlogPostCreateUpdateExtend<ObjectId>, BlogPostQuery>(objectId, blogPostUpdate);

                if (result != null)
                    return Success("BlogPost updated successfully.", null, result);
                else
                    return Error("BlogPost can not be updated.", "BlogPost can not update on db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Delete specific blogPost.
        /// </summary>
        /// <param name="id">BlogPost id.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponseObject<string>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<string>), 404)]
        [ProducesResponseType(typeof(ApiResponseObject<string>), 500)]
        public IActionResult Delete(string id)
        {
            try
            {
                var objectId = ObjectId.Parse(id);
                var blogPost = _blogPostManager.Get(objectId);

                if (blogPost == null) return NotFound("BlogPost not found.", "BlogPost not found in database.", id);

                var result = _blogPostManager.Delete(objectId);

                if (result)
                    return Success("BlogPost deleted successfully.", null, id);
                else
                    return Error("BlogPost can not be deleted.", "BlogPost can not delete from db.", id);

            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, id);
            }
        }
    }
}
