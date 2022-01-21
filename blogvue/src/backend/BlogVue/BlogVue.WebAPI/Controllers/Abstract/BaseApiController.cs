using Microsoft.AspNetCore.Mvc;

namespace BlogVue.WebAPI.Controllers.Abstract
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class BaseApiController : Controller
    {
        [NonAction]
        protected IActionResult Success<T>(string message, string internalMessage, T data)
        {
            return Success(new ApiResponseObject<T>
            {
                Success = true,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult Success<T>(ApiResponseObject<T> data)
        {
            return Ok(data);
        }

        [NonAction]
        protected IActionResult Created<T>(string message, string internalMessage, T data)
        {
            return Created(new ApiResponseObject<T>
            {
                Success = true,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult Created<T>(ApiResponseObject<T> data)
        {
            return StatusCode(201, data);
        }

        [NonAction]
        protected IActionResult NoContent<T>(string message, string internalMessage, T data)
        {
            return NoContent(new ApiResponseObject<T>
            {
                Success = true,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult NoContent<T>(ApiResponseObject<T> data)
        {
            return StatusCode(204, data);
        }

        [NonAction]
        protected IActionResult BadRequest<T>(string message, string internalMessage, T data)
        {
            return BadRequest(new ApiResponseObject<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult BadRequest<T>(ApiResponseObject<T> data)
        {
            return StatusCode(400, data);
        }

        [NonAction]
        protected IActionResult Unauthorized<T>(string message, string internalMessage, T data)
        {
            return Unauthorized(new ApiResponseObject<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult Unauthorized<T>(ApiResponseObject<T> data)
        {
            return StatusCode(401, data);
        }

        [NonAction]
        protected IActionResult Forbidden<T>(string message, string internalMessage, T data)
        {
            return Forbidden(new ApiResponseObject<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult Forbidden<T>(ApiResponseObject<T> data)
        {
            return StatusCode(403, data);
        }

        [NonAction]
        protected IActionResult NotFound<T>(string message, string internalMessage, T data)
        {
            return NotFound(new ApiResponseObject<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult NotFound<T>(ApiResponseObject<T> data)
        {
            return StatusCode(404, data);
        }

        [NonAction]
        protected IActionResult Error<T>(string message, string internalMessage, T data)
        {
            return Error(new ApiResponseObject<T>
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data
            });
        }

        [NonAction]
        protected IActionResult Error<T>(ApiResponseObject<T> data)
        {
            return StatusCode(500, data);
        }
    }
}
