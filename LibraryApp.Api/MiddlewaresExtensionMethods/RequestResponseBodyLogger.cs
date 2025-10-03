using LibraryApp.Api.Middlewares;

namespace LibraryApp.MiddlewaresExtensionMethods;

public static class RequestResponseBodyLogger
{
    public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
    }
}