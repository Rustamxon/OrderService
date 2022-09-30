using OrderService.Service.Exceptions;

namespace OrderService.Api.Middlewares;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<CustomExceptionMiddleware> logger;
    public CustomExceptionMiddleware(
        RequestDelegate next,
        ILogger<CustomExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (CustomException ex)
        {
            await HandleException(context, ex.Code, ex.Message);
        }
        catch(Exception ex)
        {
            logger.LogError(ex.ToString());

            await HandleException(context, 500, ex.Message);
        }
    }

    public async ValueTask HandleException(
        HttpContext context,
        int code, 
        string message)
    {
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(new
        {
            Code = code,
            Message = message
        });
    }
}
