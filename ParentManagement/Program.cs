using ParentManagement.Application.Interfaces;
using ParentManagement.Application.Services;
using ParentManagement.Infrastructure.Repositories;
using ParentManagement.Infrastructure.Services;
using ParentManagement.Infrastructure.Payments;
using ParentManagement.Infrastructure.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// application services
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<ISchoolRepository, InMemorySchoolRepository>();
builder.Services.AddScoped<IProductRepository, InMemoryProductRepository>();
builder.Services.AddScoped<IInventoryService, InMemoryInventoryService>();
builder.Services.AddScoped<IPaymentService, FakePaymentService>();
builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
