using System.Threading.Tasks;
using PromoManager.Application.Interfaces;
namespace PromoManager.Infrastructure.UnitOfWork;
public class InMemoryUnitOfWork : IUnitOfWork {
    public Task CommitAsync() => Task.CompletedTask;
}