using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // ✅ Many-to-Many
        public ICollection<Category> Categories { get; set; } = new List<Category>();
    }

}
