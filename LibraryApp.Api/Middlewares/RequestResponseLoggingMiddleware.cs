namespace LibraryApp.Api.Middlewares;

public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<RequestResponseLoggingMiddleware> logger;
    public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        if (context.Request.Path.Value.EndsWith("/openapi/v1.json"))
        {
            await next(context);
            return;
        }
        
        context.Request.EnableBuffering();
        context.Request.Body.Position = 0;
        using var requestBodyStream = new StreamReader(context.Request.Body, leaveOpen: true);
        string requestBodyText = await requestBodyStream.ReadToEndAsync();
        context.Request.Body.Position = 0;

        requestBodyText = requestBodyText.Replace("},","},\n");
        

        var headers = context.Request.Headers.Select(h => $"{h.Key}:{h.Value}").ToList();
        logger.LogInformation("Headers: {Headers}\n", string.Join("\n", headers));

        logger.LogInformation("Request Body: {requestBodyText}", requestBodyText);

        var originalResponseBodyStream = context.Response.Body;
        var tempStream = new MemoryStream();
        context.Response.Body = tempStream;

        try
        {
            await next(context);
            
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            using var responseBodyStream = new StreamReader(context.Response.Body);
            string responseBodyText = await responseBodyStream.ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            responseBodyText = responseBodyText.Replace("},", "},\n");
            logger.LogInformation("Response Body: {responseBodyText}", responseBodyText);
            context.Response.Body = originalResponseBodyStream;
            await tempStream.CopyToAsync(context.Response.Body);
        }
        catch (Exception exception)
        {
            context.Response.Body = originalResponseBodyStream;
            await tempStream.CopyToAsync(context.Response.Body);
            throw;
        }
       

        
        
    }

}