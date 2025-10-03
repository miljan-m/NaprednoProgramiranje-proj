using LibraryApp.Domen.Abstractions;
using LibraryApp.Domen.Models;
 
namespace LibraryApp.Infrastructure.Persistance;

public class LibraryDBContext : DbContext
{
    public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options)
    {

    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Book> Books{ get; set; }
    public DbSet<Customer> Customers{ get; set; }
    public DbSet<Rent> Rents{ get; set; }
    public DbSet<City> City{ get; set; }
    public DbSet<Address> Address{ get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<IBaseEntity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.DateModified = DateTime.Now;
            }

            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.IsDeleted = true;
                entry.Entity.DateDeleted = DateTime.Now;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var Author1 = new Author("1", "Author1Name", "Author1LastName");
        var Author2 = new Author("2", "Author2Name", "Author2LastName");
        var Author3 = new Author("3", "Author3Name", "Author3LastName");
        var Author4 = new Author("4", "Author4Name", "Author4LastName");

        var Admin1 = new Admin("1", "Admin1Name", "Admin1LastName");
        var Admin2 = new Admin("2", "Admin2Name", "Admin2LastName");
        var Admin3 = new Admin("3", "Admin3Name", "Admin3LastName");
        var Admin4 = new Admin("4", "Admin4Name", "Admin4LastName");
        var Admin5 = new Admin("5", "Admin5Name", "Admin5LastName");

        var Customer1 = new Customer("Customer1Name", "Customer1LastName", "123456");
        var Customer2 = new Customer("Customer2Name", "Customer2LastName", "239184762");
        var Customer3 = new Customer("Customer3Name", "Customer3LastName", "329456");
        var Customer4 = new Customer("Customer4Name", "Customer4LastName", "324857");
        var Customer5 = new Customer("Customer5Name", "Customer5LastName", "238476");

        modelBuilder.Entity<Book>().HasDiscriminator<string>("Discriminator").HasValue<Book>("Book").HasValue<SpecialEditionBook>("Special");
        modelBuilder.Entity<Author>().HasData(Author1, Author2, Author3, Author4);
        modelBuilder.Entity<Admin>().HasData(Admin1, Admin2, Admin3, Admin4, Admin5);

        modelBuilder.Entity<Book>().HasData(
        new Book { Title = "Alisa u zemlji cuda", Isbn = "123456oaihsf", Genre = "Avantura", Available = true },
        new Book { Title = "Lord of rings", Isbn = "asdffrghsf", Genre = "Avantura", Available = true },
        new Book { Title = "Harry Potter", Isbn = "127889asdihsf", Genre = "Avantura", Available = true },
        new Book { Title = "Murder on Nil", Isbn = "deilgoihj2343", Genre = "Avantura", Available = true },
        new Book { Title = "Le Petite Prince", Isbn = "123456oadadadasf", Genre = "Avantura", Available = true },
        new Book { Title = "The jungle book", Isbn = "189er56oaihsf", Genre = "Avantura", Available = true });

        modelBuilder.Entity<Customer>().HasData(Customer1, Customer2, Customer3, Customer4, Customer5);
        modelBuilder.Entity<Admin>().HasQueryFilter(e => e.IsDeleted == null);
        modelBuilder.Entity<Book>().HasQueryFilter(e => e.IsDeleted == null);
        modelBuilder.Entity<Author>().HasQueryFilter(e => e.IsDeleted == null);


    }

}