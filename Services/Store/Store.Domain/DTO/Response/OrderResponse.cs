using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.DTO.Response
{
    public class OrderResponse
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        // Helpful fields from User navigation
        public string UserName { get; set; }
        public string UserEmail { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
