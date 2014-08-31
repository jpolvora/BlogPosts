using System.Data.Entity;
using Contoso.Migrations;

namespace Contoso
{
    public class ContosoCtx : DbContext
    {
        public static void Initialize()
        {
            using (var ctx = new ContosoCtx())
            {
                ctx.Database.Initialize(false);
            }
        }

        static ContosoCtx()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ContosoCtx, ContosoMigration>());
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var customization in Customization.Customizations)
            {
                customization(modelBuilder);
            }
        }
    }
}