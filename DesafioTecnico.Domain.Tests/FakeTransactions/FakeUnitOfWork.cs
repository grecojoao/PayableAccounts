using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Infra.Storage.Transactions.Contracts;

namespace DesafioTecnico.Domain.Tests.FakeTransactions
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        public Task RollbackAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;

        public Task CommitAsync(CancellationToken cancellationToken) =>
            Task.CompletedTask;
    }
}