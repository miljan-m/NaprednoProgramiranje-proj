using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibraryApp.Infrastructure.Persistance
{
    public class LibraryDBContextFactory : IDesignTimeDbContextFactory<LibraryDBContext>
    {
        public LibraryDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDBContext>();

            // 👇 ovde stavi svoj connection string
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-QLQGED2\\SQLEXPRESS;Database=LibraryAppDatabse;TrustServerCertificate=True;Trusted_Connection=True;");

            return new LibraryDBContext(optionsBuilder.Options);
        }
    }
}
