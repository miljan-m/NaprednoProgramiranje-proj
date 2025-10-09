using LibraryApp.Application.Interfaces;
using LibraryApp.Mappers;
using LibraryApp.Application.CustomExceptions;

namespace LibraryApp.Application.Services;
/// <summary>
/// Servis koji upravlja poslovnom logikom za klasu <see cref="Admin"/>.
/// Omogućava kreiranje, preuzimanje, ažuriranje i brisanje admina
/// </summary>

public class AdminService : IAdminService
{
    private readonly IGenericRepository<Admin> adminRepository;
    private readonly IJSONService<Admin> adminJSON;

    /// <summary>
    /// Inicijalizuje novi <see cref="AdminService"/> sa prosleđenim repozitorijumom za administratore.
    /// </summary>
    /// <param name="adminRepository">Generički repozitorijum za entitet <see cref="Admin"/>.</param>
    public AdminService(IGenericRepository<Admin> adminRepository, IJSONService<Admin>? adminJSON)
    {
        this.adminRepository = adminRepository;
        this.adminJSON = adminJSON;
    }
    /// <summary>
    /// Vraca sve admine iz baze 
    /// </summary>
    /// <returns>Lista DTO objekata tipa Admin</returns>
    /// <exception cref="NotFoundException">Ako je baza podataka prazna.</exception>
    public async Task<IEnumerable<GetAdminsDTO>> GetAdmins()
    {
        var adminsList = await adminRepository.GetAllAsync();
        var admins = adminsList.Select(a => a.MapDomainEntitiesToDTO()).ToList();
        if (admins == null) throw new NotFoundException("Database is empty");
        return admins;
    }
    /// <summary>
    /// Dohvata jednog administratora prema njegovom identifikatoru
    /// </summary>
    /// <param name="adminId"> Jedinstveni identifikator administratora</param>
    /// <returns> DTO objekat sa podacima o administratoru</returns>
    /// <exception cref="AdminNotFoundException">Ako administrator sa datim adminId ne postoji</exception>
    public async Task<GetAdminDTO> GetAdmin(string adminId)
    {
        var admin = await adminRepository.GetOneAsync(adminId);
        if (admin == null) throw new AdminNotFoundException(adminId);
        var adminDto = admin.MapDomainEntityToDTO();
        adminJSON.WriteJSONInFile(admin);
        return adminDto;
    }
    /// <summary>
    /// Briše administratora iz baze podataka
    /// </summary>
    /// <param name="adminId">Jedinstveni identifikator administratora </param>
    /// <returns>Vraća TRUE ako je brisanje uspešno, u suprotnom baca izuzetak</returns>
    public async Task<bool> DeleteAdmin(string adminId)
    {
        return await adminRepository.DeleteAsync(adminId);
    }
    /// <summary>
    /// Ažurira podatke o postojećem administratoru.
    /// </summary>
    /// <param name="adminId">ID administratora koji se ažurira</param>
    /// <param name="adminDto">DTO sa novim vrednostima za administratora</param>
    /// <returns>Ažurirani <see cref="GetAdminDTO"/> objekat</returns>
    /// <exception cref="AdminNotFoundException">Ako administrator sa datim adminId ne postoji</exception>
    public async Task<GetAdminDTO> UpdateAdmin(string adminId, UpdateAdminDTO adminDto)
    {
        var admin = await adminRepository.GetOneAsync(adminId);
        if (admin == null) throw new AdminNotFoundException(adminId);
        var updatedAdmin = await adminRepository.UpdateAsync(adminDto.MapDtoToDomainEntity(admin), adminId);
        return updatedAdmin.MapDomainEntityToDTO();
    }
    /// <summary>
    /// Kreira novog administratora u sistemu.
    /// </summary>
    /// <param name="adminDto">DTO sa podacima o novom administratoru</param>
    /// <returns>Instanca novog admina koji je kreiran</returns>
    public async Task<Admin> CreateAdmin(CreateAdminDTO adminDto)
    {
        var admin = adminDto.MapDtoToDomainEntity();
        return await adminRepository.CreateAsync(admin);
    }
}