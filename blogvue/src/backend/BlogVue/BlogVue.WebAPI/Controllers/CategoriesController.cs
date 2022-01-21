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
    public partial class CategoriesController : BaseApiController
    {
        private readonly ICategoryManager _categoryManager;

        public CategoriesController(ICategoryManager categoryManager)
        {
            this._categoryManager = categoryManager;
        }

        /// <summary>
        /// List all categorys.
        /// </summary>
        /// <returns>User list</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseObject<List<CategoryQuery>>), 200)]
        public IActionResult Get()
        {
            var categories = _categoryManager.List<CategoryQuery>();

            return Success("Categories listed.", null, categories);
        }

        /// <summary>
        /// Get specific category.
        /// </summary>
        /// <param name="id">Category id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseObject<List<CategoryQuery>>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<string>), 404)]
        public IActionResult Get(string id)
        {
            var objectId = ObjectId.Parse(id);
            var category = _categoryManager.Get<CategoryQuery>(objectId);

            if (category == null) return NotFound("Category not found.", "Category not found in database.", id);

            return Success("Category found.", null, category);
        }


        /// <summary>
        /// Create a new category
        /// </summary>
        /// <param name="model">Category information</param>
        /// <returns>Results</returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<CategoryQuery>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<CategoryCreate>), 500)]
        public IActionResult Post([FromBody] CategoryCreate model)
        {
            try
            {
                var result = _categoryManager.Create<CategoryCreate, CategoryQuery>(model);

                if (result != null)
                    return Success("Category added successfully.", null, result);
                else
                    return Error("Category can not be added.", "Category can not insert to db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Update specific category.
        /// </summary>
        /// <param name="id">Category id.</param>
        /// <param name="model">Category model.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<CategoryUpdate>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<CategoryUpdate>), 404)]
        [ProducesResponseType(typeof(ApiResponseObject<CategoryUpdate>), 500)]
        public IActionResult Put(string id, [FromBody] CategoryUpdate model)
        {
            try
            {
                var objectId = ObjectId.Parse(id);
                var category = _categoryManager.Get(objectId);

                if (category == null) return NotFound<object>("Category not found.", "Category not found in database.", model);

                var result = _categoryManager.Update<CategoryUpdate, CategoryQuery>(objectId, model);

                if (result != null)
                    return Success("Category updated successfully.", null, result);
                else
                    return Error("Category can not be updated.", "Category can not update on db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Delete specific category.
        /// </summary>
        /// <param name="id">Category id.</param>
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
                var category = _categoryManager.Get(objectId);

                if (category == null) return NotFound("Category not found.", "Category not found in database.", id);

                var result = _categoryManager.Delete(objectId);

                if (result)
                    return Success("Category deleted successfully.", null, id);
                else
                    return Error("Category can not be deleted.", "Category can not delete from db.", id);

            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, id);
            }
        }
    }
}
