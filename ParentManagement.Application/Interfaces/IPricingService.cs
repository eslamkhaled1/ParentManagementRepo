using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentManagement.Application.Interfaces
{
    public interface IPricingService
    {
        decimal CalculatePrice(
            decimal basePrice,
            string tier,
            string? embroidery);
    }
}
