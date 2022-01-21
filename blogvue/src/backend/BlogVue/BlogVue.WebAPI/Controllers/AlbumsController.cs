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
    public partial class AlbumsController : BaseApiController
    {
        private readonly IAlbumManager _albumManager;

        public AlbumsController(IAlbumManager albumManager)
        {
            this._albumManager = albumManager;
        }

        /// <summary>
        /// List all albums.
        /// </summary>
        /// <returns>User list</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseObject<List<AlbumQuery>>), 200)]
        public IActionResult Get()
        {
            var albums = _albumManager.List<AlbumQuery>();

            return Success("Albums listed.", null, albums);
        }

        /// <summary>
        /// Get specific album.
        /// </summary>
        /// <param name="id">Album id.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseObject<List<AlbumQuery>>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<string>), 404)]
        public IActionResult Get(string id)
        {
            var objectId = ObjectId.Parse(id);
            var album = _albumManager.Get<AlbumQuery>(objectId);

            if (album == null) return NotFound("Album not found.", "Album not found in database.", id);

            return Success("Album found.", null, album);
        }


        /// <summary>
        /// Create a new album
        /// </summary>
        /// <param name="model">Album information</param>
        /// <returns>Results</returns>
        [HttpPost]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<AlbumQuery>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<AlbumCreate>), 500)]
        public IActionResult Post([FromBody] AlbumCreate model)
        {
            try
            {
                var result = _albumManager.Create<AlbumCreate, AlbumQuery>(model);

                if (result != null)
                    return Success("Album added successfully.", null, result);
                else
                    return Error("Album can not be added.", "Album can not insert to db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Update specific album.
        /// </summary>
        /// <param name="id">Album id.</param>
        /// <param name="model">Album model.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [ProducesResponseType(typeof(ApiResponseObject<AlbumUpdate>), 200)]
        [ProducesResponseType(typeof(ApiResponseObject<AlbumUpdate>), 404)]
        [ProducesResponseType(typeof(ApiResponseObject<AlbumUpdate>), 500)]
        public IActionResult Put(string id, [FromBody] AlbumUpdate model)
        {
            try
            {
                var objectId = ObjectId.Parse(id);
                var album = _albumManager.Get(objectId);

                if (album == null) return NotFound<object>("Album not found.", "Album not found in database.", model);

                var result = _albumManager.Update<AlbumUpdate, AlbumQuery>(objectId, model);

                if (result != null)
                    return Success("Album updated successfully.", null, result);
                else
                    return Error("Album can not be updated.", "Album can not update on db.", model);
            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, model);
            }
        }

        /// <summary>
        /// Delete specific album.
        /// </summary>
        /// <param name="id">Album id.</param>
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
                var album = _albumManager.Get(objectId);

                if (album == null) return NotFound("Album not found.", "Album not found in database.", id);

                var result = _albumManager.Delete(objectId);

                if (result)
                    return Success("Album deleted successfully.", null, id);
                else
                    return Error("Album can not be deleted.", "Album can not delete from db.", id);

            }
            catch (Exception ex)
            {
                return Error("Something went wrong!", ex.Message, id);
            }
        }
    }
}
