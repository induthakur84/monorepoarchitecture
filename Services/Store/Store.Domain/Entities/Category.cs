using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // ✅ Many-to-Many
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
