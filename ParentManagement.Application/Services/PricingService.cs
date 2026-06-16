using ParentManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentManagement.Application.Services
{
    public class PricingService : IPricingService
    {
        public decimal CalculatePrice(
            decimal basePrice,
            string tier,
            string? embroidery)
        {
            var price = tier switch
            {
                "GOLD" => basePrice * 0.85m,
                "SILVER" => basePrice * 0.92m,
                _ => basePrice
            };

            if (!string.IsNullOrWhiteSpace(embroidery))
            {
                price += embroidery.Length <= 3
                    ? 4.5m
                    : 8m;
            }

            return price;
        }
    }
}
