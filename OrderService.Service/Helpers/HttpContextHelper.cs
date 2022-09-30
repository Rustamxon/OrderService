using Microsoft.AspNetCore.Http;

namespace OrderService.Service.Helpers;

public class HttpContextHelper
{
    public static IHttpContextAccessor Accessor { get; set; }
    public static HttpContext HttpContext => Accessor?.HttpContext;
    public static IHeaderDictionary ResponseHeader => HttpContext?.Response?.Headers;
    public static long? UserId => GetUserId();
    public static string UserRole => HttpContext?.User.FindFirst("Role")?.Value;

    private static long? GetUserId()
    {
        long id;
        bool canParse = long.TryParse(HttpContext?.User?.Claims.FirstOrDefault(p => p.Type == "Id")?.Value, out id);
        return canParse ? id : null;
    }
}
