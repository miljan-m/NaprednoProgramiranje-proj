using Microsoft.Extensions.DependencyInjection;
using LibraryApp.Application.Services;
using LibraryApp.Application.Interfaces;
using FluentValidation;
using LibraryApp.Application.Validators;
using LibraryApp.Application.DTOs.RequestDTO.Address;
using LibraryApp.Application.Services.JSONServices;
namespace LibraryApp.Application;

public static class ApplicationServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<ISpecialEditionBookService, SpecialEditionBookService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<IValidator<City>, CityValidator>();
        services.AddScoped<IValidator<Admin>, AdminValidator>();
        services.AddScoped<IValidator<Book>, BookValidator>();
        services.AddScoped<IValidator<Author>, AuthorValidator>();
        services.AddScoped<IValidator<AddressCreateDTO>, AddressValidator>();
        services.AddScoped<IValidator<Customer>, CustomerValidator>();
        services.AddScoped<IJSONService<Book>, JSONBookService<Book>>();
        services.AddScoped<IJSONService<Address>, JSONAddressService<Address>>();
        services.AddScoped<IJSONService<Admin>, JSONAdminService<Admin>>();
        services.AddScoped<IJSONService<Author>, JSONAuthorService<Author>>();
        services.AddScoped<IJSONService<City>, JSONCityService<City>>();
        services.AddScoped<IJSONService<Customer>, JSONCustomerService<Customer>>();
        services.AddScoped<IJSONService<SpecialEditionBook>, JSONCustomerService<SpecialEditionBook>>();

        return services;
    }
}