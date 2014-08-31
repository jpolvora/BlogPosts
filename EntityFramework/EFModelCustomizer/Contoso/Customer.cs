using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }
    }
}
