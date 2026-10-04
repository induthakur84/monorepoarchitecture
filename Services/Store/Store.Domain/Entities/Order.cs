using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class Order
    {
        //pk
        public int Id { get; set; }

        // ✅ FK
        public int UserId { get; set; }

        // ✅ Navigation
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
