using System;
using PromoManager.Domain.ValueObjects;
namespace PromoManager.Application.Requests;
public record CreateCouponRequest(string Code, CouponKind Kind, decimal Amount, DateTime? ExpiresAt = null);