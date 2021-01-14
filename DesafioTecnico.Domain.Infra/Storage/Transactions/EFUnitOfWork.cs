using System.Threading;
using System.Threading.Tasks;
using DesafioTecnico.Domain.Infra.Storage.Data;
using DesafioTecnico.Domain.Infra.Storage.Transactions.Contracts;

namespace DesafioTecnico.Domain.Infra.Storage.Transactions
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public EFUnitOfWork(DataContext context) =>
            _context = context;

        public Task RollbackAsync(CancellationToken cancellationToken = default) =>
            Task.CompletedTask;

        public async Task CommitAsync(CancellationToken cancellationToken = default) =>
            await _context.SaveChangesAsync(cancellationToken);
    }
}