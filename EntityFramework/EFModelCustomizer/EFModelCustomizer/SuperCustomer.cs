using System.ComponentModel.DataAnnotations;
using Contoso;

namespace EFModelCustomizer
{
    public class SuperCustomer : Customer
    {
        [Required, StringLength(200)]
        public string Email { get; set; }
    }
}