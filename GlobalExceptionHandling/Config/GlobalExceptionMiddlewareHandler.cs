using GlobalExceptionHandling.Exceptions;
using System.Net;
using System.Text.Json;

namespace GlobalExceptionHandling.Config
{
    public class GlobalExceptionMiddlewareHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddlewareHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = context.Response;

            ResponseModel resModel = new ResponseModel();

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(NotFoundException)) {               
                resModel.responseCode= (int)HttpStatusCode.NotFound;
                resModel.responseMessage = "Data does not exist";
                response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if(exceptionType == typeof(BadRequestException)) {
                resModel.responseCode = (int)HttpStatusCode.BadRequest;
                resModel.responseMessage = "This is a bad request";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                resModel.responseCode = (int)HttpStatusCode.InternalServerError;
                resModel.responseMessage = "Server Error";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var exceptionResult = JsonSerializer.Serialize(resModel);

           

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
