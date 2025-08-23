
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PermisosApi.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // Cambia esto con los datos correctos de tu base de datos Railway
            optionsBuilder.UseNpgsql(
                "Host=postgres.railway.internal;Port=5432;Database=railway;Username=postgres;Password=LXgTWBswlkSLWIIGhlWwdVkegtMLPjmv;SSL Mode=Require;Trust Server Certificate=true;"
            );

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}