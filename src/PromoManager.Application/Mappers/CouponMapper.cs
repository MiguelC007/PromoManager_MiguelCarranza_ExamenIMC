using PromoManager.Application.DTOs;
using PromoManager.Domain.Entities;
namespace PromoManager.Application.Mappers;
public static class CouponMapper {
    public static CouponDto ToDto(Coupon c) => new CouponDto(c.Id, c.Code, c.Kind, c.Amount, c.IsActive, c.CreatedAt, c.ExpiresAt);
}