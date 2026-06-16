using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ParentManagement.Pages.Orders
{
    public class ConfirmOrderModel : PageModel
    {
        public string SchoolName { get; set; } = string.Empty;
        public List<OrderLineViewModel> Lines { get; set; } = new();

        public decimal Subtotal => CalculateSubtotal();

        public void OnGet()
        {
            // In a real app, load from a service or database.
            SchoolName = "Sample School";
            Lines = new List<OrderLineViewModel>
            {
                new OrderLineViewModel { Id = 1, Sku = "SKU-001", Embroidery = "Logo A", Quantity = 2, UnitPrice = 12.50m },
                new OrderLineViewModel { Id = 2, Sku = "SKU-002", Embroidery = "Logo B", Quantity = 1, UnitPrice = 8.00m }
            };
        }

        public IActionResult OnPost()
        {
            // Reload the canonical lines (normally from DB) then bind quantities from form values.
            OnGet();
            foreach (var line in Lines)
            {
                var key = $"qty_{line.Id}";
                if (Request.Form.TryGetValue(key, out var val))
                {
                    if (int.TryParse(val, out var q)) line.Quantity = q;
                }
            }

            // TODO: persist confirmed order
            return Page();
        }

        private decimal CalculateSubtotal()
        {
            decimal total = 0;
            foreach (var l in Lines) total += l.UnitPrice * l.Quantity;
            return total;
        }
    }

    public class OrderLineViewModel
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Embroidery { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
