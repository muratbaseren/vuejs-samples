using BlogVue.WebAPI.Business.Managers;
using BlogVue.WebAPI.Controllers.Abstract;
using BlogVue.WebAPI.Filters;
using BlogVue.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace BlogVue.WebAPI.Controllers
{
    [TypeFilter(typeof(CustomExceptionFilter))]
    public partial class TagsController : BaseApiController
    {
        private readonly ITagManager _tagManager;

        public TagsController(ITagManager tagManager)
        {
            this._tagManager = tagManager;
        }

        /// <summary>
        /// List all tags.
        /// </summary>
        /// <returns>User list</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseObject<List<TagQuery>>), 200)]
        public IActionResult Get()
        {
            var tags = _tagManager.List<TagQuery>();

            return Success("Tags listed.", null, tags);
        }

        /// <summary>
        /// Get specific tag.
        /// </summary>
        /// <param name="id">Tag id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseObject<List<TagQuery>>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<string>), 404)]
        public IActionResult Get(string id)
        {
            var objectId = ObjectId.Parse(id);
            var tag = _tagManager.Get<TagQuery>(objectId);

            if (tag == null) return NotFound("Tag not found.", "Tag not found in database.", id);

            return Success("Tag found.", null, tag);
        }


        /// <summary>
        /// Create a new tag
        /// </summary>
        /// <param name="model">Tag information</param>
        /// <returns>Results</returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<TagQuery>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<TagCreate>), 500)]
        public IActionResult Post([FromBody] TagCreate model)
        {
            try
            {
                var result = _tagManager.Create<TagCreate, TagQuery>(model);

                if (result != null)
                    return Success("Tag added successfully.", null, result);
                else
                    return Error("Tag can not be added.", "Tag can not insert to db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Update specific tag.
        /// </summary>
        /// <param name="id">Tag id.</param>
        /// <param name="model">Tag model.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<TagUpdate>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<TagUpdate>), 404)]
        [ProducesResponseType(typeof(ApiResponseObject<TagUpdate>), 500)]
        public IActionResult Put(string id, [FromBody] TagUpdate model)
        {
            try
            {
                var objectId = ObjectId.Parse(id);
                var tag = _tagManager.Get(objectId);

                if (tag == null) return NotFound<object>("Tag not found.", "Tag not found in database.", model);

                var result = _tagManager.Update<TagUpdate, TagQuery>(objectId, model);

                if (result != null)
                    return Success("Tag updated successfully.", null, result);
                else
                    return Error("Tag can not be updated.", "Tag can not update on db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Delete specific tag.
        /// </summary>
        /// <param name="id">Tag id.</param>
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
                var tag = _tagManager.Get(objectId);

                if (tag == null) return NotFound("Tag not found.", "Tag not found in database.", id);

                var result = _tagManager.Delete(objectId);

                if (result)
                    return Success("Tag deleted successfully.", null, id);
                else
                    return Error("Tag can not be deleted.", "Tag can not delete from db.", id);

            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, id);
            }
        }
    }
}
