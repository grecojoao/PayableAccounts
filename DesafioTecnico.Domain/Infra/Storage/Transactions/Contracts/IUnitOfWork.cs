using System.Threading;
using System.Threading.Tasks;

namespace DesafioTecnico.Domain.Infra.Storage.Transactions.Contracts
{
    public interface IUnitOfWork
    {
        Task RollbackAsync(CancellationToken cancellationToken);
        Task CommitAsync(CancellationToken cancellationToken);
    }
}