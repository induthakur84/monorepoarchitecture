using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.DTO.Request
{
    public class OrderRequest
    {
        public int UserId { get; set; }
        // If later you add OrderItems, Amount, Status, etc. add here
    }
}
