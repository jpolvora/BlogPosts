using Contoso;
using Contoso.Migrations;

namespace EFModelCustomizer
{
    public class CustomMigration : ContosoMigration
    {
        public CustomMigration()
        {

            AutomaticMigrationDataLossAllowed = true;

            ContextKey = GetType().BaseType.Name;

            Customization.Register(modelBuilder =>
            {
                //modelBuilder.Entity<Customer>().Map<SuperCustomer>(x => x.ToTable("Derived"));
                modelBuilder.Entity<SuperCustomer>().ToTable("Derived");
                modelBuilder.Entity<SuperCustomer>().Property(x => x.Name).HasColumnAnnotation("StringLenght", 100);
            });
        }
    }
}