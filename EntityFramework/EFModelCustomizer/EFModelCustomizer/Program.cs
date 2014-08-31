using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contoso;

namespace EFModelCustomizer
{
    class Program
    {
        static void Main(string[] args)
        {
            Customization.Register(modelBuilder =>
            {
                modelBuilder.Entity<Customer>().Map<SuperCustomer>(x => x.MapInheritedProperties());
                modelBuilder.Entity<Customer>().Property(x => x.Name).HasColumnAnnotation("StringLenght", 100);
            });


            ContosoCtx.Initialize();

            using (var db = new ContosoCtx())
            {
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
