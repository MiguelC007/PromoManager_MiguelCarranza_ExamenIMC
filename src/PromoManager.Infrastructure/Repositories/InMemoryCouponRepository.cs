using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromoManager.Domain.Entities;
using PromoManager.Domain.Interfaces;

namespace PromoManager.Infrastructure.Repositories;
public class InMemoryCouponRepository : ICouponReader, ICouponWriter {
    private readonly ConcurrentDictionary<string, Coupon> _store = new(StringComparer.InvariantCultureIgnoreCase);

    public Task AddAsync(Coupon coupon) {
        _store[coupon.Code] = coupon;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(string code) {
        _store.TryRemove(code, out _);
        return Task.CompletedTask;
    }

    public Task<bool> ExistsAsync(string code) => Task.FromResult(_store.ContainsKey(code));
    public Task<Coupon?> GetByCodeAsync(string code) {
        _store.TryGetValue(code, out var c);
        return Task.FromResult(c);
    }
    public Task<IEnumerable<Coupon>> ListAsync() => Task.FromResult(_store.Values.AsEnumerable());
    public Task UpdateAsync(Coupon coupon) {
        _store[coupon.Code] = coupon;
        return Task.CompletedTask;
    }
}