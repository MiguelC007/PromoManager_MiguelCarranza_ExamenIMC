using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PromoManager.Infrastructure.Repositories;
using PromoManager.Infrastructure.UnitOfWork;
using PromoManager.Application.Services;
using PromoManager.Domain.Interfaces;
using PromoManager.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<InMemoryCouponRepository>();
builder.Services.AddSingleton<ICouponReader>(sp => sp.GetRequiredService<InMemoryCouponRepository>());
builder.Services.AddSingleton<ICouponWriter>(sp => sp.GetRequiredService<InMemoryCouponRepository>());
builder.Services.AddSingleton<IUnitOfWork, InMemoryUnitOfWork>();
builder.Services.AddScoped<CouponService>();
var app = builder.Build();
app.MapControllers();
app.Run();