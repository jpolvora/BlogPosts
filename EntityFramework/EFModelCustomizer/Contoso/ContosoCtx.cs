using System.Data.Entity;
using Contoso.Migrations;
using EFBase;

namespace Contoso
{
    public class ContosoCtx : DbContextBase
    {
        static ContosoCtx()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContosoCtx, ContosoMigration>());
        }
        public DbSet<Customer> Customers { get; set; }
    }
}