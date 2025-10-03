using LibraryApp.Application.Interfaces;
using LibraryApp.Application.DTOs.ResponseDTO.Admin;
using LibraryApp.Application.DTOs.RequestDTO.Admin;
using FluentValidation;
using LibraryApp.Mappers;
using LibraryApp.Domen.Models;
using FluentValidation.Results;

namespace LibraryApp.Api.Controllers;

[ApiController]
[Route("admins")]
public class AdminController : ControllerBase
{

    private readonly IAdminService adminService;
    private readonly IValidator<Admin> adminValidator;
    
    public AdminController(IAdminService adminService, IValidator<Admin> adminValidator)
    {
        this.adminService = adminService;
        this.adminValidator = adminValidator;
        
    }

    /// <summary>
    /// Get a list of all existing admins
    /// </summary>
    [HttpGet]

    [EndpointSummary("Get all existing admins")]
    [EndpointDescription("This endpoint return list of all admins. Id of admins are hidden from client")]
    public async Task<ActionResult<IEnumerable<GetAdminsDTO>>> GetAdmins()
    {
        var adminsDto = await adminService.GetAdmins();
        return Ok(adminsDto);
    }

    [HttpGet("{adminId}")]
    [EndpointSummary("Get one admin")]
    [EndpointDescription("This endpoint return one admin based on provided Id")]
    public async Task<ActionResult<GetAdminDTO>> GetAdmin([FromRoute] string adminId)
    {
        var adminDto = await adminService.GetAdmin(adminId);
        return Ok(adminDto);
    }

    [HttpDelete("{adminId}")]
    [EndpointSummary("Removing admin")]
    [EndpointDescription("This endpoint removes admin with Id that was provided")]
    public async Task<ActionResult> DeleteAdmin([FromRoute] string adminId)
    {
        var isDeleted = await adminService.DeleteAdmin(adminId);
        if (isDeleted) return Ok();
        return NotFound();

    }

    [HttpPost]
    [EndpointSummary("Creation of new admin")]
    [EndpointDescription("This endpoint creates new admin based on information that has been provided in body of request")]
    public async Task<ActionResult<GetAdminDTO>> CreateAdmin([FromBody] CreateAdminDTO createAdminDto)
    {
        var adminToValidate = createAdminDto.MapDtoToDomainEntity();
        var validateResult =await adminValidator.ValidateAsync(adminToValidate);
        if (!validateResult.IsValid)
        {
            
             foreach (var fail in validateResult.Errors){
                    ModelState.AddModelError(fail.PropertyName, fail.ErrorMessage);
            }

            return ValidationProblem(ModelState);
        }
        var admin = await adminService.CreateAdmin(createAdminDto);
        return Ok(admin);
    }

    [HttpPut("{adminId}")]
    [EndpointSummary("Updating admin")]
    [EndpointDescription("This endpoint updates admin based on information that has been provided in body of request")]
    public async Task<ActionResult<GetAdminDTO>> UpdateAdmin([FromRoute] string adminId, [FromBody] UpdateAdminDTO updatedAdmin)
    {
        var admin = await adminService.UpdateAdmin(adminId, updatedAdmin);
        if (admin == null) return NotFound();
        return Ok(admin);
    }
}