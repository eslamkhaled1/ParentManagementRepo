using ParentManagement.Application.Interfaces;
using ParentManagement.Application.Models;
using ParentManagement.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ParentManagement.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ISchoolRepository _schools;
        private readonly IProductRepository _products;
        private readonly IInventoryService _inventory;
        private readonly IPricingService _pricing;
        private readonly IPaymentService _payment;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            ISchoolRepository schools,
            IProductRepository products,
            IInventoryService inventory,
            IPricingService pricing,
            IPaymentService payment,
            IEmailSender emailSender,
            ILogger<OrderService> logger)
        {
            _schools = schools;
            _products = products;
            _inventory = inventory;
            _pricing = pricing;
            _payment = payment;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<Result> ProcessOrderAsync(Order order)
        {
            if (order == null) return new Result { Success = false, Code = Models.ErrorCode.InvalidOrder, Message = "invalid order" };
            if (order.Lines == null || order.Lines.Count == 0) return new Result { Success = false, Code = Models.ErrorCode.EmptyOrder, Message = "empty order" };
            var tier = await _schools.GetTierAsync(order.SchoolId);
            if (tier == null) return new Result { Success = false, Code = Models.ErrorCode.SchoolNotFound, Message = "school not found" };

            decimal subtotal = 0m;

            foreach (var line in order.Lines)
            {
                var basePrice = await _products.GetBasePriceAsync(line.Sku);
                if (basePrice == null) return new Result { Success = false, Code = Models.ErrorCode.ProductNotFound, Message = $"product not found {line.Sku}", Details = line.Sku };

                var price = _pricing.CalculatePrice(basePrice.Value, tier, line.Embroidery);

                var stock = await _inventory.GetStockAsync(line.Sku);
                if (stock < line.Quantity) return new Result { Success = false, Code = Models.ErrorCode.OutOfStock, Message = $"out of stock {line.Sku}", Details = line.Sku };

                subtotal += price * line.Quantity;
            }

            var paid = await _payment.ChargeAsync(subtotal, order.ParentEmail);
            if (!paid) return new Result { Success = false, Code = Models.ErrorCode.PaymentFailed, Message = "payment failed" };

            try
            {
                await _emailSender.SendOrderConfirmationAsync(order.ParentEmail, "orders@brindleford.co.uk", "Order confirmed",
                    $"Your order total is £{subtotal}");
            }
            catch (System.Exception ex)
            {
                // swallow but log
                _logger.LogWarning(ex, "Failed to send order confirmation email");
            }

            return new Result { Success = true, Code = Models.ErrorCode.Ok, Message = "OK" };
        }
    }
}
