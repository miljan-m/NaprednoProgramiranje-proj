using Microsoft.OpenApi.Models;

namespace LibraryApp.Api;

public static class PresentationService
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddOpenApi(options=>options.AddDocumentTransformer((document,context,CancellationToken)=>
            {
                document.Info.Title = "My Library App";
                document.Info.Contact = new OpenApiContact()
                {
                    Email = "miljan@gmail.com",
                    Name = "Miljan"
                };
                document.Info.Description = "This is my librarry app";
                return Task.CompletedTask;
            }));

        return services;
    }
}