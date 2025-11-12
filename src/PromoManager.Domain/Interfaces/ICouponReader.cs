using PromoManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace PromoManager.Domain.Interfaces;
public interface ICouponReader {
    Task<Coupon?> GetByCodeAsync(string code);
    Task<IEnumerable<Coupon>> ListAsync();
    Task<bool> ExistsAsync(string code);
}