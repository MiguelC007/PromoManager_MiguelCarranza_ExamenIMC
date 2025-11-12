using PromoManager.Domain.Entities;
using System.Threading.Tasks;
namespace PromoManager.Domain.Interfaces;
public interface ICouponWriter {
    Task AddAsync(Coupon coupon);
    Task UpdateAsync(Coupon coupon);
    Task DeleteAsync(string code);
}