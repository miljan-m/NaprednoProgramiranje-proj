using LibraryApp.Application.Interfaces.Repositories;
using LibraryApp.Infrastructure.Persistance;

namespace LibraryApp.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly LibraryDBContext _context;
    public GenericRepository(LibraryDBContext context)
    {
        _context = context;
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetOneAsync(string id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T objToCreate)
    {
        await _context.AddAsync(objToCreate);
        await _context.SaveChangesAsync();
        return objToCreate;
    }

    public async Task<T> UpdateAsync(T objToBeUpdated, string id)
    {
        var existingObj =await _context.Set<T>().FindAsync(id);
        _context.Entry(existingObj).CurrentValues.SetValues(objToBeUpdated);
        await _context.SaveChangesAsync();
        return objToBeUpdated;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var objToDelete =await _context.Set<T>().FindAsync(id);
        _context.Set<T>().Remove(objToDelete);
        await _context.SaveChangesAsync();
        return true;
    }


}