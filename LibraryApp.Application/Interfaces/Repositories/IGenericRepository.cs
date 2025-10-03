namespace LibraryApp.Application.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    public Task<List<T>> GetAllAsync();
    public Task<T> GetOneAsync(string id);

    public Task<bool> DeleteAsync(string id);

    public Task<T> CreateAsync(T objToBeCreated);

    public Task<T> UpdateAsync(T objToUpdate,string id);
}