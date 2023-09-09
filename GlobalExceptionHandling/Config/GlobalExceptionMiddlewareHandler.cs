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
    }
}
