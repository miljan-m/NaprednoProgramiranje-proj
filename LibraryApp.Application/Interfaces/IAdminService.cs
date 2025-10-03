


namespace LibraryApp.Application.Interfaces;

public interface IAdminService
{
    public Task<IEnumerable<GetAdminsDTO>> GetAdmins();
    public Task<GetAdminDTO> GetAdmin(string adminId);
    public Task<bool> DeleteAdmin(string adminId);
    public Task<GetAdminDTO> UpdateAdmin(string adminId, UpdateAdminDTO adminDto);
    public Task<Admin> CreateAdmin(CreateAdminDTO adminDto);
}