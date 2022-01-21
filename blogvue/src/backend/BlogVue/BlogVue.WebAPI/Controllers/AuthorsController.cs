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
    public partial class AuthorsController : BaseApiController
    {
        private readonly IAuthorManager _authorManager;

        public AuthorsController(IAuthorManager authorManager)
        {
            this._authorManager = authorManager;
        }

        /// <summary>
        /// List all authors.
        /// </summary>
        /// <returns>User list</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseObject<List<AuthorQuery>>), 200)]
        public IActionResult Get()
        {
            var authors = _authorManager.List<AuthorQuery>();

            return Success("Authors listed.", null, authors);
        }

        /// <summary>
        /// Get specific author.
        /// </summary>
        /// <param name="id">Author id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseObject<List<AuthorQuery>>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<string>), 404)]
        public IActionResult Get(string id)
        {
            var objectId = ObjectId.Parse(id);
            var author = _authorManager.Get<AuthorQuery>(objectId);

            if (author == null) return NotFound("Author not found.", "Author not found in database.", id);

            return Success("Author found.", null, author);
        }


        /// <summary>
        /// Create a new author
        /// </summary>
        /// <param name="model">Author information</param>
        /// <returns>Results</returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<AuthorQuery>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<AuthorCreate>), 500)]
        public IActionResult Post([FromBody] AuthorCreate model)
        {
            try
            {
                var result = _authorManager.Create<AuthorCreate, AuthorQuery>(model);

                if (result != null)
                    return Success("Author added successfully.", null, result);
                else
                    return Error("Author can not be added.", "Author can not insert to db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Update specific author.
        /// </summary>
        /// <param name="id">Author id.</param>
        /// <param name="model">Author model.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<AuthorUpdate>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<AuthorUpdate>), 404)]
        [ProducesResponseType(typeof(ApiResponseObject<AuthorUpdate>), 500)]
        public IActionResult Put(string id, [FromBody] AuthorUpdate model)
        {
            try
            {
                var objectId = ObjectId.Parse(id);
                var author = _authorManager.Get(objectId);

                if (author == null) return NotFound<object>("Author not found.", "Author not found in database.", model);

                var result = _authorManager.Update<AuthorUpdate, AuthorQuery>(objectId, model);

                if (result != null)
                    return Success("Author updated successfully.", null, result);
                else
                    return Error("Author can not be updated.", "Author can not update on db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Delete specific author.
        /// </summary>
        /// <param name="id">Author id.</param>
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
                var author = _authorManager.Get(objectId);

                if (author == null) return NotFound("Author not found.", "Author not found in database.", id);

                var result = _authorManager.Delete(objectId);

                if (result)
                    return Success("Author deleted successfully.", null, id);
                else
                    return Error("Author can not be deleted.", "Author can not delete from db.", id);

            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, id);
            }
        }
    }
}
