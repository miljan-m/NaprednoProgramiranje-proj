namespace LibraryApp.Domen.Abstractions;

public abstract class IBaseEntity
{
    public DateTime DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
    public DateTime? DateDeleted { get; set; }
    public bool? IsDeleted { get; set; }
}