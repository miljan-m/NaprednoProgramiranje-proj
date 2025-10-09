namespace LibraryApp.Application.Interfaces;

public interface IJSONService<T> where T : class
{
    public void WriteJSONInFile(T obj);
}