using EFBase;

namespace Contoso.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class ContosoMigration : DbMigrationsConfiguration<ContosoCtx>
    {
        public ContosoMigration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ContosoCtx context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Customers.AddOrUpdate(
              p => p.Name,
              new Customer { Name = "Andrew Peters" },
              new Customer() { Name = "Brice Lambson" },
              new Customer() { Name = "Rowan Miller" }
            );

        }
    }
}
