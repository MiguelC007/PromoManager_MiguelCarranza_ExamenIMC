using System;
using PromoManager.Domain.ValueObjects;
namespace PromoManager.Application.DTOs;
public record CouponDto(Guid Id, string Code, CouponKind Kind, decimal Amount, bool IsActive, DateTime CreatedAt, DateTime? ExpiresAt);