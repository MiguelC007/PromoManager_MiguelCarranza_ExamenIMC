using System.Threading.Tasks;
namespace PromoManager.Application.Interfaces;
public interface IUnitOfWork {
    Task CommitAsync();
}