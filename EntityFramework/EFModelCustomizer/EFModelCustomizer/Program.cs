using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso;
using EFBase;

namespace EFModelCustomizer
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelCustomization.Register(modelBuilder =>
            {
                modelBuilder.Entity<SuperCustomer>().ToTable("NewCustomers");
                modelBuilder.Entity<Customer>().Property(x => x.Name).HasColumnAnnotation("StringLenght", 100);
            });



            using (var db = new ContosoCtx())
            {
                db.Database.Initialize(false);

                var customers = db.Customers.ToList();

                foreach (var customer in customers)
                {
                    Console.WriteLine("{0}, {1}", customer.Id, customer.Name);
                }
            }

            Console.WriteLine("...");

            Console.ReadKey();
        }
    }
}
