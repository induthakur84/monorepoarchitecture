// Namespace used to access HttpContext, RequestDelegate, Response, etc.
using Microsoft.AspNetCore.Http;

// Used for handling Entity Framework Core database exceptions
using Microsoft.EntityFrameworkCore;

// Used to convert C# objects into JSON format
using Newtonsoft.Json;

// Contains our custom ErrorResponse model
using SharedModel.Response;

// Contains custom exception interfaces
using SharedModel.Utility;

// Contains HTTP status codes like 404, 500, 400, etc.
using System.Net;

namespace ApiUtility.Middleware
{
    /// <summary>
    /// Global Exception Middleware
    ///
    /// This middleware catches all unhandled exceptions
    /// occurring anywhere in the application and returns
    /// a standard JSON response to the client.
    ///
    /// Instead of showing ASP.NET Core's default error page,
    /// it sends a clean API response.
    /// </summary>
    public class ExceptionMiddleware
    {
        // Represents the next middleware in the ASP.NET Core pipeline.
        // Every middleware receives a RequestDelegate object.
        private readonly RequestDelegate next;

        /// <summary>
        /// Constructor
        ///
        /// ASP.NET Core Dependency Injection automatically injects
        /// the next middleware in the request pipeline.
        /// </summary>
        /// <param name="next"></param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// This method is automatically called by ASP.NET Core
        /// for every incoming HTTP request.
        ///
        /// Every middleware must have an Invoke() or InvokeAsync() method.
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Pass the request to the next middleware.
                // If no exception occurs, the request continues normally.
                await next(context);
            }
            catch (Exception ex)
            {
                // If any exception occurs anywhere after this middleware,
                // execution comes here.

                // Instead of crashing the application,
                // we return a proper JSON error response.
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// This method decides which HTTP Status Code
        /// should be returned based on the exception type.
        ///
        /// Different exception types represent different problems.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //-------------------------------------------------------
            // CASE 1
            // Data Conflict Exceptions
            //-------------------------------------------------------
            //
            // IDataConflictsException
            // IDataValidationException
            // DbUpdateException
            //
            // Example:
            // Email already exists
            // Duplicate Primary Key
            // Unique Constraint Violation
            //
            // Returns:
            // HTTP 409 Conflict
            //
            if (exception is IDataConflictsException ||
                exception is IDataValidationException ||
                exception is DbUpdateException)

                await BuildResponse(
                    context,
                    HttpStatusCode.Conflict,
                    new ErrorResponse(
                        (int)HttpStatusCode.Conflict,

                        // Sometimes EF Core stores the real database
                        // error inside InnerException.
                        // If it exists, return it.
                        exception.InnerException == null
                            ? exception.Message
                            : exception.InnerException.Message
                    ));

            //-------------------------------------------------------
            // CASE 2
            // Application Exception
            //-------------------------------------------------------
            //
            // Example:
            // User not found
            // Product not found
            //
            // Returns:
            // HTTP 404 Not Found
            //
            else if (exception is ApplicationException)

                await BuildResponse(
                    context,
                    HttpStatusCode.NotFound,
                    new ErrorResponse(
                        (int)HttpStatusCode.NotFound,
                        exception.Message
                    ));

            //-------------------------------------------------------
            // CASE 3
            // Bad Request
            //-------------------------------------------------------
            //
            // Example:
            // Invalid input
            // Required field missing
            // Invalid request body
            //
            // Returns:
            // HTTP 400 Bad Request
            //
            else if (exception is IBadRequestException)

                await BuildResponse(
                    context,
                    HttpStatusCode.BadRequest,
                    new ErrorResponse(
                        (int)HttpStatusCode.BadRequest,
                        exception.Message
                    ));

            //-------------------------------------------------------
            // CASE 4
            // Any Other Unexpected Exception
            //-------------------------------------------------------
            //
            // Example:
            // NullReferenceException
            // DivideByZeroException
            // FileNotFoundException
            // Unknown Runtime Exception
            //
            // Returns:
            // HTTP 500 Internal Server Error
            //
            else

                await BuildResponse(
                    context,
                    HttpStatusCode.InternalServerError,
                    new ErrorResponse(
                        (int)HttpStatusCode.InternalServerError,
                        exception.Message
                    ));
        }

        /// <summary>
        /// Creates the final JSON response
        /// and sends it back to the client.
        /// </summary>
        private static async Task BuildResponse(
            HttpContext context,
            HttpStatusCode statusCode,
            ErrorResponse error)
        {
            // Set HTTP Status Code
            // Example:
            // 404
            // 400
            // 500
            context.Response.StatusCode = (int)statusCode;

            // Tell client that response is JSON
            context.Response.ContentType = "application/json";

            // Convert ErrorResponse object into JSON
            // and write it to the response body.
            await context.Response.WriteAsync(
                JsonConvert.SerializeObject(error)
            );
        }
    }
}
