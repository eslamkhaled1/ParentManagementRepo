using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentManagement.Domain.Entities
{
    public class OrderLine
    {
        public string Sku { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public string? Embroidery { get; set; }
    }
}
