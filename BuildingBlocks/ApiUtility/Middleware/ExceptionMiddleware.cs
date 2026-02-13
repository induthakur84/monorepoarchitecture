using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SharedModel.Response;
using SharedModel.Utility;
using System.Net;

namespace ApiUtility.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handle Exception
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is IDataConflictsException || exception is IDataValidationException || exception is DbUpdateException)
                await BuildResponse(context, HttpStatusCode.Conflict, new ErrorResponse((int)HttpStatusCode.Conflict, exception.InnerException == null ? exception.Message : exception.InnerException.Message));
            else if (exception is ApplicationException)
                await BuildResponse(context, HttpStatusCode.NotFound, new ErrorResponse((int)HttpStatusCode.NotFound, exception.Message));
            else if (exception is IBadRequestException)
                await BuildResponse(context, HttpStatusCode.BadRequest, new ErrorResponse((int)HttpStatusCode.BadRequest, exception.Message));
            else
                await BuildResponse(context, HttpStatusCode.InternalServerError, new ErrorResponse((int)HttpStatusCode.InternalServerError, exception.Message));
        }

        /// <summary>
        /// Build Global Exception Response. 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusCode"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static async Task BuildResponse(HttpContext context, HttpStatusCode statusCode, ErrorResponse error)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }
}
