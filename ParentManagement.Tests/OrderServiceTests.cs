using ParentManagement.Application.Services;
using ParentManagement.Application.Models;
using ParentManagement.Domain.Entities;
using ParentManagement.Infrastructure.Repositories;
using ParentManagement.Infrastructure.Services;
using ParentManagement.Infrastructure.Payments;
using ParentManagement.Infrastructure.Email;
using ParentManagement.Application.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using System.Threading.Tasks;

namespace ParentManagement.Tests
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task ProcessOrderAsync_GoldTier_WithEmbroidery_PaymentSucceeds_ReturnsOk()
        {
            // Arrange
            var schools = new InMemorySchoolRepository(); // school 1 => GOLD
            var products = new InMemoryProductRepository(); // SKU1 => 10.00
            var inventory = new InMemoryInventoryService(); // SKU1 stock 100
            var pricing = new ParentManagement.Application.Services.PricingService();
            var payment = new FakePaymentService();
            var email = new FakeEmailSender(new NullLogger<FakeEmailSender>());
            var logger = new NullLogger<OrderService>();

            var service = new OrderService(schools, products, inventory, pricing, payment, email, logger);

            var order = new Order
            {
                SchoolId = 1,
                ParentEmail = "parent@example.com",
                Lines = new System.Collections.Generic.List<OrderLine>
                {
                    new OrderLine { Sku = "SKU1", Quantity = 2, Embroidery = "AB" }
                }
            };

            // Act
            var result = await service.ProcessOrderAsync(order);

            // Assert
            Assert.True(result.Success);
            Assert.Equal(ErrorCode.Ok, result.Code);
        }
    }
}
