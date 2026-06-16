using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentManagement.Domain.Entities
{
    public class Product
    {
        public string Sku { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }
    }
}
