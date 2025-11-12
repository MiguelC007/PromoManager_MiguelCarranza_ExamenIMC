using System;
using PromoManager.Domain.ValueObjects;
using PromoManager.Domain.Exceptions;

namespace PromoManager.Domain.Entities;

public class Coupon {
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public CouponKind Kind { get; private set; }
    public decimal Amount { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? ExpiresAt { get; private set; }

    private Coupon() { }

    public Coupon(string code, CouponKind kind, decimal amount, DateTime? expiresAt = null) {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Code = code?.Trim().ToUpperInvariant() ?? throw new DomainException("Code is required");
        Kind = kind;
        Amount = amount;
        ExpiresAt = expiresAt;
        IsActive = true;
        ValidateInvariants();
    }

    private void ValidateInvariants() {
        if (string.IsNullOrWhiteSpace(Code)) throw new DomainException("Code cannot be empty");
        switch (Kind) {
            case CouponKind.FIXED:
                if (Amount <= 0) throw new DomainException("Fixed amount must be > 0");
                break;
            case CouponKind.PERCENT:
                if (Amount <= 0 || Amount > 100) throw new DomainException("Percent amount must be between 0 and 100");
                break;
            default:
                throw new DomainException("Unknown coupon kind");
        }
        if (ExpiresAt.HasValue && ExpiresAt.Value.Kind != DateTimeKind.Utc) {
            ExpiresAt = ExpiresAt.Value.ToUniversalTime();
        }
    }

    public bool Validate(DateTime now) {
        var nowUtc = now.ToUniversalTime();
        if (!IsActive) return false;
        if (ExpiresAt.HasValue && ExpiresAt.Value < nowUtc) return false;
        return true;
    }

    public bool Deactivate() {
        if (!IsActive) return false;
        IsActive = false;
        return true;
    }
}