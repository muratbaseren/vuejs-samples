using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BlogVue.WebAPI.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public CustomExceptionFilter(ILoggerFactory loggerFactory, IWebHostEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider)
        {
            _loggerFactory = loggerFactory;
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            if (_hostingEnvironment.IsDevelopment()) return;

            //string controllerName = context.ActionDescriptor.RouteValues["controller"];
            //string actionName = context.ActionDescriptor.RouteValues["action"];

            ControllerActionDescriptor controller = (ControllerActionDescriptor)context.ActionDescriptor;
            ILogger logger = _loggerFactory.CreateLogger(controller.ControllerTypeInfo);

            //logger.LogError(context.Exception, $"Controller : {controllerName} , Action : {actionName}");
            logger.LogError(context.Exception, context.Exception.Message);

            context.Result = new BadRequestObjectResult(new ApiResponseObject
            {
                Message = "Error occured.",
                InternalMessage = context.Exception.Message,
                Data = null
            });
        }
    }
}
