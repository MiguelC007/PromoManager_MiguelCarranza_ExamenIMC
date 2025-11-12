using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PromoManager.Domain.Interfaces;
using PromoManager.Domain.Entities;
using PromoManager.Application.DTOs;
using PromoManager.Application.Mappers;
using PromoManager.Application.Requests;
using PromoManager.Application.Interfaces;
using PromoManager.Domain.Exceptions;

namespace PromoManager.Application.Services;
public class CouponService {
    private readonly ICouponReader _reader;
    private readonly ICouponWriter _writer;
    private readonly IUnitOfWork _uow;

    public CouponService(ICouponReader reader, ICouponWriter writer, IUnitOfWork uow) {
        _reader = reader;
        _writer = writer;
        _uow = uow;
    }

    public async Task<CouponDto> CreateAsync(CreateCouponRequest req) {
        if (string.IsNullOrWhiteSpace(req.Code)) throw new DomainException("Code is required");
        var code = req.Code.Trim().ToUpperInvariant();
        if (await _reader.ExistsAsync(code)) throw new DomainException("Coupon already exists");
        var coupon = new Coupon(code, req.Kind, req.Amount, req.ExpiresAt);
        await _writer.AddAsync(coupon);
        await _uow.CommitAsync();
        return CouponMapper.ToDto(coupon);
    }

    public async Task<IEnumerable<CouponDto>> ListAsync() {
        var list = await _reader.ListAsync();
        var result = new List<CouponDto>();
        foreach (var c in list) result.Add(CouponMapper.ToDto(c));
        return result;
    }

    public async Task<CouponDto?> GetByCodeAsync(string code) {
        var c = await _reader.GetByCodeAsync(code.Trim().ToUpperInvariant());
        return c == null ? null : CouponMapper.ToDto(c);
    }

    public async Task<(bool, string?)> DeactivateAsync(string code) {
        var c = await _reader.GetByCodeAsync(code.Trim().ToUpperInvariant());
        if (c == null) return (false, "NotFound");
        if (!c.Deactivate()) return (false, "AlreadyInactive");
        await _writer.UpdateAsync(c);
        await _uow.CommitAsync();
        return (true, null);
    }

    public async Task<bool> DeleteAsync(string code) {
        var c = await _reader.GetByCodeAsync(code.Trim().ToUpperInvariant());
        if (c == null) return false;
        await _writer.DeleteAsync(c.Code);
        await _uow.CommitAsync();
        return true;
    }

    public async Task<(bool, string?)> ValidateAsync(string code, DateTime now) {
        var c = await _reader.GetByCodeAsync(code.Trim().ToUpperInvariant());
        if (c == null) return (false, "NotFound");
        var valid = c.Validate(now);
        if (!valid) {
            if (!c.IsActive) return (false, "Inactive");
            if (c.ExpiresAt.HasValue && c.ExpiresAt.Value < now.ToUniversalTime()) return (false, "Expired");
            return (false, "Invalid");
        }
        return (true, null);
    }
}