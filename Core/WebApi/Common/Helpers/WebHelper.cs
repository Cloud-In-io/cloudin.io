public static class WebHelper
{
    public static string? GetBaseUri(IHttpContextAccessor httpContextAccessor)
    {
        var host = httpContextAccessor.HttpContext?.Request.Host;
        var scheme = httpContextAccessor.HttpContext?.Request.Scheme;
        var pathBase = httpContextAccessor.HttpContext?.Request.PathBase;

        return $"{scheme}://{host}{pathBase}";
    }
}
