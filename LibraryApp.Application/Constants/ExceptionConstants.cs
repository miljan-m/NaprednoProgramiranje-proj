namespace LibraryApp.Application.Constants;
using Microsoft.AspNetCore.Http;

public static class ExceptionConstants
{

    public static class ExceptionErrors
    {

        public static class NotFoundException
        {
            public const string Type = "https://learn.microsoft.com/en-us/dotnet/api/microsoft.sqlserver.management.notfoundexception?view=sqlserver-2016";
            public const string Details = "Entity that you are looking for cannot be found. Identification parametar you provided doesn't exist in database. ";
            public const int Status = StatusCodes.Status404NotFound;
            public const string Title = "Not Found Exception";

        }

        public static class InvalidArgumentException
        {
            public const string Type = "https://learn.microsoft.com/en-us/dotnet/api/system.argumentexception?view=net-9.0";
            public const string Details = "Entity that you are looking for cannot be found with provided ID parametar. Please check your parametar. ";
            public const string Title = "Invalid Argument Exception";
            public const int Status = StatusCodes.Status400BadRequest;
        }


        public static class UndefinedError
        {
            public const string Type = "https://learn.microsoft.com/en-us/troubleshoot/developer/webapps/iis/site-behavior-performance/http-error-500-when-you-visit-web-site";
            public const string Details = "Error that occurred is not predicted by developer";
            public const string Title = "Undefined error occurred";
            public const int Status = StatusCodes.Status500InternalServerError;
        }

    }

}